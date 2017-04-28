using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Xml;

using Utils;

using Masterplan.Data;
using System.Linq;

namespace Masterplan.Tools.IO
{
    class iPlay4E : IHeroProvider
    {
        public string ProviderName => "iPlay4E";

        public bool ImportHero(Hero hero)
        {
            if ((hero.Key == null) || (hero.Key == ""))
                return false;

            bool ok = true;

            try
            {
                string url = GetUrlString(hero);

                WebClient client = new WebClient();
                client.Headers["User-Agent"] = "Mozilla/5.0 (Masterplan) like Gecko";
                string xml = client.DownloadString(url);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlNode character_node = doc.DocumentElement;
                hero.Name = XMLHelper.GetAttribute(character_node, "name");

                XmlNode build_node = XMLHelper.FindChild(character_node, "Build");
                if (build_node != null)
                {
                    hero.Level = XMLHelper.GetIntAttribute(build_node, "level");
                    try
                    {
                        string role = XMLHelper.GetAttribute(build_node, "role");
                        hero.Role = (HeroRoleType)Enum.Parse(typeof(HeroRoleType), role);
                    }
                    catch
                    {
                    }

                    try
                    {
                        string size = XMLHelper.GetAttribute(build_node, "size");
                        hero.Size = (CreatureSize)Enum.Parse(typeof(CreatureSize), size);
                    }
                    catch
                    {
                    }

                    hero.PowerSource = XMLHelper.GetAttribute(build_node, "powersource");
                    hero.Class = XMLHelper.GetAttribute(build_node, "name");

                    XmlNode race_node = XMLHelper.FindChild(build_node, "Race");
                    if (race_node != null)
                        hero.Race = XMLHelper.GetAttribute(race_node, "name");

                    XmlNode pp_node = XMLHelper.FindChild(build_node, "ParagonPath");
                    if (pp_node != null)
                        hero.ParagonPath = XMLHelper.GetAttribute(pp_node, "name");

                    XmlNode ed_node = XMLHelper.FindChild(build_node, "EpicDestiny");
                    if (ed_node != null)
                        hero.EpicDestiny = XMLHelper.GetAttribute(ed_node, "name");
                }

                XmlNode health_node = XMLHelper.FindChild(character_node, "Health");
                if (health_node != null)
                {
                    XmlNode max_hp_node = XMLHelper.FindChild(health_node, "MaxHitPoints");
                    if (max_hp_node != null)
                        hero.HP = XMLHelper.GetIntAttribute(max_hp_node, "value");
                }

                XmlNode move_node = XMLHelper.FindChild(character_node, "Movement");
                if (move_node != null)
                {
                    XmlNode init_node = XMLHelper.FindChild(move_node, "Initiative");
                    if (init_node != null)
                        hero.InitBonus = XMLHelper.GetIntAttribute(init_node, "value");
                }

                XmlNode def_node = XMLHelper.FindChild(character_node, "Defenses");
                if (def_node != null)
                {
                    XmlNode ac_node = XMLHelper.FindChildWithAttribute(def_node, "abbreviation", "AC");
                    if (ac_node != null)
                        hero.AC = XMLHelper.GetIntAttribute(ac_node, "value");

                    XmlNode fort_node = XMLHelper.FindChildWithAttribute(def_node, "abbreviation", "Fort");
                    if (fort_node != null)
                        hero.Fortitude = XMLHelper.GetIntAttribute(fort_node, "value");

                    XmlNode ref_node = XMLHelper.FindChildWithAttribute(def_node, "abbreviation", "Ref");
                    if (ref_node != null)
                        hero.Reflex = XMLHelper.GetIntAttribute(ref_node, "value");

                    XmlNode will_node = XMLHelper.FindChildWithAttribute(def_node, "abbreviation", "Will");
                    if (will_node != null)
                        hero.Will = XMLHelper.GetIntAttribute(will_node, "value");
                }

                XmlNode skills_node = XMLHelper.FindChild(character_node, "PassiveSkills");
                if (skills_node != null)
                {
                    XmlNode ins_node = XMLHelper.FindChildWithAttribute(skills_node, "name", "Insight");
                    if (ins_node != null)
                        hero.PassiveInsight = XMLHelper.GetIntAttribute(ins_node, "value");

                    XmlNode perc_node = XMLHelper.FindChildWithAttribute(skills_node, "name", "Perception");
                    if (perc_node != null)
                        hero.PassivePerception = XMLHelper.GetIntAttribute(perc_node, "value");
                }

                XmlNode langs_node = XMLHelper.FindChild(character_node, "Languages");
                if (langs_node != null)
                {
                    string langs = "";

                    foreach (XmlNode lang_node in langs_node.ChildNodes)
                    {
                        string lang = XMLHelper.GetAttribute(lang_node, "name");

                        if (langs != "")
                            langs += ", ";

                        langs += lang;
                    }

                    hero.Languages = langs;
                }
            }
            catch
            {
                ok = false;
            }

            return ok;
        }

        public List<Hero> ImportParty(string key, IEnumerable<Hero> ExistingHeroes)
        {
            List<Hero> heroes = new List<Hero>();

            try
            {
                string url = "http://iplay4e.appspot.com/campaigns/" + key + "/main";

                WebClient client = new WebClient();
                client.Headers["User-Agent"] = "Mozilla/5.0 (Masterplan) like Gecko";
                string xml = client.DownloadString(url);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlNode campaign_node = doc.DocumentElement;
                if (campaign_node != null)
                {
                    XmlNode chars_node = XMLHelper.FindChild(campaign_node, "Characters");
                    if (chars_node != null)
                    {
                        foreach (XmlNode char_node in chars_node.ChildNodes)
                        {
                            try
                            {
                                string hero_key = XMLHelper.GetAttribute(char_node, "key");

                                Hero hero = ExistingHeroes.SingleOrDefault(h => h.Key == hero_key);
                                
                                if (hero == null)
                                {
                                    hero = new Hero();
                                    hero.Key = hero_key;
                                }

                                bool ok = ImportHero(hero);

                                if (ok)
                                    heroes.Add(hero);
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

            return heroes;
        }

        public string GetUrlString(Hero hero)
        {
            return "http://iplay4e.appspot.com/view?xsl=jPint&key=" + hero.Key;
        }
    }
}
