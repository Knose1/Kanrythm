using Com.Github.Knose1.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Com.Github.Knose1.Common
{
	public class StretchableDeltaTime : StateMachine, IDisposable
	{
		private float startTime = -1;
		private float scaleTime = 1;
		private float elapsedTime = 0;
		private float scaledDeltaTime;

		private static StretchableDeltaTime instance;

		public static StretchableDeltaTime Instance { get => instance; }

		public float StartTime { get => startTime; }
		public float ScaleTime { get => scaleTime; set => scaleTime = Math.Max(0.001f, value); }
		public float ElapsedTime { get => elapsedTime; }
		public float ScaledDeltaTime { get => scaledDeltaTime; }
		public bool IsPlaying { get => doAction != DoActionVoid; }

		public StretchableDeltaTime() {
			if (instance) instance.Dispose();

			instance = this;
		}

		public void StartDeltaTime()
		{
			startTime = Time.time;
			doAction = DoActionNormal;
		}

		/// <summary>
		/// 
		/// </summary>
		public void TogglePauseDeltaTime()
		{
			TogglePauseDeltaTime(doAction == DoActionNormal);
		}
		public void TogglePauseDeltaTime(bool setToPause)
		{
			if (!setToPause)
			{
				doAction = DoActionNormal;
			}
			else
			{
				doAction = DoActionVoid;
			}
		}

		public void StopDeltaTime()
		{
			doAction = DoActionVoid;
			startTime = -1;
			scaleTime = 1;
			elapsedTime = 0;
			scaledDeltaTime = 0;
		}

		protected override void DoActionNormal()
		{
			base.DoActionNormal();

			float lNewDelta = Time.deltaTime * scaleTime;

			scaledDeltaTime = lNewDelta;
			elapsedTime += lNewDelta;

		}

		public void Dispose()
		{
			instance = null;
			Destroy(this);
		}

		public float GetElapsedTimeRatio(float start, float targetTime)
		{
			return (ElapsedTime - start) / targetTime;
		}
	}
}
