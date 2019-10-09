using Com.Github.Knose1.Common.File;
using Com.Github.Knose1.Kanrythm.Data;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Loader {
	static public class MapLoader
	{
		public const string MAP_FOLDER = "Map";

		private static List<Map> mapList;
		public static List<Map> Maplist { get => mapList; }

		private static DirectoryInfo streamingAssetsFolder;
		private static DirectoryInfo mapFolder;
		
		static MapLoader()
		{
			Debug.Log("MapLoader is ready");
			streamingAssetsFolder = new DirectoryInfo(Application.streamingAssetsPath);
			mapFolder = new DirectoryInfo(Application.streamingAssetsPath + "/" + MAP_FOLDER);
		}

		public static void StartLoad()
		{

			if (!streamingAssetsFolder.Exists) streamingAssetsFolder.Create();
			if (!mapFolder.Exists) mapFolder.Create();


			DirectoryInfo[] lDirectories = mapFolder.GetDirectories();
			mapList = new List<Map>();

			for (int i = lDirectories.Length - 1; i >= 0; i--)
			{
				Debug.Log("Detected map :" + lDirectories[i]);
				mapList.Add(Map.GetMap(lDirectories[i].FullName));
			}
		}
	}
}