using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Github.Knose1.Kanrythm.Data
{
	[Serializable]
	public struct MapTimingData
	{
		public float bpm;
		public float offset;
		public uint timeSignature;

		//public float Bpm { get => bpm; }
		//public float Offset { get => offset; }
		//public uint TimeSignature { get => timeSignature; }

		public MapTimingData(float bpm, float offset, uint timeSignature) {
			this.bpm = bpm;
			this.offset = offset;
			this.timeSignature = timeSignature;
		}

	}
}
