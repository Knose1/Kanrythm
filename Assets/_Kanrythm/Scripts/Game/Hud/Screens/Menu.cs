using Com.Github.Knose1.Common.Ui.Handler;
using Com.Github.Knose1.InputUtils.InputController;
using Com.Github.Knose1.Kanrythm.Data;
using Com.Github.Knose1.Kanrythm.Game;
using Com.Github.Knose1.Kanrythm.Game.Hud;
using Com.Github.Knose1.Kanrythm.Game.Hud.Screens;
using Com.Github.Knose1.Kanrythm.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.Screens
{
	[RequireComponent(typeof(Animator))]
	public class Menu : Screen
	{
		[SerializeField] private MapUiTempManager mapUiTempManager;

		[Header("Slider and Buttons")]
		[SerializeField] private SliderHandler backgroundOpacitySlider;

		[Header("Animation Trigger")]
		[SerializeField] private string playTrigger = "Play";
		[SerializeField] private string returnToMenuTrigger = "ReturnToMenu";
		[SerializeField] private string optionTrigger = "OptionDock";

		private bool isOnMainMenuDock = true;

		private Animator animator;

		#region HudContainer Down events
		public override void OnAddedToHudContainer(HudContainer hudContainer)
		{
			animator = GetComponent<Animator>();

			base.OnAddedToHudContainer(hudContainer);
			mapUiTempManager.OnSelectedMapAndDifficulty += MapButtonContainer_OnSelectedMapAndDifficulty;

			Controller.Instance.Input.Hud.Exit.performed += Exit_performed;
			backgroundOpacitySlider.Value = Config.BackgroundOpacity;
			backgroundOpacitySlider.OnValueChanged += BackgroundOpacitySlider_OnValueChanged;
		}

		private void BackgroundOpacitySlider_OnValueChanged(float arg1, UnityEngine.UI.Slider arg2)
		{
			if (arg1 < 0) arg1 = 0;
			Config.BackgroundOpacity = (uint)arg1;
		}

		public override void OnRemovedFromHudContainer(HudContainer hudContainer)
		{
			base.OnRemovedFromHudContainer(hudContainer);
			mapUiTempManager.OnSelectedMapAndDifficulty -= MapButtonContainer_OnSelectedMapAndDifficulty;

			Controller.Instance.Input.Hud.Exit.performed -= Exit_performed;
			backgroundOpacitySlider.OnValueChanged -= BackgroundOpacitySlider_OnValueChanged;
		}
		#endregion

		public void OnButtonPlay()
		{
			animator.SetTrigger(playTrigger);

			isOnMainMenuDock = false;
		}

		public void OnButtonOption()
		{
			animator.SetTrigger(optionTrigger);

			isOnMainMenuDock = false;
		}

		public void OnOpenMapFolder()
		{
			DataLoader.OpenMapFolder();
		}

		public void OnQuitButton()
		{
			//TODO : Add a Quit screen
			Application.Quit();
		}


		private void Exit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
		{
			if (!isOnMainMenuDock)
			{
				isOnMainMenuDock = true;
				animator.SetTrigger(returnToMenuTrigger);
			}
			else {
				OnQuitButton();
			}
		}

		private void MapButtonContainer_OnSelectedMapAndDifficulty(int mapId, int diffId)
		{
			Debug.Log("Starting " + mapId + ":" + diffId);

			MenuPlayMap lScreen = HudManager.Instance.GetTemplateMenuPlayMap();

			lScreen.mapId = (uint)mapId;
			lScreen.diffId = (uint)diffId;

			HudContainer.SetScreen(lScreen);
		}

	}
}
