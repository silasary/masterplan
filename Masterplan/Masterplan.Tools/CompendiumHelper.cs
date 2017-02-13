using Masterplan.Compendium;
using System;
using System.Collections.Generic;
using System.Xml;
using Utils;

namespace Masterplan.Tools
{
	internal class CompendiumHelper
	{
		public enum ItemType
		{
			Creature,
			Trap,
			MagicItem
		}

		public class CompendiumItem
		{
			public CompendiumHelper.ItemType Type;

			public string ID = "";

			public string Name = "";

			public string SourceBook = "";

			public string Info = "";

			public string URL
			{
				get
				{
					switch (this.Type)
					{
					case CompendiumHelper.ItemType.Creature:
						return "http://www.wizards.com/dndinsider/compendium/monster.aspx?id=" + this.ID;
					case CompendiumHelper.ItemType.Trap:
						return "http://www.wizards.com/dndinsider/compendium/trap.aspx?id=" + this.ID;
					case CompendiumHelper.ItemType.MagicItem:
						return "http://www.wizards.com/dndinsider/compendium/item.aspx?id=" + this.ID;
					default:
						return "";
					}
				}
			}

			public CompendiumItem(CompendiumHelper.ItemType type, string id, string name, string source_book, string info)
			{
				this.Type = type;
				this.ID = id;
				this.Name = name;
				this.SourceBook = source_book;
				this.Info = info;
			}
		}

		public class SourceBook
		{
			public string Name = "";

			public List<CompendiumHelper.CompendiumItem> Creatures = new List<CompendiumHelper.CompendiumItem>();

			public List<CompendiumHelper.CompendiumItem> Traps = new List<CompendiumHelper.CompendiumItem>();

			public List<CompendiumHelper.CompendiumItem> MagicItems = new List<CompendiumHelper.CompendiumItem>();
		}

		public static List<CompendiumHelper.CompendiumItem> GetCreatures()
		{
			List<CompendiumHelper.CompendiumItem> list = new List<CompendiumHelper.CompendiumItem>();
			try
			{
				CompendiumSearch compendiumSearch = new CompendiumSearch();
				XmlNode xmlNode = compendiumSearch.ViewAll("Monster");
				xmlNode = xmlNode.FirstChild;
				foreach (XmlNode parent in xmlNode.ChildNodes)
				{
					string id = XMLHelper.NodeText(parent, "ID");
					string name = XMLHelper.NodeText(parent, "Name");
					string source_book = XMLHelper.NodeText(parent, "SourceBook");
					string text = XMLHelper.NodeText(parent, "Level");
					if (text != "")
					{
						text = "Level " + text;
					}
					string text2 = XMLHelper.NodeText(parent, "GroupRole");
					string text3 = XMLHelper.NodeText(parent, "CombatRole");
					text2 = text2.Replace("Standard", "");
					text3 = text3.Replace("No role", "");
					string text4 = string.Concat(new string[]
					{
						text,
						" ",
						text2,
						" ",
						text3
					});
					text4 = text4.Trim();
					text4 = text4.Replace("  ", " ");
					list.Add(new CompendiumHelper.CompendiumItem(CompendiumHelper.ItemType.Creature, id, name, source_book, text4));
				}
			}
			catch
			{
				list = null;
			}
			return list;
		}

		public static List<CompendiumHelper.CompendiumItem> GetTraps()
		{
			List<CompendiumHelper.CompendiumItem> list = new List<CompendiumHelper.CompendiumItem>();
			try
			{
				CompendiumSearch compendiumSearch = new CompendiumSearch();
				XmlNode xmlNode = compendiumSearch.ViewAll("Trap");
				xmlNode = xmlNode.FirstChild;
				foreach (XmlNode parent in xmlNode.ChildNodes)
				{
					string id = XMLHelper.NodeText(parent, "ID");
					string name = XMLHelper.NodeText(parent, "Name");
					string source_book = XMLHelper.NodeText(parent, "SourceBook");
					string text = XMLHelper.NodeText(parent, "Level");
					if (text != "")
					{
						text = "Level " + text;
					}
					string text2 = XMLHelper.NodeText(parent, "GroupRole");
					string text3 = XMLHelper.NodeText(parent, "Type");
					text2 = text2.Replace("Standard", "");
					string text4 = string.Concat(new string[]
					{
						text,
						" ",
						text2,
						" ",
						text3
					});
					text4 = text4.Trim();
					text4 = text4.Replace("  ", " ");
					list.Add(new CompendiumHelper.CompendiumItem(CompendiumHelper.ItemType.Trap, id, name, source_book, text4));
				}
			}
			catch
			{
				list = null;
			}
			return list;
		}

		public static List<CompendiumHelper.CompendiumItem> GetMagicItems()
		{
			List<CompendiumHelper.CompendiumItem> list = new List<CompendiumHelper.CompendiumItem>();
			try
			{
				CompendiumSearch compendiumSearch = new CompendiumSearch();
				XmlNode xmlNode = compendiumSearch.ViewAll("Item");
				List<string> list2 = new List<string>();
				list2.Add("Head");
				list2.Add("Neck");
				list2.Add("Arms");
				list2.Add("Hands");
				list2.Add("Waist");
				list2.Add("Feet");
				xmlNode = xmlNode.FirstChild;
				foreach (XmlNode parent in xmlNode.ChildNodes)
				{
					string id = XMLHelper.NodeText(parent, "ID");
					string name = XMLHelper.NodeText(parent, "Name");
					string source_book = XMLHelper.NodeText(parent, "SourceBook");
					string text = (XMLHelper.NodeText(parent, "IsMundane") == "True") ? "Mundane" : "";
					string text2 = XMLHelper.NodeText(parent, "Level");
					if (text2 != "")
					{
						text2 = "Level " + text2;
					}
					string text3 = XMLHelper.NodeText(parent, "Category");
					if (list2.Contains(text3))
					{
						text3 += " slot item";
					}
					if (text3 == "Alchemical" || text3 == "Wondrous" || text3 == "Consumable")
					{
						text3 += " item";
					}
					if (text3 == "Whetstones")
					{
						text3 = "Whetstone";
					}
					string text4 = string.Concat(new string[]
					{
						text,
						" ",
						text2,
						" ",
						text3
					});
					text4 = text4.Trim();
					text4 = text4.Replace("  ", " ");
					list.Add(new CompendiumHelper.CompendiumItem(CompendiumHelper.ItemType.MagicItem, id, name, source_book, text4));
				}
			}
			catch
			{
				list = null;
			}
			return list;
		}
	}
}
