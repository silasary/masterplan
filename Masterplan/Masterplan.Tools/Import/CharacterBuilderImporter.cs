using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Utils;

namespace Masterplan.Tools.Import
{
    static class CharacterBuilderImporter
    {
        /// <summary>
        /// Import a .dnd4e character builder file.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static Hero ImportHero(string xml)
        {
            Hero hero = new Hero();
            try
            {
                xml = xml.Replace("RESISTANCE_+", "RESISTANCE_PLUS");
                xml = xml.Replace("CORMYR!", "CORMYR");
                xml = xml.Replace("SILVER_TONGUE,", "SILVER_TONGUE");
                XmlDocument xmlDocument = XMLHelper.LoadSource(xml);
                if (xmlDocument == null)
                {
                    return null;
                }
                XmlNode xmlNode = XMLHelper.FindChild(xmlDocument.DocumentElement, "CharacterSheet");
                if (xmlNode != null)
                {
                    XmlNode xmlNode2 = XMLHelper.FindChild(xmlNode, "Details");
                    if (xmlNode2 != null)
                    {
                        hero.Name = XMLHelper.NodeText(xmlNode2, "name").Trim();
                        hero.Player = XMLHelper.NodeText(xmlNode2, "Player").Trim();
                        hero.Level = int.Parse(XMLHelper.NodeText(xmlNode2, "Level"));
                        string text = XMLHelper.NodeText(xmlNode2, "Portrait").Trim();
                        if (text != "")
                        {
                            try
                            {
                                string text2 = "file://";
                                if (text.StartsWith(text2))
                                {
                                    text = text.Substring(text2.Length);
                                }
                                if (File.Exists(text))
                                {
                                    hero.Portrait = Image.FromFile(text);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    XmlNode xmlNode3 = XMLHelper.FindChild(xmlNode, "StatBlock");
                    if (xmlNode3 != null)
                    {
                        XmlNode xmlNode4 = get_stat_node(xmlNode3, "Hit Points");
                        if (xmlNode4 != null)
                        {
                            hero.HP = XMLHelper.GetIntAttribute(xmlNode4, "value");
                        }
                        XmlNode xmlNode5 = get_stat_node(xmlNode3, "AC");
                        if (xmlNode5 != null)
                        {
                            hero.AC = XMLHelper.GetIntAttribute(xmlNode5, "value");
                        }
                        XmlNode xmlNode6 = get_stat_node(xmlNode3, "Fortitude Defense");
                        if (xmlNode6 != null)
                        {
                            hero.Fortitude = XMLHelper.GetIntAttribute(xmlNode6, "value");
                        }
                        XmlNode xmlNode7 = get_stat_node(xmlNode3, "Reflex Defense");
                        if (xmlNode7 != null)
                        {
                            hero.Reflex = XMLHelper.GetIntAttribute(xmlNode7, "value");
                        }
                        XmlNode xmlNode8 = get_stat_node(xmlNode3, "Will Defense");
                        if (xmlNode8 != null)
                        {
                            hero.Will = XMLHelper.GetIntAttribute(xmlNode8, "value");
                        }
                        XmlNode xmlNode9 = get_stat_node(xmlNode3, "Initiative");
                        if (xmlNode9 != null)
                        {
                            hero.InitBonus = XMLHelper.GetIntAttribute(xmlNode9, "value");
                        }
                        XmlNode xmlNode10 = get_stat_node(xmlNode3, "Passive Perception");
                        if (xmlNode10 != null)
                        {
                            hero.PassivePerception = XMLHelper.GetIntAttribute(xmlNode10, "value");
                        }
                        XmlNode xmlNode11 = get_stat_node(xmlNode3, "Passive Insight");
                        if (xmlNode11 != null)
                        {
                            hero.PassiveInsight = XMLHelper.GetIntAttribute(xmlNode11, "value");
                        }
                    }
                    XmlNode xmlNode12 = XMLHelper.FindChild(xmlNode, "RulesElementTally");
                    if (xmlNode12 != null)
                    {
                        XmlNode xmlNode13 = XMLHelper.FindChildWithAttribute(xmlNode12, "type", "Race");
                        if (xmlNode13 != null)
                        {
                            hero.Race = XMLHelper.GetAttribute(xmlNode13, "name");
                        }
                        XmlNode xmlNode14 = XMLHelper.FindChildWithAttribute(xmlNode12, "type", "Class");
                        if (xmlNode14 != null)
                        {
                            hero.Class = XMLHelper.GetAttribute(xmlNode14, "name");
                        }
                        XmlNode xmlNode15 = XMLHelper.FindChildWithAttribute(xmlNode12, "type", "Paragon Path");
                        if (xmlNode15 != null)
                        {
                            hero.ParagonPath = XMLHelper.GetAttribute(xmlNode15, "name");
                        }
                        XmlNode xmlNode16 = XMLHelper.FindChildWithAttribute(xmlNode12, "type", "Epic Destiny");
                        if (xmlNode16 != null)
                        {
                            hero.EpicDestiny = XMLHelper.GetAttribute(xmlNode16, "name");
                        }
                        XmlNode xmlNode17 = XMLHelper.FindChildWithAttribute(xmlNode12, "type", "Role");
                        if (xmlNode17 != null)
                        {
                            hero.Role = (HeroRoleType)Enum.Parse(typeof(HeroRoleType), XMLHelper.GetAttribute(xmlNode17, "name"));
                        }
                        XmlNode xmlNode18 = XMLHelper.FindChildWithAttribute(xmlNode12, "type", "Power Source");
                        if (xmlNode18 != null)
                        {
                            hero.PowerSource = XMLHelper.GetAttribute(xmlNode18, "name");
                        }
                        List<XmlNode> list = XMLHelper.FindChildrenWithAttribute(xmlNode12, "type", "Language");
                        foreach (XmlNode current in list)
                        {
                            string attribute = XMLHelper.GetAttribute(current, "name");
                            if (attribute != "")
                            {
                                if (hero.Languages != "")
                                {
                                    Hero expr_3BE = hero;
                                    expr_3BE.Languages += ", ";
                                }
                                Hero expr_3D4 = hero;
                                expr_3D4.Languages += attribute;
                            }
                        }
                    }
                }
                XmlNode xmlNode19 = XMLHelper.FindChild(xmlDocument.DocumentElement, "Level");
                if (xmlNode19 != null)
                {
                    XmlNode xmlNode20 = XMLHelper.FindChildWithAttribute(xmlNode19, "name", "1");
                    if (xmlNode20 != null)
                    {
                        if (hero.Race == "")
                        {
                            XmlNode xmlNode21 = XMLHelper.FindChildWithAttribute(xmlNode20, "type", "Race");
                            if (xmlNode21 != null)
                            {
                                hero.Race = XMLHelper.GetAttribute(xmlNode21, "name");
                            }
                        }
                        if (hero.Class == "")
                        {
                            XmlNode xmlNode22 = XMLHelper.FindChildWithAttribute(xmlNode20, "type", "Class");
                            if (xmlNode22 != null)
                            {
                                hero.Class = XMLHelper.GetAttribute(xmlNode22, "name");
                            }
                        }
                    }
                    XmlNode xmlNode23 = XMLHelper.FindChildWithAttribute(xmlNode19, "name", "11");
                    if (xmlNode23 != null && hero.ParagonPath == "")
                    {
                        XmlNode xmlNode24 = XMLHelper.FindChildWithAttribute(xmlNode23, "type", "ParagonPath");
                        if (xmlNode24 != null)
                        {
                            hero.ParagonPath = XMLHelper.GetAttribute(xmlNode24, "name");
                        }
                    }
                    XmlNode xmlNode25 = XMLHelper.FindChildWithAttribute(xmlNode19, "name", "21");
                    if (xmlNode25 != null && hero.EpicDestiny == "")
                    {
                        XmlNode xmlNode26 = XMLHelper.FindChildWithAttribute(xmlNode25, "type", "EpicDestiny");
                        if (xmlNode26 != null)
                        {
                            hero.EpicDestiny = XMLHelper.GetAttribute(xmlNode26, "name");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
            return hero;
        }

        private static XmlNode get_stat_node(XmlNode parent, string name)
        {
            XmlNode xmlNode = XMLHelper.FindChildWithAttribute(parent, "name", name);
            if (xmlNode != null)
            {
                return xmlNode;
            }
            foreach (XmlNode xmlNode2 in parent.ChildNodes)
            {
                xmlNode = XMLHelper.FindChildWithAttribute(xmlNode2, "name", name);
                if (xmlNode != null)
                {
                    return xmlNode2;
                }
            }
            return null;
        }


    }
}
