using System;
using System.IO;

namespace Utils
{
    ///<summary>
    ///Class containing static methods and properties used for diagnostic logging.
    ///</summary>
    public class LogSystem
	{
		private static string fLogFile = "";

		private static int fIndent = 0;

        ///<summary>
        ///Gets or sets the path of the current logfile.
        ///If this is null or empty, no logfile is defined.
        ///</summary>
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

        ///<summary>
        ///Gets or sets a value indicating the current level of indentation.
        ///</summary>
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

        ///<summary>
        ///Sends a message to the console (and also to the logfile if one is defined).
        ///</summary>
        ///<param name="message">The message to be displayed.</param>
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

        ///<summary>
        ///Traces an object to the console (and also to the logfile if one is defined).
        ///</summary>
        ///<param name="obj">The object to be traced.</param>
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

        ///<summary>
        ///Traces an exception (the exception message, stack trace and inner exceptions) to the console (and also to the logfile if one is defined).
        ///</summary>
        ///<param name="ex">The exception to be traced.</param>
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
