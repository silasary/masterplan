using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Utils
{
	public class Serialisation<T>
	{
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
						result = (T)((object)xmlSerializer.Deserialize(xmlTextReader));
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
