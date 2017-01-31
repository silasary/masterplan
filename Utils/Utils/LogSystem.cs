using System;
using System.IO;

namespace Utils
{
	public class LogSystem
	{
		private static string fLogFile = "";

		private static int fIndent = 0;

		public static string LogFile
		{
			get
			{
				return LogSystem.fLogFile;
			}
			set
			{
				LogSystem.fLogFile = value;
			}
		}

		public static int Indent
		{
			get
			{
				return LogSystem.fIndent;
			}
			set
			{
				LogSystem.fIndent = value;
			}
		}

		public static void Trace(string message)
		{
			try
			{
				string text = "";
				for (int i = 0; i < LogSystem.fIndent; i++)
				{
					text += "\t";
				}
				text = text + message + Environment.NewLine;
				Console.Write(text);
				if (LogSystem.fLogFile != null && LogSystem.fLogFile != "")
				{
					try
					{
						string contents = DateTime.Now + "\t" + text;
						File.AppendAllText(LogSystem.fLogFile, contents);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		public static void Trace(object obj)
		{
			try
			{
				LogSystem.Trace(obj.ToString());
			}
			catch
			{
			}
		}

		public static void Trace(Exception ex)
		{
			try
			{
				LogSystem.Trace(ex.Message);
				LogSystem.Trace(ex.StackTrace);
				while (ex.InnerException != null)
				{
					ex = ex.InnerException;
					LogSystem.Indent++;
					LogSystem.Trace(ex);
					LogSystem.Indent--;
				}
			}
			catch
			{
			}
		}
	}
}
