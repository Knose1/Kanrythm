///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 26/11/2019 15:54
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Knose1.Common {
	public enum PulseType
	{
		Pulse3D,
		Pulse2D
	}
	public class PulseCamera : MonoBehaviour {

		public Camera cameraToPulse;

		
		private float minFOE = 0;
		public float deltaFOE = 10;
		public AnimationCurve pulseCurve = AnimationCurve.Linear(0,0,1,1);
		public bool fromMinToMax = true;
		public bool pingPong = false;
		public PulseType pulseType;
		[Tooltip("The time for one pulse in seconds")] public float pulseTime = 1;

		private float timestamp = 0;

		private void Start()
		{
			switch (pulseType)
			{
				case PulseType.Pulse3D:
					minFOE = cameraToPulse.fieldOfView;
					break;
				case PulseType.Pulse2D:
					minFOE = cameraToPulse.orthographicSize;
					break;
				default:
					break;
			}

			timestamp = -pulseTime;
		}

		private void Update ()
		{
			switch (pulseType)
			{
				case PulseType.Pulse3D:
					cameraToPulse.fieldOfView = DoPulse();
					break;
				case PulseType.Pulse2D:
					cameraToPulse.orthographicSize = DoPulse();
					break;
				default:
					break;
			}

			
		}

		public float DoPulse()
		{
			float lToReturn = 0;
			float lElapsedTime = StretchableDeltaTime.Instance.ElapsedTime;
			float lLerp = (fromMinToMax ? 1 : -1) * (lElapsedTime - timestamp) / pulseTime;

			if (!fromMinToMax) lLerp += 1;

			if (lLerp >= 1 && fromMinToMax) lToReturn = minFOE;
			else if (lLerp <= 0 && !fromMinToMax) lToReturn = minFOE + deltaFOE;
			else lToReturn = Mathf.Lerp(minFOE, minFOE + deltaFOE, pulseCurve.Evaluate(lLerp));

			return lToReturn;

		}

		public void Pulse()
		{
			timestamp = StretchableDeltaTime.Instance.ElapsedTime;

			if (pingPong)
				fromMinToMax = !fromMinToMax;
		}
	}

	public static class PulseCameraExtension
	{
		public static void PulseAll(this List<PulseCamera> pulseCameras)
		{
			foreach (PulseCamera pulseCamera in pulseCameras)
			{
				pulseCamera.Pulse();
			}
		}

		public static void EnableAll(this List<PulseCamera> pulseCameras, bool enabled = true)
		{
			foreach (PulseCamera pulseCamera in pulseCameras)
			{
				pulseCamera.enabled = enabled;
			}
		}
	}
}