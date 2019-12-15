using Com.Github.Knose1.Common;
using Com.Github.Knose1.Kanrythm.Data;
using System;
using UnityEngine;

namespace Com.Github.Knose1.Kanrythm.Game
{

	public class RythmMusicPlayer : MonoBehaviour
	{

		private const float MINUTES_TO_SECONDS = 60f;

		private const float NO_TIMESTAMP = -1;
		public const float MIN_TIME_SPLITTING = 1 / 32f;

		public event Action<float> OnTimeSplit;
		private AudioSource audioSource;

		private float timeByMinSplitting;
		private float currentTimeSplitIndex = 0;

		[NonSerialized] public float musicOffset = 0;
		[NonSerialized] public float timeSplitOffset = 0;

		private StretchableDeltaTime stretchableDeltaTime;

		public float playingSpeed = 1;

		public float Volume { get => audioSource.volume; set => audioSource.volume = value; }

		#if UNITY_EDITOR
		void OnValidate()
		{
			playingSpeed = Mathf.Max(Mathf.Round(playingSpeed / 0.25f) * 0.25f, 0.01f);
		}
		#endif

		private void Awake()
		{	
			audioSource = gameObject.AddComponent<AudioSource>();
			stretchableDeltaTime = gameObject.AddComponent<StretchableDeltaTime>();
		}

		private void OnDestroy()
		{
			Stop();
		}

		public void SetMusic(MapTimingData timingInfo, AudioClip currentMusic)
		{
			audioSource.clip = currentMusic;

			float lTimeByBeats = MINUTES_TO_SECONDS / timingInfo.bpm;
			timeByMinSplitting = lTimeByBeats * MIN_TIME_SPLITTING;
		}

		public void Play()
		{
			/*if (musicOffset == 0)
				audioSource.Play();*/
			audioSource.loop = false;
			audioSource.pitch = playingSpeed;
			stretchableDeltaTime.ScaleTime = playingSpeed;

			stretchableDeltaTime.StartDeltaTime();

			//musicTimestamp = Time.time;
			//startTimestamp = Time.time;

			Update();
		}

		public void Stop()
		{
			audioSource.Stop();
			stretchableDeltaTime.StopDeltaTime();
		}

		private void Update()
		{
			if (!stretchableDeltaTime.IsPlaying) return;
			tryStartMusic();
			updateTimeSplit();
		}

		private void updateTimeSplit()
		{
			float lMusicTimestamp = timeByMinSplitting * currentTimeSplitIndex;
			float lCurrentTimestamp = stretchableDeltaTime.ElapsedTime - timeSplitOffset;

			if (lCurrentTimestamp < lMusicTimestamp) return;

			for (float i = lCurrentTimestamp; i > lMusicTimestamp; i -= timeByMinSplitting)
			{
				OnTimeSplit(currentTimeSplitIndex++);
			}
		}
		private void tryStartMusic()
		{
			if (!audioSource.isPlaying && stretchableDeltaTime.ElapsedTime >= musicOffset) audioSource.Play();
		}

	}
}