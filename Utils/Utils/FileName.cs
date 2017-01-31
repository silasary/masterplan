using System;
using System.IO;

namespace Utils
{
	public static class FileName
	{
		public static string Name(string filename)
		{
			if (filename == "")
			{
				return "";
			}
			FileInfo fileInfo = new FileInfo(filename);
			string text = fileInfo.Name;
			int num = text.LastIndexOf(".");
			if (num != -1)
			{
				text = text.Remove(num);
			}
			return text;
		}

		public static string Extension(string filename)
		{
			if (filename == "")
			{
				return "";
			}
			FileInfo fileInfo = new FileInfo(filename);
			string text = fileInfo.Extension;
			if (text.StartsWith("."))
			{
				text = text.Substring(1);
			}
			return text;
		}

		public static string Directory(string filename)
		{
			if (filename == "")
			{
				return "";
			}
			FileInfo fileInfo = new FileInfo(filename);
			string text = fileInfo.DirectoryName;
			string text2 = Path.DirectorySeparatorChar.ToString();
			if (!text.EndsWith(text2))
			{
				text += text2;
			}
			return text;
		}

		public static void Change(string oldname, string newname)
		{
			File.Copy(oldname, newname);
			File.Delete(oldname);
		}

		public static string TrimInvalidCharacters(string filename)
		{
			string text = filename.Replace("\\", "");
			text = text.Replace("/", "");
			text = text.Replace(":", "");
			text = text.Replace("*", "");
			text = text.Replace("\"", "");
			text = text.Replace("?", "");
			text = text.Replace(".", "");
			text = text.Replace("|", "");
			text = text.Replace("<", "");
			return text.Replace(">", "");
		}

		public static string MakeRelative(string filename, string directory)
		{
			filename = FileName.remove_protocol(filename);
			directory = FileName.remove_protocol(directory);
			string text = Path.DirectorySeparatorChar.ToString();
			if (!directory.EndsWith(text))
			{
				directory += text;
			}
			string text2 = FileName.first_folder(filename);
			string text3 = FileName.first_folder(directory);
			if (text2 == text3)
			{
				filename = filename.Remove(0, text2.Length);
				directory = directory.Remove(0, text3.Length);
				while (true)
				{
					string text4 = FileName.first_folder(directory);
					if (text4 == "" || !filename.StartsWith(text4))
					{
						break;
					}
					filename = filename.Remove(0, text4.Length);
					directory = directory.Remove(0, text4.Length);
				}
				string str = "";
				while (true)
				{
					string text5 = FileName.first_folder(directory);
					if (text5 == "")
					{
						break;
					}
					directory = directory.Remove(0, text5.Length);
					str = str + ".." + text;
				}
				return str + filename;
			}
			return filename;
		}

		public static string MakeAbsolute(string filename, string directory)
		{
			string text = Path.DirectorySeparatorChar.ToString();
			if (directory.EndsWith(text))
			{
				directory = directory.Remove(directory.Length - text.Length);
			}
			string text2 = ".." + text;
			while (filename.StartsWith(text2))
			{
				filename = filename.Remove(0, text2.Length);
				int startIndex = directory.LastIndexOf(text2);
				directory = directory.Remove(startIndex);
			}
			return directory + text + filename;
		}

		private static string remove_protocol(string path)
		{
			string text = "://";
			int num = path.IndexOf(text);
			if (num == -1)
			{
				return path;
			}
			return path.Remove(0, num + text.Length);
		}

		private static string first_folder(string path)
		{
			string text = Path.DirectorySeparatorChar.ToString();
			int num = path.IndexOf(text);
			if (num == -1)
			{
				return "";
			}
			return path.Substring(0, num + text.Length);
		}
	}
}
