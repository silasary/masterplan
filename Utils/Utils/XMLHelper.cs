using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Utils
{
    ///<summary>
    ///Class containing XML manipulation methods.
    ///</summary>
    public class XMLHelper
	{
        ///<summary>
        ///Load an XML document.
        ///</summary>
        ///<param name="xml">The XML source</param>
        ///<returns>Returns the XML document</returns>
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

        ///<summary>
        ///Finds a named child node.
        ///</summary>
        ///<param name="parent">The parent node</param>
        ///<param name="name">The name of the child node</param>
        ///<returns>Returns the node, or null if no such node was found</returns>
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

        ///<summary>
        ///Finds the child node which has a certain value for a given attribute.
        ///</summary>
        ///<param name="parent">The parent node</param>
        ///<param name="attribute_name">The name of the attribute</param>
        ///<param name="attribute_value">The attribute value to search for</param>
        ///<returns>Returns the first such node, if one exists; null otherwise</returns>
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

        ///<summary>
        ///Gets the list of child nodes which have a certain value for a given attribute.
        ///</summary>
        ///<param name="parent">The parent node</param>
        ///<param name="attribute_name">The name of the attribute</param>
        ///<param name="attribute_value">The attribute value to search for</param>
        ///<returns>Returns the list of matching child nodes</returns>
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

        ///<summary>
        ///Gets the text of a named child node.
        ///</summary>
        ///<param name="parent">The parent node.</param>
        ///<param name="name">The name of the child node.</param>
        ///<returns>Returns the node text, or an empty string if no such node was found.</returns>
        public static string NodeText(XmlNode parent, string name)
		{
			XmlNode xmlNode = XMLHelper.FindChild(parent, name);
			if (xmlNode != null)
			{
				return xmlNode.InnerText;
			}
			return "";
		}

        ///<summary>
        ///Gets the string value of the named attribute.
        ///</summary>
        ///<param name="node">The parent node</param>
        ///<param name="name">The name of the attribute</param>
        ///<returns>Returns the attribute value</returns>
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

        ///<summary>
        ///Gets the integer value of the named attribute.
        ///</summary>
        ///<param name="node">The parent node</param>
        ///<param name="name">The name of the attribute</param>
        ///<returns>Returns the attribute value</returns>
        public static int GetIntAttribute(XmlNode node, string name)
		{
			string attribute = XMLHelper.GetAttribute(node, name);
			return int.Parse(attribute);
		}

        ///<summary>
        ///Gets the boolean value of the named attribute.
        ///</summary>
        ///<param name="node">The parent node</param>
        ///<param name="name">The name of the attribute</param>
        ///<returns>Returns the attribute value</returns>
        public static bool GetBoolAttribute(XmlNode node, string name)
		{
			string attribute = XMLHelper.GetAttribute(node, name);
			return bool.Parse(attribute);
		}
	}
}
