using Com.Github.Knose1.Kanrythm.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.UI {
	public class DifficultyContainer : MonoBehaviour {

		[SerializeField] private DifficultyButton difficultyButtonPrefab;
		[SerializeField] private Gradient difficultyColors;
		[SerializeField] private Gradient difficultyTextColors;
		[SerializeField] private bool isInverted;
		[SerializeField] private float spacing = 0;
		[SerializeField] private float rightDifficultyButtonSpacing = 0;

		public event Action<int> OnSelectedDifficulty;

		private List<DifficultyButton> difficultyButtons = new List<DifficultyButton>();
		private bool isMapSelected;

		public void GenerateDifficultyButtons(Map map)
		{

			List<string> difficulties = map.difficulties;

			DifficultyButton lButton;

			int diffCount = difficulties.Count;
			for (int i = 0; i < diffCount; i++)
			{
				lButton = Instantiate(difficultyButtonPrefab, transform);
				lButton.SetDificultyName(difficulties[i]);
				lButton.SetDificultyIndex(i);

				float lLerp = isInverted ? 1 - Mathf.InverseLerp(0, diffCount, i) : Mathf.InverseLerp(0, diffCount, i);

				lButton.GetComponent<Image>().color = difficultyColors.Evaluate(lLerp);
				lButton.GetComponentInChildren<Text>().color = difficultyTextColors.Evaluate(lLerp);

				lButton.OnSelectedDifficulty += LButton_OnSelectedDifficulty;

				difficultyButtons.Add(lButton);
			}


		}

		private void LButton_OnSelectedDifficulty(int diff)
		{
			OnSelectedDifficulty?.Invoke(diff);
		}

		public void OnMapDeselect(int mapId)
		{
			isMapSelected = false;

			Vector3 lPos = transform.localPosition;
			lPos.x = 0;
			transform.localPosition = lPos;
		}

		public void OnMapSelect(int mapId)
		{
			isMapSelected = true;
		}

		private void Update()
		{
			if (!isMapSelected) return;

			float inputHorizontal = Input.GetAxis("Horizontal");

			Vector3 lPos = transform.localPosition;
			lPos.x += inputHorizontal * 4;

			float minX = -(difficultyButtons.Count - rightDifficultyButtonSpacing) * (((RectTransform)difficultyButtonPrefab.transform).sizeDelta.x - spacing);
			float maxX = 0;

			if (minX > maxX) minX = maxX;

			lPos.x = Mathf.Clamp(lPos.x, minX, maxX);

			transform.localPosition = lPos;

		}

		protected void OnDestroy()
		{
			OnSelectedDifficulty = null;
		}
	}
}