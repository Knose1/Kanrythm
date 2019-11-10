using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Github.Knose1.Common {
	public class Controller : MonoBehaviour {
		private static Controller instance;
		public static Controller Instance { get { return instance; } }

		private GameControls input;
		public GameControls Input { get => input; }

		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}

			input = new GameControls();

			instance = this;
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}