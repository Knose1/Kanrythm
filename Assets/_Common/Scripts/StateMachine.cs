using System;
using UnityEngine;

namespace Com.Github.Knose1.Common
{
	/// <summary>
	/// A class that provides a way to change the "Update" implementation depending on the state.<br/>
	/// To set the state of an object, just write something like this :
	/// doAction = DoActionMyAction;
	/// </summary>
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
