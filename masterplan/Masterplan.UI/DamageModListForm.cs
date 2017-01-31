using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DamageModListForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private ListView DamageList;

		private ColumnHeader DmgModHdr;

		private ToolStrip DamageToolbar;

		private ToolStripButton AddDmgBtn;

		private ToolStripButton RemoveDmgBtn;

		private ToolStripButton EditDmgBtn;

		private TextBox ImmuneBox;

		private Label ImmuneLbl;

		private TextBox VulnerableBox;

		private Label VulnerableLbl;

		private TextBox ResistBox;

		private Label ResistLbl;

		private Panel ModPanel;

		private GroupBox SpecialGroup;

		private ICreature fCreature;

		private CreatureTemplate fTemplate;

		private List<DamageModifier> fModifiers;

		private List<DamageModifierTemplate> fModifierTemplates;

		public List<DamageModifier> Modifiers
		{
			get
			{
				return this.fModifiers;
			}
		}

		public List<DamageModifierTemplate> ModifierTemplates
		{
			get
			{
				return this.fModifierTemplates;
			}
		}

		public DamageModifier SelectedDamageMod
		{
			get
			{
				if (this.DamageList.SelectedItems.Count != 0)
				{
					return this.DamageList.SelectedItems[0].Tag as DamageModifier;
				}
				return null;
			}
		}

		public DamageModifierTemplate SelectedDamageModTemplate
		{
			get
			{
				if (this.DamageList.SelectedItems.Count != 0)
				{
					return this.DamageList.SelectedItems[0].Tag as DamageModifierTemplate;
				}
				return null;
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
			ListViewGroup listViewGroup = new ListViewGroup("Immunities", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Resistances", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Vulnerabilities", HorizontalAlignment.Left);
			ComponentResourceManager resources = new ComponentResourceManager(typeof(DamageModListForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.DamageList = new ListView();
			this.DmgModHdr = new ColumnHeader();
			this.DamageToolbar = new ToolStrip();
			this.AddDmgBtn = new ToolStripButton();
			this.RemoveDmgBtn = new ToolStripButton();
			this.EditDmgBtn = new ToolStripButton();
			this.ResistLbl = new Label();
			this.ResistBox = new TextBox();
			this.VulnerableLbl = new Label();
			this.VulnerableBox = new TextBox();
			this.ImmuneLbl = new Label();
			this.ImmuneBox = new TextBox();
			this.ModPanel = new Panel();
			this.SpecialGroup = new GroupBox();
			this.DamageToolbar.SuspendLayout();
			this.ModPanel.SuspendLayout();
			this.SpecialGroup.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(186, 299);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(267, 299);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 3;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.DamageList.Columns.AddRange(new ColumnHeader[]
			{
				this.DmgModHdr
			});
			this.DamageList.Dock = DockStyle.Fill;
			this.DamageList.FullRowSelect = true;
			listViewGroup.Header = "Immunities";
			listViewGroup.Name = "ImmGrp";
			listViewGroup2.Header = "Resistances";
			listViewGroup2.Name = "ResGrp";
			listViewGroup3.Header = "Vulnerabilities";
			listViewGroup3.Name = "VulnGrp";
			this.DamageList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2,
				listViewGroup3
			});
			this.DamageList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DamageList.HideSelection = false;
			this.DamageList.Location = new Point(0, 25);
			this.DamageList.MultiSelect = false;
			this.DamageList.Name = "DamageList";
			this.DamageList.Size = new Size(330, 145);
			this.DamageList.TabIndex = 1;
			this.DamageList.UseCompatibleStateImageBehavior = false;
			this.DamageList.View = View.Details;
			this.DamageList.DoubleClick += new EventHandler(this.EditDmgBtn_Click);
			this.DmgModHdr.Text = "Damage Modifier";
			this.DmgModHdr.Width = 296;
			this.DamageToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddDmgBtn,
				this.RemoveDmgBtn,
				this.EditDmgBtn
			});
			this.DamageToolbar.Location = new Point(0, 0);
			this.DamageToolbar.Name = "DamageToolbar";
			this.DamageToolbar.Size = new Size(330, 25);
			this.DamageToolbar.TabIndex = 0;
			this.DamageToolbar.Text = "toolStrip1";
			this.AddDmgBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddDmgBtn.Image = (Image)resources.GetObject("AddDmgBtn.Image");
			this.AddDmgBtn.ImageTransparentColor = Color.Magenta;
			this.AddDmgBtn.Name = "AddDmgBtn";
			this.AddDmgBtn.Size = new Size(33, 22);
			this.AddDmgBtn.Text = "Add";
			this.AddDmgBtn.Click += new EventHandler(this.AddDmgBtn_Click);
			this.RemoveDmgBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveDmgBtn.Image = (Image)resources.GetObject("RemoveDmgBtn.Image");
			this.RemoveDmgBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveDmgBtn.Name = "RemoveDmgBtn";
			this.RemoveDmgBtn.Size = new Size(54, 22);
			this.RemoveDmgBtn.Text = "Remove";
			this.RemoveDmgBtn.Click += new EventHandler(this.RemoveDmgBtn_Click);
			this.EditDmgBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditDmgBtn.Image = (Image)resources.GetObject("EditDmgBtn.Image");
			this.EditDmgBtn.ImageTransparentColor = Color.Magenta;
			this.EditDmgBtn.Name = "EditDmgBtn";
			this.EditDmgBtn.Size = new Size(31, 22);
			this.EditDmgBtn.Text = "Edit";
			this.EditDmgBtn.Click += new EventHandler(this.EditDmgBtn_Click);
			this.ResistLbl.AutoSize = true;
			this.ResistLbl.Location = new Point(6, 22);
			this.ResistLbl.Name = "ResistLbl";
			this.ResistLbl.Size = new Size(63, 13);
			this.ResistLbl.TabIndex = 0;
			this.ResistLbl.Text = "Resistance:";
			this.ResistBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ResistBox.Location = new Point(78, 19);
			this.ResistBox.Name = "ResistBox";
			this.ResistBox.Size = new Size(246, 20);
			this.ResistBox.TabIndex = 1;
			this.VulnerableLbl.AutoSize = true;
			this.VulnerableLbl.Location = new Point(6, 48);
			this.VulnerableLbl.Name = "VulnerableLbl";
			this.VulnerableLbl.Size = new Size(66, 13);
			this.VulnerableLbl.TabIndex = 2;
			this.VulnerableLbl.Text = "Vulnerability:";
			this.VulnerableBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.VulnerableBox.Location = new Point(78, 45);
			this.VulnerableBox.Name = "VulnerableBox";
			this.VulnerableBox.Size = new Size(246, 20);
			this.VulnerableBox.TabIndex = 3;
			this.ImmuneLbl.AutoSize = true;
			this.ImmuneLbl.Location = new Point(6, 74);
			this.ImmuneLbl.Name = "ImmuneLbl";
			this.ImmuneLbl.Size = new Size(51, 13);
			this.ImmuneLbl.TabIndex = 4;
			this.ImmuneLbl.Text = "Immunity:";
			this.ImmuneBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ImmuneBox.Location = new Point(78, 71);
			this.ImmuneBox.Name = "ImmuneBox";
			this.ImmuneBox.Size = new Size(246, 20);
			this.ImmuneBox.TabIndex = 5;
			this.ModPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ModPanel.Controls.Add(this.DamageList);
			this.ModPanel.Controls.Add(this.DamageToolbar);
			this.ModPanel.Location = new Point(12, 12);
			this.ModPanel.Name = "ModPanel";
			this.ModPanel.Size = new Size(330, 170);
			this.ModPanel.TabIndex = 0;
			this.SpecialGroup.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.SpecialGroup.Controls.Add(this.ImmuneBox);
			this.SpecialGroup.Controls.Add(this.ResistBox);
			this.SpecialGroup.Controls.Add(this.ImmuneLbl);
			this.SpecialGroup.Controls.Add(this.ResistLbl);
			this.SpecialGroup.Controls.Add(this.VulnerableBox);
			this.SpecialGroup.Controls.Add(this.VulnerableLbl);
			this.SpecialGroup.Location = new Point(12, 188);
			this.SpecialGroup.Name = "SpecialGroup";
			this.SpecialGroup.Size = new Size(330, 105);
			this.SpecialGroup.TabIndex = 1;
			this.SpecialGroup.TabStop = false;
			this.SpecialGroup.Text = "Special";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(354, 334);
			base.Controls.Add(this.SpecialGroup);
			base.Controls.Add(this.ModPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DamageModListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Resist / Vulnerable / Immune";
			this.DamageToolbar.ResumeLayout(false);
			this.DamageToolbar.PerformLayout();
			this.ModPanel.ResumeLayout(false);
			this.ModPanel.PerformLayout();
			this.SpecialGroup.ResumeLayout(false);
			this.SpecialGroup.PerformLayout();
			base.ResumeLayout(false);
		}

		public DamageModListForm(ICreature creature)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fCreature = creature;
			this.fModifiers = new List<DamageModifier>();
			foreach (DamageModifier current in this.fCreature.DamageModifiers)
			{
				this.fModifiers.Add(current.Copy());
			}
			this.update_damage_list();
			this.ResistBox.Text = this.fCreature.Resist;
			this.VulnerableBox.Text = this.fCreature.Vulnerable;
			this.ImmuneBox.Text = this.fCreature.Immune;
		}

		public DamageModListForm(CreatureTemplate template)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fTemplate = template;
			this.fModifierTemplates = new List<DamageModifierTemplate>();
			foreach (DamageModifierTemplate current in this.fTemplate.DamageModifierTemplates)
			{
				this.fModifierTemplates.Add(current.Copy());
			}
			this.update_damage_list();
			this.ResistBox.Text = this.fTemplate.Resist;
			this.VulnerableBox.Text = this.fTemplate.Vulnerable;
			this.ImmuneBox.Text = this.fTemplate.Immune;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveDmgBtn.Enabled = (this.SelectedDamageMod != null || this.SelectedDamageModTemplate != null);
			this.EditDmgBtn.Enabled = (this.SelectedDamageMod != null || this.SelectedDamageModTemplate != null);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.fCreature != null)
			{
				this.fCreature.DamageModifiers = this.fModifiers;
				this.fCreature.Resist = this.ResistBox.Text;
				this.fCreature.Vulnerable = this.VulnerableBox.Text;
				this.fCreature.Immune = this.ImmuneBox.Text;
			}
			if (this.fTemplate != null)
			{
				this.fTemplate.DamageModifierTemplates = this.fModifierTemplates;
				this.fTemplate.Resist = this.ResistBox.Text;
				this.fTemplate.Vulnerable = this.VulnerableBox.Text;
				this.fTemplate.Immune = this.ImmuneBox.Text;
			}
		}

		private void AddDmgBtn_Click(object sender, EventArgs e)
		{
			if (this.fCreature != null)
			{
				DamageModifier dm = new DamageModifier();
				DamageModifierForm damageModifierForm = new DamageModifierForm(dm);
				if (damageModifierForm.ShowDialog() == DialogResult.OK)
				{
					this.fModifiers.Add(damageModifierForm.Modifier);
					this.update_damage_list();
				}
			}
			if (this.fTemplate != null)
			{
				DamageModifierTemplate dmt = new DamageModifierTemplate();
				DamageModifierTemplateForm damageModifierTemplateForm = new DamageModifierTemplateForm(dmt);
				if (damageModifierTemplateForm.ShowDialog() == DialogResult.OK)
				{
					this.fModifierTemplates.Add(damageModifierTemplateForm.Modifier);
					this.update_damage_list();
				}
			}
		}

		private void RemoveDmgBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDamageMod != null)
			{
				this.fModifiers.Remove(this.SelectedDamageMod);
				this.update_damage_list();
			}
			if (this.SelectedDamageModTemplate != null)
			{
				this.fModifierTemplates.Remove(this.SelectedDamageModTemplate);
				this.update_damage_list();
			}
		}

		private void EditDmgBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDamageMod != null)
			{
				int index = this.fModifiers.IndexOf(this.SelectedDamageMod);
				DamageModifierForm damageModifierForm = new DamageModifierForm(this.SelectedDamageMod);
				if (damageModifierForm.ShowDialog() == DialogResult.OK)
				{
					this.fModifiers[index] = damageModifierForm.Modifier;
					this.update_damage_list();
				}
			}
			if (this.SelectedDamageModTemplate != null)
			{
				int index2 = this.fModifierTemplates.IndexOf(this.SelectedDamageModTemplate);
				DamageModifierTemplateForm damageModifierTemplateForm = new DamageModifierTemplateForm(this.SelectedDamageModTemplate);
				if (damageModifierTemplateForm.ShowDialog() == DialogResult.OK)
				{
					this.fModifierTemplates[index2] = damageModifierTemplateForm.Modifier;
					this.update_damage_list();
				}
			}
		}

		private void update_damage_list()
		{
			this.DamageList.Items.Clear();
			this.DamageList.ShowGroups = true;
			if (this.fModifiers != null)
			{
				foreach (DamageModifier current in this.fModifiers)
				{
					ListViewItem listViewItem = this.DamageList.Items.Add(current.ToString());
					listViewItem.Tag = current;
					if (current.Value == 0)
					{
						listViewItem.Group = this.DamageList.Groups[0];
					}
					if (current.Value < 0)
					{
						listViewItem.Group = this.DamageList.Groups[1];
					}
					if (current.Value > 0)
					{
						listViewItem.Group = this.DamageList.Groups[2];
					}
				}
			}
			if (this.fModifierTemplates != null)
			{
				foreach (DamageModifierTemplate current2 in this.fModifierTemplates)
				{
					ListViewItem listViewItem2 = this.DamageList.Items.Add(current2.ToString());
					listViewItem2.Tag = current2;
					int num = current2.HeroicValue + current2.ParagonValue + current2.EpicValue;
					if (num == 0)
					{
						listViewItem2.Group = this.DamageList.Groups[0];
					}
					if (num < 0)
					{
						listViewItem2.Group = this.DamageList.Groups[1];
					}
					if (num > 0)
					{
						listViewItem2.Group = this.DamageList.Groups[2];
					}
				}
			}
			if (this.DamageList.Items.Count == 0)
			{
				this.DamageList.ShowGroups = false;
				ListViewItem listViewItem3 = this.DamageList.Items.Add("(none)");
				listViewItem3.ForeColor = SystemColors.GrayText;
			}
		}
	}
}
