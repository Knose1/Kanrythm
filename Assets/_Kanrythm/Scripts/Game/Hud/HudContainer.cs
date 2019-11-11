using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Github.Knose1.Kanrythm.Game.Hud.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud
{
	public class HudContainer : MonoBehaviour
	{
		private Screen currentScreen;

		public void Awake()
		{
			for (int childI = transform.childCount - 1; childI >= 0; childI--)
			{
				Transform screenTrans = transform.GetChild(childI);
				Screen screenComp = screenTrans.GetComponent<Screen>();
				if (!screenComp)
				{
					Destroy(screenTrans);
					continue;
				}

				screenComp.OnRemovedFromHudContainer(this);
			}
		}

		public void SetScreen(Screen screen)
		{
			if (currentScreen)
			{
				ClearScreen();
			}

			currentScreen = screen;
			currentScreen.transform.SetParent(transform);
			currentScreen.OnAddedToHudContainer(this);
		}

		public void ClearScreen()
		{
			
			currentScreen.OnRemovedFromHudContainer(this);
			currentScreen = null;
		}
	}
}