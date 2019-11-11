using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game.Hud.UI
{
	public class DifficultyButton : MainMenuButton
	{
		public event Action<int> OnSelectedDifficulty;
		private int difficultyIndex;

		public void SetDificultyName(string name)
		{
			ButtonText = name;
		}

		public void SetDificultyIndex(int index)
		{
			difficultyIndex = index;
		}

		protected override void Start()
		{
			base.Start();
			onClick.AddListener(Button_OnClick);
		}

		private void Button_OnClick()
		{
			OnSelectedDifficulty?.Invoke(difficultyIndex);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			OnSelectedDifficulty = null;
		}
	}
}
