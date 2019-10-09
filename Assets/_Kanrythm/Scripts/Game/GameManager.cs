using System;
using System.Collections;
using UnityEngine;
using Com.Github.Knose1.Kanrythm.Loader;
using Com.Github.Knose1.Kanrythm.Data;
using Com.Github.Knose1.Common.File;
using Com.Github.Knose1.Common;
using Com.Github.Knose1.Kanrythm.Data.Timing;
using Com.Github.Knose1.Kanrythm.Game.PlayerType;
using Com.Github.Knose1.Kanrythm.Game.BeatObject;

namespace Com.Github.Knose1.Kanrythm.Game {
	/// <summary>
	/// Classe général du jeu : Elle s'occupe d'activer les différents états du jeu (load des map / ingame / menu)
	/// </summary>
	public class GameManager : StateMachine {

		/// <summary>
		/// Is autoClear is enabled, the beats will automatiquely be destroyed at the end of his move
		/// </summary>
		public bool autoClear = false;

		private RythmMusicPlayer musicPlayer;

		[SerializeField]
		private GameObject gameContainer;

		[SerializeField]
		private Player player;

		[SerializeField]
		private GameObject beatPrefab;

		#region map
		private AudioClipGetter musicLoader;
		private IEnumerator musicLoaderEnumerator;

		private Map map;
		private Difficulty currentDiff;
		#endregion map

		private void Awake()
		{
			musicPlayer = GetComponent<RythmMusicPlayer>();
		}

		override protected void Start () {
			base.Start();

			MapLoader.StartLoad();
		}
		public void StartDefault(bool autoClear)
		{
			this.autoClear = autoClear;
			LoadAndStartGame(MapLoader.Maplist[0], 0);
		}

		private void LoadAndStartGame(Map map, uint difficultyIndex)
		{
			this.map = map;
			currentDiff = map.GetDifficulty(difficultyIndex);
			musicLoader = map.GetSong();

			musicLoaderEnumerator = musicLoader.GetAudioClip();

			doAction = DoActionLoadMusic;
		}

		#region doAction
		private void DoActionLoadMusic()
		{
			if (musicLoaderEnumerator.MoveNext()) return;

			musicPlayer.SetMusic(map.timing, musicLoader.clip);

			musicLoaderEnumerator = null;
			musicLoader = null;

			player.EnablePlay();

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

			musicPlayer.Play();
			musicPlayer.onTimeSplit += LevelLoop;

			gameContainer.SetActive(true);

			doAction = DoActionVoid;
		}

		/// <summary>
		/// Fonction dans laquelle est créé les notes
		/// </summary>
		/// <param name="timeSplit"></param>
		private void LevelLoop(float timeSplit)
		{
			float lDiffTimeSplit = timeSplit / (currentDiff.TimeSplitting / RythmMusicPlayer.MIN_TIME_SPLITTING);

			//Debug.Log(lDiffTimeSplit+":"+currentDiff.TimeLine.Length);

			if (((int)lDiffTimeSplit) != lDiffTimeSplit) return;

			if (lDiffTimeSplit >= currentDiff.TimeLine.Length) return;

			//Debug.Log(lDiffTimeSplit+":"+currentDiff.TimeLine[(int)lDiffTimeSplit].rotation);
			//Debug.Log(lDiffTimeSplit+":"+currentDiff.TimeLine[(int)lDiffTimeSplit].rotation2);

			// TODO : Le jeu doit réagir avant le timeSplit
			// TODO : Création des beats

			KeyTime lCurrentKey = currentDiff.TimeLine[(int)lDiffTimeSplit];

			CreateBeat(lCurrentKey.rotation, lCurrentKey.rotation2);


			/*
			var lRotation = gameContainer.transform.rotation;
				
			lRotation.eulerAngles = new Vector3(0, 0, currentDiff.TimeLine[(int)lDiffTimeSplit].rotation);

			gameContainer.transform.rotation = lRotation;
			*/
		}
		#endregion doAction


		private void CreateBeat(float rotation, float rotation2)
		{
			CreateBeat(rotation);
			CreateBeat(rotation2);
		}

		private void CreateBeat(float rotation)
		{

			if (float.IsNaN(rotation)) return;

			Debug.Log(rotation + "/" + float.IsNaN(rotation));

			GameObject lBeat = Instantiate(beatPrefab, gameContainer.transform);

			Beat lBeatComponent = lBeat.GetComponent<Beat>();
			lBeatComponent.target = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad)) * player.GetCannonRadius();
			lBeatComponent.travelTime = currentDiff.ApproachRate;
			lBeatComponent.EnablePlay();
			lBeatComponent.autoClear = autoClear;
		}
	}
}