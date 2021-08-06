using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Com.Github.Knose1.Common.File
{
	public abstract class Getter<T>
	{
		private const char PATH_FOLDER_SPLIT = '/';

		//public const string FILE_METHOD = "file:///";
		private string path;
		public string Path
		{
			get => path;
			set
			{
				path = value;

				int lIndex = path.LastIndexOf(PATH_FOLDER_SPLIT);
				fileName = lIndex >= 0 ? path.Substring(lIndex + 1, path.Length - lIndex - 1) : path;
			}
		}

		private string fileName;
		public string FileName => fileName;

		public T result;

		public Getter(string path)
		{
			result = default;
			this.Path = path;

			Debug.Log(path);
			Debug.Log(fileName);
		}

		protected abstract UnityWebRequest GetUnityWebRequest();
		protected abstract T Download(UnityWebRequest www);
		virtual protected void PostDownload(T result) { }

		public Exception Error { get; protected set; }



		virtual public IEnumerator Get()
		{
			using (UnityWebRequest www = GetUnityWebRequest())
			{
				yield return www.SendWebRequest();

				if (www.isNetworkError)
				{
					Error = new Exception(www.error);
					Debug.LogError(www.error);
					yield break;
				}

				while (!www.isDone)
				{
					yield return www;
				}

				try
				{
					result = Download(www);
					PostDownload(result);
				}
				catch (Exception e)
				{
					Error = e;
					Debug.Log(Error);
					yield break;
				}
			}
		}
	}
}
