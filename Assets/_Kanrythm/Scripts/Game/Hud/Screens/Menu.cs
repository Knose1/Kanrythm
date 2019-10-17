using Com.Github.Knose1.Kanrythm.Game;
using Com.Github.Knose1.Kanrythm.Game.Hud;
using Com.Github.Knose1.Kanrythm.Game.Hud.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.Screens
{
	class Menu : Screen
	{
		[SerializeField] private MapUiTempManager mapUiTempManager;


		public override void OnAddedToHudContainer(HudContainer hudContainer)
		{
			base.OnAddedToHudContainer(hudContainer);
			mapUiTempManager.OnSelectedMapAndDifficulty += MapButtonContainer_OnSelectedMapAndDifficulty;
		}

		public override void OnRemovedFromHudContainer(HudContainer hudContainer)
		{
			base.OnRemovedFromHudContainer(hudContainer);
			mapUiTempManager.OnSelectedMapAndDifficulty -= MapButtonContainer_OnSelectedMapAndDifficulty;
		}


		private void MapButtonContainer_OnSelectedMapAndDifficulty(int mapId, int diffId)
		{
			MenuPlayMap lScreen = HudManager.Instance.GetTemplateMenuPlayMap();

			lScreen.mapId = (uint)mapId;
			lScreen.diffId = (uint)diffId;

			HudContainer.SetScreen(lScreen);
		}
	}
}
