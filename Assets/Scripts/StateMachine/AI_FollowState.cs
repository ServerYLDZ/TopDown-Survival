using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AI_FollowState : AI_State
{
    public AI_State followAttackTargetState;
    public AI_State FarmState;
    public AI_State CuttingState;
    public override AI_State RunState(Ally actor)
    {
        if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target) 
        if( GameManager.Instance.playerActor.GetComponent<PlayerController>().target.interactionType == InteractableType.Enemy)
        {
                actor.target = GameManager.Instance.playerActor.GetComponent<PlayerController>().target.transform;
                
                GameManager.Instance.IsActorFolowPlayerPosUseForNow();
                actor.Bar.DOFade(1, 1);
               actor.ArmorBarActive();
                return followAttackTargetState;
        }
        if (actor.currentState == ActorState.Farming)
        {

            return FarmState;
        }
        if (actor.currentState == ActorState.Cuting)
        {

            return CuttingState;
        }

        actor.FollowPlayer();
        return this;
    }

    

}
