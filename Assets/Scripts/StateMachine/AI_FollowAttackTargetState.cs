using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FollowAttackTargetState : AI_State
{
    public AI_State attackState;
    public AI_State followState;
    
    public override AI_State RunState(Ally actor)
    {

       float dis= actor.weapon.attackDistance;
        if (actor.target)
        {


            if (Vector3.Distance(actor.target.transform.position, actor.transform.position) <= dis)
            {

                actor.currentState = ActorState.Attack;
                return attackState;


            }

                Vector3 dir = (actor.target.transform.position - actor.transform.position).normalized;
                Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
                actor.transform.rotation = Quaternion.Slerp(actor.transform.rotation, lookRot, Time.deltaTime * actor.lookRotationSpeed);




                actor.agent.SetDestination(actor.target.transform.position);
            
        }
        else
        {
            actor.actorBusy = false;
            actor.currentState = ActorState.Follow;
            return followState;
        }
        return this;
    }

   


}
