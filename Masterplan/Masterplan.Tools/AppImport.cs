using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Xml;
using Utils;

namespace Masterplan.Tools
{
    internal class AppImport
    {
        public static Creature ImportCreature(string xml)
        {
            Creature creature = null;
            try
            {
                XmlDocument xmlDocument = XMLHelper.LoadSource(xml);
                if (xmlDocument == null)
                {
                    return null;
                }
                XmlNode documentElement = xmlDocument.DocumentElement;
                creature = new Creature();
                bool flag = false;
                foreach (XmlNode xmlNode in documentElement.ChildNodes)
                {
                    if (!(xmlNode.Name == "ID"))
                    {
                        if (xmlNode.Name == "AbilityScores")
                        {
                            try
                            {
                                XmlNode firstChild = xmlNode.FirstChild;
                                foreach (XmlNode xmlNode2 in firstChild.ChildNodes)
                                {
                                    string a = XMLHelper.NodeText(xmlNode2, "Name");
                                    int num = XMLHelper.GetIntAttribute(xmlNode2, "FinalValue");
                                    num = Math.Max(num, 0);
                                    if (a == "Strength")
                                    {
                                        creature.Strength.Score = num;
                                    }
                                    if (a == "Constitution")
                                    {
                                        creature.Constitution.Score = num;
                                    }
                                    if (a == "Dexterity")
                                    {
                                        creature.Dexterity.Score = num;
                                    }
                                    if (a == "Intelligence")
                                    {
                                        creature.Intelligence.Score = num;
                                    }
                                    if (a == "Wisdom")
                                    {
                                        creature.Wisdom.Score = num;
                                    }
                                    if (a == "Charisma")
                                    {
                                        creature.Charisma.Score = num;
                                    }
                                }
                                continue;
                            }
                            catch
                            {
                                LogSystem.Trace("Error parsing " + xmlNode.Name);
                                continue;
                            }
                        }
                        if (xmlNode.Name == "Defenses")
                        {
                            try
                            {
                                XmlNode firstChild2 = xmlNode.FirstChild;
                                foreach (XmlNode xmlNode3 in firstChild2.ChildNodes)
                                {
                                    string a2 = XMLHelper.NodeText(xmlNode3, "Name");
                                    int intAttribute = XMLHelper.GetIntAttribute(xmlNode3, "FinalValue");
                                    if (a2 == "AC")
                                    {
                                        creature.AC = intAttribute;
                                    }
                                    if (a2 == "Fortitude")
                                    {
                                        creature.Fortitude = intAttribute;
                                    }
                                    if (a2 == "Reflex")
                                    {
                                        creature.Reflex = intAttribute;
                                    }
                                    if (a2 == "Will")
                                    {
                                        creature.Will = intAttribute;
                                    }
                                }
                                continue;
                            }
                            catch
                            {
                                LogSystem.Trace("Error parsing " + xmlNode.Name);
                                continue;
                            }
                        }
                        if (!(xmlNode.Name == "AttackBonuses"))
                        {
                            if (xmlNode.Name == "Skills")
                            {
                                try
                                {
                                    string text = "";
                                    XmlNode firstChild3 = xmlNode.FirstChild;
                                    foreach (XmlNode xmlNode4 in firstChild3.ChildNodes)
                                    {
                                        string text2 = XMLHelper.NodeText(xmlNode4, "Name");
                                        int intAttribute2 = XMLHelper.GetIntAttribute(xmlNode4, "FinalValue");
                                        bool flag2 = false;
                                        string text3 = XMLHelper.NodeText(xmlNode4, "Trained");
                                        if (text3 != "")
                                        {
                                            flag2 = bool.Parse(text3);
                                        }
                                        if (flag2)
                                        {
                                            if (text != "")
                                            {
                                                text += ", ";
                                            }
                                            string text4 = (intAttribute2 >= 0) ? "+" : "";
                                            object obj = text;
                                            text = string.Concat(new object[]
                                            {
                                                obj,
                                                text2,
                                                " ",
                                                text4,
                                                intAttribute2
                                            });
                                        }
                                    }
                                    creature.Skills = text;
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Name")
                            {
                                try
                                {
                                    creature.Name = xmlNode.InnerText;
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Level")
                            {
                                try
                                {
                                    creature.Level = int.Parse(xmlNode.InnerText);
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Size")
                            {
                                try
                                {
                                    XmlNode xmlNode5 = XMLHelper.FindChild(xmlNode, "ReferencedObject");
                                    if (xmlNode5 != null)
                                    {
                                        XmlNode xmlNode6 = XMLHelper.FindChild(xmlNode5, "Name");
                                        if (xmlNode6 != null)
                                        {
                                            string innerText = xmlNode6.InnerText;
                                            creature.Size = (CreatureSize)Enum.Parse(typeof(CreatureSize), innerText);
                                        }
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Origin")
                            {
                                try
                                {
                                    XmlNode xmlNode7 = XMLHelper.FindChild(xmlNode, "ReferencedObject");
                                    if (xmlNode7 != null)
                                    {
                                        XmlNode xmlNode8 = XMLHelper.FindChild(xmlNode7, "Name");
                                        if (xmlNode8 != null)
                                        {
                                            string innerText2 = xmlNode8.InnerText;
                                            creature.Origin = (CreatureOrigin)Enum.Parse(typeof(CreatureOrigin), innerText2);
                                        }
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Type")
                            {
                                try
                                {
                                    XmlNode xmlNode9 = XMLHelper.FindChild(xmlNode, "ReferencedObject");
                                    if (xmlNode9 != null)
                                    {
                                        XmlNode xmlNode10 = XMLHelper.FindChild(xmlNode9, "Name");
                                        if (xmlNode10 != null)
                                        {
                                            string value = xmlNode10.InnerText.Replace(" ", "");
                                            creature.Type = (CreatureType)Enum.Parse(typeof(CreatureType), value);
                                        }
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "GroupRole")
                            {
                                try
                                {
                                    XmlNode xmlNode11 = XMLHelper.FindChild(xmlNode, "ReferencedObject");
                                    if (xmlNode11 != null)
                                    {
                                        XmlNode xmlNode12 = XMLHelper.FindChild(xmlNode11, "Name");
                                        if (xmlNode12 != null)
                                        {
                                            string innerText3 = xmlNode12.InnerText;
                                            if (innerText3 == "Minion")
                                            {
                                                Minion minion = new Minion();
                                                if (flag)
                                                {
                                                    ComplexRole complexRole = creature.Role as ComplexRole;
                                                    minion.HasRole = true;
                                                    minion.Type = complexRole.Type;
                                                }
                                                creature.Role = minion;
                                            }
                                            else
                                            {
                                                RoleFlag flag3 = (RoleFlag)Enum.Parse(typeof(RoleFlag), innerText3);
                                                ComplexRole complexRole2 = creature.Role as ComplexRole;
                                                complexRole2.Flag = flag3;
                                            }
                                        }
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Role")
                            {
                                try
                                {
                                    XmlNode xmlNode13 = XMLHelper.FindChild(xmlNode, "ReferencedObject");
                                    if (xmlNode13 != null)
                                    {
                                        XmlNode xmlNode14 = XMLHelper.FindChild(xmlNode13, "Name");
                                        if (xmlNode14 != null)
                                        {
                                            string innerText4 = xmlNode14.InnerText;
                                            RoleType type = (RoleType)Enum.Parse(typeof(RoleType), innerText4);
                                            if (creature.Role is ComplexRole)
                                            {
                                                ComplexRole complexRole3 = creature.Role as ComplexRole;
                                                complexRole3.Type = type;
                                            }
                                            if (creature.Role is Minion)
                                            {
                                                Minion minion2 = creature.Role as Minion;
                                                minion2.HasRole = true;
                                                minion2.Type = type;
                                            }
                                            flag = true;
                                        }
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "IsLeader")
                            {
                                try
                                {
                                    string innerText5 = xmlNode.InnerText;
                                    bool flag4 = bool.Parse(innerText5);
                                    if (creature.Role is ComplexRole && flag4)
                                    {
                                        ComplexRole complexRole4 = creature.Role as ComplexRole;
                                        complexRole4.Leader = true;
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Items")
                            {
                                try
                                {
                                    foreach (XmlNode parent in xmlNode.ChildNodes)
                                    {
                                        XmlNode parent2 = XMLHelper.FindChild(parent, "Item");
                                        XmlNode xmlNode15 = XMLHelper.FindChild(XMLHelper.FindChild(parent2, "ReferencedObject"), "Name");
                                        string innerText6 = xmlNode15.InnerText;
                                        int num2 = int.Parse(XMLHelper.NodeText(parent, "Quantity"));
                                        if (creature.Equipment != "")
                                        {
                                            Creature expr_8A6 = creature;
                                            expr_8A6.Equipment += ", ";
                                        }
                                        Creature expr_8BC = creature;
                                        expr_8BC.Equipment += innerText6;
                                        if (num2 != 1)
                                        {
                                            Creature expr_8D4 = creature;
                                            expr_8D4.Equipment = expr_8D4.Equipment + " x" + num2;
                                        }
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Languages")
                            {
                                try
                                {
                                    string text5 = "";
                                    foreach (XmlNode parent3 in xmlNode.ChildNodes)
                                    {
                                        XmlNode xmlNode16 = XMLHelper.FindChild(parent3, "ReferencedObject");
                                        if (xmlNode16 != null)
                                        {
                                            XmlNode xmlNode17 = XMLHelper.FindChild(xmlNode16, "Name");
                                            if (xmlNode17 != null)
                                            {
                                                string innerText7 = xmlNode17.InnerText;
                                                if (text5 != "")
                                                {
                                                    text5 += ", ";
                                                }
                                                text5 += innerText7;
                                            }
                                        }
                                    }
                                    creature.Languages = text5;
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Senses")
                            {
                                try
                                {
                                    string text6 = "";
                                    foreach (XmlNode parent4 in xmlNode.ChildNodes)
                                    {
                                        XmlNode xmlNode18 = XMLHelper.FindChild(parent4, "ReferencedObject");
                                        if (xmlNode18 != null)
                                        {
                                            string text7 = XMLHelper.NodeText(xmlNode18, "Name");
                                            string text8 = XMLHelper.NodeText(parent4, "Range");
                                            if (text8 != "" && text8 != "0")
                                            {
                                                text7 = text7 + " " + text8;
                                            }
                                            if (text6 != "")
                                            {
                                                text6 += ", ";
                                            }
                                            text6 += text7;
                                        }
                                    }
                                    if (text6 != "")
                                    {
                                        if (creature.Senses != "")
                                        {
                                            Creature expr_B0F = creature;
                                            expr_B0F.Senses += "; ";
                                        }
                                        Creature expr_B25 = creature;
                                        expr_B25.Senses += text6;
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Regeneration")
                            {
                                try
                                {
                                    Regeneration regeneration = new Regeneration();
                                    regeneration.Value = XMLHelper.GetIntAttribute(xmlNode, "FinalValue");
                                    string text9 = XMLHelper.NodeText(xmlNode, "Details");
                                    if (text9 != "")
                                    {
                                        regeneration.Details = text9;
                                    }
                                    if (regeneration.Value != 0 || regeneration.Details != "")
                                    {
                                        creature.Regeneration = regeneration;
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Keywords")
                            {
                                try
                                {
                                    string text10 = "";
                                    foreach (XmlNode parent5 in xmlNode.ChildNodes)
                                    {
                                        XmlNode xmlNode19 = XMLHelper.FindChild(parent5, "ReferencedObject");
                                        if (xmlNode19 != null)
                                        {
                                            XmlNode xmlNode20 = XMLHelper.FindChild(xmlNode19, "Name");
                                            if (xmlNode20 != null)
                                            {
                                                string innerText8 = xmlNode20.InnerText;
                                                if (text10 != "")
                                                {
                                                    text10 += ", ";
                                                }
                                                text10 += innerText8;
                                            }
                                        }
                                        creature.Keywords = text10;
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Alignment")
                            {
                                try
                                {
                                    XmlNode xmlNode21 = XMLHelper.FindChild(xmlNode, "ReferencedObject");
                                    if (xmlNode21 != null)
                                    {
                                        XmlNode xmlNode22 = XMLHelper.FindChild(xmlNode21, "Name");
                                        if (xmlNode22 != null)
                                        {
                                            string innerText9 = xmlNode22.InnerText;
                                            creature.Alignment = innerText9;
                                        }
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Powers")
                            {
                                try
                                {
                                    foreach (XmlNode power_node in xmlNode.ChildNodes)
                                    {
                                        AppImport.import_power(power_node, creature);
                                    }
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "Initiative")
                            {
                                try
                                {
                                    creature.Initiative = XMLHelper.GetIntAttribute(xmlNode, "FinalValue");
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (xmlNode.Name == "HitPoints")
                            {
                                try
                                {
                                    creature.HP = XMLHelper.GetIntAttribute(xmlNode, "FinalValue");
                                    continue;
                                }
                                catch
                                {
                                    LogSystem.Trace("Error parsing " + xmlNode.Name);
                                    continue;
                                }
                            }
                            if (!(xmlNode.Name == "ActionPoints") && !(xmlNode.Name == "Experience") && !(xmlNode.Name == "Auras"))
                            {
                                if (xmlNode.Name == "LandSpeed")
                                {
                                    try
                                    {
                                        XmlNode node = XMLHelper.FindChild(xmlNode, "Speed");
                                        creature.Movement = XMLHelper.GetIntAttribute(node, "FinalValue").ToString();
                                        string text11 = "";
                                        XmlNode xmlNode23 = XMLHelper.FindChild(xmlNode, "Details");
                                        if (xmlNode23 != null)
                                        {
                                            text11 = xmlNode23.InnerText;
                                        }
                                        if (text11 != "")
                                        {
                                            Creature expr_EEB = creature;
                                            expr_EEB.Movement = expr_EEB.Movement + " " + text11;
                                        }
                                        continue;
                                    }
                                    catch
                                    {
                                        LogSystem.Trace("Error parsing " + xmlNode.Name);
                                        continue;
                                    }
                                }
                                if (xmlNode.Name == "Speeds")
                                {
                                    try
                                    {
                                        foreach (XmlNode parent6 in xmlNode.ChildNodes)
                                        {
                                            XmlNode xmlNode24 = XMLHelper.FindChild(parent6, "ReferencedObject");
                                            XmlNode node2 = XMLHelper.FindChild(parent6, "Speed");
                                            XmlNode xmlNode25 = XMLHelper.FindChild(parent6, "Details");
                                            string innerText10 = xmlNode24.FirstChild.NextSibling.InnerText;
                                            int intAttribute3 = XMLHelper.GetIntAttribute(node2, "FinalValue");
                                            string text12 = (xmlNode25 != null) ? xmlNode25.InnerText : "";
                                            if (creature.Movement != "")
                                            {
                                                Creature expr_FCC = creature;
                                                expr_FCC.Movement += ", ";
                                            }
                                            string text13 = "";
                                            if (innerText10 != "")
                                            {
                                                text13 += innerText10;
                                            }
                                            if (text12 != "")
                                            {
                                                if (text13 != "")
                                                {
                                                    text13 += " ";
                                                }
                                                text13 += text12;
                                            }
                                            Creature expr_1037 = creature;
                                            object obj = expr_1037.Movement;
                                            expr_1037.Movement = string.Concat(new object[]
                                            {
                                                obj,
                                                text13,
                                                " ",
                                                intAttribute3
                                            });
                                        }
                                        continue;
                                    }
                                    catch
                                    {
                                        LogSystem.Trace("Error parsing " + xmlNode.Name);
                                        continue;
                                    }
                                }
                                if (!(xmlNode.Name == "SavingThrows"))
                                {
                                    if (xmlNode.Name == "Resistances")
                                    {
                                        try
                                        {
                                            string text14 = "";
                                            foreach (XmlNode parent7 in xmlNode.ChildNodes)
                                            {
                                                XmlNode xmlNode26 = XMLHelper.FindChild(XMLHelper.FindChild(parent7, "ReferencedObject"), "Name");
                                                XmlNode node3 = XMLHelper.FindChild(parent7, "Amount");
                                                XmlNode xmlNode27 = XMLHelper.FindChild(parent7, "Details");
                                                string innerText11 = xmlNode26.InnerText;
                                                int intAttribute4 = XMLHelper.GetIntAttribute(node3, "FinalValue");
                                                string text15 = (xmlNode27 != null) ? xmlNode27.InnerText : "";
                                                if (text15 == "")
                                                {
                                                    DamageModifier damageModifier = DamageModifier.Parse(innerText11, -intAttribute4);
                                                    if (damageModifier != null)
                                                    {
                                                        creature.DamageModifiers.Add(damageModifier);
                                                        continue;
                                                    }
                                                }
                                                if (!(innerText11 == "") || !(text15 == ""))
                                                {
                                                    if (text14 != "")
                                                    {
                                                        text14 += ", ";
                                                    }
                                                    string text16 = "";
                                                    if (innerText11 != "0")
                                                    {
                                                        text16 += innerText11;
                                                    }
                                                    if (intAttribute4 > 0)
                                                    {
                                                        object obj = text14;
                                                        text14 = string.Concat(new object[]
                                                        {
                                                            obj,
                                                            text16,
                                                            " ",
                                                            intAttribute4
                                                        });
                                                    }
                                                    else
                                                    {
                                                        text14 += text16;
                                                    }
                                                    if (text15 != "")
                                                    {
                                                        if (text14 != "")
                                                        {
                                                            text14 += " ";
                                                        }
                                                        text14 += text15;
                                                    }
                                                }
                                            }
                                            creature.Resist = text14;
                                            continue;
                                        }
                                        catch
                                        {
                                            LogSystem.Trace("Error parsing " + xmlNode.Name);
                                            continue;
                                        }
                                    }
                                    if (xmlNode.Name == "Weaknesses")
                                    {
                                        try
                                        {
                                            string text17 = "";
                                            foreach (XmlNode parent8 in xmlNode.ChildNodes)
                                            {
                                                XmlNode xmlNode28 = XMLHelper.FindChild(XMLHelper.FindChild(parent8, "ReferencedObject"), "Name");
                                                XmlNode node4 = XMLHelper.FindChild(parent8, "Amount");
                                                XmlNode xmlNode29 = XMLHelper.FindChild(parent8, "Details");
                                                string innerText12 = xmlNode28.InnerText;
                                                int intAttribute5 = XMLHelper.GetIntAttribute(node4, "FinalValue");
                                                string text18 = (xmlNode29 != null) ? xmlNode29.InnerText : "";
                                                if (text18 == "")
                                                {
                                                    DamageModifier damageModifier2 = DamageModifier.Parse(innerText12, intAttribute5);
                                                    if (damageModifier2 != null)
                                                    {
                                                        creature.DamageModifiers.Add(damageModifier2);
                                                        continue;
                                                    }
                                                }
                                                if (!(innerText12 == "") || !(text18 == ""))
                                                {
                                                    if (text17 != "")
                                                    {
                                                        text17 += ", ";
                                                    }
                                                    string text19 = "";
                                                    if (innerText12 != "0")
                                                    {
                                                        text19 += innerText12;
                                                    }
                                                    if (intAttribute5 > 0)
                                                    {
                                                        object obj = text17;
                                                        text17 = string.Concat(new object[]
                                                        {
                                                            obj,
                                                            text19,
                                                            " ",
                                                            intAttribute5
                                                        });
                                                    }
                                                    else
                                                    {
                                                        text17 += text19;
                                                    }
                                                    if (text18 != "")
                                                    {
                                                        if (text17 != "")
                                                        {
                                                            text17 += " ";
                                                        }
                                                        text17 += text18;
                                                    }
                                                }
                                            }
                                            creature.Vulnerable = text17;
                                            continue;
                                        }
                                        catch
                                        {
                                            LogSystem.Trace("Error parsing " + xmlNode.Name);
                                            continue;
                                        }
                                    }
                                    if (xmlNode.Name == "Immunities")
                                    {
                                        try
                                        {
                                            string text20 = "";
                                            foreach (XmlNode parent9 in xmlNode.ChildNodes)
                                            {
                                                XmlNode xmlNode30 = XMLHelper.FindChild(XMLHelper.FindChild(parent9, "ReferencedObject"), "Name");
                                                DamageModifier damageModifier3 = DamageModifier.Parse(xmlNode30.InnerText, 0);
                                                if (damageModifier3 != null)
                                                {
                                                    creature.DamageModifiers.Add(damageModifier3);
                                                }
                                                else
                                                {
                                                    if (text20 != "")
                                                    {
                                                        text20 += ", ";
                                                    }
                                                    text20 += xmlNode30.InnerText;
                                                }
                                            }
                                            creature.Immune = text20;
                                            continue;
                                        }
                                        catch
                                        {
                                            LogSystem.Trace("Error parsing " + xmlNode.Name);
                                            continue;
                                        }
                                    }
                                    if (xmlNode.Name == "Tactics")
                                    {
                                        try
                                        {
                                            creature.Tactics = xmlNode.InnerText;
                                            continue;
                                        }
                                        catch
                                        {
                                            LogSystem.Trace("Error parsing " + xmlNode.Name);
                                            continue;
                                        }
                                    }
                                    if (!(xmlNode.Name == "SourceBook") && !(xmlNode.Name == "Description") && !(xmlNode.Name == "Race") && !(xmlNode.Name == "TemplateApplications") && !(xmlNode.Name == "EliteUpgradeID") && !(xmlNode.Name == "FullPortrait") && !(xmlNode.Name == "CompendiumUrl") && !(xmlNode.Name == "Phasing") && !(xmlNode.Name == "SourceBooks"))
                                    {
                                        LogSystem.Trace("Unhandled XML node: " + xmlNode.Name);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return creature;
        }

        private static void import_power(XmlNode power_node, Creature c)
        {
            try
            {
                CreaturePower creaturePower = new CreaturePower();
                creaturePower.Name = XMLHelper.NodeText(power_node, "Name");
                string text = XMLHelper.NodeText(power_node, "Requirements");
                if (text != "")
                {
                    creaturePower.Condition = text;
                }
                string a = XMLHelper.NodeText(power_node, "Type");
                if (a != "Trait")
                {
                    string text2 = XMLHelper.NodeText(power_node, "Action");
                    string a2 = XMLHelper.NodeText(power_node, "IsBasic");
                    string text3 = XMLHelper.NodeText(power_node, "Usage");
                    creaturePower.Action = new PowerAction();
                    if (a2 == "true")
                    {
                        creaturePower.Action.Use = PowerUseType.Basic;
                    }
                    else if (text3.StartsWith("At-Will"))
                    {
                        creaturePower.Action.Use = PowerUseType.AtWill;
                    }
                    else
                    {
                        creaturePower.Action.Use = PowerUseType.Encounter;
                        if (!text3.StartsWith("Encounter") && text3.ToLower().StartsWith("recharge"))
                        {
                            string text4 = XMLHelper.NodeText(power_node, "UsageDetails");
                            if (text4 == "")
                            {
                                text4 = "Recharges on 6";
                            }
                            if (text4 == "2")
                            {
                                text4 = "Recharges on 2-6";
                            }
                            if (text4 == "3")
                            {
                                text4 = "Recharges on 3-6";
                            }
                            if (text4 == "4")
                            {
                                text4 = "Recharges on 4-6";
                            }
                            if (text4 == "5")
                            {
                                text4 = "Recharges on 5-6";
                            }
                            if (text4 == "6")
                            {
                                text4 = "Recharges on 6";
                            }
                            creaturePower.Action.Recharge = text4;
                        }
                    }
                    if (text2.ToLower().StartsWith("standard"))
                    {
                        creaturePower.Action.Action = ActionType.Standard;
                    }
                    if (text2.ToLower().StartsWith("move"))
                    {
                        creaturePower.Action.Action = ActionType.Move;
                    }
                    if (text2.ToLower().StartsWith("minor"))
                    {
                        creaturePower.Action.Action = ActionType.Minor;
                    }
                    if (text2.ToLower().StartsWith("immediate interrupt"))
                    {
                        creaturePower.Action.Action = ActionType.Interrupt;
                    }
                    if (text2.ToLower().StartsWith("immediate reaction"))
                    {
                        creaturePower.Action.Action = ActionType.Reaction;
                    }
                    if (text2.ToLower().StartsWith("opportunity"))
                    {
                        creaturePower.Action.Action = ActionType.Opportunity;
                    }
                    if (text2.ToLower().StartsWith("free"))
                    {
                        creaturePower.Action.Action = ActionType.Free;
                    }
                    if (text2.ToLower().StartsWith("none"))
                    {
                        creaturePower.Action.Action = ActionType.None;
                    }
                    if (text2.ToLower().StartsWith("no action"))
                    {
                        creaturePower.Action.Action = ActionType.None;
                    }
                    if (text2 == "")
                    {
                        creaturePower.Action.Action = ActionType.None;
                    }
                }
                else
                {
                    XmlNode xmlNode = XMLHelper.FindChild(power_node, "Range");
                    if (xmlNode != null)
                    {
                        int intAttribute = XMLHelper.GetIntAttribute(xmlNode, "FinalValue");
                        string text5 = XMLHelper.NodeText(power_node, "Details");
                        if (intAttribute != 0)
                        {
                            Aura aura = new Aura();
                            aura.Name = creaturePower.Name;
                            aura.Details = intAttribute + " " + text5;
                            c.Auras.Add(aura);
                            return;
                        }
                        creaturePower.Action = null;
                        creaturePower.Details = text5;
                    }
                }
                if (creaturePower.Action != null)
                {
                    creaturePower.Action.Trigger = XMLHelper.NodeText(power_node, "Trigger");
                }
                if (creaturePower.Action != null && creaturePower.Action.Trigger != "")
                {
                    string text6 = creaturePower.Action.Trigger.Trim();
                    if (text6.StartsWith(", "))
                    {
                        text6 = text6.Substring(2);
                    }
                    if (text6.StartsWith("; "))
                    {
                        text6 = text6.Substring(2);
                    }
                    if (text6.StartsWith("("))
                    {
                        text6 = text6.Substring(1);
                    }
                    if (text6.EndsWith(")"))
                    {
                        text6 = text6.Substring(0, text6.Length - 1);
                    }
                    creaturePower.Action.Trigger = text6;
                }
                XmlNode xmlNode2 = XMLHelper.FindChild(power_node, "Keywords");
                if (xmlNode2 != null)
                {
                    foreach (XmlNode parent in xmlNode2.ChildNodes)
                    {
                        XmlNode parent2 = XMLHelper.FindChild(parent, "ReferencedObject");
                        string text7 = XMLHelper.NodeText(parent2, "Name");
                        if (text7 != "")
                        {
                            if (creaturePower.Keywords != "")
                            {
                                CreaturePower expr_477 = creaturePower;
                                expr_477.Keywords += ", ";
                            }
                            CreaturePower expr_48D = creaturePower;
                            expr_48D.Keywords += text7;
                        }
                    }
                }
                XmlNode xmlNode3 = XMLHelper.FindChild(power_node, "Attacks");
                if (xmlNode3 != null)
                {
                    string text8 = "";
                    string text9 = "";
                    string text10 = "";
                    string text11 = "";
                    foreach (XmlNode xmlNode4 in xmlNode3.ChildNodes)
                    {
                        XmlNode xmlNode5 = XMLHelper.FindChild(xmlNode4, "AttackBonuses");
                        bool flag = xmlNode5 != null && xmlNode5.ChildNodes.Count != 0;
                        foreach (XmlNode xmlNode6 in xmlNode4.ChildNodes)
                        {
                            if (!(xmlNode6.Name == "Name"))
                            {
                                if (xmlNode6.Name == "Range")
                                {
                                    text8 = xmlNode6.InnerText.ToLower();
                                    text8 = text8.Replace("basic ", "");
                                }
                                else if (xmlNode6.Name == "Targets")
                                {
                                    text9 = xmlNode6.InnerText;
                                }
                                else if (xmlNode6.Name == "AttackBonuses")
                                {
                                    if (xmlNode6.FirstChild != null)
                                    {
                                        int intAttribute2 = XMLHelper.GetIntAttribute(xmlNode6.FirstChild, "FinalValue");
                                        XmlNode parent3 = XMLHelper.FindChild(xmlNode6.FirstChild, "Defense");
                                        XmlNode xmlNode7 = XMLHelper.FindChild(XMLHelper.FindChild(parent3, "ReferencedObject"), "DefenseName");
                                        string innerText = xmlNode7.InnerText;
                                        creaturePower.Attack = new PowerAttack();
                                        creaturePower.Attack.Bonus = intAttribute2;
                                        creaturePower.Attack.Defence = (DefenceType)Enum.Parse(typeof(DefenceType), innerText);
                                    }
                                }
                                else if (xmlNode6.Name == "Description")
                                {
                                    creaturePower.Description = xmlNode6.InnerText;
                                }
                                else if (xmlNode6.Name == "Damage")
                                {
                                    text10 = Statistics.NormalDamage(c.Level);
                                }
                                else
                                {
                                    string text12 = XMLHelper.NodeText(xmlNode6, "Name");
                                    if (text12 == "")
                                    {
                                        text12 = "Hit";
                                    }
                                    if (flag || (!(text12 == "Hit") && !(text12 == "Miss")))
                                    {
                                        XmlNode xmlNode8 = XMLHelper.FindChild(xmlNode6, "Damage");
                                        if (xmlNode8 != null)
                                        {
                                            text10 = XMLHelper.NodeText(xmlNode8, "Expression");
                                        }
                                        string text13 = XMLHelper.NodeText(xmlNode6, "Description");
                                        if (text10 != "" && text13 == "")
                                        {
                                            text13 = "damage";
                                        }
                                        if (!(text13 == ""))
                                        {
                                            string text14 = string.Concat(new string[]
                                            {
                                                text12,
                                                ": ",
                                                text10,
                                                " ",
                                                text13
                                            });
                                            string text15 = XMLHelper.NodeText(xmlNode6, "Special");
                                            if (text15 != "")
                                            {
                                                text14 = text14 + Environment.NewLine + "Special: " + text15;
                                            }
                                            XmlNode xmlNode9 = XMLHelper.FindChild(xmlNode6, "Attacks");
                                            if (xmlNode9 != null)
                                            {
                                                foreach (XmlNode attack_node in xmlNode9.ChildNodes)
                                                {
                                                    string text16 = AppImport.secondary_attack(attack_node);
                                                    if (text16 != "")
                                                    {
                                                        text14 += Environment.NewLine;
                                                        text14 += text16;
                                                    }
                                                }
                                            }
                                            if (text11 != "")
                                            {
                                                text11 += "\n";
                                            }
                                            text11 += text14;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    string text17 = text8;
                    if (text9 != "")
                    {
                        text17 = text17 + " (" + text9 + ")";
                    }
                    creaturePower.Range = text17;
                    creaturePower.Details = text11;
                }
                else
                {
                    creaturePower.Details = XMLHelper.NodeText(power_node, "Details");
                }
                c.CreaturePowers.Add(creaturePower);
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
        }

        private static string secondary_attack(XmlNode attack_node)
        {
            string str = XMLHelper.NodeText(attack_node, "Name");
            string str2 = XMLHelper.NodeText(attack_node, "Description");
            string text = str + ": " + str2;
            string text2 = "";
            string text3 = "";
            string text4 = "";
            foreach (XmlNode xmlNode in attack_node.ChildNodes)
            {
                string name;
                if ((name = xmlNode.Name) != null)
                {
                    if (!(name == "Hit"))
                    {
                        if (!(name == "Miss"))
                        {
                            if (name == "Effect")
                            {
                                text4 = AppImport.secondary_attack_details(xmlNode);
                            }
                        }
                        else
                        {
                            text3 = AppImport.secondary_attack_details(xmlNode);
                        }
                    }
                    else
                    {
                        text2 = AppImport.secondary_attack_details(xmlNode);
                    }
                }
            }
            if (text2 != "")
            {
                text = text + Environment.NewLine + "Hit: " + text2;
            }
            if (text3 != "")
            {
                text = text + Environment.NewLine + "Miss: " + text3;
            }
            if (text4 != "")
            {
                text = text + Environment.NewLine + "Effect: " + text4;
            }
            return text;
        }

        private static string secondary_attack_details(XmlNode details_node)
        {
            XmlNode parent = XMLHelper.FindChild(details_node, "Damage");
            string text = XMLHelper.NodeText(parent, "Expression");
            string text2 = XMLHelper.NodeText(details_node, "Description");
            if (text != "" && text2 != "")
            {
                return text + " " + text2;
            }
            if (text != "" && text2 == "")
            {
                return text + " damage";
            }
            if (text == "" && text2 != "")
            {
                return text2;
            }
            return "";
        }

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
                        XmlNode xmlNode4 = AppImport.get_stat_node(xmlNode3, "Hit Points");
                        if (xmlNode4 != null)
                        {
                            hero.HP = XMLHelper.GetIntAttribute(xmlNode4, "value");
                        }
                        XmlNode xmlNode5 = AppImport.get_stat_node(xmlNode3, "AC");
                        if (xmlNode5 != null)
                        {
                            hero.AC = XMLHelper.GetIntAttribute(xmlNode5, "value");
                        }
                        XmlNode xmlNode6 = AppImport.get_stat_node(xmlNode3, "Fortitude Defense");
                        if (xmlNode6 != null)
                        {
                            hero.Fortitude = XMLHelper.GetIntAttribute(xmlNode6, "value");
                        }
                        XmlNode xmlNode7 = AppImport.get_stat_node(xmlNode3, "Reflex Defense");
                        if (xmlNode7 != null)
                        {
                            hero.Reflex = XMLHelper.GetIntAttribute(xmlNode7, "value");
                        }
                        XmlNode xmlNode8 = AppImport.get_stat_node(xmlNode3, "Will Defense");
                        if (xmlNode8 != null)
                        {
                            hero.Will = XMLHelper.GetIntAttribute(xmlNode8, "value");
                        }
                        XmlNode xmlNode9 = AppImport.get_stat_node(xmlNode3, "Initiative");
                        if (xmlNode9 != null)
                        {
                            hero.InitBonus = XMLHelper.GetIntAttribute(xmlNode9, "value");
                        }
                        XmlNode xmlNode10 = AppImport.get_stat_node(xmlNode3, "Passive Perception");
                        if (xmlNode10 != null)
                        {
                            hero.PassivePerception = XMLHelper.GetIntAttribute(xmlNode10, "value");
                        }
                        XmlNode xmlNode11 = AppImport.get_stat_node(xmlNode3, "Passive Insight");
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

        [Obsolete]
        public static bool ImportIPlay4e(Hero hero)
        {
            return IPlay4E.ImportIPlay4e(hero);
        }

        [Obsolete]
        public static List<Hero> ImportParty(string key)
        {
            return IPlay4E.ImportParty(key);
        }

    }
}
