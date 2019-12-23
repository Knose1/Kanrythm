///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 26/11/2019 15:54
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.Github.Knose1.Common {
	public class PulseCamera : MonoBehaviour {

		public Camera cameraToPulse;

		private float minFOE = 0;
		public float deltaFOE = 10;
		public AnimationCurve pulseCurve = AnimationCurve.Linear(0,0,1,1);
		public bool fromMinToMax = true;
		public bool pingPong = false;
		[Tooltip("The time for one pulse in seconds")] public float pulseTime = 1;

		private float timestamp = 0;

		private void Start()
		{
			minFOE = cameraToPulse.fieldOfView;

			timestamp = -pulseTime;
		}

		private void Update ()
		{
			float lElapsedTime = StretchableDeltaTime.Instance.ElapsedTime;
			float lLerp = (fromMinToMax ? 1 : -1) * (lElapsedTime - timestamp) / pulseTime;

			if (!fromMinToMax) lLerp += 1;


			if			(lLerp >= 1 && fromMinToMax)	cameraToPulse.fieldOfView = minFOE;
			else if		(lLerp <= 0 && !fromMinToMax)	cameraToPulse.fieldOfView = minFOE + deltaFOE;
			else										cameraToPulse.fieldOfView = Mathf.Lerp(minFOE, minFOE + deltaFOE, pulseCurve.Evaluate(lLerp));

		}

		public void Pulse()
		{
			timestamp = StretchableDeltaTime.Instance.ElapsedTime;

			if (pingPong)
				fromMinToMax = !fromMinToMax;
		}
	}
}