using Com.Github.Knose1.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Com.Github.Knose1.Common.InputController;
using static GameControls;

namespace Com.Github.Knose1.Kanrythm.Game.PlayerType {
	public class Player : StateMachine {

		[SerializeField]
		private Transform canonTargetPosition;

		[Header("Cannon 1 (line)")]
		[SerializeField] private CanonState cannon1Line;

		[Header("Cannon 2 (no line)")]
		[SerializeField] private CanonState cannon2NoLine;

		private SpriteRenderer[] childrenRenderer;
		private List<float> childrenOriginalAlpha;

		/// <summary>
		/// Give the controle to the player
		/// </summary>
		public void EnablePlay()
		{
			Debug.Log("play enabled");
			doAction = DoActionNormal;

			GameplayActions control = Controller.Instance.Input.Gameplay;

			control.LockCannon.performed += LockCannon_performed;

		}
		Vector3 vecBeforeCannonLock = Vector3.right;
		float mouseVsCannonAngleDelta = 0;

		private bool isLockCannonUp;

		/*private float alpha = 1;
		public float Alpha { get => alpha; set {
				
			}
		}*/

		private void LockCannon_performed(InputAction.CallbackContext obj)
		{
			isLockCannonUp = true;
		}

		override protected void DoActionNormal()
		{
			base.DoActionNormal();
			Vector3 lMousePos = Input.mousePosition;
			Vector3 lVec = new Vector3
			{
				x = lMousePos.x - Screen.width / 2,
				y = lMousePos.y - Screen.height / 2
			};



			Debug.DrawLine(Vector3.zero, lVec, new Color(1,0,0,1));
			Debug.DrawLine(Vector3.zero, vecBeforeCannonLock, new Color(0,1,0,1));

			GameplayActions control = Controller.Instance.Input.Gameplay;

			if (control.LockCannon.phase == InputActionPhase.Started)
			{
				//cannon1Line.localRotation = cannon1Line.rotation;
				cannon2NoLine.transform.rotation = Quaternion.Euler(0, 0, (float)Math.Atan2(-lVec.y, -lVec.x) * Mathf.Rad2Deg);
			}
			else if (isLockCannonUp)
			{
				mouseVsCannonAngleDelta += Vector3.SignedAngle(vecBeforeCannonLock, lVec, Vector3.back);
				isLockCannonUp = false;
			}
			else
			{
				vecBeforeCannonLock = lVec;
				transform.rotation = Quaternion.Euler(0, 0, mouseVsCannonAngleDelta + (float)Math.Atan2(lVec.y, lVec.x) * Mathf.Rad2Deg);
			}

			cannon1Line.IsTriggered   = control.Cannon1.ReadValue<float>() != 0;
			cannon2NoLine.IsTriggered = control.Cannon2.ReadValue<float>() != 0;

		}

		public float GetCannonRadius()
		{
			return canonTargetPosition.position.magnitude;
		}

		private void OnDestroy()
		{
			if (Controller.Instance) Controller.Instance.Input.Gameplay.LockCannon.performed -= LockCannon_performed;
		}
	}
}