using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Utils;

namespace Masterplan.Tools.Import
{
    class IPlay4E : IHeroProvider
    {
        public string ProviderName => "iPlay4E";

        public bool ImportHero(Hero hero)
        {
            if (hero.Key == null || hero.Key == "")
            {
                return false;
            }
            try
            {
                string address = GetUrlString(hero);
                WebClient webClient = new WebClient();
                webClient.Headers["User-Agent"] = "Mozilla/5.0 (Masterplan) like Gecko";
                string xml = webClient.DownloadString(address);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);
                XmlNode documentElement = xmlDocument.DocumentElement;
                hero.Name = XMLHelper.GetAttribute(documentElement, "name");
                XmlNode xmlNode = XMLHelper.FindChild(documentElement, "Build");
                if (xmlNode != null)
                {
                    hero.Level = XMLHelper.GetIntAttribute(xmlNode, "level");
                    try
                    {
                        string role = XMLHelper.GetAttribute(xmlNode, "role");
                        hero.Role = (HeroRoleType)Enum.Parse(typeof(HeroRoleType), role);
                    }
                    catch
                    {
                    }
                    try
                    {
                        string size = XMLHelper.GetAttribute(xmlNode, "size");
                        hero.Size = (CreatureSize)Enum.Parse(typeof(CreatureSize), size);
                    }
                    catch
                    {
                    }
                    hero.PowerSource = XMLHelper.GetAttribute(xmlNode, "powersource");
                    hero.Class = XMLHelper.GetAttribute(xmlNode, "name");
                    XmlNode raceElement = XMLHelper.FindChild(xmlNode, "Race");
                    if (raceElement != null)
                    {
                        hero.Race = XMLHelper.GetAttribute(raceElement, "name");
                    }
                    XmlNode paragonPath = XMLHelper.FindChild(xmlNode, "ParagonPath");
                    if (paragonPath != null)
                    {
                        hero.ParagonPath = XMLHelper.GetAttribute(paragonPath, "name");
                    }
                    XmlNode xmlNode4 = XMLHelper.FindChild(xmlNode, "EpicDestiny");
                    if (xmlNode4 != null)
                    {
                        hero.EpicDestiny = XMLHelper.GetAttribute(xmlNode4, "name");
                    }
                }
                XmlNode xmlNode5 = XMLHelper.FindChild(documentElement, "Health");
                if (xmlNode5 != null)
                {
                    XmlNode xmlNode6 = XMLHelper.FindChild(xmlNode5, "MaxHitPoints");
                    if (xmlNode6 != null)
                    {
                        hero.HP = XMLHelper.GetIntAttribute(xmlNode6, "value");
                    }
                }
                XmlNode xmlNode7 = XMLHelper.FindChild(documentElement, "Movement");
                if (xmlNode7 != null)
                {
                    XmlNode xmlNode8 = XMLHelper.FindChild(xmlNode7, "Initiative");
                    if (xmlNode8 != null)
                    {
                        hero.InitBonus = XMLHelper.GetIntAttribute(xmlNode8, "value");
                    }
                }
                XmlNode xmlNode9 = XMLHelper.FindChild(documentElement, "Defenses");
                if (xmlNode9 != null)
                {
                    XmlNode xmlNode10 = XMLHelper.FindChildWithAttribute(xmlNode9, "abbreviation", "AC");
                    if (xmlNode10 != null)
                    {
                        hero.AC = XMLHelper.GetIntAttribute(xmlNode10, "value");
                    }
                    XmlNode xmlNode11 = XMLHelper.FindChildWithAttribute(xmlNode9, "abbreviation", "Fort");
                    if (xmlNode11 != null)
                    {
                        hero.Fortitude = XMLHelper.GetIntAttribute(xmlNode11, "value");
                    }
                    XmlNode xmlNode12 = XMLHelper.FindChildWithAttribute(xmlNode9, "abbreviation", "Ref");
                    if (xmlNode12 != null)
                    {
                        hero.Reflex = XMLHelper.GetIntAttribute(xmlNode12, "value");
                    }
                    XmlNode xmlNode13 = XMLHelper.FindChildWithAttribute(xmlNode9, "abbreviation", "Will");
                    if (xmlNode13 != null)
                    {
                        hero.Will = XMLHelper.GetIntAttribute(xmlNode13, "value");
                    }
                }
                XmlNode xmlNode14 = XMLHelper.FindChild(documentElement, "PassiveSkills");
                if (xmlNode14 != null)
                {
                    XmlNode xmlNode15 = XMLHelper.FindChildWithAttribute(xmlNode14, "name", "Insight");
                    if (xmlNode15 != null)
                    {
                        hero.PassiveInsight = XMLHelper.GetIntAttribute(xmlNode15, "value");
                    }
                    XmlNode xmlNode16 = XMLHelper.FindChildWithAttribute(xmlNode14, "name", "Perception");
                    if (xmlNode16 != null)
                    {
                        hero.PassivePerception = XMLHelper.GetIntAttribute(xmlNode16, "value");
                    }
                }
                XmlNode xmlNode17 = XMLHelper.FindChild(documentElement, "Languages");
                if (xmlNode17 != null)
                {
                    string text = "";
                    foreach (XmlNode node in xmlNode17.ChildNodes)
                    {
                        string attribute3 = XMLHelper.GetAttribute(node, "name");
                        if (text != "")
                        {
                            text += ", ";
                        }
                        text += attribute3;
                    }
                    hero.Languages = text;
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public List<Hero> ImportParty(string key)
        {
            List<Hero> list = new List<Hero>();
            try
            {
                string address = "http://iplay4e.appspot.com/campaigns/" + key + "/main";
                WebClient webClient = new WebClient();
                webClient.Headers["User-Agent"] = "Mozilla/5.0 (Masterplan) like Gecko";
                string xml = webClient.DownloadString(address);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);
                XmlNode documentElement = xmlDocument.DocumentElement;
                if (documentElement != null)
                {
                    XmlNode xmlNode = XMLHelper.FindChild(documentElement, "Characters");
                    if (xmlNode != null)
                    {
                        foreach (XmlNode node in xmlNode.ChildNodes)
                        {
                            try
                            {
                                string heroKey = XMLHelper.GetAttribute(node, "key");
                                Hero hero = new Hero()
                                {
                                    Key = heroKey
                                };
                                bool success = ImportHero(hero);
                                if (success)
                                {
                                    list.Add(hero);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return list;
        }

        public string GetUrlString(Hero hero)
        {
            return "http://iplay4e.appspot.com/view?xsl=jPint&key=" + hero.Key;
        }
    }
}
