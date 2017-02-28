using System;
using System.IO;

namespace Utils
{
    ///<summary>
    ///Class providing a set of static methods for file manipulation.
    ///</summary>
    public static class FileName
	{
        ///<summary>
        ///Returns the name part of a filename, removing the directory and extension.
        ///</summary>
        ///<param name="filename">The full filename.</param>
        ///<returns>Returns the name of the file.</returns>
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

        ///<summary>
        ///Returns the extension part of a filename.
        ///</summary>
        ///<param name="filename">The full filename.</param>
        ///<returns>Returns the extension.</returns>
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

        ///<summary>
        ///Returns the directory part of a filename, including the final directory separator character.
        ///</summary>
        ///<param name="filename">The full filename.</param>
        ///<returns>Returns the path of the directory.</returns>
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

        ///<summary>
        ///Changes the location of a file on disc.
        ///</summary>
        ///<param name="oldname">The current path of the file.</param>
        ///<param name="newname">The new path of the file.</param>
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

        ///<summary>
        ///Converts an absolute path into a relative path.
        ///</summary>
        ///<param name="filename">The full path of the file.</param>
        ///<param name="directory">The directory.</param>
        ///<returns>Returns the path of the file relative to the directory.</returns>
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

        ///<summary>
        ///Converts a relative path into an absolute path.
        ///This method does not check whether the file exists.
        ///</summary>
        ///<param name="filename">The relative path.</param>
        ///<param name="directory">The directory the relative path is relative to.</param>
        ///<returns>Returns the absolute path.</returns>
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
