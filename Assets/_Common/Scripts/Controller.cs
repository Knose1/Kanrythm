using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Github.Knose1.Common {
	public class Controller : MonoBehaviour {
		private const string REBINDING_LOG_PREFIX = "[Rebinding] ";
		private static Controller instance;
		public static Controller Instance { get { return instance; } }

		private GameControls input;
		private List<InputAction> rebindListCompare = new List<InputAction>();
		private InputActionRebindingExtensions.RebindingOperation currentActionRebinding;

		public GameControls Input { get => input; }

		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}

			input = new GameControls();

			instance = this;

			
		}

		public void RebindCannon1()
		{
			rebindListCompare.Clear();
			rebindListCompare.Add(input.Gameplay.Cannon2);
			rebindListCompare.Add(input.Gameplay.LockCannon);
			Rebind(input.Gameplay.Cannon1);
		}

		public void RebindCannon2()
		{
			rebindListCompare.Clear();
			rebindListCompare.Add(input.Gameplay.Cannon1);
			rebindListCompare.Add(input.Gameplay.LockCannon);
			Rebind(input.Gameplay.Cannon2);
		}

		public void RebindLockCannon()
		{
			rebindListCompare.Clear();
			rebindListCompare.Add(input.Gameplay.Cannon1);
			rebindListCompare.Add(input.Gameplay.Cannon2);
			Rebind(input.Gameplay.LockCannon);
		}

		public void Rebind(InputAction inputAction)
		{
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
			currentActionRebinding = null;
		}

		private void Rebinding_OnCancel(InputActionRebindingExtensions.RebindingOperation obj)
		{
			Debug.Log(REBINDING_LOG_PREFIX + "Canceled");
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