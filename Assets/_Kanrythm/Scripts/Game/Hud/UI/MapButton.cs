using Com.Github.Knose1.Kanrythm.Data;
using Com.Github.Knose1.Kanrythm.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.UI
{
	/// <summary>
	/// The main button of a map button, it's contained in the MapButtonContainer
	/// </summary>
	class MapButton : MainMenuButton
	{
		private List<Difficulty> difficulties = new List<Difficulty>();
		[NonSerialized] public int mapId = -1;
		private Map map;

		public event Action<int> OnDeselectMap;
		public event Action<int> OnSelectMap;
		public event Action<int, int> OnSelectedMapAndDifficulty;

		override protected void Start()
		{
			base.Start();

			if (!Application.isPlaying) return;

			onClick.AddListener(Button_OnClick);

			map = MapLoader.Maplist[mapId];
			ButtonText = map.name;

			DifficultyContainer lDiffContainer = GetComponentInChildren<DifficultyContainer>();
			
			lDiffContainer.GenerateDifficultyButtons(map);
			lDiffContainer.OnSelectedDifficulty += DiffContainer_OnSelectedDifficulty;
		}

		private void DiffContainer_OnSelectedDifficulty(int diffId)
		{
			OnSelectedMapAndDifficulty?.Invoke(mapId, diffId);
		}

		override protected void OnDestroy()
		{
			base.OnDestroy();
			OnSelectMap = null;
			OnDeselectMap = null;
			OnSelectedMapAndDifficulty = null;
		}

		private void Button_OnClick()
		{
			if (currentSelectionState == SelectionState.Selected) {	return;}

			OnSelectMap?.Invoke(mapId);

			GetComponentInChildren<DifficultyContainer>().OnMapSelect(mapId);
			GetComponentInChildren<Text>().color = Color.white;

			Select();
		}

		private void OnSelectionChange(BaseEventData eventData)
		{
			if (eventData.selectedObject != gameObject && eventData.selectedObject.GetComponentInParent<MapButton>() == this) {
				Select();
				return;
			}

			OnDeselectMap?.Invoke(mapId);

			GetComponentInChildren<DifficultyContainer>().OnMapDeselect(mapId);
			GetComponentInChildren<Text>().color = Color.black;
		}

		protected override void OnValidate()
		{
			ButtonText = "_PlaceOlder";
			base.OnValidate();
		}
	}
}
