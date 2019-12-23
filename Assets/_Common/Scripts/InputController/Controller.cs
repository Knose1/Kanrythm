using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Github.Knose1.Common.InputController {

	public struct RebindingFunction
	{
		public readonly Action function;
		public readonly string nameInInspector;
		public readonly string defaultKeyName;

		public RebindingFunction(Action function, string nameInInspector, string defaultKeyName)
		{
			this.defaultKeyName = defaultKeyName;
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
				throw new Exception("There are two active Controller in the scene");
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
			OnRebindEnd(obj.action.controls[0]);
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

		#if UNITY_EDITOR
		[Obsolete("This function can ONLY be used by Unity To generate a new Controller", false)]
		public static void TryCreateController(UnityEditor.MenuCommand menu)
		{
			if (!FindObjectOfType<Controller>()) CreateController(menu);
		}
		[Obsolete("This function can ONLY be used by Unity to generate a new Controller", false), MenuItem("GameObject/Input/Controller", false, 10)]
		
		public static void CreateController(UnityEditor.MenuCommand menu)
		{
			if (FindObjectOfType<Controller>()) throw new Exception("There is already an active Controller in the scene");

			//Root GameObject
			GameObject gameObject = new GameObject("Controller");
			Controller controller = gameObject.AddComponent<Controller>();
		}
		#endif
	}
} 