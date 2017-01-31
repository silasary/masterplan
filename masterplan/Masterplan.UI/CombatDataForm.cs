using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CombatDataForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label InitLbl;

		private NumericUpDown InitBox;

		private Label DamageLbl;

		private NumericUpDown DamageBox;

		private TextBox HPBox;

		private Panel ConditionPanel;

		private ListView EffectList;

		private ToolStrip Toolbar;

		private ColumnHeader EffectHdr;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ToolStripButton AddBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton SavesBtn;

		private ToolStripButton DmgBtn;

		private NumericUpDown TempHPBox;

		private Label TempHPLbl;

		private HitPointGauge HPGauge;

		private Label LabelLbl;

		private TextBox LabelBox;

		private ColumnHeader EffectDurationHdr;

		private NumericUpDown AltitudeBox;

		private Label AltitudeLbl;

		private CombatData fData;

		private EncounterCard fCard;

		private Encounter fEncounter;

		private CombatData fCurrentActor;

		private int fCurrentRound = -2147483648;

		public CombatData Data
		{
			get
			{
				return this.fData;
			}
		}

		public OngoingCondition SelectedCondition
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
			ListViewGroup listViewGroup = new ListViewGroup("Ongoing Conditions", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Ongoing Damage", HorizontalAlignment.Left);
			ComponentResourceManager resources = new ComponentResourceManager(typeof(CombatDataForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.InitLbl = new Label();
			this.InitBox = new NumericUpDown();
			this.DamageLbl = new Label();
			this.DamageBox = new NumericUpDown();
			this.HPBox = new TextBox();
			this.ConditionPanel = new Panel();
			this.EffectList = new ListView();
			this.EffectHdr = new ColumnHeader();
			this.EffectDurationHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.DmgBtn = new ToolStripButton();
			this.SavesBtn = new ToolStripButton();
			this.TempHPBox = new NumericUpDown();
			this.TempHPLbl = new Label();
			this.LabelLbl = new Label();
			this.LabelBox = new TextBox();
			this.HPGauge = new HitPointGauge();
			this.AltitudeBox = new NumericUpDown();
			this.AltitudeLbl = new Label();
			((ISupportInitialize)this.InitBox).BeginInit();
			((ISupportInitialize)this.DamageBox).BeginInit();
			this.ConditionPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			((ISupportInitialize)this.TempHPBox).BeginInit();
			((ISupportInitialize)this.AltitudeBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(231, 351);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 13;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(312, 351);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 14;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.InitLbl.AutoSize = true;
			this.InitLbl.Location = new Point(12, 143);
			this.InitLbl.Name = "InitLbl";
			this.InitLbl.Size = new Size(49, 13);
			this.InitLbl.TabIndex = 7;
			this.InitLbl.Text = "Initiative:";
			this.InitBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InitBox.Location = new Point(73, 141);
			this.InitBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.InitBox.Name = "InitBox";
			this.InitBox.Size = new Size(276, 20);
			this.InitBox.TabIndex = 8;
			this.DamageLbl.AutoSize = true;
			this.DamageLbl.Location = new Point(12, 53);
			this.DamageLbl.Name = "DamageLbl";
			this.DamageLbl.Size = new Size(50, 13);
			this.DamageLbl.TabIndex = 2;
			this.DamageLbl.Text = "Damage:";
			this.DamageBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageBox.Location = new Point(73, 51);
			NumericUpDown arg_418_0 = this.DamageBox;
			int[] array = new int[4];
			array[0] = 1000;
			arg_418_0.Maximum = new decimal(array);
			this.DamageBox.Name = "DamageBox";
			this.DamageBox.Size = new Size(276, 20);
			this.DamageBox.TabIndex = 3;
			this.DamageBox.ValueChanged += new EventHandler(this.DamageBox_ValueChanged);
			this.HPBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HPBox.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.HPBox.Location = new Point(73, 103);
			this.HPBox.Name = "HPBox";
			this.HPBox.ReadOnly = true;
			this.HPBox.Size = new Size(276, 20);
			this.HPBox.TabIndex = 6;
			this.HPBox.TabStop = false;
			this.HPBox.TextAlign = HorizontalAlignment.Center;
			this.ConditionPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ConditionPanel.BorderStyle = BorderStyle.FixedSingle;
			this.ConditionPanel.Controls.Add(this.EffectList);
			this.ConditionPanel.Controls.Add(this.Toolbar);
			this.ConditionPanel.Location = new Point(12, 193);
			this.ConditionPanel.Name = "ConditionPanel";
			this.ConditionPanel.Size = new Size(337, 152);
			this.ConditionPanel.TabIndex = 11;
			this.EffectList.BorderStyle = BorderStyle.None;
			this.EffectList.Columns.AddRange(new ColumnHeader[]
			{
				this.EffectHdr,
				this.EffectDurationHdr
			});
			this.EffectList.Dock = DockStyle.Fill;
			this.EffectList.FullRowSelect = true;
			listViewGroup.Header = "Ongoing Conditions";
			listViewGroup.Name = "ConditionHdr";
			listViewGroup2.Header = "Ongoing Damage";
			listViewGroup2.Name = "DmgHdr";
			this.EffectList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2
			});
			this.EffectList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EffectList.HideSelection = false;
			this.EffectList.Location = new Point(0, 25);
			this.EffectList.MultiSelect = false;
			this.EffectList.Name = "EffectList";
			this.EffectList.Size = new Size(335, 125);
			this.EffectList.TabIndex = 1;
			this.EffectList.TileSize = new Size(200, 30);
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Tile;
			this.EffectList.SizeChanged += new EventHandler(this.EffectList_SizeChanged);
			this.EffectList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.EffectHdr.Text = "Effect";
			this.EffectHdr.Width = 120;
			this.EffectDurationHdr.Text = "Duration";
			this.EffectDurationHdr.Width = 141;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddBtn,
				this.RemoveBtn,
				this.EditBtn,
				this.toolStripSeparator1,
				this.DmgBtn,
				this.SavesBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(335, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)resources.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new Size(33, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)resources.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.DmgBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DmgBtn.Image = (Image)resources.GetObject("DmgBtn.Image");
			this.DmgBtn.ImageTransparentColor = Color.Magenta;
			this.DmgBtn.Name = "DmgBtn";
			this.DmgBtn.Size = new Size(105, 22);
			this.DmgBtn.Text = "Ongoing Damage";
			this.DmgBtn.Click += new EventHandler(this.DmgBtn_Click);
			this.SavesBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SavesBtn.Image = (Image)resources.GetObject("SavesBtn.Image");
			this.SavesBtn.ImageTransparentColor = Color.Magenta;
			this.SavesBtn.Name = "SavesBtn";
			this.SavesBtn.Size = new Size(40, 22);
			this.SavesBtn.Text = "Saves";
			this.SavesBtn.Click += new EventHandler(this.SavesBtn_Click);
			this.TempHPBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TempHPBox.Location = new Point(73, 77);
			NumericUpDown arg_ACC_0 = this.TempHPBox;
			int[] array2 = new int[4];
			array2[0] = 1000;
			arg_ACC_0.Maximum = new decimal(array2);
			this.TempHPBox.Name = "TempHPBox";
			this.TempHPBox.Size = new Size(276, 20);
			this.TempHPBox.TabIndex = 5;
			this.TempHPBox.ValueChanged += new EventHandler(this.TempHPBox_ValueChanged);
			this.TempHPLbl.AutoSize = true;
			this.TempHPLbl.Location = new Point(12, 79);
			this.TempHPLbl.Name = "TempHPLbl";
			this.TempHPLbl.Size = new Size(55, 13);
			this.TempHPLbl.TabIndex = 4;
			this.TempHPLbl.Text = "Temp HP:";
			this.LabelLbl.AutoSize = true;
			this.LabelLbl.Location = new Point(12, 15);
			this.LabelLbl.Name = "LabelLbl";
			this.LabelLbl.Size = new Size(36, 13);
			this.LabelLbl.TabIndex = 0;
			this.LabelLbl.Text = "Label:";
			this.LabelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LabelBox.Location = new Point(73, 12);
			this.LabelBox.Name = "LabelBox";
			this.LabelBox.Size = new Size(314, 20);
			this.LabelBox.TabIndex = 1;
			this.HPGauge.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.HPGauge.Damage = 0;
			this.HPGauge.FullHP = 0;
			this.HPGauge.Location = new Point(355, 51);
			this.HPGauge.Name = "HPGauge";
			this.HPGauge.Size = new Size(32, 294);
			this.HPGauge.TabIndex = 12;
			this.HPGauge.TempHP = 0;
			this.AltitudeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.AltitudeBox.Location = new Point(73, 167);
			this.AltitudeBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.AltitudeBox.Name = "AltitudeBox";
			this.AltitudeBox.Size = new Size(276, 20);
			this.AltitudeBox.TabIndex = 10;
			this.AltitudeLbl.AutoSize = true;
			this.AltitudeLbl.Location = new Point(12, 169);
			this.AltitudeLbl.Name = "AltitudeLbl";
			this.AltitudeLbl.Size = new Size(45, 13);
			this.AltitudeLbl.TabIndex = 9;
			this.AltitudeLbl.Text = "Altitude:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(399, 386);
			base.Controls.Add(this.AltitudeBox);
			base.Controls.Add(this.AltitudeLbl);
			base.Controls.Add(this.LabelBox);
			base.Controls.Add(this.LabelLbl);
			base.Controls.Add(this.HPGauge);
			base.Controls.Add(this.TempHPBox);
			base.Controls.Add(this.TempHPLbl);
			base.Controls.Add(this.ConditionPanel);
			base.Controls.Add(this.HPBox);
			base.Controls.Add(this.DamageBox);
			base.Controls.Add(this.DamageLbl);
			base.Controls.Add(this.InitBox);
			base.Controls.Add(this.InitLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CombatDataForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Combatant";
			((ISupportInitialize)this.InitBox).EndInit();
			((ISupportInitialize)this.DamageBox).EndInit();
			this.ConditionPanel.ResumeLayout(false);
			this.ConditionPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			((ISupportInitialize)this.TempHPBox).EndInit();
			((ISupportInitialize)this.AltitudeBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public CombatDataForm(CombatData data, EncounterCard card, Encounter enc, CombatData current_actor, int current_round, bool allow_name_edit)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.EffectList_SizeChanged(null, null);
			this.fData = data.Copy();
			this.fCard = card;
			this.fEncounter = enc;
			this.fCurrentActor = current_actor;
			this.fCurrentRound = current_round;
			if (this.fData.Initiative == -2147483648)
			{
				this.fData.Initiative = 0;
			}
			this.Text = this.fData.DisplayName;
			this.LabelBox.Text = this.fData.DisplayName;
			if (!allow_name_edit)
			{
				this.LabelBox.Enabled = false;
			}
			this.update_hp();
			this.InitBox.Value = this.fData.Initiative;
			this.AltitudeBox.Value = this.fData.Altitude;
			this.update_effects();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			bool enabled = false;
			foreach (OngoingCondition current in this.fData.Conditions)
			{
				if (current.Type == OngoingType.Damage && current.Value > 0)
				{
					enabled = true;
					break;
				}
			}
			this.RemoveBtn.Enabled = (this.SelectedCondition != null);
			this.EditBtn.Enabled = (this.SelectedCondition != null);
			this.SavesBtn.Enabled = (this.fData.Conditions.Count > 0);
			this.DmgBtn.Enabled = enabled;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fData.DisplayName = this.LabelBox.Text;
			this.fData.Initiative = (int)this.InitBox.Value;
			this.fData.Altitude = (int)this.AltitudeBox.Value;
		}

		private void DamageBox_ValueChanged(object sender, EventArgs e)
		{
			this.fData.Damage = (int)this.DamageBox.Value;
			this.update_hp();
		}

		private void TempHPBox_ValueChanged(object sender, EventArgs e)
		{
			this.fData.TempHP = (int)this.TempHPBox.Value;
			this.update_hp();
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			OngoingCondition condition = new OngoingCondition();
			EffectForm effectForm = new EffectForm(condition, this.fEncounter, this.fCurrentActor, this.fCurrentRound);
			if (effectForm.ShowDialog() == DialogResult.OK)
			{
				this.fData.Conditions.Add(effectForm.Effect);
				this.update_effects();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCondition != null)
			{
				this.fData.Conditions.Remove(this.SelectedCondition);
				this.update_effects();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCondition != null)
			{
				int index = this.fData.Conditions.IndexOf(this.SelectedCondition);
				EffectForm effectForm = new EffectForm(this.SelectedCondition, this.fEncounter, this.fCurrentActor, this.fCurrentRound);
				if (effectForm.ShowDialog() == DialogResult.OK)
				{
					this.fData.Conditions[index] = effectForm.Effect;
					this.update_effects();
				}
			}
		}

		private void DmgBtn_Click(object sender, EventArgs e)
		{
			OngoingDamageForm ongoingDamageForm = new OngoingDamageForm(this.fData, this.fCard, this.fEncounter);
			if (ongoingDamageForm.ShowDialog() == DialogResult.OK)
			{
				this.update_hp();
			}
		}

		private void SavesBtn_Click(object sender, EventArgs e)
		{
			SavingThrowForm savingThrowForm = new SavingThrowForm(this.fData, this.fCard, this.fEncounter);
			if (savingThrowForm.ShowDialog() == DialogResult.OK)
			{
				this.update_effects();
			}
		}

		private void update_hp()
		{
			this.DamageBox.Value = this.fData.Damage;
			this.TempHPBox.Value = this.fData.TempHP;
			int num = 0;
			if (this.fCard != null)
			{
				num = this.fCard.HP;
			}
			else
			{
				foreach (Hero current in Session.Project.Heroes)
				{
					if (this.fData.DisplayName == current.Name)
					{
						num = current.HP;
					}
				}
			}
			int num2 = num - this.fData.Damage;
			this.HPBox.Text = num2 + " HP";
			if (this.fData.TempHP > 0)
			{
				TextBox expr_DD = this.HPBox;
				object text = expr_DD.Text;
				expr_DD.Text = string.Concat(new object[]
				{
					text,
					"; ",
					this.fData.TempHP,
					" temp HP"
				});
			}
			if (num2 + this.fData.TempHP <= 0)
			{
				TextBox expr_13B = this.HPBox;
				expr_13B.Text += " (dead)";
			}
			else if (num2 <= num / 2)
			{
				TextBox expr_15E = this.HPBox;
				expr_15E.Text += " (bloodied)";
			}
			this.HPGauge.FullHP = num;
			this.HPGauge.Damage = this.fData.Damage;
			this.HPGauge.TempHP = this.fData.TempHP;
		}

		private void update_effects()
		{
			this.EffectList.Items.Clear();
			this.EffectList.ShowGroups = true;
			foreach (OngoingCondition current in this.fData.Conditions)
			{
				string text = current.ToString();
				string text2 = current.GetDuration(this.fEncounter);
				if (text2 == "")
				{
					text2 = "until the end of the encounter";
				}
				ListViewItem listViewItem = this.EffectList.Items.Add(text);
				listViewItem.SubItems.Add(text2);
				listViewItem.Tag = current;
				listViewItem.Group = this.EffectList.Groups[(current.Type == OngoingType.Condition) ? 0 : 1];
			}
			if (this.fData.Conditions.Count == 0)
			{
				this.EffectList.ShowGroups = false;
				ListViewItem listViewItem2 = this.EffectList.Items.Add("(no ongoing effects)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
		}

		private void EffectList_SizeChanged(object sender, EventArgs e)
		{
			int width = this.EffectList.Width - (SystemInformation.VerticalScrollBarWidth + 6);
			this.EffectList.TileSize = new Size(width, this.EffectList.TileSize.Height);
		}
	}
}
