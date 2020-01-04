using System;
using System.Collections;
using UnityEngine;
using Com.Github.Knose1.Kanrythm.Loader;
using Com.Github.Knose1.Kanrythm.Data;
using Com.Github.Knose1.Common.File;
using Com.Github.Knose1.Common;
using Com.Github.Knose1.InputUtils.InputController;
using Com.Github.Knose1.Kanrythm.Data.Timing;
using Com.Github.Knose1.Kanrythm.Game.PlayerType;
using Com.Github.Knose1.Kanrythm.Game.BeatObject;
using System.Collections.Generic;

namespace Com.Github.Knose1.Kanrythm.Game {

	/// <summary>
	///	%Game manager, it loads the map, the song and creates the beats
	/// </summary>
	[RequireComponent(typeof(RythmMusicPlayer))]
	public class GameManager : StateMachine
	{
		#region GetInstance
		private static GameManager instance;
		public static GameManager Instance {
			get {
				if (instance) return instance;

				GameManager lNewGameManager = new GameObject("GameManager").AddComponent<GameManager>();
				lNewGameManager.transform.parent = GameRootAndObjectLibrary.Instance.ManagerContainer;

				return instance = lNewGameManager;
			}
		}
		#endregion

		/// <summary>
		/// The global offset (in seconds).												<br/>
		/// When playing, the player must have a little time to check the controles.	<br/>
		/// This is used not to surprise the player.									<br/>
		/// </summary>
		private const float GLOBAL_MAP_OFFSET_BEFORE_START_MAP = 1;

		/// <summary>
		/// The fadeout time of the maps
		/// </summary>
		private const float GLOBAL_MAP_OFFSET_BFORE_END_MAP = 5;

		/// <summary>
		/// If autoClear is enabled, the beats will automatiquely be destroyed when hiting the player's white outerborder (100% accuracy)
		/// </summary>
		[SerializeField] public bool autoClear = false;
		[SerializeField] private List<PulseCamera> pulseCameras = new List<PulseCamera>();

		[SerializeField] private GameObject gameContainer = default;

		private ScreenSpriteScaller background;
		[SerializeField] private string backgroundSortingLayerName = "Game";
		[SerializeField] private int backgroundLayerID;

		private RythmMusicPlayer musicPlayer;
		private SpriteRenderer blackOverlay;
		private SpriteRenderer blackBackground;
		private Player player;

		private float fadeOutTime = 0;
		private float fadeTimestamp;

		#region Events
		public static event Action OnStart;
		public static event Action OnEnd;
		#endregion

		#region map
		private MapLoader mapLoader;

		private Map map;
		private Difficulty currentDiff;
		#endregion map

		private void Awake()
		{
			musicPlayer = gameObject.GetComponent<RythmMusicPlayer>();
			instance = this;
		}

		override protected void Start()
		{
			base.Start();
		}

		private void OnDestroy()
		{
			StopMap();

			instance = null;
		}

		/// <summary>
		/// Start a map
		/// </summary>
		/// <param name="mapId">The map id</param>
		/// <param name="difficultyId">The id of the difficulty</param>
		/// <param name="autoClear">Whenever to destroy or not then beats when hiting the player's white outerborder (100% accuracy)</param>
		public void StartMap(uint mapId = 0, uint difficultyId = 0, bool autoClear = false)
		{
			this.autoClear = autoClear;
			LoadAndStartGame(DataLoader.Maplist[(int)mapId], difficultyId);

			blackOverlay = Instantiate(GameRootAndObjectLibrary.Instance.BlackOverlay, gameContainer.transform);
			blackOverlay.enabled = false;

			blackBackground = Instantiate(GameRootAndObjectLibrary.Instance.BlackBackground, gameContainer.transform);
			blackBackground.color = new Color(0,0,0, Config.BackgroundOpacity / (float)Config.MAX_BACKGROUND_VALUE);

			player = Instantiate(GameRootAndObjectLibrary.Instance.PlayerPrefab, gameContainer.transform);

			player.EnablePlay();
			pulseCameras.EnableAll();
			Controller.Instance.Input.Gameplay.Enable();
		}

		public void StopMap()
		{
			fadeOutTime = 0;
			fadeTimestamp = 0;
			doAction = DoActionVoid;

			Controller.Instance?.Input.Gameplay.Disable();
			musicPlayer.Stop();
			pulseCameras.EnableAll(false);

			OnEnd?.Invoke();
			if (player)			Destroy(player.gameObject);
			if (blackOverlay)	Destroy(blackOverlay);
			if (blackBackground)	Destroy(blackBackground);
			if (background)		Destroy(background.gameObject);
			if (mapLoader != null) mapLoader.Dispose();
		}

