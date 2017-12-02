using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFlagOnExit : StateMachineBehaviour
{

    public string Parameter;
    public bool Value;
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(Parameter, Value);
    }
}
