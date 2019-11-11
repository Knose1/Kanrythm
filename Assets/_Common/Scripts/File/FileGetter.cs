using UnityEngine;
using UnityEditor;
using System.IO;

namespace Com.Github.Knose1.Common.File {

	public class FileGetter {

		public DirectoryInfo directory;
		public FileInfo file;
		private bool isDirectory;
		public bool IsDirectory { get; }

		private FileGetter(DirectoryInfo directory, FileInfo file)
		{
			isDirectory = directory.Exists;
			this.directory = directory;
			this.file = file;
		}

		public static FileGetter GetFile(string path)
		{
			return new FileGetter(new DirectoryInfo(path), new FileInfo(path));
		}
	}
}