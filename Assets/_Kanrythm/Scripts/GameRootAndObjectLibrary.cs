using Com.Github.Knose1.Kanrythm.Game.BeatObject;
using Com.Github.Knose1.Kanrythm.Game.PlayerType;
using System;
using UnityEngine;
namespace Com.Github.Knose1.Kanrythm
{
	public class GameRootAndObjectLibrary : MonoBehaviour
	{

		private static GameRootAndObjectLibrary instance;

		[SerializeField] private Transform managerContainer;
		public Transform ManagerContainer { get => managerContainer; }

		[SerializeField] private Player playerPrefab;
		public Player PlayerPrefab { get => playerPrefab; }

		[SerializeField] private Beat beatPrefab;
		public Beat BeatPrefab { get => beatPrefab; }

		[SerializeField] private SpriteRenderer blackBackground;
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