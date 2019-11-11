using Com.Github.Knose1.Common;
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
	[RequireComponent(typeof(Animator))]
	public class Menu : Screen
	{
		[SerializeField] private MapUiTempManager mapUiTempManager;

		[Header("Animation Trigger")]
		[SerializeField] private string playTrigger = "Play";
		[SerializeField] private string returnToMenuTrigger = "ReturnToMenu";

		private bool isOnTheLeftDock = true;

		private Animator animator;

		#region HudContainer Down events
		public override void OnAddedToHudContainer(HudContainer hudContainer)
		{
			animator = GetComponent<Animator>();

			base.OnAddedToHudContainer(hudContainer);
			mapUiTempManager.OnSelectedMapAndDifficulty += MapButtonContainer_OnSelectedMapAndDifficulty;

			Debug.Log("Added to hud");
			Controller.Instance.Input.Hud.Exit.performed += Exit_performed;
		}

		public override void OnRemovedFromHudContainer(HudContainer hudContainer)
		{
			base.OnRemovedFromHudContainer(hudContainer);
			mapUiTempManager.OnSelectedMapAndDifficulty -= MapButtonContainer_OnSelectedMapAndDifficulty;

			Debug.Log("Removed from hud");
			Controller.Instance.Input.Hud.Exit.performed -= Exit_performed;
		}
		#endregion

		public void OnButtonPlay()
		{
			animator.SetTrigger(playTrigger);

			isOnTheLeftDock = false;
		}

		private void Exit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
		{
			Debug.Log("hi");
			if (!isOnTheLeftDock)
			{
				isOnTheLeftDock = true;
				animator.SetTrigger(returnToMenuTrigger);
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
