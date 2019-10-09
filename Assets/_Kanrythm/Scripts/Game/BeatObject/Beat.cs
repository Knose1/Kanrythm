using Com.Github.Knose1.Common;
using Com.Github.Knose1.Kanrythm.Game.PlayerType;
using System;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game.BeatObject {
	public class Beat : StateMachine {
		

		private SpriteRenderer spriteRenderer;
		private Color startColor;

		/// <summary>
		/// Is autoClear is enabled, the beat will automatiquely be destroyed
		/// </summary>
		[NonSerialized] public bool autoClear = false;

		[NonSerialized] public float moveDuration = 0;
		[NonSerialized] public Vector3 target = Vector3.zero;
		[NonSerialized] public float travelTime;

		[SerializeField] private float endSize = 1;
		[SerializeField] private float fadeOutTime = 0.1f;
		[SerializeField] private string beatReciverTag;
		[SerializeField] private Transform destroyParticles;

		private float startTimeStamp;
		private float moveEndTimeStamp;
		private Vector3 startCoordinates;
		private Vector3 startSize;

		protected override void Start()
		{
			base.Start();
			spriteRenderer = GetComponent<SpriteRenderer>();
			startColor = spriteRenderer.color;
		}

		public void EnablePlay()
		{
			doAction = DoActionNormal;

			startTimeStamp = StretchableDeltaTime.Instance.ElapsedTime;
			startCoordinates = transform.position;
			startSize = transform.localScale;
		}

		protected override void DoActionNormal()
		{
			base.DoActionNormal();

			float lElapsedTimeRatio = GetElapsedTimeRatio(startTimeStamp, travelTime);

			Move(lElapsedTimeRatio);
			Resize(lElapsedTimeRatio);

			if ((transform.position - startCoordinates).magnitude > (target - startCoordinates).magnitude)
			{
				moveEndTimeStamp = StretchableDeltaTime.Instance.ElapsedTime;
				doAction = DoActionFadeOut;
				if (autoClear) DestroyWithParticle();
			}
		}

		protected void DoActionFadeOut()
		{
			Move(GetElapsedTimeRatio(startTimeStamp, travelTime));
			Alpha(GetElapsedTimeRatio(moveEndTimeStamp, fadeOutTime));

			if (spriteRenderer.color.a == 0)
			{
				doAction = DoActionVoid;
				Destroy(gameObject);
			}
		}


		private float GetElapsedTimeRatio(float start, float targetTime)
		{
			return (StretchableDeltaTime.Instance.ElapsedTime - start) / targetTime;
		}



		private void Alpha(float elapsedTimeRatio)
		{
			Color lColor = startColor;
			lColor.a = Mathf.Lerp(1, 0, elapsedTimeRatio);
			spriteRenderer.color = lColor;
		}

		private void Resize(float elapsedTimeRatio)
		{
			transform.localScale = Vector3.Lerp(startSize, Vector3.one * endSize, elapsedTimeRatio);
		}

		private void Move(float elapsedTimeRatio)
		{
			transform.position = Vector3.LerpUnclamped(startCoordinates, target, elapsedTimeRatio);
		}




		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (doAction == DoActionVoid) return;

			CanonState canonState = collision.gameObject.GetComponent<CanonState>();
			if (canonState && canonState.IsTriggered)
			{
				DestroyWithParticle();
			}
		}

		private void DestroyWithParticle()
		{
			Transform lParticle = Instantiate(destroyParticles, transform.parent);
			lParticle.position = transform.position;

			Destroy(gameObject);
		}
	}
}