using Com.Github.Knose1.InputUtils.InputController;
using Com.Github.Knose1.Kanrythm.Game.Hud;
using Com.Github.Knose1.Kanrythm.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.Screens
{
	class MenuPlayMap : Screen
	{
		[SerializeField] private Button buttonPlay;
		[SerializeField] private Button buttonPlayInAutoplayMode;

		[NonSerialized] public uint mapId = 0;
		[NonSerialized] public uint diffId = 0;

		#region HudContainer Down events
		public override void OnAddedToHudContainer(HudContainer hudContainer)
		{
			base.OnAddedToHudContainer(hudContainer);
			buttonPlay				.onClick.AddListener(Button_OnClick_Play);
			buttonPlayInAutoplayMode.onClick.AddListener(Button_OnClick_Autoplay);

			Controller.Instance.Input.Hud.Exit.performed += Exit_performed;
		}
		public override void OnRemovedFromHudContainer(HudContainer hudContainer)
		{
			buttonPlay				.onClick.RemoveAllListeners();
			buttonPlayInAutoplayMode.onClick.RemoveAllListeners();

			Controller.Instance.Input.Hud.Exit.performed -= Exit_performed;

			base.OnRemovedFromHudContainer(hudContainer);
		}
		#endregion

		private void Exit_performed(InputAction.CallbackContext obj)
		{
			HudContainer.SetScreen(HudManager.Instance.GetTemplateMenu());
		}

		private void Button_OnClick_Play()
		{
			GameManager.Instance.StartMap(mapId, diffId);
		}

		private void Button_OnClick_Autoplay()
		{
			GameManager.Instance.StartMap(mapId, diffId, true);
		}
	}
}
