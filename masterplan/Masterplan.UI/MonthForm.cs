using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class MonthForm : Form
	{
		private MonthInfo fMonthInfo;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label DaysLbl;

		private NumericUpDown DaysBox;

		private NumericUpDown LeapModBox;

		private Label LeapModLbl;

		private NumericUpDown LeapPeriodBox;

		private Label LeapPeriodLbl;

		private GroupBox MonthGroup;

		private GroupBox LeapGroup;

		public MonthInfo MonthInfo
		{
			get
			{
				return this.fMonthInfo;
			}
		}

		public MonthForm(MonthInfo month)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fMonthInfo = month.Copy();
			this.NameBox.Text = this.fMonthInfo.Name;
			this.DaysBox.Value = this.fMonthInfo.DayCount;
			this.LeapModBox.Value = this.fMonthInfo.LeapModifier;
			this.LeapPeriodBox.Value = Math.Max(2, this.fMonthInfo.LeapPeriod);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.LeapPeriodLbl.Enabled = (this.LeapModBox.Value != 0m);
			this.LeapPeriodBox.Enabled = (this.LeapModBox.Value != 0m);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fMonthInfo.Name = this.NameBox.Text;
			this.fMonthInfo.DayCount = (int)this.DaysBox.Value;
			this.fMonthInfo.LeapModifier = (int)this.LeapModBox.Value;
			this.fMonthInfo.LeapPeriod = (int)this.LeapPeriodBox.Value;
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
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.DaysLbl = new Label();
			this.DaysBox = new NumericUpDown();
			this.LeapModBox = new NumericUpDown();
			this.LeapModLbl = new Label();
			this.LeapPeriodBox = new NumericUpDown();
			this.LeapPeriodLbl = new Label();
			this.MonthGroup = new GroupBox();
			this.LeapGroup = new GroupBox();
			((ISupportInitialize)this.DaysBox).BeginInit();
			((ISupportInitialize)this.LeapModBox).BeginInit();
			((ISupportInitialize)this.LeapPeriodBox).BeginInit();
			this.MonthGroup.SuspendLayout();
			this.LeapGroup.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(93, 194);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(174, 194);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 3;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(6, 22);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(59, 19);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(172, 20);
			this.NameBox.TabIndex = 1;
			this.DaysLbl.AutoSize = true;
			this.DaysLbl.Location = new Point(6, 47);
			this.DaysLbl.Name = "DaysLbl";
			this.DaysLbl.Size = new Size(34, 13);
			this.DaysLbl.TabIndex = 2;
			this.DaysLbl.Text = "Days:";
			this.DaysBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DaysBox.Location = new Point(59, 45);
			NumericUpDown arg_31D_0 = this.DaysBox;
			int[] array = new int[4];
			array[0] = 1;
			arg_31D_0.Minimum = new decimal(array);
			this.DaysBox.Name = "DaysBox";
			this.DaysBox.Size = new Size(172, 20);
			this.DaysBox.TabIndex = 3;
			NumericUpDown arg_36C_0 = this.DaysBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_36C_0.Value = new decimal(array2);
			this.LeapModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LeapModBox.Location = new Point(59, 19);
			this.LeapModBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.LeapModBox.Name = "LeapModBox";
			this.LeapModBox.Size = new Size(172, 20);
			this.LeapModBox.TabIndex = 1;
			this.LeapModLbl.AutoSize = true;
			this.LeapModLbl.Location = new Point(6, 21);
			this.LeapModLbl.Name = "LeapModLbl";
			this.LeapModLbl.Size = new Size(34, 13);
			this.LeapModLbl.TabIndex = 0;
			this.LeapModLbl.Text = "Days:";
			this.LeapPeriodBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LeapPeriodBox.Location = new Point(59, 45);
			NumericUpDown arg_481_0 = this.LeapPeriodBox;
			int[] array3 = new int[4];
			array3[0] = 2;
			arg_481_0.Minimum = new decimal(array3);
			this.LeapPeriodBox.Name = "LeapPeriodBox";
			this.LeapPeriodBox.Size = new Size(172, 20);
			this.LeapPeriodBox.TabIndex = 3;
			NumericUpDown arg_4D3_0 = this.LeapPeriodBox;
			int[] array4 = new int[4];
			array4[0] = 4;
			arg_4D3_0.Value = new decimal(array4);
			this.LeapPeriodLbl.AutoSize = true;
			this.LeapPeriodLbl.Location = new Point(6, 47);
			this.LeapPeriodLbl.Name = "LeapPeriodLbl";
			this.LeapPeriodLbl.Size = new Size(40, 13);
			this.LeapPeriodLbl.TabIndex = 2;
			this.LeapPeriodLbl.Text = "Period:";
			this.MonthGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MonthGroup.Controls.Add(this.NameBox);
			this.MonthGroup.Controls.Add(this.NameLbl);
			this.MonthGroup.Controls.Add(this.DaysLbl);
			this.MonthGroup.Controls.Add(this.DaysBox);
			this.MonthGroup.Location = new Point(12, 12);
			this.MonthGroup.Name = "MonthGroup";
			this.MonthGroup.Size = new Size(237, 83);
			this.MonthGroup.TabIndex = 0;
			this.MonthGroup.TabStop = false;
			this.MonthGroup.Text = "Month Info";
			this.LeapGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LeapGroup.Controls.Add(this.LeapPeriodBox);
			this.LeapGroup.Controls.Add(this.LeapModLbl);
			this.LeapGroup.Controls.Add(this.LeapModBox);
			this.LeapGroup.Controls.Add(this.LeapPeriodLbl);
			this.LeapGroup.Location = new Point(12, 101);
			this.LeapGroup.Name = "LeapGroup";
			this.LeapGroup.Size = new Size(237, 80);
			this.LeapGroup.TabIndex = 1;
			this.LeapGroup.TabStop = false;
			this.LeapGroup.Text = "Leap Years";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(261, 229);
			base.Controls.Add(this.LeapGroup);
			base.Controls.Add(this.MonthGroup);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MonthForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Month";
			((ISupportInitialize)this.DaysBox).EndInit();
			((ISupportInitialize)this.LeapModBox).EndInit();
			((ISupportInitialize)this.LeapPeriodBox).EndInit();
			this.MonthGroup.ResumeLayout(false);
			this.MonthGroup.PerformLayout();
			this.LeapGroup.ResumeLayout(false);
			this.LeapGroup.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
