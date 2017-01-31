using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class RechargeForm : Form
	{
		private IContainer components;

		private Button CancelBtn;

		private Button OKBtn;

		private Panel ListPanel;

		private ListView EffectList;

		private ColumnHeader PowerHdr;

		private ColumnHeader SaveHdr;

		private ToolStrip Toolbar;

		private ToolStripButton RollBtn;

		private Label InfoLbl;

		private ColumnHeader ResultHdr;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton SavedBtn;

		private ToolStripButton NotSavedBtn;

		private ColumnHeader RechargeHdr;

		private CombatData fData;

		private EncounterCard fCard;

		private Dictionary<Guid, int> fRolls = new Dictionary<Guid, int>();

		public Guid SelectedPowerID
		{
			get
			{
				if (this.EffectList.SelectedItems.Count != 0)
				{
					return (Guid)this.EffectList.SelectedItems[0].Tag;
				}
				return Guid.Empty;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(RechargeForm));
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.ListPanel = new Panel();
			this.EffectList = new ListView();
			this.PowerHdr = new ColumnHeader();
			this.RechargeHdr = new ColumnHeader();
			this.SaveHdr = new ColumnHeader();
			this.ResultHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.RollBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.SavedBtn = new ToolStripButton();
			this.NotSavedBtn = new ToolStripButton();
			this.InfoLbl = new Label();
			this.ListPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(508, 277);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(427, 277);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.ListPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ListPanel.Controls.Add(this.EffectList);
			this.ListPanel.Controls.Add(this.Toolbar);
			this.ListPanel.Location = new Point(12, 33);
			this.ListPanel.Name = "ListPanel";
			this.ListPanel.Size = new Size(571, 238);
			this.ListPanel.TabIndex = 1;
			this.EffectList.Columns.AddRange(new ColumnHeader[]
			{
				this.PowerHdr,
				this.RechargeHdr,
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
			this.EffectList.Size = new Size(571, 213);
			this.EffectList.TabIndex = 1;
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Details;
			this.PowerHdr.Text = "Power";
			this.PowerHdr.Width = 200;
			this.RechargeHdr.Text = "Recharge Condition";
			this.RechargeHdr.Width = 150;
			this.SaveHdr.Text = "Roll";
			this.SaveHdr.TextAlign = HorizontalAlignment.Center;
			this.SaveHdr.Width = 76;
			this.ResultHdr.Text = "Result";
			this.ResultHdr.Width = 111;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.RollBtn,
				this.toolStripSeparator2,
				this.SavedBtn,
				this.NotSavedBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(571, 25);
			this.Toolbar.TabIndex = 2;
			this.Toolbar.Text = "toolStrip1";
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
			this.SavedBtn.Size = new Size(111, 22);
			this.SavedBtn.Text = "Mark as Recharged";
			this.SavedBtn.Click += new EventHandler(this.SavedBtn_Click);
			this.NotSavedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NotSavedBtn.Image = (Image)componentResourceManager.GetObject("NotSavedBtn.Image");
			this.NotSavedBtn.ImageTransparentColor = Color.Magenta;
			this.NotSavedBtn.Name = "NotSavedBtn";
			this.NotSavedBtn.Size = new Size(134, 22);
			this.NotSavedBtn.Text = "Mark as Not Recharged";
			this.NotSavedBtn.Click += new EventHandler(this.NotSavedBtn_Click);
			this.InfoLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InfoLbl.Location = new Point(12, 9);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(571, 21);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "The following expended powers have recharge conditions.";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(595, 312);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.ListPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RechargeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Recharging";
			this.ListPanel.ResumeLayout(false);
			this.ListPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		public RechargeForm(CombatData data, EncounterCard card)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fData = data;
			this.fCard = card;
			this.Text = "Power Recharging: " + this.fData.DisplayName;
			foreach (Guid current in this.fData.UsedPowers)
			{
				CreaturePower creaturePower = this.get_power(current);
				if (creaturePower != null && creaturePower.Action != null && !(creaturePower.Action.Recharge == ""))
				{
					this.fRolls[current] = Session.Dice(1, 6);
				}
			}
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RollBtn.Enabled = (this.SelectedPowerID != Guid.Empty);
			if (this.SelectedPowerID == Guid.Empty)
			{
				this.SavedBtn.Enabled = false;
				this.NotSavedBtn.Enabled = false;
				return;
			}
			int num = this.fRolls[this.SelectedPowerID];
			this.SavedBtn.Enabled = (num != 2147483647);
			this.NotSavedBtn.Enabled = (num != -2147483648);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			List<Guid> list = new List<Guid>();
			foreach (Guid current in this.fRolls.Keys)
			{
				if (this.fRolls.ContainsKey(current))
				{
					int num = this.fRolls[current];
					CreaturePower creaturePower = this.get_power(current);
					if (creaturePower != null && creaturePower.Action != null && !(creaturePower.Action.Recharge == ""))
					{
						int num2 = this.get_minimum(creaturePower.Action.Recharge);
						if (num2 != 0 && num >= num2)
						{
							list.Add(current);
						}
					}
				}
			}
			foreach (Guid current2 in list)
			{
				this.fData.UsedPowers.Remove(current2);
			}
		}

		private void RollBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPowerID != Guid.Empty)
			{
				this.fRolls[this.SelectedPowerID] = Session.Dice(1, 6);
				this.update_list();
			}
		}

		private void SavedBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPowerID != Guid.Empty)
			{
				this.fRolls[this.SelectedPowerID] = 2147483647;
				this.update_list();
			}
		}

		private void NotSavedBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPowerID != Guid.Empty)
			{
				this.fRolls[this.SelectedPowerID] = -2147483648;
				this.update_list();
			}
		}

		private void update_list()
		{
			Guid selectedPowerID = this.SelectedPowerID;
			this.EffectList.BeginUpdate();
			this.EffectList.Items.Clear();
			foreach (Guid current in this.fData.UsedPowers)
			{
				if (this.fRolls.ContainsKey(current))
				{
					CreaturePower creaturePower = this.get_power(current);
					if (creaturePower != null)
					{
						int num = this.fRolls[current];
						ListViewItem listViewItem = this.EffectList.Items.Add(creaturePower.Name);
						listViewItem.SubItems.Add(creaturePower.Action.Recharge);
						listViewItem.Tag = creaturePower.ID;
						if (current == selectedPowerID)
						{
							listViewItem.Selected = true;
						}
						if (num == -2147483648)
						{
							listViewItem.SubItems.Add("-");
							listViewItem.SubItems.Add("Not recharged");
						}
						else if (num == 2147483647)
						{
							listViewItem.SubItems.Add("-");
							listViewItem.SubItems.Add("Recharged");
							listViewItem.ForeColor = SystemColors.GrayText;
						}
						else
						{
							int num2 = this.get_minimum(creaturePower.Action.Recharge);
							if (num2 == 2147483647)
							{
								listViewItem.SubItems.Add("Not rolled");
								listViewItem.SubItems.Add("Not rolled");
							}
							else
							{
								listViewItem.SubItems.Add(num.ToString());
								if (num >= num2)
								{
									listViewItem.SubItems.Add("Recharged");
									listViewItem.ForeColor = SystemColors.GrayText;
								}
								else
								{
									listViewItem.SubItems.Add("Not recharged");
								}
							}
						}
					}
				}
			}
			if (this.EffectList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.EffectList.Items.Add("(no rechargable powers)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			this.EffectList.EndUpdate();
		}

		private CreaturePower get_power(Guid power_id)
		{
			List<CreaturePower> creaturePowers = this.fCard.CreaturePowers;
			foreach (CreaturePower current in creaturePowers)
			{
				if (current.ID == power_id)
				{
					return current;
				}
			}
			return null;
		}

		private int get_minimum(string recharge_str)
		{
			int result = 2147483647;
			if (recharge_str.Contains("6"))
			{
				result = 6;
			}
			if (recharge_str.Contains("5"))
			{
				result = 5;
			}
			if (recharge_str.Contains("4"))
			{
				result = 4;
			}
			if (recharge_str.Contains("3"))
			{
				result = 3;
			}
			if (recharge_str.Contains("2"))
			{
				result = 2;
			}
			return result;
		}
	}
}
