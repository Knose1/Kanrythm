using Com.Github.Knose1.Common.File;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Com.Github.Knose1.Kanrythm.Data {

	[Serializable]
	public class Map {
		public const string MAIN_JSON = "data.json";
		public const string MAP_EXTENTION = ".map";

		/// <summary>
		/// The path to the map directory
		/// </summary>
		public string directoryPath;

		/// <summary>
		/// The name of the map
		/// </summary>
		public string name;

		/// <summary>
		/// The relative path to the audio file
		/// </summary>
		public string audio;

		/// <summary>
		/// The name of the difficulties (it's also the name of the files.dat)
		/// </summary>
		public List<string> difficulties;

		/// <summary>
		/// The timing infos of the map
		/// </summary>
		public MapTimingData timing;

		/// <summary>
		/// Type of the audio
		/// </summary>
		private AudioType audioType;
		internal string background;

		private Map() { }

		public Exception LoadError { get; private set; }

		public static Map GetMap(string directoryPath)
		{
			Map lMap = new Map();
			try
			{
				string lJsonString = HandleTextFile.ReadString(Path.Combine(directoryPath, MAIN_JSON));

				lMap = JsonUtility.FromJson<Map>(lJsonString);

				lMap.directoryPath = directoryPath;


				lMap.audioType = AudioClipGetter.GetAudioType(lMap.audio);
			}
			catch (Exception error)
			{
				lMap.LoadError = error;
			}

			return lMap;
		}

		public Difficulty GetDifficulty(uint id)
		{
			return GetDifficulty((int)id);
		}
		public Difficulty GetDifficulty(int id)
		{
			return Difficulty.GetDifficulty(Path.Combine(directoryPath, difficulties[id] + MAP_EXTENTION), this);
		}

		public List<Difficulty> GetDifficulties()
		{
			List<Difficulty> lToReturn = new List<Difficulty>();

			int lLength = difficulties.Count;

			for (int i = 0; i < lLength; i++)
			{
				lToReturn.Add(GetDifficulty(i));
			}

			return lToReturn;
		}

		public AudioClipGetter GetSong()
		{
			Uri lUri = new Uri(Path.Combine(directoryPath.Replace("\\", "/"), audio));

			Debug.Log("Loading songfile : " + lUri.AbsoluteUri);

			return new AudioClipGetter(lUri.AbsoluteUri, audioType);
		}
	}
}
