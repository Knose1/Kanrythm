using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Com.Github.Knose1.Common.File
{
	public class Texture2DGetter : Getter<Texture2D>
	{

		public Texture2DGetter(string path) : base(path) {}
		
		protected override UnityWebRequest GetUnityWebRequest()
		{
			return UnityWebRequestTexture.GetTexture(Path);
		}

		protected override Texture2D Download(UnityWebRequest www)
		{
			return DownloadHandlerTexture.GetContent(www);
		}

		protected override void PostDownload(Texture2D result)
		{
			result.name = FileName;
		}
	}
}
