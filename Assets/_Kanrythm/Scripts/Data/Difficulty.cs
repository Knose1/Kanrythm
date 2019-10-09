using Com.Github.Knose1.Common.File;
using Com.Github.Knose1.Kanrythm.Data.Timing;
using System;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Data
{
	public class Difficulty
	{
		private const string COMMENT_CHAR = ";";

		private const string MAIN_ROTATION = "mainrotation";
		private const string ROTATION_CANON2 = "rotationcanon2";

		private static readonly string[] lineSplitter = new string[] { "\r\n", "\n" };




		private float timeSplitting;
		private float approachRate;
		private TimeLine timeLine;

		public float TimeSplitting { get => timeSplitting; }
		public float ApproachRate { get => approachRate; }
		public TimeLine TimeLine { get => timeLine; }

		public TimeLine GetTimeLine()
		{
			return timeLine;
		}

		private Difficulty(string rawCode)
		{

			string[] lLines = rawCode.Split(lineSplitter, StringSplitOptions.RemoveEmptyEntries);

			string[] lTimelineIndex = new string[] { };
			string[] lTimelineRotation = new string[] { };
			string[] lTimelineRotation2 = new string[] { };

			string lCurrentLine;

			for (int i = lLines.Length - 1; i >= 0; i--)
			{
				lCurrentLine = lLines[i].ToLower().Trim();

				// WARNING : DO NOT REPEAT URSELF
				if (lCurrentLine.StartsWith(COMMENT_CHAR)) continue;
				else if (lCurrentLine.StartsWith("timesplitting"))
				{
					timeSplitting = 1f / uint.Parse(lCurrentLine.Split('=')[1]);
				}
				else if (lCurrentLine.StartsWith("approachrate"))
				{
					approachRate = float.Parse(lCurrentLine.Split('=')[1]);
				}
				else if (lCurrentLine.StartsWith("index"))
				{
					lTimelineIndex = lCurrentLine.Replace(" ", "").Replace("index=", "").Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
				}
				else if (lCurrentLine.StartsWith(MAIN_ROTATION))
				{
					lTimelineRotation = lCurrentLine.Replace(" ", "").Replace(MAIN_ROTATION+"=", "").Split(new string[] { "|" }, StringSplitOptions.None);
				}
				else if (lCurrentLine.StartsWith(ROTATION_CANON2))
				{
					lTimelineRotation2 = lCurrentLine.Replace(" ", "").Replace(ROTATION_CANON2+"=", "").Split(new string[] { "|" }, StringSplitOptions.None);
				}
			}

			
			timeLine = new TimeLine();

			float lRotation1;
			float lRotation2;

			for (int i = lTimelineIndex.Length - 1; i >= 0; i--)
			{
				lRotation1 = (lTimelineRotation [i] != "" ? float.Parse(lTimelineRotation [i]) : float.NaN);
				lRotation2 = (lTimelineRotation2[i] != "" ? float.Parse(lTimelineRotation2[i]) : float.NaN);

				timeLine.Set(int.Parse(lTimelineIndex[i]), lRotation1, lRotation2);
			}
		}


		public static Difficulty GetDifficulty(string path)
		{
			string lRaw = HandleTextFile.ReadString(path);

			return new Difficulty(lRaw);
		}
	}
}