///-----------------------------------------------------------------
/// Author : Knose1
/// Date : 04/01/2020 02:02
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Data {
	public static class Config {
		public static readonly Vector2 canvasSize = new Vector2(2160,1440);

		/// <summary>
		/// 10000 corrispond to 100.00%
		/// </summary>
		public const uint MAX_BACKGROUND_VALUE = 10000;

		private static uint backgroundOpacity = MAX_BACKGROUND_VALUE / 2;
		public static uint BackgroundOpacity {
			get => backgroundOpacity;
			set
			{
				backgroundOpacity = value;
				UpdateConfigFile();
			} 
		
		}

		static Config()
		{
			Debug.Log("Config Loaded");
		}

		private static void UpdateConfigFile()
		{
			Debug.LogWarning(new NotImplementedException());
		}
	}
}