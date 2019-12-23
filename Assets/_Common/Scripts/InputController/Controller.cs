using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Github.Knose1.Common.InputController {

	public struct RebindingFunction
	{
		public readonly Action function;
		public readonly string nameInInspector;

		public RebindingFunction(Action function, string nameInInspector)
		{
			this.function = function;
			this.nameInInspector = nameInInspector;
		}
	}

	public partial class Controller : MonoBehaviour
	{
		private const string REBINDING_LOG_PREFIX = "[Rebinding] ";
		private static Controller instance;
		public static Controller Instance { get { return instance; } }

		private List<InputAction> rebindListCompare;
		protected List<RebindingFunction> rebindingFunctions;
		internal List<RebindingFunction> RebindingFunctions => rebindingFunctions;

		protected InputActionRebindingExtensions.RebindingOperation currentActionRebinding;

		public static event Action<InputControl> OnRebindEnd;

		private void Awake() {
			if (instance){
				Destroy(gameObject);
				return;
			}

			InitControllerProject();

			instance = this;
		}


		public void Rebind(InputAction inputAction, List<InputAction> rebindListCompare)
		{
			this.rebindListCompare = rebindListCompare;
			if (currentActionRebinding != null) currentActionRebinding.Cancel();

			Debug.Log(REBINDING_LOG_PREFIX + inputAction.name);

			currentActionRebinding = inputAction.PerformInteractiveRebinding()
				.OnPotentialMatch(Rebinding_OnPotentialMatch)
				.OnCancel(Rebinding_OnCancel)
				.OnComplete(Rebinding_OnComplete);
			currentActionRebinding.Start();
		}

		private void Rebinding_OnComplete(InputActionRebindingExtensions.RebindingOperation obj)
		{
			Debug.Log(REBINDING_LOG_PREFIX + "Complete");
			OnRebindEnd(obj.selectedControl);
			currentActionRebinding = null;
		}

		private void Rebinding_OnCancel(InputActionRebindingExtensions.RebindingOperation obj)
		{
			Debug.Log(REBINDING_LOG_PREFIX + "Canceled");
			OnRebindEnd(obj.action.activeControl);
			currentActionRebinding = null;
		}

		private void Rebinding_OnPotentialMatch(InputActionRebindingExtensions.RebindingOperation obj)
		{
			Debug.Log(REBINDING_LOG_PREFIX + obj.selectedControl.path);

			if (obj.selectedControl == Keyboard.current.escapeKey)
			{
				obj.Cancel();
				return;
			}

			for (int i = rebindListCompare.Count - 1; i >= 0; i--)
			{
				if (obj.selectedControl == rebindListCompare[i].controls[0])
				{
					obj.Cancel();
					return;
				}
			}

			obj.Complete();
		}

		private void Update()
		{
		}

		private void OnDestroy(){
			if (this == instance) instance = null;

			input.Disable();
		}
	}
} 