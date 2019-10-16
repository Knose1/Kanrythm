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
				buttonTextComponent.text = buttonText;
			}
		}

		protected Text buttonTextComponent;


		override protected void Awake()
		{
			base.Start();
			if (!buttonTextComponent) buttonTextComponent = GetComponentInChildren<Text>();
		}
	}
}