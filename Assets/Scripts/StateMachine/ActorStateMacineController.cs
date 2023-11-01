using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateMacineController : MonoBehaviour
{
    // Start is called before the first frame update
    public AI_State currentState;
    private void Update()
    {
        RunStateMachine();
    }
    private void  RunStateMachine()
    {
        AI_State state = currentState.RunState(GetComponent<Ally>());
        if (currentState != null)
        {
            currentState = state;
        }
    }
}
