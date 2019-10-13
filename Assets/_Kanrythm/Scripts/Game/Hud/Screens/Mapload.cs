using Com.Github.Knose1.Kanrythm.Game.Hud;
using Com.Github.Knose1.Kanrythm.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.Screens
{
	public class Mapload : Screen
	{
		public override void OnAddedToHudContainer(HudContainer hudContainer)
		{
			base.OnAddedToHudContainer(hudContainer);
			MapLoader.StartLoad(MapLoader_OnFinish);
		}

		private void MapLoader_OnFinish()
		{
			Debug.Log("Map loader has finished");
			HudContainer.SetScreen(HudManager.Instance.GetTemplateMenu());
		}
		
	}
}
