using System;
using System.Collections;
using UnityEngine;
using Com.Github.Knose1.Kanrythm.Loader;
using Com.Github.Knose1.Kanrythm.Data;
using Com.Github.Knose1.Common.File;
using Com.Github.Knose1.Common;
using Com.Github.Knose1.Common.InputController;
using Com.Github.Knose1.Kanrythm.Data.Timing;
using Com.Github.Knose1.Kanrythm.Game.PlayerType;
using Com.Github.Knose1.Kanrythm.Game.BeatObject;

namespace Com.Github.Knose1.Kanrythm.Game {

	/// <summary>
	///	%Game manager, it loads the map, the song and creates the beats
	/// </summary>
	public class GameManager : StateMachine
	{
		#region GetInstance
		private static GameManager instance;
		public static GameManager Instance { get {
				if (instance) return instance;

				GameManager lNewGameManager = new GameObject("GameManager").AddComponent<GameManager>();

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
		public bool autoClear = false;

		private RythmMusicPlayer musicPlayer;
		private GameObject gameContainer;
		private SpriteRenderer blackOverlay;
		private Player player;

		private float fadeOutTime = 0;
		private float fadeTimestamp;

		#region Events
		public static event Action OnStart;
		public static event Action OnEnd;
		#endregion

		#region map
		private AudioClipGetter musicLoader;
		private IEnumerator musicLoaderEnumerator;

		private Map map;
		private Difficulty currentDiff;
		private bool hasStartedDestroy = false;
		#endregion map

		private void Awake()
		{
			musicPlayer = gameObject.AddComponent<RythmMusicPlayer>();
			instance = this;
		}

		override protected void Start()
		{
			transform.parent = GameRootAndObjectLibrary.Instance.ManagerContainer;

			base.Start();
		}

		private void OnDestroy()
		{
			Controller.Instance?.Input.Gameplay.Disable();

			OnEnd?.Invoke();
			Destroy(player);
			Destroy(gameContainer);
			Destroy(blackOverlay);

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
			LoadAndStartGame(MapLoader.Maplist[(int)mapId], difficultyId);

			gameContainer = new GameObject("GameContainer");
			blackOverlay = Instantiate(GameRootAndObjectLibrary.Instance.BlackBackground);
			blackOverlay.enabled = false;

			player = Instantiate(GameRootAndObjectLibrary.Instance.PlayerPrefab, gameContainer.transform);

			player.EnablePlay();
			Controller.Instance.Input.Gameplay.Enable();
		}

		private void LoadAndStartGame(Map map, uint difficultyIndex)
		{
			this.map = map;
			currentDiff = map.GetDifficulty(difficultyIndex);
			musicLoader = map.GetSong();

			musicLoaderEnumerator = musicLoader.GetAudioClip();

			doAction = DoActionLoadMusic;

			OnStart?.Invoke();
		}

		#region doAction
		private void DoActionLoadMusic()
		{
			if (musicLoaderEnumerator.MoveNext()) return;

			musicPlayer.SetMusic(map.timing, musicLoader.clip);

			musicLoaderEnumerator = null;
			musicLoader = null;

			//Permet d'attendre 1 frame avant le lancement de la musique
			doAction = DoActionStartGame;
		}

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
			float timeRatio = (StretchableDeltaTime.Instance.ElapsedTime - fadeTimestamp) / fadeOutTime;
			musicPlayer.Volume = Mathf.Lerp(1, 0, timeRatio);

			Color lColor = blackOverlay.color;
			lColor.a = Mathf.Lerp(0, 1, timeRatio);
			blackOverlay.color = lColor;
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
				if (hasStartedDestroy) return;

				fadeOutTime = GLOBAL_MAP_OFFSET_BFORE_END_MAP - 1;
				fadeTimestamp = StretchableDeltaTime.Instance.ElapsedTime;

				blackOverlay.enabled = true;
				doAction = DoActionFadeOut;

				Destroy(gameObject, GLOBAL_MAP_OFFSET_BFORE_END_MAP);

				hasStartedDestroy = true;

				return;
			}



			//Get the current KeyTime
			KeyTime lCurrentKey = currentDiff.TimeLine[(int)lDiffTimeSplit];

			//Create Beat(s)
			CreateBeat(lCurrentKey.rotation, lCurrentKey.rotation2);

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
	}
}