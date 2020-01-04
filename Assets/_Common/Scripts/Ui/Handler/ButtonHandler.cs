///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 04/01/2020 03:09
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Common.Ui.Handler {
	public class ButtonHandler : MonoBehaviour {
		[SerializeField] private Button current;
		public Button Current { 
			get => current;
			set 
			{
				if (current) current.onClick.RemoveListener(Current_OnClick);
				current = value;
				current.onClick.AddListener(Current_OnClick);
			}
		}

		public event Action<Button> OnClick; 

		private void Start () {
			Current = current;
		}

		private void Current_OnClick()
		{
			OnClick?.Invoke(current);
		}
	}
}