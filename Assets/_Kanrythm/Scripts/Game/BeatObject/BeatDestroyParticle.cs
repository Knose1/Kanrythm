using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game.BeatObject {
	public class BeatDestroyParticle : MonoBehaviour {

		/// <summary>
		/// The duration time of the particle system in Seconds
		/// </summary>
		[SerializeField] private float duration;

		private void Start () {
			Destroy(gameObject, duration);
		}
	}
}