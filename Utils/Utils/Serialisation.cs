using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Utils
{
    ///<summary>
    ///Class containing static methods for serialising (loading and saving) an object.
    ///</summary>
    ///<typeparam name="T">The type of object to be serialised.</typeparam>
    public class Serialisation<T>
	{
        ///<summary>
        ///Loads an object of type T from a file.
        ///</summary>
        ///<param name="filename">The full path of the file.</param>
        ///<param name="mode">The mode in which the object was saved.</param>
        ///<returns>Returns the loaded object, or default(T) if the object could not be loaded.</returns>
        public static T Load(string filename, SerialisationMode mode)
		{
			T result = default(T);
			try
			{
				switch (mode)
				{
				case SerialisationMode.Binary:
				{
					Stream stream = new FileStream(filename, FileMode.Open);
					try
					{
						IFormatter formatter = new BinaryFormatter();
						result = (T)((object)formatter.Deserialize(stream));
					}
					catch
					{
						result = default(T);
					}
					stream.Close();
					break;
				}
				case SerialisationMode.XML:
				{
					XmlTextReader xmlTextReader = new XmlTextReader(filename);
					try
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
						result = (T)xmlSerializer.Deserialize(xmlTextReader);
					}
					catch
					{
						result = default(T);
					}
					xmlTextReader.Close();
					break;
				}
				}
			}
			catch (Exception)
			{
				result = default(T);
			}
			return result;
		}

        ///<summary>
        ///Saves an object of type T to a file.
        ///</summary>
        ///<param name="filename">The full path of the file.</param>
        ///<param name="obj">The object to be saved.</param>
        ///<param name="mode">The mode in which the object was saved.</param>
        ///<returns>Returns true if the object was saved successfully; false otherwise.</returns>
        public static bool Save(string filename, T obj, SerialisationMode mode)
		{
			bool flag = false;
			string text = filename + ".save";
			try
			{
				switch (mode)
				{
				case SerialisationMode.Binary:
				{
					Stream stream = new FileStream(text, FileMode.Create);
					try
					{
						IFormatter formatter = new BinaryFormatter();
						formatter.Serialize(stream, obj);
						stream.Flush();
						flag = true;
					}
					catch (Exception value)
					{
						Console.WriteLine(value);
						flag = false;
					}
					stream.Close();
					break;
				}
				case SerialisationMode.XML:
				{
					XmlTextWriter xmlTextWriter = new XmlTextWriter(text, Encoding.UTF8);
					xmlTextWriter.Formatting = Formatting.Indented;
					try
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
						xmlSerializer.Serialize(xmlTextWriter, obj);
						xmlTextWriter.Flush();
						flag = true;
					}
					catch (Exception value2)
					{
						Console.WriteLine(value2);
						flag = false;
					}
					xmlTextWriter.Close();
					break;
				}
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			if (flag)
			{
				if (File.Exists(filename))
				{
					File.Delete(filename);
				}
				File.Move(text, filename);
			}
			return flag;
		}
	}
}
