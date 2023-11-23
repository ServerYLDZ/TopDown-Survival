using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FollowState : AI_State
{
    public AI_State followAttackTargetState;
    public AI_State FarmState;
    public override AI_State RunState(Ally actor)
    {
        if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target) 
        if( GameManager.Instance.playerActor.GetComponent<PlayerController>().target.interactionType == InteractableType.Enemy)
        {
                actor.target = GameManager.Instance.playerActor.GetComponent<PlayerController>().target.transform;
                
                GameManager.Instance.IsActorFolowPlayerPosUseForNow();
            return followAttackTargetState;
        }
        if (actor.currentState == ActorState.Farming)
        {

            return FarmState;
        }

        
        actor.FollowPlayer();
        return this;
    }

    

}
