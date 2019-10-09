using Com.Github.Knose1.Common;
using System;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game.PlayerType {
	public class Player : StateMachine {

		[SerializeField]
		private Transform canonTargetPosition;

		[Header("Cannon 1 (line)")]
		[SerializeField] private CanonState cannon1Line;
		[SerializeField] private string inputClickCannon1Line;
		[SerializeField] private string inputLockCannon1Line;

		[Header("Cannon 2 (no line)")]
		[SerializeField] private CanonState cannon2NoLine;
		[SerializeField] private string inputClickCannon2NoLine;

		/// <summary>
		/// Donne le contr�le au joueur
		/// </summary>
		public void EnablePlay()
		{
			Debug.Log("play enabled");
			doAction = DoActionNormal;
		}

		Vector3 vecBeforeCannonLock = Vector3.right;
		float mouseVsCannonAngleDelta = 0;

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


			if (Input.GetButton(inputLockCannon1Line))
			{
				//cannon1Line.localRotation = cannon1Line.rotation;
				cannon2NoLine.transform.rotation = Quaternion.Euler(0, 0, (float)Math.Atan2(-lVec.y, -lVec.x) * Mathf.Rad2Deg);
			}
			else if (Input.GetButtonUp(inputLockCannon1Line))
			{
				mouseVsCannonAngleDelta += Vector3.SignedAngle( vecBeforeCannonLock, lVec, Vector3.back);
			}
			else
			{
				vecBeforeCannonLock = lVec;
				transform.rotation = Quaternion.Euler(0, 0, mouseVsCannonAngleDelta + (float)Math.Atan2(lVec.y, lVec.x) * Mathf.Rad2Deg);
			}

			cannon1Line.IsTriggered   = Input.GetButton(inputClickCannon1Line  );
			cannon2NoLine.IsTriggered = Input.GetButton(inputClickCannon2NoLine);

		}

		public float GetCannonRadius()
		{
			return canonTargetPosition.position.magnitude;
		}
	}
}