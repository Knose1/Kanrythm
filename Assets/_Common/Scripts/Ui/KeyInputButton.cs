///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 22/12/2019 23:44
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Com.Github.Knose1.Common.InputController;

namespace Com.Github.Knose1.Common.Ui {
	[Serializable]
	public class RebindStartEvent : UnityEvent
	{

	}
	public class RebindEndEvent : UnityEvent<InputControl>
	{

	}

	public class KeyInputButton : UIBehaviour
	{

		[SerializeField] private RebindStartEvent startRebind;
		public RebindStartEvent StartRebind { get => startRebind; }

		[SerializeField] private RebindEndEvent endRebind;
		public RebindEndEvent EndRebind { get => endRebind; }


		[SerializeField, HideInInspector] protected int rebindedFunction;
		internal int RebindedFuction { get => rebindedFunction; set => rebindedFunction = value; }

		private void OnMouseDown()
		{
			startRebind.Invoke();
			Controller.OnRebindEnd += Controller_OnRebindEnd;
			Controller.Instance.RebindingFunctions[rebindedFunction].function();
		}

		private void Controller_OnRebindEnd(InputControl obj)
		{
			endRebind.Invoke(obj);
		}
	}
}