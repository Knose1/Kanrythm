using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.Github.Knose1.Common.Scrolling
{
	[AddComponentMenu("Layout/Scrollable/Vertical Scrollable Layout Group")]
	public class VerticalScrollableLayoutGroup : ScrollingBehaviour
	{
		override public void DoScroll()
		{
			float inputVertical = GetInput().y;

			_scroll += inputVertical;
			_scroll = Mathf.Clamp(_scroll, 0, 1);

			UpdateChildTransform();
		}
	}
}
