using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

namespace Com.Github.Knose1.Common.Scrolling
{
	public class ScrollingBehaviour : MonoBehaviour
	{
		public float arrowScrollSpeed = 4;
		public float mouseScrollSpeed = 10;

		protected float scrollX = 0;
		protected float scrollY = 0;

		public float minX = 0;
		public float maxX = 0;

		public float minY = 0;
		public float maxY = 0;

		public string horizontalAxis = "Horizontal";
		public string verticalAxis = "Vertical";

		public KeyCode keyInvertMouseScroll = KeyCode.LeftShift;

		public bool allowMouseScroll;

		private Vector3 startPosition;

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
			lPos.x += inputHorizontal;

			if (minX > maxX) minX = maxX;

			lPos.x = Mathf.Clamp(scrollX, minX, maxX) + startPosition.x;

			transform.localPosition = lPos;
		}

		public void doScrollVertical()
		{
			float inputVertical = GetInput().y;

			Vector3 lPos = transform.localPosition;
			lPos.y += inputVertical;

			if (minY > maxY) minY = maxY;

			lPos.y = Mathf.Clamp(scrollY, minY, maxY);

			transform.localPosition = lPos;
		}

		private Vector2 GetInput()
		{
			if (Input.mouseScrollDelta != Vector2.zero && allowMouseScroll)
			{
				Vector2 lInputMouseScroll = Input.mouseScrollDelta;

				if (Input.GetKey(keyInvertMouseScroll)) {
					float lTempX = lInputMouseScroll.x;
					lInputMouseScroll.x = lInputMouseScroll.y;
					lInputMouseScroll.y = lTempX;
				}

				return Input.mouseScrollDelta * mouseScrollSpeed;
			}
			return new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis)) * arrowScrollSpeed;
		}

	}
}
