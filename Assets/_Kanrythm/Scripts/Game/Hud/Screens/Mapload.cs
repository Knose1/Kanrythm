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
			DataLoader.StartLoad(DataLoader_OnFinish);
		}

		private void DataLoader_OnFinish()
		{
			Debug.Log(nameof(DataLoader)+ " has finished");
			HudContainer.SetScreen(HudManager.Instance.GetTemplateMenu());
		}
		
	}
}
