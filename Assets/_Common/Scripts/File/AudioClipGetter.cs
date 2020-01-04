using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Com.Github.Knose1.Common.File
{
	public class AudioClipGetter : Getter<AudioClip>
	{
		public AudioType audioType;

		public AudioClipGetter(string path						) : this(path, GetAudioType(path)) {}
		public AudioClipGetter(string path, AudioType audioType	) : base(path)
		{
			result = null;
			this.Path = path;
			this.audioType = audioType;
		}

		protected override UnityWebRequest GetUnityWebRequest()
		{
			return UnityWebRequestMultimedia.GetAudioClip(Path, audioType);
		}

		protected override AudioClip Download(UnityWebRequest www)
		{
			return DownloadHandlerAudioClip.GetContent(www);
		}

		protected override void PostDownload(AudioClip result)
		{
			result.name = FileName;
		}

		public static AudioType GetAudioType(string path)
		{
			int lDotIndex = path.LastIndexOf(".");
			if (lDotIndex == -1) return AudioType.UNKNOWN;

			string lAudioExtension = path.Substring(lDotIndex + 1);

			switch (lAudioExtension.ToLower())
			{
				case "mp3":
					return AudioType.MPEG;
				case "ogg":
					return AudioType.OGGVORBIS;
				case "wav":
					return AudioType.WAV;
				default:
					return AudioType.UNKNOWN;
			}
		}
	}
}
