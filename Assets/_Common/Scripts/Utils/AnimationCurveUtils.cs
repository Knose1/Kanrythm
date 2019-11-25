using System;
using System.Linq;
using UnityEngine;

namespace Com.Github.Knose1.Common.Utils
{
	public static class AnimationCurveUtils
	{
		public static float GetLength(AnimationCurve animationCurve, float step = 0.03f)
		{
			float length = 0;

			GetMinAndMaxTime(animationCurve, out float lMinCurve, out float lMaxCurve);

			float previousStepValue = animationCurve.Evaluate(lMinCurve);
			float currentStep = lMinCurve + step;

			while (currentStep != lMaxCurve)
			{
				length += Math.Abs(animationCurve.Evaluate(currentStep) - previousStepValue);

				if (currentStep > lMaxCurve) currentStep = lMaxCurve;

				currentStep += step;
			}

			return length;
		}

		public static float GetLength(AnimationCurve animationCurveX, AnimationCurve animationCurveY, float step = 0.03f)
		{
			float length = 0;

			GetMinAndMaxTime(animationCurveX, animationCurveY, out float lMinCurve, out float lMaxCurve);

			Vector2 previousStepValue = new Vector2(animationCurveX.Evaluate(lMinCurve), animationCurveY.Evaluate(lMinCurve));
			float currentStep = lMinCurve + step;

			while (currentStep != lMaxCurve)
			{
				length += (new Vector2(animationCurveX.Evaluate(currentStep), animationCurveY.Evaluate(currentStep)) - previousStepValue).magnitude;

				if (currentStep > lMaxCurve) currentStep = lMaxCurve;

				currentStep += step;
			}

			return length;
		}

		public static void GetMinAndMaxTime(AnimationCurve animationCurve, out float min, out float max)
		{
			min = animationCurve.keys.FirstOrDefault().time;
			max = animationCurve.keys.LastOrDefault().time;
		}

		public static void GetMinAndMaxTime(AnimationCurve animationCurve, AnimationCurve animationCurve2, out float min, out float max)
		{
			min = animationCurve.keys.FirstOrDefault().time;
			max = animationCurve.keys.LastOrDefault().time;

			float lMin2 = animationCurve2.keys.FirstOrDefault().time;
			float lMax2 = animationCurve2.keys.LastOrDefault().time;

			if (lMin2 < min) min = lMin2;
			if (lMax2 > max) max = lMax2;
		}

	}
}
