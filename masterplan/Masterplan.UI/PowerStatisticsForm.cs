using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class PowerStatisticsForm : Form
	{
		private List<CreaturePower> fPowers;

		private int fCreatures;

		private IContainer components;

		private WebBrowser Browser;

		public PowerStatisticsForm(List<CreaturePower> powers, int creatures)
		{
			this.InitializeComponent();
			this.fPowers = powers;
			this.fCreatures = creatures;
			this.update_table();
		}

		private void update_table()
		{
			List<string> list = new List<string>();
			list.AddRange(HTML.GetHead("Power Statistics", "", DisplaySize.Small));
			list.Add("<BODY>");
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Number of Powers</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD colspan=2>");
			list.Add("Number of powers");
			list.Add("</TD>");
			list.Add("<TD align=right>");
			list.Add(this.fPowers.Count.ToString());
			list.Add("</TD>");
			list.Add("</TR>");
			if (this.fCreatures != 0)
			{
				double num = (double)this.fPowers.Count / (double)this.fCreatures;
				list.Add("<TR>");
				list.Add("<TD colspan=2>");
				list.Add("Powers per creature");
				list.Add("</TD>");
				list.Add("<TD align=right>");
				list.Add(num.ToString("F1"));
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			if (this.fPowers.Count != 0)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				Dictionary<string, int> condition_breakdown = this.get_condition_breakdown();
				if (condition_breakdown.Count != 0)
				{
					list.Add("<TR class=heading>");
					list.Add("<TD colspan=3>");
					list.Add("<B>Conditions</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					List<Pair<string, int>> list2 = this.sort_breakdown(condition_breakdown);
					foreach (Pair<string, int> current in list2)
					{
						int second = current.Second;
						if (second != 0)
						{
							double num2 = (double)second / (double)this.fPowers.Count;
							list.Add("<TR>");
							list.Add("<TD colspan=2>");
							list.Add(current.First);
							list.Add("</TD>");
							list.Add("<TD align=right>");
							list.Add(string.Concat(new object[]
							{
								second,
								" (",
								num2.ToString("P0"),
								")"
							}));
							list.Add("</TD>");
							list.Add("</TR>");
						}
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			if (this.fPowers.Count != 0)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				Dictionary<string, int> damage_type_breakdown = this.get_damage_type_breakdown();
				if (damage_type_breakdown.Count != 0)
				{
					list.Add("<TR class=heading>");
					list.Add("<TD colspan=3>");
					list.Add("<B>Damage Types</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					List<Pair<string, int>> list3 = this.sort_breakdown(damage_type_breakdown);
					foreach (Pair<string, int> current2 in list3)
					{
						int second2 = current2.Second;
						double num3 = (double)second2 / (double)this.fPowers.Count;
						list.Add("<TR>");
						list.Add("<TD colspan=2>");
						list.Add(current2.First);
						list.Add("</TD>");
						list.Add("<TD align=right>");
						list.Add(string.Concat(new object[]
						{
							second2,
							" (",
							num3.ToString("P0"),
							")"
						}));
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			if (this.fPowers.Count != 0)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				Dictionary<string, int> keyword_breakdown = this.get_keyword_breakdown();
				if (keyword_breakdown.Count != 0)
				{
					list.Add("<TR class=heading>");
					list.Add("<TD colspan=3>");
					list.Add("<B>Keywords</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					List<Pair<string, int>> list4 = this.sort_breakdown(keyword_breakdown);
					foreach (Pair<string, int> current3 in list4)
					{
						int second3 = current3.Second;
						double num4 = (double)second3 / (double)this.fPowers.Count;
						list.Add("<TR>");
						list.Add("<TD colspan=2>");
						list.Add(current3.First);
						list.Add("</TD>");
						list.Add("<TD align=right>");
						list.Add(string.Concat(new object[]
						{
							second3,
							" (",
							num4.ToString("P0"),
							")"
						}));
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			if (this.fPowers.Count != 0)
			{
				Dictionary<string, double> category_breakdown = this.get_category_breakdown();
				if (category_breakdown.Count != 0)
				{
					list.Add("<P class=table>");
					list.Add("<TABLE>");
					list.Add("<TR class=heading>");
					list.Add("<TD colspan=3>");
					list.Add("<B>Powers Per Category</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					foreach (string current4 in category_breakdown.Keys)
					{
						double num5 = category_breakdown[current4];
						list.Add("<TR>");
						list.Add("<TD colspan=2>");
						list.Add(current4);
						list.Add("</TD>");
						list.Add("<TD align=right>");
						list.Add(num5.ToString("P0"));
						list.Add("</TD>");
						list.Add("</TR>");
					}
					list.Add("</TABLE>");
					list.Add("</P>");
				}
			}
			if (this.fPowers.Count != 0)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				Dictionary<string, int> damage_expression_breakdown = this.get_damage_expression_breakdown();
				if (damage_expression_breakdown.Count != 0)
				{
					list.Add("<TR class=heading>");
					list.Add("<TD colspan=3>");
					list.Add("<B>Damage</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					List<Pair<string, int>> list5 = this.sort_breakdown(damage_expression_breakdown);
					foreach (Pair<string, int> current5 in list5)
					{
						int second4 = current5.Second;
						double num6 = (double)second4 / (double)this.fPowers.Count;
						DiceExpression diceExpression = DiceExpression.Parse(current5.First);
						list.Add("<TR>");
						list.Add("<TD colspan=2>");
						list.Add(string.Concat(new object[]
						{
							current5.First,
							" (avg ",
							diceExpression.Average,
							", max ",
							diceExpression.Maximum,
							")"
						}));
						list.Add("</TD>");
						list.Add("<TD align=right>");
						list.Add(string.Concat(new object[]
						{
							second4,
							" (",
							num6.ToString("P0"),
							")"
						}));
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			list.Add("</BODY>");
			list.Add("</HTML>");
			this.Browser.DocumentText = HTML.Concatenate(list);
		}

		private int count_powers(string text)
		{
			if (text == "Immobilised")
			{
				text = "Immobilized";
			}
			int num = 0;
			foreach (CreaturePower current in this.fPowers)
			{
				if (current.Details.ToLower().Contains(text.ToLower()))
				{
					num++;
				}
			}
			return num;
		}

		private List<Pair<string, int>> sort_breakdown(Dictionary<string, int> breakdown)
		{
			List<Pair<string, int>> list = new List<Pair<string, int>>();
			foreach (string current in breakdown.Keys)
			{
				list.Add(new Pair<string, int>(current, breakdown[current]));
			}
			list.Sort((Pair<string, int> x, Pair<string, int> y) => x.Second.CompareTo(y.Second) * -1);
			return list;
		}

		private Dictionary<string, int> get_condition_breakdown()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			List<string> conditions = Conditions.GetConditions();
			foreach (string current in conditions)
			{
				int num = this.count_powers(current);
				if (num != 0)
				{
					dictionary[current] = num;
				}
			}
			return dictionary;
		}

		private Dictionary<string, int> get_damage_type_breakdown()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Array values = Enum.GetValues(typeof(DamageType));
			foreach (DamageType damageType in values)
			{
				if (damageType != DamageType.Untyped)
				{
					string text = damageType.ToString();
					int num = this.count_powers(text);
					if (num != 0)
					{
						dictionary[text] = num;
					}
				}
			}
			return dictionary;
		}

		private Dictionary<string, double> get_category_breakdown()
		{
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			Array values = Enum.GetValues(typeof(CreaturePowerCategory));
			foreach (CreaturePowerCategory creaturePowerCategory in values)
			{
				int num = 0;
				foreach (CreaturePower current in this.fPowers)
				{
					if (current.Category == creaturePowerCategory)
					{
						num++;
					}
				}
				dictionary[creaturePowerCategory.ToString()] = (double)num / (double)this.fPowers.Count;
			}
			return dictionary;
		}

		private Dictionary<string, int> get_damage_expression_breakdown()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (CreaturePower current in this.fPowers)
			{
				DiceExpression diceExpression = DiceExpression.Parse(current.Damage);
				if (diceExpression != null && diceExpression.Maximum != 0)
				{
					string text = diceExpression.ToString();
					if (!dictionary.ContainsKey(text))
					{
						dictionary[text] = 0;
					}
					Dictionary<string, int> dictionary2;
					string key;
					(dictionary2 = dictionary)[key = text] = dictionary2[key] + 1;
				}
			}
			return dictionary;
		}

		private Dictionary<string, int> get_keyword_breakdown()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (CreaturePower current in this.fPowers)
			{
				string[] array = current.Keywords.Split(new string[]
				{
					",",
					";"
				}, StringSplitOptions.RemoveEmptyEntries);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					string text2 = text.Trim();
					if (!dictionary.ContainsKey(text2))
					{
						dictionary[text2] = 0;
					}
					Dictionary<string, int> dictionary2;
					string key;
					(dictionary2 = dictionary)[key = text2] = dictionary2[key] + 1;
				}
			}
			return dictionary;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.Browser = new WebBrowser();
			base.SuspendLayout();
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new Size(364, 362);
			this.Browser.TabIndex = 2;
			this.Browser.WebBrowserShortcutsEnabled = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(364, 362);
			base.Controls.Add(this.Browser);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerStatisticsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Statistics";
			base.ResumeLayout(false);
		}
	}
}
