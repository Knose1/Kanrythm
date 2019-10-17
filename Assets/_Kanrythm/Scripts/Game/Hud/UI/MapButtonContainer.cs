using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.UI
{
	class MapButtonContainer : MonoBehaviour
	{
		[SerializeField] RectTransform mask2;
		[SerializeField] Shadow mapButtonShadow;

		public void SetScaleY(float scaleY)
		{
			Vector2 lVec = ((RectTransform)transform).sizeDelta;

			lVec.y = scaleY - mapButtonShadow.effectDistance.y;

			((RectTransform)transform).sizeDelta = lVec;

			//------------------------------------------

			lVec = mask2.sizeDelta;

			lVec.y = scaleY;

			mask2.sizeDelta = lVec;
		}
	}
}
