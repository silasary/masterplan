using System;
using System.IO;

namespace Utils
{
	/// <summary>
	/// Class providing a set of static methods for file manipulation.
	/// </summary>
	public static class FileName
	{
        /// <summary>
        /// Returns the name part of a filename, removing the directory and extension.
        /// </summary>
        /// <param name="filename">The full filename.</param>
        /// <returns>Returns the name of the file.</returns>
        [Obsolete("Use System.IO.Path.GetFileNameWithoutExtension instead")]
        public static string Name(string filename)
		{
			if (filename == "")
				return "";

            return Path.GetFileNameWithoutExtension(filename);
		}

		/// <summary>
		/// Returns the extension part of a filename.
		/// </summary>
		/// <param name="filename">The full filename.</param>
		/// <returns>Returns the extension.</returns>
        [Obsolete("Use System.IO.Path instead")]
		public static string Extension(string filename)
		{
			if (filename == "")
				return "";

			var fi = new FileInfo(filename);
            var ext = fi.Extension;

            if (ext.StartsWith("."))
				ext = ext.Substring(1);

			return ext;
		}

		/// <summary>
		/// Returns the directory part of a filename, including the final directory separator character.
		/// </summary>
		/// <param name="filename">The full filename.</param>
		/// <returns>Returns the path of the directory.</returns>
        [Obsolete("Use System.IO.Path instead")]
		public static string Directory(string filename)
		{
			if (filename == "")
				return "";

			var fi = new FileInfo(filename);
            var dirname = fi.DirectoryName;

            var separator = Path.DirectorySeparatorChar.ToString();
            if (!dirname.EndsWith(separator))
				dirname += separator;

			return dirname;
		}

		/// <summary>
		/// Changes the location of a file on disc.
		/// </summary>
		/// <param name="oldname">The current path of the file.</param>
		/// <param name="newname">The new path of the file.</param>
		public static void Change(string oldname, string newname)
		{
			File.Copy(oldname, newname);
			File.Delete(oldname);
		}

		/// <summary>
		/// Removes invalid characters from a filename.
		/// </summary>
		/// <param name="filename">The filename to check.</param>
		/// <returns>Returns the trimmed filename</returns>
		public static string TrimInvalidCharacters(string filename)
		{
			string result = filename;

			result = result.Replace("\\", "");
			result = result.Replace("/", "");
			result = result.Replace(":", "");
			result = result.Replace("*", "");
			result = result.Replace("\"", "");
			result = result.Replace("?", "");
			result = result.Replace(".", "");
			result = result.Replace("|", "");
			result = result.Replace("<", "");
			result = result.Replace(">", "");

			return result;
		}

		#region Relative / absolute

		/// <summary>
		/// Converts an absolute path into a relative path.
		/// </summary>
		/// <param name="filename">The full path of the file.</param>
		/// <param name="directory">The directory.</param>
		/// <returns>Returns the path of the file relative to the directory.</returns>
		public static string MakeRelative(string filename, string directory)
        {
            // Strip initial protocol bit from each
            filename = remove_protocol(filename);
            directory = remove_protocol(directory);

            // Make sure the directory ends with "\\"
            var separator = Path.DirectorySeparatorChar.ToString();
            if (!directory.EndsWith(separator))
                directory += separator;

            // Make sure the first bit (the device) is the same
            var f_device = first_folder(filename);
            var p_device = first_folder(directory);
            if (f_device == p_device)
            {
                // Remove them
                filename = filename.Remove(0, f_device.Length);
                directory = directory.Remove(0, p_device.Length);
            }
            else
            {
                // Different devices / volumes
                return filename;
            }

            // Remove the common part
            while (true)
            {
                var d_folder = first_folder(directory);
                if (d_folder == "")
                    break;

                // Make sure the filename starts with this folder
                if (!filename.StartsWith(d_folder))
                    break;

                // Remove the first folder from each string
                filename = filename.Remove(0, d_folder.Length);
                directory = directory.Remove(0, d_folder.Length);
            }

            // Count the number of folders left on the directory
            var prefix = "";
            var builder = new System.Text.StringBuilder();
            builder.Append(prefix);
            while (true)
            {
                var folder = first_folder(directory);
                if (folder == "")
                    break;

                directory = directory.Remove(0, folder.Length);
                builder.Append(".." + separator);
            }
            prefix = builder.ToString();

            return prefix + filename;
        }

        /// <summary>
        /// Converts a relative path into an absolute path.
        /// This method does not check whether the file exists.
        /// </summary>
        /// <param name="filename">The relative path.</param>
        /// <param name="directory">The directory the relative path is relative to.</param>
        /// <returns>Returns the absolute path.</returns>
        public static string MakeAbsolute(string filename, string directory)
		{
			// Make sure the directory does not end with "\\"
			var separator = Path.DirectorySeparatorChar.ToString();
            if (directory.EndsWith(separator))
				directory = directory.Remove(directory.Length - separator.Length);

			var up_one = ".." + separator;
            while (filename.StartsWith(up_one))
			{
				filename = filename.Remove(0, up_one.Length);

				// Remove the last folder from the directory
				var index = directory.LastIndexOf(up_one);
                directory = directory.Remove(index);
			}

			return directory + separator + filename;
		}

		private static string remove_protocol(string path)
		{
			var sep = "://";
            var index = path.IndexOf(sep);
            if (index == -1)
				return path;

			return path.Remove(0, index + sep.Length);
		}

		private static string first_folder(string path)
		{
			var separator = Path.DirectorySeparatorChar.ToString();
            var index = path.IndexOf(separator);
            if (index == -1)
				return "";

			return path.Substring(0, index + separator.Length);
		}

		#endregion
	}
}
