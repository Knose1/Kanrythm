using Com.Github.Knose1.Kanrythm.Game.Hud;
using Com.Github.Knose1.Kanrythm.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.Screens
{
	class Menu : Screen
	{
		[SerializeField] private Button buttonPlay;
		[SerializeField] private Button buttonPlayInAutoplayMode;

		public override void OnAddedToHudContainer(HudContainer hudContainer)
		{
			base.OnAddedToHudContainer(hudContainer);
			buttonPlay				.onClick.AddListener(Button_OnClick_Play);
			buttonPlayInAutoplayMode.onClick.AddListener(Button_OnClick_Autoplay);
		}

		public override void OnRemovedFromHudContainer(HudContainer hudContainer)
		{
			buttonPlay				.onClick.RemoveAllListeners();
			buttonPlayInAutoplayMode.onClick.RemoveAllListeners();

			base.OnRemovedFromHudContainer(hudContainer);
		}

		private void Button_OnClick_Play()
		{
			GameManager.Instance.StartMap();
		}

		private void Button_OnClick_Autoplay()
		{
			GameManager.Instance.StartMap(autoClear:true);
		}
	}
}
