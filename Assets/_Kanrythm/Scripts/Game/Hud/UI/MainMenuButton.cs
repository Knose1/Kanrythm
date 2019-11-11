using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud {
	public class MainMenuButton : Button {

		private string buttonText = "button";
		protected string ButtonText
		{
			get => buttonText;
			set
			{
				buttonText = value;

				if (!buttonTextComponent) buttonTextComponent = GetComponentInChildren<Text>();
				buttonTextComponent.text = buttonText;
			}
		}

		protected Text buttonTextComponent;


		override protected void Awake()
		{
			base.Start();

			onClick.AddListener(Button_OnClick);
		}

		private void Button_OnClick()
		{
		}
	}
}