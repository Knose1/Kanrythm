using Com.Github.Knose1.Kanrythm.Data;
using Com.Github.Knose1.Kanrythm.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.UI
{
	class MapButton : MainMenuButton
	{
		const float NOT_SELECTED = 193;
		const float SELECTED	 = 300;

		private List<Difficulty> difficulties = new List<Difficulty>();
		[NonSerialized] public int mapId = -1;
		private Map map;
		private bool mapHasABackground;

		override protected void Start()
		{
			base.Start();

			if (!Application.isPlaying) return;

			onClick.AddListener(Button_OnClick);

			GetComponentInParent<MapButtonContainer>().SetScaleY(NOT_SELECTED);

			map = MapLoader.Maplist[mapId];

			ButtonText = map.name;
			//GameRootAndObjectLibrary.Instance.DifficultyColors;

			mapHasABackground = map.background.Length > 0;

			List<string> difficulties = map.difficulties;

			DifficultyButton lButton;

			for (int i = - 1; i >= 0; i--)
			{
				lButton = Instantiate(GameRootAndObjectLibrary.Instance.DifficultyButtonPrefab, transform);
				lButton.SetDificultyName(difficulties[i]);
				lButton.SetDificultyIndex(i);
			}

		}

		protected override void OnValidate()
		{
			ButtonText = "_PlaceOlder";
			base.OnValidate();
		}

		private void Button_OnClick()
		{
			GetComponentInParent<MapButtonContainer>().SetScaleY(SELECTED);
		}

		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			GetComponentInParent<MapButtonContainer>().SetScaleY(NOT_SELECTED);
		}
	}
}
