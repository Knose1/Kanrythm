using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Com.Github.Knose1.Common.File
{
	public class AudioClipGetter
	{
		//public const string FILE_METHOD = "file:///";
		public AudioClip clip;
		public string path;
		public AudioType audioType;

		public AudioClipGetter(string path, AudioType audioType)
		{
			clip = null;
			this.path = path;
			this.audioType = audioType;
		}


		public IEnumerator GetAudioClip()
		{
			UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, audioType);
			
			yield return www.SendWebRequest();

			if (www.isNetworkError)
			{
				Debug.LogError(www.error);
				yield break;
			}

			while (!www.isDone)
			{
				yield return www;
			}

			clip = DownloadHandlerAudioClip.GetContent(www);
		}

		public static AudioType GetAudioType(string path)
		{
			int lDotIndex = path.LastIndexOf(".");
			if (lDotIndex == -1) return AudioType.UNKNOWN;

			string audioExtension = path.Substring(lDotIndex + 1);

			switch (audioExtension.ToLower())
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
