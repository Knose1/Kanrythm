using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.Github.Knose1.Common.Scrolling
{
	[AddComponentMenu("Layout/Vertical Scrollable Layout Group")]
	[RequireComponent(typeof(ScrollingConfig))]
	public class VerticalScrollableLayoutGroup : VerticalLayoutGroup, IScrollingBehaviour
	{
		private ScrollingConfig scrollingConfig;
		public float ArrowScrollSpeed		{ get => scrollingConfig.arrowScrollSpeed;		set => scrollingConfig.arrowScrollSpeed = value;	}
		public float MouseScrollSpeed		{ get => scrollingConfig.mouseScrollSpeed;		set => scrollingConfig.mouseScrollSpeed = value;	}
		public bool AllowMouseScroll		{ get => scrollingConfig.allowMouseScroll;		set => scrollingConfig.allowMouseScroll = value;	}
		public bool AllowDrag				{ get => scrollingConfig.allowDrag;				set => scrollingConfig.allowDrag = value;			}
		public KeyCode KeyScrollHorizontal	{ get => scrollingConfig.keyScrollHorizontal;	set => scrollingConfig.keyScrollHorizontal = value;	}
		


		protected override void Awake()
		{
			base.Awake();
			scrollingConfig = GetComponent<ScrollingConfig>(); 
		}

		public void DoScroll()
		{
			Vector3 lPosition = transform.position;
			lPosition.x = -(transform.childCount - padding.right) * (((RectTransform)transform).sizeDelta.x - spacing);

			transform.position = lPosition;
		}

		public Vector2 GetInput()
		{
			throw new NotImplementedException();
		}

		public override void CalculateLayoutInputVertical()
		{
			base.CalculateLayoutInputHorizontal();

			Transform lChild;
			LayoutElement lChildLayout;
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				lChild = transform.GetChild(i);
				lChildLayout = lChild.GetComponent<LayoutElement>();
				if (!lChildLayout) lChildLayout = lChild.gameObject.AddComponent<LayoutElement>();

			}
		}
	}
}
