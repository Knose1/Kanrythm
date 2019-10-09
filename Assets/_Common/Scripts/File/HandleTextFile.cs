using System.IO;

namespace Com.Github.Knose1.Common.File {
	static public class HandleTextFile
	{
		static public StreamWriter WriteString(string path)
		{
			//Write some text to the test.txt file
			StreamWriter writer = new StreamWriter(path, true);
			return writer;
		}

		static public string ReadString(string path)
		{
			//Read the text from directly from the test.txt file
			StreamReader reader = new StreamReader(path);

			string lToReturn = reader.ReadToEnd();

			reader.Close();

			return lToReturn;
		}

	}
}
