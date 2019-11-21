using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.Github.Knose1.Common.Scrolling
{
	public interface IScrollingBehaviour
	{
		float ArrowScrollSpeed { get; set; }
		float MouseScrollSpeed { get; set; }

		bool AllowMouseScroll { get; set; }
		bool AllowDrag { get; set; }
		KeyCode KeyScrollHorizontal { get; set; }

		void DoScroll();
		Vector2 GetInput();
	}

	#if !UNITY_EDITOR
	internal class Note : MonoBehaviour {
		public float arrowScrollSpeed = 4;
		public float mouseScrollSpeed = 10;

		protected float scrollX = 0;
		protected float scrollY = 0;

		public float minX = 0;
		public float maxX = 0;

		public float minY = 0;
		public float maxY = 0;

		public string mouseXAxis = "MouseX";
		public string mouseYAxis = "MouseY";

		public AnimationCurve xRelativeCurve = AnimationCurve.Linear(0,0,9000,9000);
		public AnimationCurve yRelativeCurve = AnimationCurve.Linear(0,0,9000,9000);
		public AnimationCurve scaleCurve     = AnimationCurve.Constant(0,1,1);
		public bool scaleCurveOnX = true;

		public bool allowMouseScroll = true;
		public bool allowDrag = false;

		protected Vector3 startPosition;
		protected RectTransform rectTransform;

		public KeyCode keyInvertMouseScroll;

		protected void Awake()
		{
			rectTransform = transform as RectTransform;
		}

		protected void Start()
		{
			SetStartPosition(transform.position);
		}

		private void SetStartPosition(Vector3 position)
		{
			startPosition = position;
		}

		public void doScrollHorizontal()
		{
			float inputHorizontal = GetInput().x;

			//if (allowMouseScroll) return 0;

			Vector3 lPos = transform.localPosition;
			scrollX += inputHorizontal;
			scrollX = Mathf.Clamp(scrollX, minX, maxX);

			if (minX > maxX) minX = maxX;

			lPos.x = xRelativeCurve.Evaluate(scrollX) + startPosition.x;

			transform.localPosition = lPos;

			if (scaleCurveOnX) ScaleObjects();
		}

		private void ScaleObjects()
		{
			transform.localScale = scaleCurve.Evaluate(scrollX) * Vector3.one;
		}

		public void doScrollVertical()
		{
			float inputVertical = GetInput().y;

			Vector3 lPos = transform.localPosition;
			scrollY += inputVertical;
			scrollY = Mathf.Clamp(scrollY, minY, maxY);

			if (minY > maxY) minY = maxY;

			lPos.y = yRelativeCurve.Evaluate(scrollY);

			transform.localPosition = lPos;

			if (!scaleCurveOnX) transform.localScale = scaleCurve.Evaluate(scrollY) * Vector3.one;
		}

		private Vector2 GetInput()
		{
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, Camera.current, out Vector2 lLocalPoint))
			{
				if (Input.mouseScrollDelta != Vector2.zero && allowMouseScroll)
				{
					Vector2 lInputMouseScroll = Input.mouseScrollDelta;

					if (Input.GetKey(keyInvertMouseScroll))
					{
						float lTempX = lInputMouseScroll.x;
						lInputMouseScroll.x = lInputMouseScroll.y;
						lInputMouseScroll.y = lTempX;
					}

					return Input.mouseScrollDelta * mouseScrollSpeed;
				}
				else if (Input.GetMouseButton(0) && allowDrag)
				{
					return new Vector2(Input.GetAxis(mouseXAxis), Input.GetAxis(mouseYAxis)) * arrowScrollSpeed;
				}

			}

			return Vector2.zero;
		}
	}
	#endif
}
