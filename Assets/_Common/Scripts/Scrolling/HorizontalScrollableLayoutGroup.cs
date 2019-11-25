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
			float inputHorizontal = GetInput().x;

			_scroll += inputHorizontal;
			_scroll = Mathf.Clamp(_scroll, 0, 1);

			UpdateChildTransform();
		}
	}
}
