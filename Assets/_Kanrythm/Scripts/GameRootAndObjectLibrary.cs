using Com.Github.Knose1.Kanrythm.Game.BeatObject;
using Com.Github.Knose1.Kanrythm.Game.Hud.UI;
using Com.Github.Knose1.Kanrythm.Game.PlayerType;
using System;
using UnityEngine;
namespace Com.Github.Knose1.Kanrythm
{
	public class GameRootAndObjectLibrary : MonoBehaviour
	{

		private static GameRootAndObjectLibrary instance;

		[SerializeField] private Transform managerContainer = default;
		public Transform ManagerContainer { get => managerContainer; }

		[SerializeField] private Player playerPrefab = default;
		public Player PlayerPrefab { get => playerPrefab; }

		[SerializeField] private Beat beatPrefab = default;
		public Beat BeatPrefab { get => beatPrefab; }

		[SerializeField] private SpriteRenderer blackOverlay = default;
		public SpriteRenderer BlackOverlay { get => blackOverlay; }

		[SerializeField] private SpriteRenderer blackBackground = default;
		public SpriteRenderer BlackBackground { get => blackBackground; }

		/// <summary>
		/// Unique instance of the classe     
		/// </summary>
		public static GameRootAndObjectLibrary Instance
		{
			get
			{
				if (instance == null) instance = new GameRootAndObjectLibrary();
				return instance;
			}
		}


		private GameRootAndObjectLibrary() { }

		public void Awake()
		{
			instance = this;
		}

		public void OnDestroy()
		{
			instance = null;
		}
	}
}