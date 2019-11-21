using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Com.Github.Knose1.Common.Scrolling
{
	[AddComponentMenu("Layout/Scrolling Config")]
	public class ScrollingConfig : MonoBehaviour
	{
		public float arrowScrollSpeed = 4;
		public float mouseScrollSpeed = 10;
		public bool allowMouseScroll = true;
		public bool allowDrag = false;
		public KeyCode keyScrollHorizontal = KeyCode.LeftShift;

		public string mouseXAxis = "MouseX";
		public string mouseYAxis = "MouseY";

		public AnimationCurve xRelativeCurve = AnimationCurve.Linear(0,0,9000,9000);
		public AnimationCurve yRelativeCurve = AnimationCurve.Linear(0,0,9000,9000);
		public AnimationCurve scaleCurve     = AnimationCurve.Constant(0,1,1);
	}
}
