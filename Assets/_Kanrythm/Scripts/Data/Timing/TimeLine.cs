using System;
using System.Collections.Generic;

namespace Com.Github.Knose1.Kanrythm.Data.Timing
{
	public class TimeLine
	{
		private List<KeyTime> _timeline = new List<KeyTime>();

		public TimeLine() : base() { }

		public int Length { get => _timeline.Count; }

		public void Add(float rotation = float.NaN)
		{
			_timeline.Add(new KeyTime(rotation));
		}
		public void Set(int index, float rotationMain = float.NaN, float rotation2 = float.NaN)
		{
			int lCount = _timeline.Count;
			if (index == lCount) Add(rotationMain);
			else if (index > lCount)
			{
				createUntilIndex(index);
				Add(rotationMain);
			}
			else
			{
				_timeline[index] = new KeyTime(rotationMain, rotation2);
			}
		}
		public void RemoveAt(int index)
		{
			_timeline[index] = KeyTime.Default;
		}

		#region private
		private void createUntilIndex(int index)
		{
			while (_timeline.Count < index)
			{
				_timeline.Add(KeyTime.Default);
			}
		}
		#endregion private

		virtual public KeyTime this[int index]
		{
			get
			{
				return _timeline[index];
			}

			set
			{
				_timeline[index] = value;
			}
		}
	}
}