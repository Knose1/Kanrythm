using Com.Github.Knose1.Common.Twinning.Curve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Github.Knose1.Common.Twinning.Curve
{
	public class Exp : Curve
	{
		public Exp()
		{
			minX = 0;
			maxY = 10;
		}

		protected override float GetCurve(float x)
		{
			return (float)Math.Exp(x);
		}
	}
}