using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Com.Github.Knose1.Kanrythm.Data.Timing
{
	public struct KeyTime
	{
		public float rotation;
		public float rotation2;

		public KeyTime(float rotation = float.NaN, float rotation2 = float.NaN)
		{
			this.rotation = rotation;
			this.rotation2 = rotation2;
		}

		static public KeyTime Default {
			get {
				return new KeyTime(float.NaN, float.NaN);
			}	
		}
	}
}