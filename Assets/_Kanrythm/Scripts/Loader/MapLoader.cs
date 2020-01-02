using Com.Github.Knose1.Common;
using Com.Github.Knose1.Kanrythm.Data;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Loader {
	static public class MapLoaderZ
	{
		public const string MAP_FOLDER = "Map";

		static MapLoaderZ()
		{
			Debug.Log("MapLoader is ready");
		}

		public static void StartLoad(Map mapToLoad, in Action OnFinish)
		{
			LoaderBehaviour lMapLoaderBehaviour = new GameObject(nameof(LoaderBehaviour)).AddComponent<LoaderBehaviour>();
			lMapLoaderBehaviour.enumerator = mapToLoad.LoadMedias();
			lMapLoaderBehaviour.OnFinish += OnFinish;
			//lMapLoaderBehaviour.LoadStart();
		}
	}
}