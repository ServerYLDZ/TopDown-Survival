using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CuttingState : AI_State
{
    public AI_State followState;
    public override AI_State RunState(Ally actor)
    {
        if (actor.currentState == ActorState.Follow)
        {
            actor.target = null;
            actor.actorBusy = false;
            return followState;
        }


        actor.target = GameManager.Instance.woodTree.nodes.transform;

        Vector3 dir = (actor.target.transform.position - actor.transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        actor.transform.rotation = Quaternion.Slerp(actor.transform.rotation, lookRot, Time.deltaTime * actor.lookRotationSpeed);

        actor.agent.SetDestination(actor.target.position);


        return this;
    }
}
