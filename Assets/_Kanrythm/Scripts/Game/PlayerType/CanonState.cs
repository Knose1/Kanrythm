using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game.PlayerType {
	public class CanonState : MonoBehaviour {

		[SerializeField] private bool isRed = false; 
		[SerializeField] private string isRedName = "IsRed";
		[SerializeField] private string defaultStateName = "default";
		[SerializeField] private string triggerOnStateName = "selected";

		private bool isTriggered = false;
		public bool IsTriggered { get => isTriggered; set => TriggerState(value); }

		private Animator animator;
		[SerializeField] private Collider2D circleCollider;


		private void Start () {
			animator = GetComponent<Animator>();
			animator.SetBool(isRedName, isRed);

			circleCollider.enabled = false;
		}

		public void TriggerState(bool isTriggered)
		{
			circleCollider.enabled = isTriggered;
			this.isTriggered = isTriggered;
			animator.SetTrigger((isTriggered ? triggerOnStateName : defaultStateName));

		}
	}
}