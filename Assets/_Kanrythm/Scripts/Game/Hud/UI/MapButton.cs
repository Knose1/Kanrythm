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
		private const float NO_TIMESTAMP = -1;

		[NonSerialized] public int mapId = -1;
		private Map map;

		public event Action<int> OnDeselectMap;
		public event Action<int> OnSelectMap;
		public event Action<int, int> OnSelectedMapAndDifficulty;

		private float selectedTimestamp = NO_TIMESTAMP;
		private float requiredTimeBeforeNextSelected = 0.02f;

		override protected void Start()
		{
			base.Start();

			if (!Application.isPlaying) return;

			onClick.AddListener(Button_OnClick);

			map = MapLoader.Maplist[mapId];
			ButtonText = map.name;

			DifficultyContainer lDiffContainer = GetComponentInChildren<DifficultyContainer>();
			
			lDiffContainer.GenerateDifficultyButtons(map);
			lDiffContainer.OnSelectedDifficulty += LDiffContainer_OnSelectedDifficulty;
		}

		private void LDiffContainer_OnSelectedDifficulty(int diffId)
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

		public void Focus()
		{
			if (selectedTimestamp != NO_TIMESTAMP) return;

			selectedTimestamp = Time.time;

			GetComponentInChildren<DifficultyContainer>().OnMapSelect(mapId);
			GetComponentInChildren<Text>().color = Color.white;

			Select();
			base.OnSelect(null);
		}

		public void Unfocus()
		{
			if (selectedTimestamp == NO_TIMESTAMP) return;

			selectedTimestamp = NO_TIMESTAMP;
			OnDeselectMap?.Invoke(mapId);

			GetComponentInChildren<DifficultyContainer>().OnMapDeselect(mapId);
			GetComponentInChildren<Text>().color = Color.black;

			base.OnDeselect(null);
		}

		private void Button_OnClick()
		{

			if (Time.time - selectedTimestamp < requiredTimeBeforeNextSelected) return;

			if (currentSelectionState == SelectionState.Selected) {
				OnDeselectMap?.Invoke(mapId);
				return;
			}
			else
				OnSelectMap?.Invoke(mapId);
		}

		/// <summary>
		/// Empty, use base.OnDeselect instead
		/// </summary>
		public override void OnDeselect(BaseEventData eventData) {}

		#if UNITY_EDITOR
		protected override void OnValidate()
		{
			ButtonText = "_PlaceOlder";
			base.OnValidate();
		}
		#endif
	}
}
