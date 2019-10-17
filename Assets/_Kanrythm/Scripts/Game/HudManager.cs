using Com.Github.Knose1.Assets._Kanrythm.Scripts.Game.Hud.Screens;
using Com.Github.Knose1.Kanrythm.Game;
using Com.Github.Knose1.Kanrythm.Game.Hud;
using Com.Github.Knose1.Kanrythm.Game.Hud.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game
{
	class HudManager : MonoBehaviour
	{
		private static HudManager instance;
		public static HudManager Instance { get => instance; }

		[SerializeField] private HudContainer hudContainer;

		[SerializeField] private Mapload mapLoadTemplate;
		[SerializeField] private Menu menuTemplate;

		public Mapload GetTemplateMapLoad() { return UnityEngine.Object.Instantiate(mapLoadTemplate, hudContainer.transform); }
		public Menu GetTemplateMenu()	    { return UnityEngine.Object.Instantiate(menuTemplate, hudContainer.transform); }

		private void Awake()
		{
			instance = this;
		}
		
		private void Start()
		{
			GameManager.OnStart += GameManager_OnStart;
			GameManager.OnEnd += GameManager_OnEnd;

			hudContainer.SetScreen(GetTemplateMapLoad());

		}

		private void GameManager_OnStart()
		{
			hudContainer.ClearScreen();
		}

		private void GameManager_OnEnd()
		{
			hudContainer.SetScreen(GetTemplateMenu());
		}
	}
}
