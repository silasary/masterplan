using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Utils
{
	public class XMLHelper
	{
		public static XmlDocument LoadSource(string xml)
		{
			XmlDocument result;
			try
			{
				StringReader input = new StringReader(xml);
				XmlReader reader = XmlReader.Create(input);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(reader);
				result = xmlDocument;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static XmlNode CreateChild(XmlDocument doc, XmlNode parent, string name)
		{
			XmlNode result;
			try
			{
				XmlElement newChild = doc.CreateElement(name);
				result = parent.AppendChild(newChild);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static XmlNode FindChild(XmlNode parent, string name)
		{
			foreach (XmlNode xmlNode in parent.ChildNodes)
			{
				if (xmlNode.Name == name)
				{
					return xmlNode;
				}
			}
			return null;
		}

		public static XmlNode FindChildWithAttribute(XmlNode parent, string attribute_name, string attribute_value)
		{
			foreach (XmlNode xmlNode in parent.ChildNodes)
			{
				string attribute = XMLHelper.GetAttribute(xmlNode, attribute_name);
				if (attribute == attribute_value)
				{
					return xmlNode;
				}
			}
			return null;
		}

		public static List<XmlNode> FindChildrenWithAttribute(XmlNode parent, string attribute_name, string attribute_value)
		{
			List<XmlNode> list = new List<XmlNode>();
			foreach (XmlNode xmlNode in parent.ChildNodes)
			{
				string attribute = XMLHelper.GetAttribute(xmlNode, attribute_name);
				if (attribute == attribute_value)
				{
					list.Add(xmlNode);
				}
			}
			return list;
		}

		public static string NodeText(XmlNode parent, string name)
		{
			XmlNode xmlNode = XMLHelper.FindChild(parent, name);
			if (xmlNode != null)
			{
				return xmlNode.InnerText;
			}
			return "";
		}

		public static string GetAttribute(XmlNode node, string name)
		{
			foreach (XmlAttribute xmlAttribute in node.Attributes)
			{
				if (xmlAttribute.Name == name)
				{
					return xmlAttribute.Value;
				}
			}
			return "";
		}

		public static int GetIntAttribute(XmlNode node, string name)
		{
			string attribute = XMLHelper.GetAttribute(node, name);
			return int.Parse(attribute);
		}

		public static bool GetBoolAttribute(XmlNode node, string name)
		{
			string attribute = XMLHelper.GetAttribute(node, name);
			return bool.Parse(attribute);
		}
	}
}
