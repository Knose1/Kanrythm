using Com.Github.Knose1.Common.Scrolling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.UI
{
	/// <summary>
	/// A monobehaviour that manage the masking of the MapButton
	/// 
	/// MapButtonContainer < MapButton < DifficultyContainer < DifficultyButton
	/// </summary>
	class MapButtonContainer : MonoBehaviour
	{

		[SerializeField] private float notSelectedHeight = 193;
		[SerializeField] private float selectedHeight = 300;
		[SerializeField] private float notSelectedPriority = 1;
		[SerializeField] private float selectedPriority = 2;


		[SerializeField] private ScrollingLayoutElement scrollingLayoutElement;
		[SerializeField] private RectTransform mask2;
		[SerializeField] private Shadow mapButtonShadow;
		[SerializeField] private MapButton mapButton;
		public event Action<MapButtonContainer> OnSelectMap;
		public event Action<MapButtonContainer> OnDeselectMap;

		public MapButton MapButton { get => mapButton; }

		/// <summary>
		/// param1 : int map																	<br/>
		/// param2 : int difficulty																<br/>
		/// </summary>
		public event Action<int, int> OnSelectedMapAndDifficulty;

		private void Awake()
		{
			mapButton = GetComponentInChildren<MapButton>();
			mapButton.OnSelectMap += MapButton_OnSelectMap;
			mapButton.OnDeselectMap += MapButton_OnDeselectMap;
			mapButton.OnSelectedMapAndDifficulty += MapButton_OnSelectedMapAndDifficulty; ;

			SetScaleY(notSelectedHeight);
			scrollingLayoutElement.priority = notSelectedPriority;
		}

		private void MapButton_OnSelectedMapAndDifficulty(int mapId, int difficultyId)
		{
			OnSelectedMapAndDifficulty?.Invoke(mapId, difficultyId);
		}

		private void OnDestroy()
		{
			OnSelectedMapAndDifficulty = null;
		}

		protected void SetScaleY(float scaleY)
		{
			Vector2 lVec = ((RectTransform)transform).sizeDelta;

			lVec.y = scaleY - mapButtonShadow.effectDistance.y;

			((RectTransform)transform).sizeDelta = lVec;

			//------------------------------------------

			lVec = mask2.sizeDelta;

			lVec.y = scaleY;

			mask2.sizeDelta = lVec;
		}

		private void MapButton_OnDeselectMap(int mapId)
		{
			SetScaleY(notSelectedHeight);
			scrollingLayoutElement.priority = notSelectedPriority;

			OnDeselectMap?.Invoke(this);
		}

		private void MapButton_OnSelectMap(int mapId)
		{
			SetScaleY(selectedHeight);
			scrollingLayoutElement.priority = selectedPriority;

			OnSelectMap?.Invoke(this);
		}
	}
}
