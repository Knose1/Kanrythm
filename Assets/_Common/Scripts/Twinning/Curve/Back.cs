using Com.Github.Knose1.Common.Twinning.Curve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Github.Knose1.Common.Twinning.Curve
{
	public class Back : Sin
	{
		public Back(float backCoef = 4f) : base()
		{
			if (backCoef <= 2) backCoef = 2.1f;

			minX = (float)-Math.PI / backCoef;
			maxY = (float) Math.PI / 2;
		}
	}
}