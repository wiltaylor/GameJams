using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButtonTriggers : StateMachineBehaviour
{

    public List<string> Triggers;
	
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(var trig in Triggers)
	        animator.ResetTrigger(trig);
    }

}
