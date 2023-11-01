using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_State : MonoBehaviour
{
    public abstract AI_State RunState(Ally actor);
  
}
