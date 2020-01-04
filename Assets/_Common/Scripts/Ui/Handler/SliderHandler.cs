///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 04/01/2020 03:09
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.Knose1.Common.Ui.Handler {
	public class SliderHandler : MonoBehaviour {
		[SerializeField] private Slider current;
		public Slider Current { 
			get => current;
			set 
			{
				if (current) current.onValueChanged.RemoveListener(Current_OnValueChanged);
				current = value;
				current.onValueChanged.AddListener(Current_OnValueChanged);
			}
		}

		public float Value { get => Current.value; set => Current.value = value; }

		public event Action<float, Slider> OnValueChanged; 

		private void Start () {
			Current = current;
		}

		private void Current_OnValueChanged(float arg0)
		{
			OnValueChanged?.Invoke(arg0, current);
		}
	}
}