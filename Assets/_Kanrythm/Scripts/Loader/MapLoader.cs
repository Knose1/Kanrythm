using Com.Github.Knose1.Common;
using Com.Github.Knose1.Kanrythm.Data;
using System;
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

		public static void StartLoad(in Action OnFinish)
		{

			if (!streamingAssetsFolder.Exists) streamingAssetsFolder.Create();
			if (!mapFolder.Exists) mapFolder.Create();

			MapLoaderBehaviour lMapLoaderBehaviour = new GameObject("MapLoaderBehaviour").AddComponent<MapLoaderBehaviour>();
			lMapLoaderBehaviour.enumerator = GetMapEnumerator();
			lMapLoaderBehaviour.OnFinish += OnFinish;
		}

		private static IEnumerator<Map> GetMapEnumerator() {
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

		public class MapLoaderBehaviour : StateMachine
		{
			public event Action OnFinish;

			internal IEnumerator<Map> enumerator;

			override protected void Start()
			{
				doAction = DoActionNormal;
			}

			protected override void DoActionNormal()
			{
				base.DoActionNormal();
				if (enumerator.MoveNext()) return;

				OnFinish?.Invoke();
				OnFinish = null;
				doAction = DoActionVoid;
				Destroy(gameObject);
			}


		}

		public static void OpenMapFolder()
		{
			Application.OpenURL(mapFolder.FullName);
		}
	}
}