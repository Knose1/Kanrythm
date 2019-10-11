using Com.Github.Knose1.Kanrythm.Game.Hud;
using Com.Github.Knose1.Kanrythm.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.Screens
{
	class Menu : Screen
	{
		public override void OnAddedToHudContainer(HudContainer hudContainer)
		{
			base.OnAddedToHudContainer(hudContainer);
			MapLoader.StartLoad();
		}
	}
}
