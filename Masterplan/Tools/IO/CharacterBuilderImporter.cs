using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

using Utils;

using Masterplan.Data;

namespace Masterplan.Tools.IO
{
    class CharacterBuilderImporter
    {
        #region Character Builder

        public static Hero ImportHero(string xml)
        {
            Hero hero = new Hero();

            try
            {
                xml = xml.Replace("RESISTANCE_+", "RESISTANCE_PLUS");
                xml = xml.Replace("CORMYR!", "CORMYR");
                xml = xml.Replace("SILVER_TONGUE,", "SILVER_TONGUE");

                XmlDocument doc = XMLHelper.LoadSource(xml);
                if (doc == null)
                    return null;

                #region Character Sheet

                XmlNode cs_node = XMLHelper.FindChild(doc.DocumentElement, "CharacterSheet");
                if (cs_node != null)
                {
                    XmlNode details_node = XMLHelper.FindChild(cs_node, "Details");
                    if (details_node != null)
                    {
                        hero.Name = XMLHelper.NodeText(details_node, "name").Trim();
                        hero.Player = XMLHelper.NodeText(details_node, "Player").Trim();
                        hero.Level = int.Parse(XMLHelper.NodeText(details_node, "Level"));

                        string portrait_file = XMLHelper.NodeText(details_node, "Portrait").Trim();
                        if (portrait_file != "")
                        {
                            try
                            {
                                string preamble = "file://";
                                if (portrait_file.StartsWith(preamble))
                                    portrait_file = portrait_file.Substring(preamble.Length);

                                if (File.Exists(portrait_file))
                                    hero.Portrait = Image.FromFile(portrait_file);
                            }
                            catch
                            {
                            }
                        }
                    }

                    XmlNode stats_node = XMLHelper.FindChild(cs_node, "StatBlock");
                    if (stats_node != null)
                    {
                        // HP
                        XmlNode hp_node = get_stat_node(stats_node, "Hit Points");
                        if (hp_node != null)
                            hero.HP = XMLHelper.GetIntAttribute(hp_node, "value");

                        // AC
                        XmlNode ac_node = get_stat_node(stats_node, "AC");
                        if (ac_node != null)
                            hero.AC = XMLHelper.GetIntAttribute(ac_node, "value");

                        // Fortitude
                        XmlNode fort_node = get_stat_node(stats_node, "Fortitude Defense");
                        if (fort_node != null)
                            hero.Fortitude = XMLHelper.GetIntAttribute(fort_node, "value");

                        // Reflex
                        XmlNode ref_node = get_stat_node(stats_node, "Reflex Defense");
                        if (ref_node != null)
                            hero.Reflex = XMLHelper.GetIntAttribute(ref_node, "value");

                        // Will
                        XmlNode will_node = get_stat_node(stats_node, "Will Defense");
                        if (will_node != null)
                            hero.Will = XMLHelper.GetIntAttribute(will_node, "value");

                        // Initiative bonus
                        XmlNode init_node = get_stat_node(stats_node, "Initiative");
                        if (init_node != null)
                            hero.InitBonus = XMLHelper.GetIntAttribute(init_node, "value");

                        // Passive perception
                        XmlNode perc_node = get_stat_node(stats_node, "Passive Perception");
                        if (perc_node != null)
                            hero.PassivePerception = XMLHelper.GetIntAttribute(perc_node, "value");

                        // Passive insight
                        XmlNode ins_node = get_stat_node(stats_node, "Passive Insight");
                        if (ins_node != null)
                            hero.PassiveInsight = XMLHelper.GetIntAttribute(ins_node, "value");
                    }

                    XmlNode rules_node = XMLHelper.FindChild(cs_node, "RulesElementTally");
                    if (rules_node != null)
                    {
                        // Race
                        XmlNode race_node = XMLHelper.FindChildWithAttribute(rules_node, "type", "Race");
                        if (race_node != null)
                            hero.Race = XMLHelper.GetAttribute(race_node, "name");

                        // Class
                        XmlNode class_node = XMLHelper.FindChildWithAttribute(rules_node, "type", "Class");
                        if (class_node != null)
                            hero.Class = XMLHelper.GetAttribute(class_node, "name");

                        // Paragon Path
                        XmlNode pp_node = XMLHelper.FindChildWithAttribute(rules_node, "type", "Paragon Path");
                        if (pp_node != null)
                            hero.ParagonPath = XMLHelper.GetAttribute(pp_node, "name");

                        // Epic Destiny
                        XmlNode ed_node = XMLHelper.FindChildWithAttribute(rules_node, "type", "Epic Destiny");
                        if (ed_node != null)
                            hero.EpicDestiny = XMLHelper.GetAttribute(ed_node, "name");

                        // Role
                        XmlNode role_node = XMLHelper.FindChildWithAttribute(rules_node, "type", "Role");
                        if (role_node != null)
                            hero.Role = (HeroRoleType)Enum.Parse(typeof(HeroRoleType), XMLHelper.GetAttribute(role_node, "name"));

                        // Power source
                        XmlNode source_node = XMLHelper.FindChildWithAttribute(rules_node, "type", "Power Source");
                        if (source_node != null)
                            hero.PowerSource = XMLHelper.GetAttribute(source_node, "name");

                        // Languages
                        List<XmlNode> lang_nodes = XMLHelper.FindChildrenWithAttribute(rules_node, "type", "Language");
                        foreach (XmlNode lang_node in lang_nodes)
                        {
                            string lang = XMLHelper.GetAttribute(lang_node, "name");
                            if (lang != "")
                            {
                                if (hero.Languages != "")
                                    hero.Languages += ", ";

                                hero.Languages += lang;
                            }
                        }
                    }
                }

                #endregion

                #region Levels

                XmlNode level_node = XMLHelper.FindChild(doc.DocumentElement, "Level");
                if (level_node != null)
                {
                    XmlNode level_1 = XMLHelper.FindChildWithAttribute(level_node, "name", "1");
                    if (level_1 != null)
                    {
                        if (hero.Race == "")
                        {
                            XmlNode race_node = XMLHelper.FindChildWithAttribute(level_1, "type", "Race");
                            if (race_node != null)
                                hero.Race = XMLHelper.GetAttribute(race_node, "name");
                        }

                        if (hero.Class == "")
                        {
                            XmlNode class_node = XMLHelper.FindChildWithAttribute(level_1, "type", "Class");
                            if (class_node != null)
                                hero.Class = XMLHelper.GetAttribute(class_node, "name");
                        }
                    }

                    XmlNode level_11 = XMLHelper.FindChildWithAttribute(level_node, "name", "11");
                    if (level_11 != null)
                    {
                        if (hero.ParagonPath == "")
                        {
                            XmlNode pp_node = XMLHelper.FindChildWithAttribute(level_11, "type", "ParagonPath");
                            if (pp_node != null)
                                hero.ParagonPath = XMLHelper.GetAttribute(pp_node, "name");
                        }
                    }

                    XmlNode level_21 = XMLHelper.FindChildWithAttribute(level_node, "name", "21");
                    if (level_21 != null)
                    {
                        if (hero.EpicDestiny == "")
                        {
                            XmlNode ed_node = XMLHelper.FindChildWithAttribute(level_21, "type", "EpicDestiny");
                            if (ed_node != null)
                                hero.EpicDestiny = XMLHelper.GetAttribute(ed_node, "name");
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }

            return hero;
        }

        static XmlNode get_stat_node(XmlNode parent, string name)
        {
            XmlNode node = XMLHelper.FindChildWithAttribute(parent, "name", name);
            if (node != null)
                return node;

            foreach (XmlNode child in parent.ChildNodes)
            {
                node = XMLHelper.FindChildWithAttribute(child, "name", name);
                if (node != null)
                    return child;
            }

            return null;
        }

        #endregion
    }
}