		private void LoadAndStartGame(Map map, uint difficultyIndex)
		{
			this.map = map;

			mapLoader = new MapLoader();
			mapLoader.OnFinish += MapLoader_OnFinish;
			mapLoader.StartLoad(map, (int)difficultyIndex);
		}

		private void MapLoader_OnFinish(MapLoader loader)
		{
			currentDiff = loader.Difficulty;

			musicPlayer.SetMusic(map.timing, loader.AudioClipGetter.result);
			if (loader.BackgroundGetter != null) background = ScreenSpriteScaller.GenerateFillTexture(loader.BackgroundGetter.result, gameContainer.transform);
			background.Sprite.name = loader.BackgroundGetter.FileName;
			background.SpriteRenderer.sortingLayerName = backgroundSortingLayerName;
			background.gameObject.layer = backgroundLayerID;
			background.canvasSize = Config.canvasSize;

			loader.Dispose();
			mapLoader = null;

			OnStart?.Invoke();

			//Permet d'attendre 1 frame avant le lancement de la musique
			doAction = DoActionStartGame;
		}

		#region doAction
		private void DoActionStartGame()
		{

			musicPlayer.musicOffset = currentDiff.ApproachRate;

			if (map.timing.offset >= 0)
			{
				musicPlayer.timeSplitOffset = map.timing.offset;
			}
			else
			{
				musicPlayer.musicOffset -= map.timing.offset;
			}

			musicPlayer.musicOffset		+= GLOBAL_MAP_OFFSET_BEFORE_START_MAP;
			musicPlayer.timeSplitOffset += GLOBAL_MAP_OFFSET_BEFORE_START_MAP;

			musicPlayer.Play();
			musicPlayer.OnTimeSplit += LevelLoop;

			gameContainer.SetActive(true);

			doAction = DoActionVoid;
		}
		
		private void DoActionFadeOut()
		{
			float timeRatio = GetTimeRatio(fadeTimestamp, fadeOutTime);
			musicPlayer.Volume = Mathf.Lerp(1, 0, timeRatio);

			Color lColor = blackOverlay.color;
			lColor.a = Mathf.Lerp(0, 1, timeRatio);
			blackOverlay.color = lColor;
		}

		private void DoActionCheckIsFadeEnded()
		{
			float timeRatio = GetTimeRatio(fadeTimestamp, fadeOutTime);

			if (timeRatio > 1) StopMap();
		}
		#endregion doAction

		/// <summary>
		/// Function in witch beats are instantied
		/// </summary>
		/// <param name="timeSplit"></param>
		private void LevelLoop(float timeSplit)
		{

			float lDiffTimeSplit = timeSplit / (currentDiff.TimeSplitting / RythmMusicPlayer.MIN_TIME_SPLITTING);

			//Are we on a timesplit ?
			if (((int)lDiffTimeSplit) != lDiffTimeSplit) return; //If not, return

			//Has the map ended ?
			if (lDiffTimeSplit >= currentDiff.TimeLine.Length)
			{
				fadeOutTime = GLOBAL_MAP_OFFSET_BFORE_END_MAP - 1;
				fadeTimestamp = StretchableDeltaTime.Instance.ElapsedTime;

				blackOverlay.enabled = true;
				doAction = DoActionFadeOut;
				doAction += DoActionCheckIsFadeEnded;

				musicPlayer.OnTimeSplit -= LevelLoop;

				return;
			}



			//Get the current KeyTime
			KeyTime lCurrentKey = currentDiff.TimeLine[(int)lDiffTimeSplit];

			//Create Beat(s)
			CreateBeat(lCurrentKey.rotation, lCurrentKey.rotation2);
			pulseCameras.PulseAll();
		}

		#region CreateBeat
		protected void CreateBeat(float rotation, float rotation2)
		{
			CreateBeat(rotation);
			CreateBeat(rotation2);
		}
		protected void CreateBeat(float rotation)
		{

			if (float.IsNaN(rotation)) return;

			//Debug.Log(rotation + "/" + float.IsNaN(rotation));

			Beat lBeat = Instantiate(GameRootAndObjectLibrary.Instance.BeatPrefab, gameContainer.transform);

			lBeat.target = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad)) * player.GetCannonRadius();
			lBeat.travelTime = currentDiff.ApproachRate;
			lBeat.EnablePlay();
			lBeat.autoClear = autoClear;
		}
		#endregion
	
		private float GetTimeRatio(float timestamp, float duration)
		{
			return (StretchableDeltaTime.Instance.ElapsedTime - timestamp) / duration;
		}
	}
}