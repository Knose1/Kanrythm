using Com.Github.Knose1.Kanrythm.Data;
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
		const float NO_IMAGE = 200;
		const float NO_BG_SELECTED = 300;
		const float IMAGE	 = 300;
		const float IMAGE_BG = 500;

		private List<Difficulty> difficulties = new List<Difficulty>();
		[NonSerialized] public int mapId;
		private Map map;
		private bool mapHasABackground;

		override protected void Start()
		{
			onClick.AddListener(Button_OnClick);

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
			float lHeightToSet = mapHasABackground ? IMAGE_BG : NO_BG_SELECTED;

			Vector2 lVec = ((RectTransform)transform.parent).sizeDelta;

			lVec.y = lHeightToSet;

			((RectTransform)transform.parent).sizeDelta = lVec;
		}

		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			Debug.Log("hi");
		}
	}
}
