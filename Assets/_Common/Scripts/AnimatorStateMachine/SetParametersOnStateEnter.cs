using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Com.Github.Knose1.Common.AnimatorStateMachine
{
	public class SetParametersOnStateEnter : SetParameterOnState
	{
		

		// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			for (int i = triggers.Count - 1; i >= 0; i--)
			{
				animator.SetTrigger(triggers[i]);
			}

			for (int i = bools.Count - 1; i >= 0; i--)
			{
				animator.SetBool(bools[i], boolsValues[i]);
			}

			for (int i = floats.Count - 1; i >= 0; i--)
			{
				animator.SetFloat(floats[i], floatsValues[i]);
			}

			for (int i = ints.Count - 1; i >= 0; i--)
			{
				animator.SetInteger(ints[i], intsValues[i]);
			}
		}
	}
}
