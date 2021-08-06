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
			string lToReturn;

			//Read the text from directly from the test.txt file
			using (StreamReader reader = new StreamReader(path)) 
			{

				lToReturn = reader.ReadToEnd();

				reader.Close();
			}
			return lToReturn;
		}

	}
}
