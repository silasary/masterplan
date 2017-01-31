using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class SavingThrowForm : Form
	{
		private IContainer components;

		private Button CancelBtn;

		private Button OKBtn;

		private Label ModLbl;

		private NumericUpDown ModBox;

		private Panel ListPanel;

		private ListView EffectList;

		private ColumnHeader EffectHdr;

		private ColumnHeader SaveHdr;

		private ToolStrip Toolbar;

		private ToolStripButton AddBtn;

		private ToolStripButton SubtractBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton RollBtn;

		private Label InfoLbl;

		private ColumnHeader ResultHdr;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton SavedBtn;

		private ToolStripButton NotSavedBtn;

		private CombatData fData;

		private EncounterCard fCard;

		private Encounter fEncounter;

		private Dictionary<OngoingCondition, int> fRolls = new Dictionary<OngoingCondition, int>();

		public OngoingCondition SelectedEffect
		{
			get
			{
				if (this.EffectList.SelectedItems.Count != 0)
				{
					return this.EffectList.SelectedItems[0].Tag as OngoingCondition;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SavingThrowForm));
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.ModLbl = new Label();
			this.ModBox = new NumericUpDown();
			this.ListPanel = new Panel();
			this.EffectList = new ListView();
			this.EffectHdr = new ColumnHeader();
			this.SaveHdr = new ColumnHeader();
			this.ResultHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.SubtractBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.RollBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.SavedBtn = new ToolStripButton();
			this.NotSavedBtn = new ToolStripButton();
			this.InfoLbl = new Label();
			((ISupportInitialize)this.ModBox).BeginInit();
			this.ListPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(415, 277);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(334, 277);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.ModLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.ModLbl.AutoSize = true;
			this.ModLbl.Location = new Point(12, 282);
			this.ModLbl.Name = "ModLbl";
			this.ModLbl.Size = new Size(47, 13);
			this.ModLbl.TabIndex = 2;
			this.ModLbl.Text = "Modifier:";
			this.ModBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ModBox.Location = new Point(65, 280);
			this.ModBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.ModBox.Name = "ModBox";
			this.ModBox.Size = new Size(263, 20);
			this.ModBox.TabIndex = 3;
			this.ModBox.ValueChanged += new EventHandler(this.ModBox_ValueChanged);
			this.ListPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ListPanel.Controls.Add(this.EffectList);
			this.ListPanel.Controls.Add(this.Toolbar);
			this.ListPanel.Location = new Point(12, 33);
			this.ListPanel.Name = "ListPanel";
			this.ListPanel.Size = new Size(478, 238);
			this.ListPanel.TabIndex = 1;
			this.EffectList.Columns.AddRange(new ColumnHeader[]
			{
				this.EffectHdr,
				this.SaveHdr,
				this.ResultHdr
			});
			this.EffectList.Dock = DockStyle.Fill;
			this.EffectList.FullRowSelect = true;
			this.EffectList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EffectList.HideSelection = false;
			this.EffectList.Location = new Point(0, 25);
			this.EffectList.MultiSelect = false;
			this.EffectList.Name = "EffectList";
			this.EffectList.Size = new Size(478, 213);
			this.EffectList.TabIndex = 1;
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Details;
			this.EffectHdr.Text = "Effect";
			this.EffectHdr.Width = 257;
			this.SaveHdr.Text = "Roll";
			this.SaveHdr.TextAlign = HorizontalAlignment.Center;
			this.SaveHdr.Width = 76;
			this.ResultHdr.Text = "Result";
			this.ResultHdr.Width = 111;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddBtn,
				this.SubtractBtn,
				this.toolStripSeparator1,
				this.RollBtn,
				this.toolStripSeparator2,
				this.SavedBtn,
				this.NotSavedBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(478, 25);
			this.Toolbar.TabIndex = 2;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new Size(23, 22);
			this.AddBtn.Text = "+";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
			this.SubtractBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SubtractBtn.Image = (Image)componentResourceManager.GetObject("SubtractBtn.Image");
			this.SubtractBtn.ImageTransparentColor = Color.Magenta;
			this.SubtractBtn.Name = "SubtractBtn";
			this.SubtractBtn.Size = new Size(23, 22);
			this.SubtractBtn.Text = "-";
			this.SubtractBtn.Click += new EventHandler(this.SubtractBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.RollBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RollBtn.Image = (Image)componentResourceManager.GetObject("RollBtn.Image");
			this.RollBtn.ImageTransparentColor = Color.Magenta;
			this.RollBtn.Name = "RollBtn";
			this.RollBtn.Size = new Size(41, 22);
			this.RollBtn.Text = "Reroll";
			this.RollBtn.Click += new EventHandler(this.RollBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.SavedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SavedBtn.Image = (Image)componentResourceManager.GetObject("SavedBtn.Image");
			this.SavedBtn.ImageTransparentColor = Color.Magenta;
			this.SavedBtn.Name = "SavedBtn";
			this.SavedBtn.Size = new Size(86, 22);
			this.SavedBtn.Text = "Mark as Saved";
			this.SavedBtn.Click += new EventHandler(this.SavedBtn_Click);
			this.NotSavedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NotSavedBtn.Image = (Image)componentResourceManager.GetObject("NotSavedBtn.Image");
			this.NotSavedBtn.ImageTransparentColor = Color.Magenta;
			this.NotSavedBtn.Name = "NotSavedBtn";
			this.NotSavedBtn.Size = new Size(109, 22);
			this.NotSavedBtn.Text = "Mark as Not Saved";
			this.NotSavedBtn.Click += new EventHandler(this.NotSavedBtn_Click);
			this.InfoLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InfoLbl.Location = new Point(12, 9);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(478, 21);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "Saving throws are due to be rolled against the following effects.";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(502, 312);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.ListPanel);
			base.Controls.Add(this.ModBox);
			base.Controls.Add(this.ModLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SavingThrowForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Saving Throws";
			((ISupportInitialize)this.ModBox).EndInit();
			this.ListPanel.ResumeLayout(false);
			this.ListPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public SavingThrowForm(CombatData data, EncounterCard card, Encounter enc)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fData = data;
			this.fCard = card;
			this.fEncounter = enc;
			this.Text = "Saving Throws: " + this.fData.DisplayName;
			foreach (OngoingCondition current in this.fData.Conditions)
			{
				if (current.Duration == DurationType.SaveEnds)
				{
					int value = (this.fCard != null) ? Session.Dice(1, 20) : 0;
					this.fRolls[current] = value;
				}
			}
			int value2 = 0;
			if (this.fCard != null)
			{
				switch (this.fCard.Flag)
				{
				case RoleFlag.Elite:
					value2 = 2;
					break;
				case RoleFlag.Solo:
					value2 = 5;
					break;
				}
			}
			this.ModBox.Value = value2;
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.AddBtn.Enabled = (this.SelectedEffect != null);
			this.SubtractBtn.Enabled = (this.SelectedEffect != null);
			this.RollBtn.Enabled = (this.SelectedEffect != null);
			if (this.SelectedEffect == null)
			{
				this.SavedBtn.Enabled = false;
				this.NotSavedBtn.Enabled = false;
				return;
			}
			int num = this.fRolls[this.SelectedEffect];
			this.SavedBtn.Enabled = (num != 2147483647);
			this.NotSavedBtn.Enabled = (num != -2147483648);
		}

		private void ModBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			int num = (int)this.ModBox.Value;
			List<OngoingCondition> list = new List<OngoingCondition>();
			foreach (OngoingCondition current in this.fData.Conditions)
			{
				if (current.Duration == DurationType.SaveEnds)
				{
					int num2 = this.fRolls[current];
					if (num2 != 0)
					{
						int num3 = num2 + num;
						if (num3 >= 10)
						{
							list.Add(current);
						}
					}
				}
			}
			foreach (OngoingCondition current2 in list)
			{
				this.fData.Conditions.Remove(current2);
			}
		}

		private void update_list()
		{
			OngoingCondition selectedEffect = this.SelectedEffect;
			this.EffectList.BeginUpdate();
			this.EffectList.Items.Clear();
			foreach (OngoingCondition current in this.fData.Conditions)
			{
				if (current.Duration == DurationType.SaveEnds)
				{
					int num = (int)this.ModBox.Value;
					int num2 = this.fRolls[current];
					ListViewItem listViewItem = this.EffectList.Items.Add(current.ToString(this.fEncounter, false));
					listViewItem.Tag = current;
					if (current == selectedEffect)
					{
						listViewItem.Selected = true;
					}
					if (num2 == 0)
					{
						listViewItem.SubItems.Add("(not rolled)");
						listViewItem.SubItems.Add("(not rolled)");
						listViewItem.ForeColor = SystemColors.GrayText;
					}
					else if (num2 == -2147483648)
					{
						listViewItem.SubItems.Add("-");
						listViewItem.SubItems.Add("Not saved");
					}
					else if (num2 == 2147483647)
					{
						listViewItem.SubItems.Add("-");
						listViewItem.SubItems.Add("Saved");
						listViewItem.ForeColor = SystemColors.GrayText;
					}
					else
					{
						int num3 = num2 + current.SavingThrowModifier + num;
						if (num3 == num2)
						{
							listViewItem.SubItems.Add(num2.ToString());
						}
						else
						{
							listViewItem.SubItems.Add(num2 + " => " + num3);
						}
						if (num3 >= 10)
						{
							listViewItem.SubItems.Add("Saved");
							listViewItem.ForeColor = SystemColors.GrayText;
						}
						else
						{
							listViewItem.SubItems.Add("Not saved");
						}
					}
				}
			}
			if (this.EffectList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.EffectList.Items.Add("(no conditions)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			this.EffectList.EndUpdate();
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				Dictionary<OngoingCondition, int> dictionary;
				OngoingCondition selectedEffect;
				(dictionary = this.fRolls)[selectedEffect = this.SelectedEffect] = dictionary[selectedEffect] + 1;
				this.update_list();
			}
		}

		private void SubtractBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				Dictionary<OngoingCondition, int> dictionary;
				OngoingCondition selectedEffect;
				(dictionary = this.fRolls)[selectedEffect = this.SelectedEffect] = dictionary[selectedEffect] - 1;
				this.fRolls[this.SelectedEffect] = Math.Max(this.fRolls[this.SelectedEffect], 0);
				this.update_list();
			}
		}

		private void RollBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				this.fRolls[this.SelectedEffect] = Session.Dice(1, 20);
				this.update_list();
			}
		}

		private void SavedBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				this.fRolls[this.SelectedEffect] = 2147483647;
				this.update_list();
			}
		}

		private void NotSavedBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				this.fRolls[this.SelectedEffect] = -2147483648;
				this.update_list();
			}
		}
	}
}
