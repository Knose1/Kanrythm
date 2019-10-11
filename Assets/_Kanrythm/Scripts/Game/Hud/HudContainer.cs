using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Github.Knose1.Kanrythm.Game.Hud.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud
{
	public class HudContainer : MonoBehaviour
	{
		public event Action<Canvas, CanvasScaler, GraphicRaycaster, RectTransform> OnAwake;

		public void Awake()
		{
			Canvas lCanvas = gameObject.AddComponent<Canvas>();
			CanvasScaler lCanvasScaler = gameObject.AddComponent<CanvasScaler>();
			GraphicRaycaster lGraphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
			RectTransform lRectTransform = gameObject.AddComponent<RectTransform>();

		}

		public void SetScreen(Preload preloadTemplate)
		{
			throw new NotImplementedException();
		}

		public void ClearScreen()
		{
			throw new NotImplementedException();
		}
	}
}