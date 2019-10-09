using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Com.Github.Knose1.Common
{
	abstract public class StateMachine : MonoBehaviour
	{
		protected Action doAction;

		virtual protected void Start()
		{
			if (doAction == default) doAction = DoActionVoid;
		}

		virtual protected void Update()
		{
			doAction();
		}

		protected void DoActionVoid() {}
		virtual protected void DoActionNormal() {}
	}
}
