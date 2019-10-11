using Com.Github.Knose1.Kanrythm.Game;
using Com.Github.Knose1.Kanrythm.Game.Hud;
using Com.Github.Knose1.Kanrythm.Game.Hud.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game
{
	class HudManager : MonoBehaviour
	{
		[SerializeField] private GameManager gameManager;
		[SerializeField] private HudContainer hudContainer;

		[SerializeField] private Preload preloadTemplate;

		private void Start()
		{
			gameManager.OnStart += GameManager_OnStart;

			Preload lTemplate = Instantiate(preloadTemplate);
			hudContainer.SetScreen(lTemplate);

		}

		private void GameManager_OnStart()
		{
			throw new NotImplementedException();
		}

		private void HudContainer_Awake(in Canvas canvas, in CanvasScaler canvasScaler, in GraphicRaycaster graphicRaycaster, in RectTransform rectTransform)
		{
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.pixelPerfect = false;

			canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			canvasScaler.referenceResolution = new Vector2(2700, 1400);
			canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Shrink;
			canvasScaler.referencePixelsPerUnit = 100;

			graphicRaycaster.ignoreReversedGraphics = true;
			graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
		}
	}
}
