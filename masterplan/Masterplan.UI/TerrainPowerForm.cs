using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TerrainPowerForm : Form
	{
		private TerrainPower fPower;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Label TypeLbl;

		private ComboBox TypeBox;

		private Label ActionLbl;

		private ComboBox ActionBox;

		private TabControl Pages;

		private TabPage GeneralPage;

		private TabPage CheckPage;

		private TabPage AttackPage;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox RequirementBox;

		private Label RequirementLbl;

		private TextBox FlavourBox;

		private Label FlavourLbl;

		private TextBox SuccessBox;

		private Label SuccessLbl;

		private TextBox FailureBox;

		private Label FailureLbl;

		private TextBox CheckBox;

		private Label CheckLbl;

		private TextBox TargetBox;

		private Label TargetLbl;

		private TextBox HitBox;

		private Label HitLbl;

		private TextBox MissBox;

		private Label MissLbl;

		private TextBox EffectBox;

		private Label EffectLbl;

		private TextBox AttackBox;

		private Label AttackLbl;

		public TerrainPower Power
		{
			get
			{
				return this.fPower;
			}
		}

		public TerrainPowerForm(TerrainPower power)
		{
			this.InitializeComponent();
			Array values = Enum.GetValues(typeof(TerrainPowerType));
			foreach (TerrainPowerType terrainPowerType in values)
			{
				this.TypeBox.Items.Add(terrainPowerType);
			}
			Array values2 = Enum.GetValues(typeof(ActionType));
			foreach (ActionType actionType in values2)
			{
				this.ActionBox.Items.Add(actionType);
			}
			this.fPower = power.Copy();
			this.NameBox.Text = this.fPower.Name;
			this.TypeBox.SelectedItem = this.fPower.Type;
			this.ActionBox.SelectedItem = this.fPower.Action;
			this.FlavourBox.Text = this.fPower.FlavourText;
			this.RequirementBox.Text = this.fPower.Requirement;
			this.CheckBox.Text = this.fPower.Check;
			this.SuccessBox.Text = this.fPower.Success;
			this.FailureBox.Text = this.fPower.Failure;
			this.AttackBox.Text = this.fPower.Attack;
			this.TargetBox.Text = this.fPower.Target;
			this.HitBox.Text = this.fPower.Hit;
			this.MissBox.Text = this.fPower.Miss;
			this.EffectBox.Text = this.fPower.Effect;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fPower.Name = this.NameBox.Text;
			this.fPower.Type = (TerrainPowerType)this.TypeBox.SelectedItem;
			this.fPower.Action = (ActionType)this.ActionBox.SelectedItem;
			this.fPower.FlavourText = this.FlavourBox.Text;
			this.fPower.Requirement = this.RequirementBox.Text;
			this.fPower.Check = this.CheckBox.Text;
			this.fPower.Success = this.SuccessBox.Text;
			this.fPower.Failure = this.FailureBox.Text;
			this.fPower.Attack = this.AttackBox.Text;
			this.fPower.Target = this.TargetBox.Text;
			this.fPower.Hit = this.HitBox.Text;
			this.fPower.Miss = this.MissBox.Text;
			this.fPower.Effect = this.EffectBox.Text;
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
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.TypeLbl = new Label();
			this.TypeBox = new ComboBox();
			this.ActionLbl = new Label();
			this.ActionBox = new ComboBox();
			this.Pages = new TabControl();
			this.GeneralPage = new TabPage();
			this.RequirementBox = new TextBox();
			this.RequirementLbl = new Label();
			this.FlavourBox = new TextBox();
			this.FlavourLbl = new Label();
			this.CheckPage = new TabPage();
			this.SuccessBox = new TextBox();
			this.SuccessLbl = new Label();
			this.FailureBox = new TextBox();
			this.FailureLbl = new Label();
			this.CheckBox = new TextBox();
			this.CheckLbl = new Label();
			this.AttackPage = new TabPage();
			this.TargetBox = new TextBox();
			this.TargetLbl = new Label();
			this.HitBox = new TextBox();
			this.HitLbl = new Label();
			this.MissBox = new TextBox();
			this.MissLbl = new Label();
			this.EffectBox = new TextBox();
			this.EffectLbl = new Label();
			this.AttackBox = new TextBox();
			this.AttackLbl = new Label();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Pages.SuspendLayout();
			this.GeneralPage.SuspendLayout();
			this.CheckPage.SuspendLayout();
			this.AttackPage.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(58, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(276, 20);
			this.NameBox.TabIndex = 1;
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(12, 41);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new Size(34, 13);
			this.TypeLbl.TabIndex = 2;
			this.TypeLbl.Text = "Type:";
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(58, 38);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new Size(276, 21);
			this.TypeBox.TabIndex = 3;
			this.ActionLbl.AutoSize = true;
			this.ActionLbl.Location = new Point(12, 68);
			this.ActionLbl.Name = "ActionLbl";
			this.ActionLbl.Size = new Size(40, 13);
			this.ActionLbl.TabIndex = 4;
			this.ActionLbl.Text = "Action:";
			this.ActionBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ActionBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.ActionBox.FormattingEnabled = true;
			this.ActionBox.Location = new Point(58, 65);
			this.ActionBox.Name = "ActionBox";
			this.ActionBox.Size = new Size(276, 21);
			this.ActionBox.TabIndex = 5;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.GeneralPage);
			this.Pages.Controls.Add(this.CheckPage);
			this.Pages.Controls.Add(this.AttackPage);
			this.Pages.Location = new Point(12, 92);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(322, 275);
			this.Pages.TabIndex = 6;
			this.GeneralPage.Controls.Add(this.RequirementBox);
			this.GeneralPage.Controls.Add(this.RequirementLbl);
			this.GeneralPage.Controls.Add(this.FlavourBox);
			this.GeneralPage.Controls.Add(this.FlavourLbl);
			this.GeneralPage.Location = new Point(4, 22);
			this.GeneralPage.Name = "GeneralPage";
			this.GeneralPage.Padding = new Padding(3);
			this.GeneralPage.Size = new Size(314, 249);
			this.GeneralPage.TabIndex = 0;
			this.GeneralPage.Text = "General";
			this.GeneralPage.UseVisualStyleBackColor = true;
			this.RequirementBox.AcceptsReturn = true;
			this.RequirementBox.AcceptsTab = true;
			this.RequirementBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.RequirementBox.Location = new Point(81, 151);
			this.RequirementBox.Multiline = true;
			this.RequirementBox.Name = "RequirementBox";
			this.RequirementBox.ScrollBars = ScrollBars.Vertical;
			this.RequirementBox.Size = new Size(227, 92);
			this.RequirementBox.TabIndex = 3;
			this.RequirementLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.RequirementLbl.AutoSize = true;
			this.RequirementLbl.Location = new Point(6, 154);
			this.RequirementLbl.Name = "RequirementLbl";
			this.RequirementLbl.Size = new Size(70, 13);
			this.RequirementLbl.TabIndex = 2;
			this.RequirementLbl.Text = "Requirement:";
			this.FlavourBox.AcceptsReturn = true;
			this.FlavourBox.AcceptsTab = true;
			this.FlavourBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.FlavourBox.Location = new Point(81, 6);
			this.FlavourBox.Multiline = true;
			this.FlavourBox.Name = "FlavourBox";
			this.FlavourBox.ScrollBars = ScrollBars.Vertical;
			this.FlavourBox.Size = new Size(227, 139);
			this.FlavourBox.TabIndex = 1;
			this.FlavourLbl.AutoSize = true;
			this.FlavourLbl.Location = new Point(6, 9);
			this.FlavourLbl.Name = "FlavourLbl";
			this.FlavourLbl.Size = new Size(69, 13);
			this.FlavourLbl.TabIndex = 0;
			this.FlavourLbl.Text = "Flavour Text:";
			this.CheckPage.Controls.Add(this.SuccessBox);
			this.CheckPage.Controls.Add(this.SuccessLbl);
			this.CheckPage.Controls.Add(this.FailureBox);
			this.CheckPage.Controls.Add(this.FailureLbl);
			this.CheckPage.Controls.Add(this.CheckBox);
			this.CheckPage.Controls.Add(this.CheckLbl);
			this.CheckPage.Location = new Point(4, 22);
			this.CheckPage.Name = "CheckPage";
			this.CheckPage.Padding = new Padding(3);
			this.CheckPage.Size = new Size(314, 249);
			this.CheckPage.TabIndex = 2;
			this.CheckPage.Text = "Check";
			this.CheckPage.UseVisualStyleBackColor = true;
			this.SuccessBox.AcceptsReturn = true;
			this.SuccessBox.AcceptsTab = true;
			this.SuccessBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.SuccessBox.Location = new Point(62, 99);
			this.SuccessBox.Multiline = true;
			this.SuccessBox.Name = "SuccessBox";
			this.SuccessBox.ScrollBars = ScrollBars.Vertical;
			this.SuccessBox.Size = new Size(246, 69);
			this.SuccessBox.TabIndex = 3;
			this.SuccessLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.SuccessLbl.AutoSize = true;
			this.SuccessLbl.Location = new Point(5, 99);
			this.SuccessLbl.Name = "SuccessLbl";
			this.SuccessLbl.Size = new Size(51, 13);
			this.SuccessLbl.TabIndex = 2;
			this.SuccessLbl.Text = "Success:";
			this.FailureBox.AcceptsReturn = true;
			this.FailureBox.AcceptsTab = true;
			this.FailureBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.FailureBox.Location = new Point(62, 174);
			this.FailureBox.Multiline = true;
			this.FailureBox.Name = "FailureBox";
			this.FailureBox.ScrollBars = ScrollBars.Vertical;
			this.FailureBox.Size = new Size(246, 69);
			this.FailureBox.TabIndex = 5;
			this.FailureLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.FailureLbl.AutoSize = true;
			this.FailureLbl.Location = new Point(5, 174);
			this.FailureLbl.Name = "FailureLbl";
			this.FailureLbl.Size = new Size(41, 13);
			this.FailureLbl.TabIndex = 4;
			this.FailureLbl.Text = "Failure:";
			this.CheckBox.AcceptsReturn = true;
			this.CheckBox.AcceptsTab = true;
			this.CheckBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.CheckBox.Location = new Point(62, 6);
			this.CheckBox.Multiline = true;
			this.CheckBox.Name = "CheckBox";
			this.CheckBox.ScrollBars = ScrollBars.Vertical;
			this.CheckBox.Size = new Size(246, 87);
			this.CheckBox.TabIndex = 1;
			this.CheckLbl.AutoSize = true;
			this.CheckLbl.Location = new Point(6, 9);
			this.CheckLbl.Name = "CheckLbl";
			this.CheckLbl.Size = new Size(41, 13);
			this.CheckLbl.TabIndex = 0;
			this.CheckLbl.Text = "Check:";
			this.AttackPage.Controls.Add(this.TargetBox);
			this.AttackPage.Controls.Add(this.TargetLbl);
			this.AttackPage.Controls.Add(this.HitBox);
			this.AttackPage.Controls.Add(this.HitLbl);
			this.AttackPage.Controls.Add(this.MissBox);
			this.AttackPage.Controls.Add(this.MissLbl);
			this.AttackPage.Controls.Add(this.EffectBox);
			this.AttackPage.Controls.Add(this.EffectLbl);
			this.AttackPage.Controls.Add(this.AttackBox);
			this.AttackPage.Controls.Add(this.AttackLbl);
			this.AttackPage.Location = new Point(4, 22);
			this.AttackPage.Name = "AttackPage";
			this.AttackPage.Padding = new Padding(3);
			this.AttackPage.Size = new Size(314, 249);
			this.AttackPage.TabIndex = 3;
			this.AttackPage.Text = "Attack";
			this.AttackPage.UseVisualStyleBackColor = true;
			this.TargetBox.AcceptsReturn = true;
			this.TargetBox.AcceptsTab = true;
			this.TargetBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.TargetBox.Location = new Point(61, 57);
			this.TargetBox.Multiline = true;
			this.TargetBox.Name = "TargetBox";
			this.TargetBox.ScrollBars = ScrollBars.Vertical;
			this.TargetBox.Size = new Size(248, 42);
			this.TargetBox.TabIndex = 3;
			this.TargetLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.TargetLbl.AutoSize = true;
			this.TargetLbl.Location = new Point(7, 60);
			this.TargetLbl.Name = "TargetLbl";
			this.TargetLbl.Size = new Size(41, 13);
			this.TargetLbl.TabIndex = 2;
			this.TargetLbl.Text = "Target:";
			this.HitBox.AcceptsReturn = true;
			this.HitBox.AcceptsTab = true;
			this.HitBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.HitBox.Location = new Point(60, 105);
			this.HitBox.Multiline = true;
			this.HitBox.Name = "HitBox";
			this.HitBox.ScrollBars = ScrollBars.Vertical;
			this.HitBox.Size = new Size(248, 42);
			this.HitBox.TabIndex = 5;
			this.HitLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.HitLbl.AutoSize = true;
			this.HitLbl.Location = new Point(6, 108);
			this.HitLbl.Name = "HitLbl";
			this.HitLbl.Size = new Size(40, 13);
			this.HitLbl.TabIndex = 4;
			this.HitLbl.Text = "On Hit:";
			this.MissBox.AcceptsReturn = true;
			this.MissBox.AcceptsTab = true;
			this.MissBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.MissBox.Location = new Point(60, 153);
			this.MissBox.Multiline = true;
			this.MissBox.Name = "MissBox";
			this.MissBox.ScrollBars = ScrollBars.Vertical;
			this.MissBox.Size = new Size(248, 42);
			this.MissBox.TabIndex = 7;
			this.MissLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.MissLbl.AutoSize = true;
			this.MissLbl.Location = new Point(6, 156);
			this.MissLbl.Name = "MissLbl";
			this.MissLbl.Size = new Size(48, 13);
			this.MissLbl.TabIndex = 6;
			this.MissLbl.Text = "On Miss:";
			this.EffectBox.AcceptsReturn = true;
			this.EffectBox.AcceptsTab = true;
			this.EffectBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.EffectBox.Location = new Point(61, 201);
			this.EffectBox.Multiline = true;
			this.EffectBox.Name = "EffectBox";
			this.EffectBox.ScrollBars = ScrollBars.Vertical;
			this.EffectBox.Size = new Size(248, 42);
			this.EffectBox.TabIndex = 9;
			this.EffectLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.EffectLbl.AutoSize = true;
			this.EffectLbl.Location = new Point(6, 204);
			this.EffectLbl.Name = "EffectLbl";
			this.EffectLbl.Size = new Size(38, 13);
			this.EffectLbl.TabIndex = 8;
			this.EffectLbl.Text = "Effect:";
			this.AttackBox.AcceptsReturn = true;
			this.AttackBox.AcceptsTab = true;
			this.AttackBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.AttackBox.Location = new Point(61, 6);
			this.AttackBox.Multiline = true;
			this.AttackBox.Name = "AttackBox";
			this.AttackBox.ScrollBars = ScrollBars.Vertical;
			this.AttackBox.Size = new Size(248, 45);
			this.AttackBox.TabIndex = 1;
			this.AttackLbl.AutoSize = true;
			this.AttackLbl.Location = new Point(7, 9);
			this.AttackLbl.Name = "AttackLbl";
			this.AttackLbl.Size = new Size(41, 13);
			this.AttackLbl.TabIndex = 0;
			this.AttackLbl.Text = "Attack:";
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(178, 373);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 7;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(259, 373);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 8;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(346, 408);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.ActionBox);
			base.Controls.Add(this.ActionLbl);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.TypeLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TerrainPowerForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Terrain Power";
			this.Pages.ResumeLayout(false);
			this.GeneralPage.ResumeLayout(false);
			this.GeneralPage.PerformLayout();
			this.CheckPage.ResumeLayout(false);
			this.CheckPage.PerformLayout();
			this.AttackPage.ResumeLayout(false);
			this.AttackPage.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
