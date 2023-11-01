using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_IdleState : AI_State
{
    public AI_State followState;
    public override AI_State RunState(Ally actor)
    {
        if (actor.currentState == ActorState.Follow)
        {
            return followState;
        }

        return this;
    }

 
    
}
