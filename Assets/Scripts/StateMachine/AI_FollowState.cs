using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FollowState : AI_State
{
    public AI_State followAttackTargetState;
    
    public override AI_State RunState(Actor actor)
    {
        if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target) 
        if( GameManager.Instance.playerActor.GetComponent<PlayerController>().target.interactionType == InteractableType.Enemy)
        {
                actor.target = GameManager.Instance.playerActor.GetComponent<PlayerController>().target.transform;
                GameManager.Instance.IsActorFolowPlayerPosUseForNow();
            return followAttackTargetState;
        }


        //dusman varsa saldit satate gecer
        actor.FollowPlayer();
        return this;
    }

    

}
