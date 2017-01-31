using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PowerBuilderForm : Form
	{
		private CreaturePower fPower;

		private ICreature fCreature;

		private bool fFromFunctionalTemplate;

		private List<string> fExamples = new List<string>();

		private IContainer components;

		private ToolStrip Toolbar;

		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private WebBrowser StatBlockBrowser;

		private ToolStripButton PowerBrowserBtn;

		public CreaturePower Power
		{
			get
			{
				return this.fPower;
			}
		}

		public PowerBuilderForm(CreaturePower power, ICreature creature, bool functional_template)
		{
			this.InitializeComponent();
			this.fPower = power;
			this.fCreature = creature;
			this.fFromFunctionalTemplate = functional_template;
			this.refresh_examples();
			this.update_statblock();
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "power")
			{
				if (e.Url.LocalPath == "info")
				{
					e.Cancel = true;
					PowerInfoForm powerInfoForm = new PowerInfoForm(this.fPower);
					if (powerInfoForm.ShowDialog() == DialogResult.OK)
					{
						this.fPower.Name = powerInfoForm.PowerName;
						this.fPower.Keywords = powerInfoForm.PowerKeywords;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "action")
				{
					e.Cancel = true;
					PowerAction action = this.fPower.Action;
					PowerActionForm powerActionForm = new PowerActionForm(action);
					if (powerActionForm.ShowDialog() == DialogResult.OK)
					{
						this.fPower.Action = powerActionForm.Action;
						this.refresh_examples();
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "prerequisite")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fPower.Condition, "Power Prerequisite", null);
					if (detailsForm.ShowDialog() == DialogResult.OK)
					{
						this.fPower.Condition = detailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "range")
				{
					e.Cancel = true;
					PowerRangeForm powerRangeForm = new PowerRangeForm(this.fPower);
					if (powerRangeForm.ShowDialog() == DialogResult.OK)
					{
						this.fPower.Range = powerRangeForm.PowerRange;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "attack")
				{
					e.Cancel = true;
					PowerAttack powerAttack = this.fPower.Attack;
					if (powerAttack == null)
					{
						powerAttack = new PowerAttack();
					}
					int level = (this.fCreature != null) ? this.fCreature.Level : 0;
					IRole role = (this.fCreature != null) ? this.fCreature.Role : null;
					PowerAttackForm powerAttackForm = new PowerAttackForm(powerAttack, this.fFromFunctionalTemplate, level, role);
					if (powerAttackForm.ShowDialog() == DialogResult.OK)
					{
						this.fPower.Attack = powerAttackForm.Attack;
						this.refresh_examples();
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "clearattack")
				{
					e.Cancel = true;
					this.fPower.Attack = null;
					this.refresh_examples();
					this.update_statblock();
				}
				if (e.Url.LocalPath == "details")
				{
					e.Cancel = true;
					PowerDetailsForm powerDetailsForm = new PowerDetailsForm(this.fPower.Details, this.fCreature);
					if (powerDetailsForm.ShowDialog() == DialogResult.OK)
					{
						this.fPower.Details = powerDetailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "desc")
				{
					e.Cancel = true;
					DetailsForm detailsForm2 = new DetailsForm(this.fPower.Description, "Power Description", null);
					if (detailsForm2.ShowDialog() == DialogResult.OK)
					{
						this.fPower.Description = detailsForm2.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "details")
			{
				if (e.Url.LocalPath == "refresh")
				{
					e.Cancel = true;
					this.refresh_examples();
					this.update_statblock();
				}
				try
				{
					int index = int.Parse(e.Url.LocalPath);
					e.Cancel = true;
					this.fPower.Details = this.fExamples[index];
					this.fExamples.RemoveAt(index);
					if (this.fExamples.Count == 0)
					{
						this.refresh_examples();
					}
					this.update_statblock();
				}
				catch
				{
				}
			}
		}

		private void refresh_examples()
		{
			this.fExamples.Clear();
			List<ICreature> list = new List<ICreature>();
			foreach (Creature current in Session.Creatures)
			{
				if (current != null && current.Level == this.fCreature.Level && current.Role.ToString() == this.fCreature.Role.ToString())
				{
					list.Add(current);
				}
			}
			if (Session.Project != null)
			{
				foreach (CustomCreature current2 in Session.Project.CustomCreatures)
				{
					if (current2 != null && current2.Level == this.fCreature.Level && current2.Role.ToString() == this.fCreature.Role.ToString())
					{
						list.Add(current2);
					}
				}
			}
			List<string> list2 = new List<string>();
			foreach (ICreature current3 in list)
			{
				foreach (CreaturePower current4 in current3.CreaturePowers)
				{
					if (this.fPower.Category == current4.Category && !(current4.Details == ""))
					{
						list2.Add(current4.Details);
					}
				}
			}
			if (list2.Count != 0)
			{
				for (int num = 0; num != 5; num++)
				{
					if (list2.Count == 0)
					{
						return;
					}
					int index = Session.Random.Next(list2.Count);
					string item = list2[index];
					list2.RemoveAt(index);
					this.fExamples.Add(item);
				}
			}
		}

		private void update_statblock()
		{
			int level = (this.fCreature != null) ? this.fCreature.Level : 0;
			IRole role = (this.fCreature != null) ? this.fCreature.Role : null;
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<TABLE class=clear>");
			head.Add("<TR class=clear>");
			head.Add("<TD class=clear>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.AddRange(this.fPower.AsHTML(null, CardMode.Build, this.fFromFunctionalTemplate));
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</TD>");
			head.Add("<TD class=clear>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD colspan=2><B>Power Advice</B></TD>");
			head.Add("</TR>");
			head.Add("<TR class=shaded>");
			head.Add("<TD colspan=2><B>Attack Bonus</B></TD>");
			head.Add("</TR>");
			head.Add("<TR>");
			head.Add("<TD>Attack vs Armour Class</TD>");
			head.Add("<TD align=center>+" + Statistics.AttackBonus(DefenceType.AC, level, role) + "</TD>");
			head.Add("</TR>");
			head.Add("<TR>");
			head.Add("<TD>Attack vs Other Defence</TD>");
			head.Add("<TD align=center>+" + Statistics.AttackBonus(DefenceType.Fortitude, level, role) + "</TD>");
			head.Add("</TR>");
			if (role != null)
			{
				head.Add("<TR class=shaded>");
				head.Add("<TD colspan=2><B>Damage</B></TD>");
				head.Add("</TR>");
				if (role is Minion)
				{
					head.Add("<TR>");
					head.Add("<TD>Minion Damage</TD>");
					head.Add("<TD align=center>" + Statistics.Damage(level, DamageExpressionType.Minion) + "</TD>");
					head.Add("</TR>");
				}
				else
				{
					head.Add("<TR>");
					head.Add("<TD>Damage vs Single Targets</TD>");
					head.Add("<TD align=center>" + Statistics.Damage(level, DamageExpressionType.Normal) + "</TD>");
					head.Add("</TR>");
					head.Add("<TR>");
					head.Add("<TD>Damage vs Multiple Targets</TD>");
					head.Add("<TD align=center>" + Statistics.Damage(level, DamageExpressionType.Multiple) + "</TD>");
					head.Add("</TR>");
				}
				if (this.fExamples.Count != 0)
				{
					head.Add("<TR class=shaded>");
					head.Add("<TD><B>Example Power Details</B></TD>");
					head.Add("<TD align=center><A href=details:refresh>(refresh)</A></TD>");
					head.Add("</TR>");
					foreach (string current in this.fExamples)
					{
						int num = this.fExamples.IndexOf(current);
						head.Add("<TR>");
						head.Add(string.Concat(new object[]
						{
							"<TD colspan=2>",
							current,
							" <A href=details:",
							num,
							">(use this)</A></TD>"
						}));
						head.Add("</TR>");
					}
				}
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</TD>");
			head.Add("</TR>");
			head.Add("</TABLE>");
			head.Add("</BODY>");
			head.Add("</HTML>");
			this.StatBlockBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void PowerBrowserBtn_Click(object sender, EventArgs e)
		{
			int level = (this.fCreature != null) ? this.fCreature.Level : 0;
			IRole role = (this.fCreature != null) ? this.fCreature.Role : null;
			PowerBrowserForm powerBrowserForm = new PowerBrowserForm(null, level, role, null);
			if (powerBrowserForm.ShowDialog() == DialogResult.OK && powerBrowserForm.SelectedPower != null)
			{
				this.fPower = powerBrowserForm.SelectedPower.Copy();
				this.fPower.ID = Guid.NewGuid();
				this.update_statblock();
			}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PowerBuilderForm));
			this.Toolbar = new ToolStrip();
			this.PowerBrowserBtn = new ToolStripButton();
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.StatBlockBrowser = new WebBrowser();
			this.Toolbar.SuspendLayout();
			this.BtnPnl.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.PowerBrowserBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(664, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.PowerBrowserBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerBrowserBtn.Image = (Image)componentResourceManager.GetObject("PowerBrowserBtn.Image");
			this.PowerBrowserBtn.ImageTransparentColor = Color.Magenta;
			this.PowerBrowserBtn.Name = "PowerBrowserBtn";
			this.PowerBrowserBtn.Size = new Size(89, 22);
			this.PowerBrowserBtn.Text = "Power Browser";
			this.PowerBrowserBtn.Click += new EventHandler(this.PowerBrowserBtn_Click);
			this.BtnPnl.Controls.Add(this.CancelBtn);
			this.BtnPnl.Controls.Add(this.OKBtn);
			this.BtnPnl.Dock = DockStyle.Bottom;
			this.BtnPnl.Location = new Point(0, 333);
			this.BtnPnl.Name = "BtnPnl";
			this.BtnPnl.Size = new Size(664, 35);
			this.BtnPnl.TabIndex = 2;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(577, 6);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(496, 6);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.StatBlockBrowser.AllowWebBrowserDrop = false;
			this.StatBlockBrowser.Dock = DockStyle.Fill;
			this.StatBlockBrowser.IsWebBrowserContextMenuEnabled = false;
			this.StatBlockBrowser.Location = new Point(0, 25);
			this.StatBlockBrowser.MinimumSize = new Size(20, 20);
			this.StatBlockBrowser.Name = "StatBlockBrowser";
			this.StatBlockBrowser.ScriptErrorsSuppressed = true;
			this.StatBlockBrowser.Size = new Size(664, 308);
			this.StatBlockBrowser.TabIndex = 2;
			this.StatBlockBrowser.WebBrowserShortcutsEnabled = false;
			this.StatBlockBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(664, 368);
			base.Controls.Add(this.StatBlockBrowser);
			base.Controls.Add(this.BtnPnl);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Builder";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.BtnPnl.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
