///-----------------------------------------------------------------
/// Author : AymerickHebert
/// Date : 16/10/2019 12:29
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud {
	public class MainMenuButton : MonoBehaviour {

		[SerializeField] string buttonText = "button";
		[SerializeField] Text buttonTextComponent;

		private void OnValidate()
		{
			if (!buttonTextComponent) buttonTextComponent = GetComponentInChildren<Text>();

			buttonTextComponent.text = buttonText;
		}
	}
}