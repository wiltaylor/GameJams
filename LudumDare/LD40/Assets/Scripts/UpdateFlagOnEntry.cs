using UnityEngine;

public class UpdateFlagOnEntry : StateMachineBehaviour
{

    public string Parameter;
    public bool Value;
	 
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    animator.SetBool(Parameter, Value);
	}
}
