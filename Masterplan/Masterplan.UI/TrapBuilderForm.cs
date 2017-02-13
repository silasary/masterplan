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
	internal class TrapBuilderForm : Form
	{
		private Trap fTrap;

		private IContainer components;

		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private WebBrowser StatBlockBrowser;

		private ToolStrip Toolbar;

		private ToolStripDropDownButton OptionsMenu;

		private ToolStripMenuItem OptionsCopy;

		private ToolStripButton LevelDownBtn;

		private ToolStripButton LevelUpBtn;

		private ToolStripLabel LevelLbl;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileExport;

		public Trap Trap
		{
			get
			{
				return this.fTrap;
			}
		}

		public TrapBuilderForm(Trap trap)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fTrap = trap.Copy();
			this.update_statblock();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.LevelDownBtn.Enabled = (this.fTrap.Level > 1);
		}

		private void OptionsCopy_Click(object sender, EventArgs e)
		{
			TrapSelectForm trapSelectForm = new TrapSelectForm();
			if (trapSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fTrap = trapSelectForm.Trap.Copy();
				this.update_statblock();
			}
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "build")
			{
				if (e.Url.LocalPath == "profile")
				{
					e.Cancel = true;
					TrapProfileForm trapProfileForm = new TrapProfileForm(this.fTrap);
					if (trapProfileForm.ShowDialog() == DialogResult.OK)
					{
						this.fTrap.Name = trapProfileForm.Trap.Name;
						this.fTrap.Type = trapProfileForm.Trap.Type;
						this.fTrap.Level = trapProfileForm.Trap.Level;
						this.fTrap.Role = trapProfileForm.Trap.Role;
						this.fTrap.Initiative = trapProfileForm.Trap.Initiative;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "readaloud")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fTrap.ReadAloud, "Read-Aloud Text", null);
					if (detailsForm.ShowDialog() == DialogResult.OK)
					{
						this.fTrap.ReadAloud = detailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "desc")
				{
					e.Cancel = true;
					DetailsForm detailsForm2 = new DetailsForm(this.fTrap.Description, "Description", null);
					if (detailsForm2.ShowDialog() == DialogResult.OK)
					{
						this.fTrap.Description = detailsForm2.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "details")
				{
					e.Cancel = true;
					DetailsForm detailsForm3 = new DetailsForm(this.fTrap.Details, "Details", null);
					if (detailsForm3.ShowDialog() == DialogResult.OK)
					{
						this.fTrap.Details = detailsForm3.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addskill")
				{
					e.Cancel = true;
					TrapSkillForm trapSkillForm = new TrapSkillForm(new TrapSkillData
					{
						SkillName = "Perception",
						DC = AI.GetSkillDC(Difficulty.Moderate, this.fTrap.Level)
					}, this.fTrap.Level);
					if (trapSkillForm.ShowDialog() == DialogResult.OK)
					{
						this.fTrap.Skills.Add(trapSkillForm.SkillData);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addattack")
				{
					e.Cancel = true;
					TrapAttack trapAttack = new TrapAttack();
					trapAttack.Name = "Attack";
					this.fTrap.Attacks.Add(trapAttack);
					this.update_statblock();
				}
				if (e.Url.LocalPath == "addcm")
				{
					e.Cancel = true;
					string cm = "";
					TrapCountermeasureForm trapCountermeasureForm = new TrapCountermeasureForm(cm, this.fTrap.Level);
					if (trapCountermeasureForm.ShowDialog() == DialogResult.OK)
					{
						this.fTrap.Countermeasures.Add(trapCountermeasureForm.Countermeasure);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "trigger")
				{
					e.Cancel = true;
					DetailsForm detailsForm4 = new DetailsForm(this.fTrap.Trigger, "Trigger", null);
					if (detailsForm4.ShowDialog() == DialogResult.OK)
					{
						this.fTrap.Trigger = detailsForm4.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attackaction")
			{
				e.Cancel = true;
				Guid id = new Guid(e.Url.LocalPath);
				TrapAttack trapAttack2 = this.fTrap.FindAttack(id);
				if (trapAttack2 != null)
				{
					TrapActionForm trapActionForm = new TrapActionForm(trapAttack2);
					if (trapActionForm.ShowDialog() == DialogResult.OK)
					{
						trapAttack2.Name = trapActionForm.Attack.Name;
						trapAttack2.Action = trapActionForm.Attack.Action;
						trapAttack2.Range = trapActionForm.Attack.Range;
						trapAttack2.Target = trapActionForm.Attack.Target;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attackremove")
			{
				e.Cancel = true;
				Guid id2 = new Guid(e.Url.LocalPath);
				TrapAttack trapAttack3 = this.fTrap.FindAttack(id2);
				if (trapAttack3 != null)
				{
					this.fTrap.Attacks.Remove(trapAttack3);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "attackattack")
			{
				e.Cancel = true;
				Guid id3 = new Guid(e.Url.LocalPath);
				TrapAttack trapAttack4 = this.fTrap.FindAttack(id3);
				if (trapAttack4 != null)
				{
					PowerAttackForm powerAttackForm = new PowerAttackForm(trapAttack4.Attack, false, this.fTrap.Level, this.fTrap.Role);
					if (powerAttackForm.ShowDialog() == DialogResult.OK)
					{
						trapAttack4.Attack = powerAttackForm.Attack;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attackhit")
			{
				e.Cancel = true;
				Guid id4 = new Guid(e.Url.LocalPath);
				TrapAttack trapAttack5 = this.fTrap.FindAttack(id4);
				if (trapAttack5 != null)
				{
					DetailsForm detailsForm5 = new DetailsForm(trapAttack5.OnHit, "On Hit", null);
					if (detailsForm5.ShowDialog() == DialogResult.OK)
					{
						trapAttack5.OnHit = detailsForm5.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attackmiss")
			{
				e.Cancel = true;
				Guid id5 = new Guid(e.Url.LocalPath);
				TrapAttack trapAttack6 = this.fTrap.FindAttack(id5);
				if (trapAttack6 != null)
				{
					DetailsForm detailsForm6 = new DetailsForm(trapAttack6.OnMiss, "On Miss", null);
					if (detailsForm6.ShowDialog() == DialogResult.OK)
					{
						trapAttack6.OnMiss = detailsForm6.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attackeffect")
			{
				e.Cancel = true;
				Guid id6 = new Guid(e.Url.LocalPath);
				TrapAttack trapAttack7 = this.fTrap.FindAttack(id6);
				if (trapAttack7 != null)
				{
					DetailsForm detailsForm7 = new DetailsForm(trapAttack7.Effect, "Effect", null);
					if (detailsForm7.ShowDialog() == DialogResult.OK)
					{
						trapAttack7.Effect = detailsForm7.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "attacknotes")
			{
				e.Cancel = true;
				Guid id7 = new Guid(e.Url.LocalPath);
				TrapAttack trapAttack8 = this.fTrap.FindAttack(id7);
				if (trapAttack8 != null)
				{
					DetailsForm detailsForm8 = new DetailsForm(trapAttack8.Notes, "Notes", null);
					if (detailsForm8.ShowDialog() == DialogResult.OK)
					{
						trapAttack8.Notes = detailsForm8.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "skill")
			{
				e.Cancel = true;
				Guid id8 = new Guid(e.Url.LocalPath);
				TrapSkillData trapSkillData = this.fTrap.FindSkill(id8);
				if (trapSkillData != null)
				{
					int index = this.fTrap.Skills.IndexOf(trapSkillData);
					TrapSkillForm trapSkillForm2 = new TrapSkillForm(trapSkillData, this.fTrap.Level);
					if (trapSkillForm2.ShowDialog() == DialogResult.OK)
					{
						this.fTrap.Skills[index] = trapSkillForm2.SkillData;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "skillremove")
			{
				e.Cancel = true;
				Guid id9 = new Guid(e.Url.LocalPath);
				TrapSkillData trapSkillData2 = this.fTrap.FindSkill(id9);
				if (trapSkillData2 != null)
				{
					this.fTrap.Skills.Remove(trapSkillData2);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "cm")
			{
				e.Cancel = true;
				int index2 = int.Parse(e.Url.LocalPath);
				string cm2 = this.fTrap.Countermeasures[index2];
				TrapCountermeasureForm trapCountermeasureForm2 = new TrapCountermeasureForm(cm2, this.fTrap.Level);
				if (trapCountermeasureForm2.ShowDialog() == DialogResult.OK)
				{
					this.fTrap.Countermeasures[index2] = trapCountermeasureForm2.Countermeasure;
					this.update_statblock();
				}
			}
		}

		private void update_statblock()
		{
			List<string> head = HTML.GetHead("Trap", "", DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<TABLE class=clear>");
			head.Add("<TR class=clear>");
			head.Add("<TD class=clear>");
			head.Add("<P class=table>");
			head.Add(HTML.Trap(this.fTrap, null, false, false, true, DisplaySize.Small));
			head.Add("</P>");
			head.Add("</TD>");
			head.Add("<TD class=clear>");
			head.AddRange(this.get_advice());
			head.Add("</TD>");
			head.Add("</TR>");
			head.Add("</TABLE>");
			head.Add("</BODY>");
			head.Add("</HTML>");
			this.StatBlockBrowser.DocumentText = HTML.Concatenate(head);
		}

		private List<string> get_advice()
		{
			int num = 2;
			int num2 = this.fTrap.Level + 5;
			int num3 = this.fTrap.Level + 3;
			bool flag = false;
			if (this.fTrap.Role is ComplexRole)
			{
				ComplexRole complexRole = this.fTrap.Role as ComplexRole;
				if (complexRole.Flag == RoleFlag.Elite || complexRole.Flag == RoleFlag.Solo)
				{
					flag = true;
				}
			}
			if (flag)
			{
				num += 2;
				num2 += 2;
				num3 += 2;
			}
			List<string> list = new List<string>();
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD colspan=2><B>Initiative Advice</B></TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Initiative</TD>");
			list.Add("<TD align=center>+" + num + "</TD>");
			list.Add("</TR>");
			list.Add("<TR class=heading>");
			list.Add("<TD colspan=2><B>Attack Advice</B></TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Attack vs Armour Class</TD>");
			list.Add("<TD align=center>+" + num2 + "</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Attack vs Other Defence</TD>");
			list.Add("<TD align=center>+" + num3 + "</TD>");
			list.Add("</TR>");
			list.Add("<TR class=heading>");
			list.Add("<TD colspan=2><B>Damage Advice</B></TD>");
			list.Add("</TR>");
			if (this.fTrap.Role is Minion)
			{
				list.Add("<TR>");
				list.Add("<TD>Minion Damage</TD>");
				list.Add("<TD align=center>" + Statistics.Damage(this.fTrap.Level, DamageExpressionType.Minion) + "</TD>");
				list.Add("</TR>");
			}
			else
			{
				list.Add("<TR>");
				list.Add("<TD>Damage vs Single Targets</TD>");
				list.Add("<TD align=center>" + Statistics.Damage(this.fTrap.Level, DamageExpressionType.Normal) + "</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>Damage vs Multiple Targets</TD>");
				list.Add("<TD align=center>" + Statistics.Damage(this.fTrap.Level, DamageExpressionType.Multiple) + "</TD>");
				list.Add("</TR>");
			}
			list.Add("<TR class=heading>");
			list.Add("<TD colspan=2><B>Skill Advice</B></TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Easy DC</TD>");
			list.Add("<TD align=center>" + AI.GetSkillDC(Difficulty.Easy, this.fTrap.Level) + "</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Moderate DC</TD>");
			list.Add("<TD align=center>" + AI.GetSkillDC(Difficulty.Moderate, this.fTrap.Level) + "</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Hard DC</TD>");
			list.Add("<TD align=center>" + AI.GetSkillDC(Difficulty.Hard, this.fTrap.Level) + "</TD>");
			list.Add("</TR>");
			list.Add("</TABLE>");
			list.Add("</P>");
			return list;
		}

		private void LevelUpBtn_Click(object sender, EventArgs e)
		{
			this.fTrap.AdjustLevel(1);
			this.update_statblock();
		}

		private void LevelDownBtn_Click(object sender, EventArgs e)
		{
			this.fTrap.AdjustLevel(-1);
			this.update_statblock();
		}

		private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Export Trap";
			saveFileDialog.FileName = this.fTrap.Name;
			saveFileDialog.Filter = Program.TrapFilter;
			if (saveFileDialog.ShowDialog() == DialogResult.OK && !Serialisation<Trap>.Save(saveFileDialog.FileName, this.fTrap, SerialisationMode.Binary))
			{
				string text = "The trap could not be exported.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(TrapBuilderForm));
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.StatBlockBrowser = new WebBrowser();
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileExport = new ToolStripMenuItem();
			this.OptionsMenu = new ToolStripDropDownButton();
			this.OptionsCopy = new ToolStripMenuItem();
			this.LevelDownBtn = new ToolStripButton();
			this.LevelUpBtn = new ToolStripButton();
			this.LevelLbl = new ToolStripLabel();
			this.BtnPnl.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.BtnPnl.Controls.Add(this.CancelBtn);
			this.BtnPnl.Controls.Add(this.OKBtn);
			this.BtnPnl.Dock = DockStyle.Bottom;
			this.BtnPnl.Location = new Point(0, 443);
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
			this.StatBlockBrowser.Size = new Size(664, 418);
			this.StatBlockBrowser.TabIndex = 2;
			this.StatBlockBrowser.WebBrowserShortcutsEnabled = false;
			this.StatBlockBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.FileMenu,
				this.OptionsMenu,
				this.LevelDownBtn,
				this.LevelUpBtn,
				this.LevelLbl
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(664, 25);
			this.Toolbar.TabIndex = 3;
			this.Toolbar.Text = "Toolbar";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FileMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.FileExport
			});
			this.FileMenu.Image = (Image)resources.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new Size(152, 22);
			this.FileExport.Text = "Export...";
			this.FileExport.Click += new EventHandler(this.FileExport_Click);
			this.OptionsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OptionsMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.OptionsCopy
			});
			this.OptionsMenu.Image = (Image)resources.GetObject("OptionsMenu.Image");
			this.OptionsMenu.ImageTransparentColor = Color.Magenta;
			this.OptionsMenu.Name = "OptionsMenu";
			this.OptionsMenu.Size = new Size(62, 22);
			this.OptionsMenu.Text = "Options";
			this.OptionsCopy.Name = "OptionsCopy";
			this.OptionsCopy.Size = new Size(197, 22);
			this.OptionsCopy.Text = "Copy an Existing Trap...";
			this.OptionsCopy.Click += new EventHandler(this.OptionsCopy_Click);
			this.LevelDownBtn.Alignment = ToolStripItemAlignment.Right;
			this.LevelDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelDownBtn.Image = (Image)resources.GetObject("LevelDownBtn.Image");
			this.LevelDownBtn.ImageTransparentColor = Color.Magenta;
			this.LevelDownBtn.Name = "LevelDownBtn";
			this.LevelDownBtn.Size = new Size(23, 22);
			this.LevelDownBtn.Text = "-";
			this.LevelDownBtn.ToolTipText = "Level Down";
			this.LevelDownBtn.Click += new EventHandler(this.LevelDownBtn_Click);
			this.LevelUpBtn.Alignment = ToolStripItemAlignment.Right;
			this.LevelUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelUpBtn.Image = (Image)resources.GetObject("LevelUpBtn.Image");
			this.LevelUpBtn.ImageTransparentColor = Color.Magenta;
			this.LevelUpBtn.Name = "LevelUpBtn";
			this.LevelUpBtn.Size = new Size(23, 22);
			this.LevelUpBtn.Text = "+";
			this.LevelUpBtn.ToolTipText = "Level Up";
			this.LevelUpBtn.Click += new EventHandler(this.LevelUpBtn_Click);
			this.LevelLbl.Alignment = ToolStripItemAlignment.Right;
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(37, 22);
			this.LevelLbl.Text = "Level:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(664, 478);
			base.Controls.Add(this.StatBlockBrowser);
			base.Controls.Add(this.Toolbar);
			base.Controls.Add(this.BtnPnl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TrapBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Trap / Hazard Builder";
			this.BtnPnl.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
