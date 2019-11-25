using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Com.Github.Knose1.Common.AnimatorStateMachine
{
	public abstract class SetParameterOnState : StateMachineBehaviour
	{
		public List<string> triggers;

		public List<string> bools;
		public List<bool>   boolsValues;

		public List<string> floats;
		public List<float>  floatsValues;

		public List<string> ints;
		public List<int>    intsValues;
	}
}
