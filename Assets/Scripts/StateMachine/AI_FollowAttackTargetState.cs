using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FollowAttackTargetState : AI_State
{
    public AI_State attackState;
    
    public override AI_State RunState(Actor actor)
    {

       float dis= actor.weapon.attackDistance; ;
        if(GameManager.Instance.playerActor.GetComponent<PlayerController>().target)
        if (Vector3.Distance(GameManager.Instance.playerActor.GetComponent<PlayerController>().target.transform.position, actor.transform.position) <= dis)
        {
            actor.target = GameManager.Instance.playerActor.GetComponent<PlayerController>().target.transform;
            actor.currentState = ActorState.Attack;
            return attackState; 
        }

        if (actor.target)
        {
            Vector3 dir = (actor.target.transform.position - actor.transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            actor.transform.rotation = Quaternion.Slerp(actor.transform.rotation, lookRot, Time.deltaTime * actor.lookRotationSpeed);
        }
     

        if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target)
        actor.agent.SetDestination(GameManager.Instance.playerActor.GetComponent<PlayerController>().target.transform.position); 

        return this;
    }

   


}
