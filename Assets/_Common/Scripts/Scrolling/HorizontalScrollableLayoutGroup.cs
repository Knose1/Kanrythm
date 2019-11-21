using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.Github.Knose1.Common.Scrolling
{
	[AddComponentMenu("Layout/Horizontal Scrollable Layout Group")]
	public class HorizontalScrollableLayoutGroup : HorizontalLayoutGroup, IScrollingBehaviour
	{
		[SerializeField] public float ArrowScrollSpeed { get; set; } = 4;
		[SerializeField] public float MouseScrollSpeed { get; set; } = 10;

		[SerializeField] public bool AllowMouseScroll { get; set; } = false;

		[SerializeField] public bool AllowDrag { get; set; } = false;

		[SerializeField] public KeyCode KeyScrollHorizontal { get; set; } = KeyCode.LeftShift;

		public void DoScroll()
		{
			throw new NotImplementedException();
		}

		public Vector2 GetInput()
		{
			throw new NotImplementedException();
		}

		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
		}
	}
}
