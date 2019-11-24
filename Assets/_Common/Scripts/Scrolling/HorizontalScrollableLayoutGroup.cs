using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.Github.Knose1.Common.Scrolling
{
	[AddComponentMenu("Layout/Scrollable/Horizontal Scrollable Layout Group")]
	public class HorizontalScrollableLayoutGroup : ScrollingBehaviour
	{

		override public void DoScroll()
		{
			Vector3 lPosition = transform.position;

			float lTotalPriority = GetPriority();
			float lMax = (lTotalPriority + 1  - (lTotalPriority - maxVisibleChild)/2) / maxVisibleChild;
			float lMin = -lMax;

			float inputHorizontal = GetInput().x;

			scroll += inputHorizontal;
			scroll = Mathf.Clamp(scroll, lMin, lMax);

			UpdateChildTransform();
		}
	}
}
