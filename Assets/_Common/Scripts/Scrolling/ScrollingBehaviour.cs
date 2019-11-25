using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.Github.Knose1.Common.Scrolling
{
	[ExecuteAlways]
	abstract public class ScrollingBehaviour : UIBehaviour
	{

		[Header("Input")]
		public float dragScrollSpeed = 0.01f;
		public float mouseScrollSpeed = 0.05f;
		public string mouseXAxis = "MouseX";
		public string mouseYAxis = "MouseY";
		public KeyCode keyScrollHorizontal = KeyCode.LeftShift;
		public bool allowMouseScroll = true;
		public bool allowDrag = false;

		[Header("Curve")]
		[Tooltip("The min value of the curves must be 0 and the max 1 on the two axis")]
		public AnimationCurve xRelativeCurve = AnimationCurve.Linear(0,0,1,0);
		public AnimationCurve yRelativeCurve = AnimationCurve.Linear(0,0,1,0);
		public AnimationCurve scaleCurve     = AnimationCurve.Constant(0,1,1);

		public RectOffset curveRect;
		public float minCurveScale;
		public float maxCurveScale;

		[Header("Other")]
		public float maxVisibleChild = 1;
		[Range(0,1)] public float scroll = 0.5f;
		[NonSerialized] public float _scroll = 0.5f;
		public bool inspectorOverrideScroll = true;
		public bool inspectorRelativeScroll = false;

		protected RectTransform rectTransform;

		public abstract void DoScroll();

		// Rappel envoyé au graphique après une modification des enfants de Transform
		protected void OnTransformChildrenChanged()
		{
			Start();
			UpdateChildTransform();
		}

		protected override void OnRectTransformDimensionsChange()
		{
			OnTransformChildrenChanged();
		}

		protected void UpdateChildTransform()
		{
			if (inspectorOverrideScroll) _scroll = scroll;

			float lScroll = _scroll;

			if (inspectorRelativeScroll) lScroll += scroll;

			lScroll = Mathf.Clamp(lScroll, 0, 1);

			int lChildCount = transform.childCount;
			RectTransform lChildTransform;

			float lCurrentEvaluate = 0;
			float lCurrentEvaluate2 = 0;

			float lCurveXMin = rectTransform.rect.xMin + curveRect.left;
			float lCurveXMax = rectTransform.rect.width / 2 - curveRect.right;
			float lCurveYMin = rectTransform.rect.yMin + curveRect.top;
			float lCurveYMax = rectTransform.rect.height / 2 - curveRect.bottom;

			float lCurveX;
			float lCurveY;

			float lTotalPriority = GetPriority(out List<float> lPriorityList);

			float lMax = (lTotalPriority + 1  - (lTotalPriority - maxVisibleChild)/2) / maxVisibleChild;
			float lMin = -lMax;

			for (int i = 0; i < lChildCount; i++)
			{
				lChildTransform = transform.GetChild(i) as RectTransform;

				lCurrentEvaluate = (lPriorityList[i] - (lTotalPriority - maxVisibleChild) / 2) / maxVisibleChild;

				lCurrentEvaluate2 = lCurrentEvaluate + Mathf.Lerp(lMin, lMax, lScroll);


				//lCurrentEvaluate2 = Mathf.Clamp(lCurrentEvaluate2, 0, 1);


				lCurveX = xRelativeCurve.Evaluate(lCurrentEvaluate2) * 2 * lCurveXMax + lCurveXMin;
				lCurveY = yRelativeCurve.Evaluate(lCurrentEvaluate2) * 2 * lCurveYMax + lCurveYMin;

				if (lCurrentEvaluate2 > 1)
				{
					lCurveX += lChildTransform.rect.width;
					lCurveY += lChildTransform.rect.height;
				}
				else if (lCurrentEvaluate2 < 0)
				{
					lCurveX -= lChildTransform.rect.width;
					lCurveY -= lChildTransform.rect.height;
				}

				lChildTransform.localPosition = new Vector2(lCurveX, lCurveY);

				lChildTransform.localScale = Vector3.one * (scaleCurve.Evaluate(lCurrentEvaluate2) * (maxCurveScale - minCurveScale) + minCurveScale);
			}
		}

		protected float GetPriority() => GetPriority(indexInPriority: out _);
		protected float GetPriority(out List<float> indexInPriority)
		{
			ScrollingLayoutElement lChildLayoutElement;
			float lChildCount = transform.childCount;
			float lTotalPriority = 0;
			indexInPriority = new List<float>();

			for (int i = 0; i < lChildCount; i++)
			{
				lChildLayoutElement = transform.GetChild(i).GetComponent<ScrollingLayoutElement>();
				if (lChildLayoutElement)
				{
					lTotalPriority += lChildLayoutElement.priority;
					indexInPriority.Add(lTotalPriority - lChildLayoutElement.priority / 2);
					continue;
				}
				lTotalPriority += 1;
				indexInPriority.Add(lTotalPriority);
			}

			return lTotalPriority;
		}

		private void SetFocusOnChild(RectTransform childRect)
		{
			if (childRect.parent != transform)
				throw new Exception("\'" + childRect.gameObject.name + "\' is not a child of \'" + gameObject.name + "\'.");

			throw new NotImplementedException();
		}

		protected Vector2 GetInput()
		{
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, Camera.current, out Vector2 lLocalPoint))
			{
				if (Input.mouseScrollDelta != Vector2.zero && allowMouseScroll)
				{
					Vector2 lInputMouseScroll = Input.mouseScrollDelta;

					if (Input.GetKey(keyScrollHorizontal))
					{
						float lTempX = lInputMouseScroll.x;
						lInputMouseScroll.x = lInputMouseScroll.y;
						lInputMouseScroll.y = lTempX;
					}

					return lInputMouseScroll * mouseScrollSpeed;
				}
				else if (Input.GetMouseButton(0) && allowDrag)
				{
					return new Vector2(Input.GetAxis(mouseXAxis), Input.GetAxis(mouseYAxis)) * dragScrollSpeed;
				}

			}

			return Vector2.zero;
		}

		protected override void Start()
		{
			base.Start();
			rectTransform = transform as RectTransform;
		}

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			Start();

			if (maxVisibleChild < 1) maxVisibleChild = 1;
			UpdateChildTransform();
		}
#endif
	}
}
