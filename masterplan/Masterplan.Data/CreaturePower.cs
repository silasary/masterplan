using Masterplan.Properties;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Masterplan.Data
{
	[Serializable]
	public class CreaturePower : IComparable<CreaturePower>
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private PowerAction fAction;

		private string fKeywords = "";

		private string fCondition = "";

		private string fRange = "";

		private PowerAttack fAttack;

		private string fDescription = "";

		private string fDetails = "";

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public PowerAction Action
		{
			get
			{
				return this.fAction;
			}
			set
			{
				this.fAction = value;
			}
		}

		public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
			}
		}

		public string Condition
		{
			get
			{
				return this.fCondition;
			}
			set
			{
				this.fCondition = value;
			}
		}

		public string Range
		{
			get
			{
				return this.fRange;
			}
			set
			{
				this.fRange = value;
			}
		}

		public PowerAttack Attack
		{
			get
			{
				return this.fAttack;
			}
			set
			{
				this.fAttack = value;
			}
		}

		public string Description
		{
			get
			{
				return this.fDescription;
			}
			set
			{
				this.fDescription = value;
			}
		}

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
			}
		}

		public string Damage
		{
			get
			{
				return AI.ExtractDamage(this.fDetails);
			}
		}

		public CreaturePowerCategory Category
		{
			get
			{
				if (this.fAction == null)
				{
					return CreaturePowerCategory.Trait;
				}
				if (this.fAction.Trigger != null && this.fAction.Trigger != "")
				{
					return CreaturePowerCategory.Triggered;
				}
				switch (this.fAction.Action)
				{
				case ActionType.Standard:
					return CreaturePowerCategory.Standard;
				case ActionType.Move:
					return CreaturePowerCategory.Move;
				case ActionType.Minor:
					return CreaturePowerCategory.Minor;
				case ActionType.Reaction:
				case ActionType.Interrupt:
				case ActionType.Opportunity:
					return CreaturePowerCategory.Triggered;
				case ActionType.Free:
					return CreaturePowerCategory.Free;
				default:
					return CreaturePowerCategory.Other;
				}
			}
		}

		public CreaturePower Copy()
		{
			return new CreaturePower
			{
				ID = this.fID,
				Name = this.fName,
				Action = ((this.fAction != null) ? this.fAction.Copy() : null),
				Keywords = this.fKeywords,
				Condition = this.fCondition,
				Range = this.fRange,
				Attack = ((this.fAttack != null) ? this.fAttack.Copy() : null),
				Description = this.fDescription,
				Details = this.fDetails
			};
		}

		public int CompareTo(CreaturePower rhs)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.fAction != null && this.fAction.Use == PowerUseType.Basic)
			{
				flag = true;
			}
			if (rhs.Action != null && rhs.Action.Use == PowerUseType.Basic)
			{
				flag2 = true;
			}
			if (flag != flag2)
			{
				if (flag)
				{
					return -1;
				}
				if (flag2)
				{
					return 1;
				}
			}
			if (flag && flag2)
			{
				bool flag3 = this.fRange.ToLower().Contains("melee");
				bool flag4 = rhs.Range.ToLower().Contains("melee");
				if (flag3 != flag4)
				{
					if (flag3)
					{
						return -1;
					}
					if (flag4)
					{
						return 1;
					}
				}
			}
			if (!flag && !flag2)
			{
				bool flag5 = this.fRange.ToLower().Contains("double");
				bool flag6 = rhs.Range.ToLower().Contains("double");
				if (flag5 != flag6)
				{
					if (flag5)
					{
						return -1;
					}
					if (flag6)
					{
						return 1;
					}
				}
			}
			return this.fName.CompareTo(rhs.Name);
		}

		public override string ToString()
		{
			return this.fName;
		}

		public List<string> AsHTML(CombatData cd, CardMode mode, bool functional_template)
		{
			bool flag = mode == CardMode.Combat && cd != null && cd.UsedPowers.Contains(this.fID);
			string str = "Actions";
			switch (this.Category)
			{
			case CreaturePowerCategory.Trait:
				str = "Traits";
				break;
			case CreaturePowerCategory.Standard:
				str = "Standard Actions";
				break;
			case CreaturePowerCategory.Move:
				str = "Move Actions";
				break;
			case CreaturePowerCategory.Minor:
				str = "Minor Actions";
				break;
			case CreaturePowerCategory.Free:
				str = "Free Actions";
				break;
			case CreaturePowerCategory.Triggered:
				str = "Triggered Actions";
				break;
			case CreaturePowerCategory.Other:
				str = "Other Actions";
				break;
			}
			List<string> list = new List<string>();
			if (mode == CardMode.Build)
			{
				list.Add("<TR class=creature>");
				list.Add("<TD colspan=3>");
				list.Add("<A href=power:action style=\"color:white\"><B>" + str + "</B> (click here to change the action)</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (!flag)
			{
				list.Add("<TR class=shaded>");
			}
			else
			{
				list.Add("<TR class=shaded_dimmed>");
			}
			list.Add("<TD colspan=3>");
			list.Add(this.power_topline(cd, mode));
			list.Add("</TD>");
			list.Add("</TR>");
			if (!flag)
			{
				list.Add("<TR>");
			}
			else
			{
				list.Add("<TR class=dimmed>");
			}
			list.Add("<TD colspan=3>");
			list.Add(this.power_content(mode));
			list.Add("</TD>");
			list.Add("</TR>");
			if (mode == CardMode.Combat)
			{
				if (flag)
				{
					list.Add("<TR>");
					list.Add("<TD class=indent colspan=3>");
					list.Add(string.Concat(new object[]
					{
						"<A href=\"refresh:",
						cd.ID,
						";",
						this.fID,
						"\">(recharge this power)</A>"
					}));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				else if (this.fAction != null && (this.fAction.Use == PowerUseType.Encounter || this.fAction.Use == PowerUseType.Daily))
				{
					list.Add("<TR>");
					list.Add("<TD class=indent colspan=3>");
					list.Add(string.Concat(new object[]
					{
						"<A href=\"refresh:",
						cd.ID,
						";",
						this.fID,
						"\">(use this power)</A>"
					}));
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			if (functional_template)
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Note</B>: This power is part of a functional template, and so its attack bonus will be increased by the level of the creature it is applied to.");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			return list;
		}

		private string power_topline(CombatData cd, CardMode mode)
		{
			string text = "";
			Image image = null;
			string text2 = this.fRange.ToLower();
			if (text2.Contains("melee"))
			{
				if (this.fAction != null && this.fAction.Use == PowerUseType.Basic)
				{
					image = Resources.MeleeBasic;
				}
				else
				{
					image = Resources.Melee;
				}
			}
			if (text2.Contains("ranged"))
			{
				if (this.fAction != null && this.fAction.Use == PowerUseType.Basic)
				{
					image = Resources.RangedBasic;
				}
				else
				{
					image = Resources.Ranged;
				}
			}
			if (text2.Contains("area"))
			{
				image = Resources.Area;
			}
			if (text2.Contains("close"))
			{
				image = Resources.Close;
			}
			if (image == null && this.fAttack != null && this.fAction != null)
			{
				if (this.fAction.Use == PowerUseType.Basic)
				{
					image = Resources.MeleeBasic;
				}
				else
				{
					image = Resources.Melee;
				}
			}
			text = text + "<B>" + HTML.Process(this.fName, true) + "</B>";
			if (mode == CardMode.Combat && cd != null)
			{
				bool flag = false;
				if (!cd.UsedPowers.Contains(this.fID))
				{
					if (this.fAttack != null)
					{
						flag = true;
					}
					if (this.fAction != null && this.fAction.Use == PowerUseType.Encounter)
					{
						flag = true;
					}
				}
				if (flag)
				{
					text = string.Concat(new object[]
					{
						"<A href=\"power:",
						cd.ID,
						";",
						this.fID,
						"\">",
						text,
						"</A>"
					});
				}
			}
			if (mode == CardMode.Build)
			{
				text = "<A href=power:info>" + text + "</A>";
			}
			if (image != null)
			{
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Png);
				byte[] inArray = memoryStream.ToArray();
				string text3 = Convert.ToBase64String(inArray);
				if (text3 != null && text3 != "")
				{
					text = "<img src=data:image/png;base64," + text3 + ">" + text;
				}
			}
			if (this.fKeywords != "")
			{
				string text4 = HTML.Process(this.fKeywords, true);
				if (mode == CardMode.Build)
				{
					text4 = "<A href=power:info>" + text4 + "</A>";
				}
				text = text + " (" + text4 + ")";
			}
			string text5 = this.power_parenthesis(mode);
			if (text5 != "")
			{
				text = text + " &diams; " + text5;
			}
			return text;
		}

		private string power_parenthesis(CardMode mode)
		{
			if (this.fCondition == "" && this.fAction == null)
			{
				return "";
			}
			string text = "";
			if (this.fAction != null)
			{
				string str = this.fAction.ToString();
				if (mode == CardMode.Build)
				{
					str = "<A href=power:action>" + str + "</A>";
				}
				text += str;
			}
			return text;
		}

		private string power_content(CardMode mode)
		{
			List<string> list = new List<string>();
			string text = "";
			if (this.fDescription != null)
			{
				text = HTML.Process(this.fDescription, true);
			}
			if (text == null)
			{
				text = "";
			}
			if (mode == CardMode.Build)
			{
				if (text == "")
				{
					text = "Set read-aloud description (optional)";
				}
				text = "<A href=power:desc>" + text + "</A>";
			}
			if (text != "")
			{
				list.Add("<I>" + text + "</I>");
			}
			if (mode == CardMode.Build)
			{
				list.Add("");
			}
			if (this.fAction != null && this.fAction.Trigger != "")
			{
				ActionType action = this.fAction.Action;
				string text2;
				if (action != ActionType.None)
				{
					switch (action)
					{
					case ActionType.Reaction:
						text2 = "immediate reaction";
						break;
					case ActionType.Interrupt:
						text2 = "immediate interrupt";
						break;
					default:
						text2 = this.fAction.Action.ToString().ToLower() + " action";
						break;
					}
				}
				else
				{
					text2 = "no action";
				}
				if (mode != CardMode.Build)
				{
					list.Add("Trigger (" + text2 + "): " + this.fAction.Trigger);
				}
				else
				{
					list.Add(string.Concat(new string[]
					{
						"Trigger (<A href=power:action>",
						text2,
						"</A>): <A href=power:action>",
						this.fAction.Trigger,
						"</A>"
					}));
				}
			}
			string text3 = HTML.Process(this.fCondition, true);
			if (text3 == "" && mode == CardMode.Build)
			{
				text3 = "No prerequisite";
			}
			if (text3 != "")
			{
				if (mode == CardMode.Build)
				{
					text3 = "<A href=power:prerequisite>" + text3 + "</A>";
				}
				text3 = "Prerequisite: " + text3;
				list.Add(text3);
			}
			string text4 = (this.fRange != null) ? this.fRange : "";
			string text5 = (this.fAttack != null) ? this.fAttack.ToString() : "";
			if (mode == CardMode.Build)
			{
				if (text4 == "")
				{
					text4 = "<A href=power:range>The power's range and its target(s) are not set</A>";
				}
				else
				{
					text4 = "<A href=power:range>" + text4 + "</A>";
				}
				if (text5 == "")
				{
					text5 = "<A href=power:attack>Click here to make this an attack power</A>";
				}
				else
				{
					text5 = "<A href=power:attack>" + text5 + "</A> <A href=power:clearattack>(clear attack)</A>";
				}
			}
			if (text4 != "")
			{
				list.Add("Range: " + text4);
			}
			if (text5 != "")
			{
				list.Add("Attack: " + text5);
			}
			if (mode == CardMode.Build)
			{
				list.Add("");
			}
			string text6 = HTML.Process(this.fDetails, true);
			if (text6 == null)
			{
				text6 = "";
			}
			if (mode == CardMode.Build)
			{
				if (text6 == "")
				{
					text6 = "Specify the power's effects";
				}
				text6 = "<A href=power:details>" + text6 + "</A>";
			}
			if (text6 != "")
			{
				list.Add(text6);
			}
			if (mode == CardMode.Build)
			{
				list.Add("");
			}
			if (this.fAction != null && this.fAction.SustainAction != ActionType.None)
			{
				string str = this.fAction.SustainAction.ToString();
				if (mode == CardMode.Build)
				{
					str = "<A href=power:action>" + str + "</A>";
				}
				list.Add("Sustain: " + str);
			}
			string text7 = "";
			foreach (string current in list)
			{
				if (text7 != "")
				{
					text7 += "<BR>";
				}
				text7 += current;
			}
			if (text7 == "")
			{
				text7 = "(no details)";
			}
			return text7;
		}

		public void ExtractAttackDetails()
		{
			if (this.fAttack != null)
			{
				return;
			}
			if (!this.fDetails.Contains("vs"))
			{
				return;
			}
			string[] array = this.fDetails.Split(new string[]
			{
				";"
			}, StringSplitOptions.RemoveEmptyEntries);
			this.fDetails = "";
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				string text2 = text.Trim();
				bool flag = false;
				int num = text2.IndexOf("vs");
				if (num != -1 && this.fAttack == null)
				{
					string text3 = text2.Substring(0, num);
					string text4 = text2.Substring(num);
					string text5 = "1234567890";
					int num2 = text3.LastIndexOfAny(text5.ToCharArray());
					if (num2 != -1)
					{
						int bonus = 0;
						DefenceType defence = DefenceType.AC;
						bool flag2 = false;
						bool flag3 = false;
						if (text4.Contains("AC"))
						{
							defence = DefenceType.AC;
							flag3 = true;
						}
						if (text4.Contains("Fort"))
						{
							defence = DefenceType.Fortitude;
							flag3 = true;
						}
						if (text4.Contains("Ref"))
						{
							defence = DefenceType.Reflex;
							flag3 = true;
						}
						if (text4.Contains("Will"))
						{
							defence = DefenceType.Will;
							flag3 = true;
						}
						if (flag3)
						{
							try
							{
								num2 = Math.Max(0, num2 - 2);
								string s = text3.Substring(num2);
								bonus = int.Parse(s);
								flag2 = true;
							}
							catch
							{
								flag2 = false;
							}
						}
						if (flag2 && flag3)
						{
							this.fAttack = new PowerAttack();
							this.fAttack.Bonus = bonus;
							this.fAttack.Defence = defence;
							flag = true;
						}
					}
				}
				if (!flag)
				{
					if (this.fDetails != "")
					{
						this.fDetails += "; ";
					}
					this.fDetails += text2;
				}
			}
		}
	}
}
