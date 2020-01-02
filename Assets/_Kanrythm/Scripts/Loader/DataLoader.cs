using Com.Github.Knose1.Common;
using Com.Github.Knose1.Kanrythm.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Loader {
	static public class DataLoader
	{
		public const string MAP_FOLDER = "Map";

		private static List<Map> mapList;
		public static List<Map> Maplist { get => mapList; }

		private static DirectoryInfo streamingAssetsFolder;
		public static DirectoryInfo StreamingAssetsFolder => streamingAssetsFolder;

		private static DirectoryInfo mapFolder;
		public static DirectoryInfo MapFolder => mapFolder;

		static DataLoader()
		{
			streamingAssetsFolder = new DirectoryInfo(Application.streamingAssetsPath);
			mapFolder = new DirectoryInfo(Application.streamingAssetsPath + "/" + MAP_FOLDER);
			Debug.Log("DataLoader is ready");
		}

		public static void StartLoad(in Action OnFinish)
		{
			if (!streamingAssetsFolder.Exists) streamingAssetsFolder.Create();
			if (!mapFolder.Exists) mapFolder.Create();

			LoaderBehaviour lMapLoaderBehaviour = new GameObject(nameof(LoaderBehaviour)).AddComponent<LoaderBehaviour>();
			lMapLoaderBehaviour.enumerator = GetMapEnumerator();
			lMapLoaderBehaviour.OnFinish += OnFinish;
			lMapLoaderBehaviour.LoadStart();
		}

		private static IEnumerator GetMapEnumerator() {
			Map lMap;

			DirectoryInfo[] lDirectories = mapFolder.GetDirectories();
			mapList = new List<Map>();
			yield return null;

			for (int i = lDirectories.Length - 1; i >= 0; i--)
			{
				Debug.Log("Loading map :" + lDirectories[i]);

				lMap = Map.GetMap(lDirectories[i].FullName);

				if (lMap.LoadError != null) {
					yield return lMap;
					continue;
				}
				mapList.Add(lMap);

				yield return null;
			}
		}

		
		public static void OpenMapFolder()
		{
			Application.OpenURL(mapFolder.FullName);
		}
	}
}