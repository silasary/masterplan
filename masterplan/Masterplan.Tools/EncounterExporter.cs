using Masterplan.Data;
using System;
using System.Xml;
using Utils;

namespace Masterplan.Tools
{
	internal class EncounterExporter
	{
		public static string ExportXML(Encounter enc)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.CreateElement("Encounter"));
			XMLHelper.CreateChild(xmlDocument, xmlDocument.DocumentElement, "Source").InnerText = "Masterplan Adventure Design Studio";
			XmlNode parent = XMLHelper.CreateChild(xmlDocument, xmlDocument.DocumentElement, "Creatures");
			foreach (EncounterSlot current in enc.Slots)
			{
				ICreature creature = Session.FindCreature(current.Card.CreatureID, SearchType.Global);
				foreach (CombatData current2 in current.CombatData)
				{
					XmlNode parent2 = XMLHelper.CreateChild(xmlDocument, parent, "Creature");
					string text = "";
					if (creature.Role is Minion)
					{
						text += "Minion";
					}
					foreach (RoleType current3 in current.Card.Roles)
					{
						if (text != "")
						{
							text += ", ";
						}
						text += current3;
					}
					if (current.Card.Leader)
					{
						text += " (L)";
					}
					XMLHelper.CreateChild(xmlDocument, parent2, "Name").InnerText = current2.DisplayName;
					XMLHelper.CreateChild(xmlDocument, parent2, "Level").InnerText = current.Card.Level.ToString();
					XMLHelper.CreateChild(xmlDocument, parent2, "Role").InnerText = text;
					XMLHelper.CreateChild(xmlDocument, parent2, "Size").InnerText = creature.Size.ToString();
					XMLHelper.CreateChild(xmlDocument, parent2, "Type").InnerText = creature.Type.ToString();
					XMLHelper.CreateChild(xmlDocument, parent2, "Origin").InnerText = creature.Origin.ToString();
					XMLHelper.CreateChild(xmlDocument, parent2, "Keywords").InnerText = creature.Keywords;
					XMLHelper.CreateChild(xmlDocument, parent2, "Size").InnerText = creature.Size.ToString();
					XMLHelper.CreateChild(xmlDocument, parent2, "HP").InnerText = current.Card.HP.ToString();
					XMLHelper.CreateChild(xmlDocument, parent2, "InitBonus").InnerText = current.Card.Initiative.ToString();
					XMLHelper.CreateChild(xmlDocument, parent2, "Speed").InnerText = current.Card.Movement;
					XmlNode parent3 = XMLHelper.CreateChild(xmlDocument, parent2, "Defenses");
					XMLHelper.CreateChild(xmlDocument, parent3, "AC").InnerText = current.Card.AC.ToString();
					XMLHelper.CreateChild(xmlDocument, parent3, "Fortitude").InnerText = current.Card.Fortitude.ToString();
					XMLHelper.CreateChild(xmlDocument, parent3, "Reflex").InnerText = current.Card.Reflex.ToString();
					XMLHelper.CreateChild(xmlDocument, parent3, "Will").InnerText = current.Card.Will.ToString();
					if (current.Card.Regeneration != null)
					{
						XmlNode parent4 = XMLHelper.CreateChild(xmlDocument, parent2, "Regeneration");
						XMLHelper.CreateChild(xmlDocument, parent4, "Value").InnerText = current.Card.Regeneration.Value.ToString();
						XMLHelper.CreateChild(xmlDocument, parent4, "Details").InnerText = current.Card.Regeneration.Details;
					}
					XmlNode parent5 = XMLHelper.CreateChild(xmlDocument, parent2, "Damage");
					foreach (DamageModifier current4 in current.Card.DamageModifiers)
					{
						if (current4.Value < 0)
						{
							XmlNode parent6 = XMLHelper.CreateChild(xmlDocument, parent5, "Resist");
							XMLHelper.CreateChild(xmlDocument, parent6, "Type").InnerText = current4.Type.ToString();
							XMLHelper.CreateChild(xmlDocument, parent6, "Details").InnerText = Math.Abs(current4.Value).ToString();
						}
						else if (current4.Value > 0)
						{
							XmlNode parent7 = XMLHelper.CreateChild(xmlDocument, parent5, "Vulnerable");
							XMLHelper.CreateChild(xmlDocument, parent7, "Type").InnerText = current4.Type.ToString();
							XMLHelper.CreateChild(xmlDocument, parent7, "Details").InnerText = Math.Abs(current4.Value).ToString();
						}
						else
						{
							XmlNode parent8 = XMLHelper.CreateChild(xmlDocument, parent5, "Immune");
							XMLHelper.CreateChild(xmlDocument, parent8, "Type").InnerText = current4.Type.ToString();
						}
					}
					if (current.Card.Resist != "")
					{
						XMLHelper.CreateChild(xmlDocument, parent5, "Resist").InnerText = current.Card.Resist;
					}
					if (current.Card.Vulnerable != "")
					{
						XMLHelper.CreateChild(xmlDocument, parent5, "Vulnerable").InnerText = current.Card.Vulnerable;
					}
					if (current.Card.Immune != "")
					{
						XMLHelper.CreateChild(xmlDocument, parent5, "Immune").InnerText = current.Card.Immune;
					}
					XmlNode parent9 = XMLHelper.CreateChild(xmlDocument, parent2, "AbilityModifiers");
					XMLHelper.CreateChild(xmlDocument, parent9, "Strength").InnerText = creature.Strength.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, parent9, "Constitution").InnerText = creature.Constitution.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, parent9, "Dexterity").InnerText = creature.Dexterity.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, parent9, "Intelligence").InnerText = creature.Intelligence.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, parent9, "Wisdom").InnerText = creature.Wisdom.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, parent9, "Charisma").InnerText = creature.Charisma.Modifier.ToString();
					XMLHelper.CreateChild(xmlDocument, parent2, "Senses").InnerText = current.Card.Senses;
					XMLHelper.CreateChild(xmlDocument, parent2, "Skills").InnerText = current.Card.Skills;
					XMLHelper.CreateChild(xmlDocument, parent2, "Equipment").InnerText = current.Card.Equipment;
					XMLHelper.CreateChild(xmlDocument, parent2, "Tactics").InnerText = current.Card.Tactics;
				}
			}
			XmlNode parent10 = XMLHelper.CreateChild(xmlDocument, xmlDocument.DocumentElement, "PCs");
			foreach (Hero current5 in Session.Project.Heroes)
			{
				XmlNode parent11 = XMLHelper.CreateChild(xmlDocument, parent10, "PC");
				XMLHelper.CreateChild(xmlDocument, parent11, "Name").InnerText = current5.Name;
				XMLHelper.CreateChild(xmlDocument, parent11, "Description").InnerText = current5.Info;
				XMLHelper.CreateChild(xmlDocument, parent11, "Size").InnerText = current5.Size.ToString();
				XMLHelper.CreateChild(xmlDocument, parent11, "HP").InnerText = current5.HP.ToString();
				XMLHelper.CreateChild(xmlDocument, parent11, "InitBonus").InnerText = current5.InitBonus.ToString();
				XmlNode parent12 = XMLHelper.CreateChild(xmlDocument, parent11, "Defenses");
				XMLHelper.CreateChild(xmlDocument, parent12, "AC").InnerText = current5.AC.ToString();
				XMLHelper.CreateChild(xmlDocument, parent12, "Fortitude").InnerText = current5.Fortitude.ToString();
				XMLHelper.CreateChild(xmlDocument, parent12, "Reflex").InnerText = current5.Reflex.ToString();
				XMLHelper.CreateChild(xmlDocument, parent12, "Will").InnerText = current5.Will.ToString();
				if (current5.Key != "")
				{
					XMLHelper.CreateChild(xmlDocument, parent11, "Key").InnerText = current5.Key;
				}
			}
			return xmlDocument.OuterXml;
		}
	}
}
