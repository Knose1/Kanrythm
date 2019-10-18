using Com.Github.Knose1.Kanrythm.Game.Hud.UI;
using Com.Github.Knose1.Kanrythm.Loader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game.Hud {

	/// <summary>
	/// It contains all the map button containers
	/// </summary>
	public class MapUiTempManager : MonoBehaviour {

		[SerializeField] MapButtonContainer mapButtonContainerTemplate;

		private List<MapButtonContainer> mapContainers = new List<MapButtonContainer>();

		/// <summary>
		/// param1 : int map																	<br/>
		/// param2 : int difficulty																<br/>
		/// </summary>
		public event Action<int, int> OnSelectedMapAndDifficulty;

		private MapButtonContainer currentSelectedMap;

		private void Start () {
			int mapCount = MapLoader.Maplist.Count;

			for (int i = 0; i < mapCount; i++)
			{
				MapButtonContainer lButtonContainer = Instantiate(mapButtonContainerTemplate, transform);
				//TODO : Add the events and don't forget to destroy them onDestroy

				lButtonContainer.MapButton.mapId = i;
				lButtonContainer.OnSelectedMapAndDifficulty += LButtonContainer_OnSelectedMapAndDifficulty;
				lButtonContainer.OnSelectMap += LButtonContainer_OnSelectMap;

				mapContainers.Add(lButtonContainer);
			}
		}

		private void OnDestroy()
		{
			OnSelectedMapAndDifficulty = null;
			currentSelectedMap = null;
		}

		private void LButtonContainer_OnSelectMap(MapButtonContainer obj)
		{
			currentSelectedMap?.MapButton.Unfocus();

			currentSelectedMap = obj;

			currentSelectedMap.MapButton.Focus();
		}

		private void LButtonContainer_OnSelectedMapAndDifficulty(int mapId, int diffId)
		{
			OnSelectedMapAndDifficulty?.Invoke(mapId, diffId);
		}

		private void Update()
		{
			
		}
	}
}