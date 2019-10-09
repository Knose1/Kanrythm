using Com.Github.Knose1.Common.File;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Com.Github.Knose1.Kanrythm.Data {

	[Serializable]
	public class Map {
		public const string MAIN_JSON = "data.json";
		public const string MAP_EXTENTION = ".map";

		public string directoryPath;
		public string name;
		public string audio;
		public List<string> difficulties;
		public MapTimingData timing;
		private AudioType audioType;

		private Map() { }

		public static Map GetMap(string directoryPath)
		{
			string lJsonString = HandleTextFile.ReadString(directoryPath + "/" + MAIN_JSON);
			Map lMap = JsonUtility.FromJson<Map>(lJsonString);

			lMap.directoryPath = directoryPath;


			lMap.audioType = AudioClipGetter.GetAudioType(lMap.audio);

			return lMap;
		}

		public Difficulty GetDifficulty(uint id)
		{
			return Difficulty.GetDifficulty(directoryPath + "/" + difficulties[(int)id] + MAP_EXTENTION);
		}

		public AudioClipGetter GetSong()
		{
			Uri lUri = new Uri(directoryPath.Replace("\\", "/") + "/" + audio);

			Debug.Log("Loading songfile : " + lUri.AbsoluteUri);

			return new AudioClipGetter(lUri.AbsoluteUri, audioType);
		}
	}
}
