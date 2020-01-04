using Com.Github.Knose1.Common;
using Com.Github.Knose1.Common.File;
using Com.Github.Knose1.Kanrythm.Data;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Loader {
	
	public class MapLoader : IDisposable
	{
		private LoaderBehaviour loaderBehaviour;

		private Map mapToLoad;
		private int difficultyId;

		public AudioClipGetter	AudioClipGetter		{ get; protected set; }
		public Texture2DGetter	BackgroundGetter	{ get; protected set; }
		public Difficulty		Difficulty			{ get; protected set; }

		public delegate void MapLoaderFinish(MapLoader loader);
		public event MapLoaderFinish OnFinish;
		
		public void StartLoad(Map mapToLoad, int difficultyId)
		{
			this.mapToLoad = mapToLoad;
			this.difficultyId = difficultyId;
			StartLoad();
		}

		protected void StartLoad()
		{
			AudioClipGetter = mapToLoad.GetSong();

			LoaderBehaviour lSongLoaderBehaviour = loaderBehaviour = new GameObject(nameof(LoaderBehaviour)).AddComponent<LoaderBehaviour>();
			lSongLoaderBehaviour.enumerator = AudioClipGetter.Get();
			lSongLoaderBehaviour.OnFinish += LSongLoaderBehaviour_OnFinish;
			lSongLoaderBehaviour.LoadStart();
		}

		/// <summary>
		/// Event when the Song is Loaded
		/// </summary>
		private void LSongLoaderBehaviour_OnFinish()
		{
			Difficulty = mapToLoad.GetDifficulty(difficultyId);

			if (!Difficulty.HasBackground)
			{
				Finish();
				return;
			}

			BackgroundGetter = Difficulty.GetBackground();

			LoaderBehaviour lBackgroundLoaderBehaviour = loaderBehaviour = new GameObject(nameof(LoaderBehaviour)).AddComponent<LoaderBehaviour>();
			lBackgroundLoaderBehaviour.enumerator = BackgroundGetter.Get();
			lBackgroundLoaderBehaviour.OnFinish += LBackgroundLoaderBehaviour_OnFinish;
			lBackgroundLoaderBehaviour.LoadStart();
		}

		/// <summary>
		/// Event when the Difficulty's background is Loaded
		/// </summary>
		private void LBackgroundLoaderBehaviour_OnFinish()
		{
			Finish();
		}

		private void Finish() => OnFinish?.Invoke(this);

		public void Dispose()
		{
			UnityEngine.Object.Destroy(loaderBehaviour);

			OnFinish = null;
			mapToLoad = null;
			AudioClipGetter = null;
			BackgroundGetter = null;
			Difficulty = null;
			loaderBehaviour = null;
		}
	}
}