using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DateForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label CalendarLbl;

		private ComboBox CalendarBox;

		private Label YearLbl;

		private ComboBox MonthBox;

		private Label MonthLbl;

		private NumericUpDown YearBox;

		private Label DayLbl;

		private NumericUpDown DayBox;

		private CalendarDate fDate;

		public CalendarDate Date
		{
			get
			{
				return this.fDate;
			}
		}

		public Calendar SelectedCalendar
		{
			get
			{
				return this.CalendarBox.SelectedItem as Calendar;
			}
		}

		public MonthInfo SelectedMonth
		{
			get
			{
				return this.MonthBox.SelectedItem as MonthInfo;
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
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.CalendarLbl = new Label();
			this.CalendarBox = new ComboBox();
			this.YearLbl = new Label();
			this.MonthBox = new ComboBox();
			this.MonthLbl = new Label();
			this.YearBox = new NumericUpDown();
			this.DayLbl = new Label();
			this.DayBox = new NumericUpDown();
			((ISupportInitialize)this.YearBox).BeginInit();
			((ISupportInitialize)this.DayBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(111, 129);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 8;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(192, 129);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 9;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CalendarLbl.AutoSize = true;
			this.CalendarLbl.Location = new Point(12, 15);
			this.CalendarLbl.Name = "CalendarLbl";
			this.CalendarLbl.Size = new Size(52, 13);
			this.CalendarLbl.TabIndex = 0;
			this.CalendarLbl.Text = "Calendar:";
			this.CalendarBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.CalendarBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.CalendarBox.FormattingEnabled = true;
			this.CalendarBox.Location = new Point(70, 12);
			this.CalendarBox.Name = "CalendarBox";
			this.CalendarBox.Size = new Size(197, 21);
			this.CalendarBox.TabIndex = 1;
			this.CalendarBox.SelectedIndexChanged += new EventHandler(this.CalendarBox_SelectedIndexChanged);
			this.YearLbl.AutoSize = true;
			this.YearLbl.Location = new Point(12, 41);
			this.YearLbl.Name = "YearLbl";
			this.YearLbl.Size = new Size(32, 13);
			this.YearLbl.TabIndex = 2;
			this.YearLbl.Text = "Year:";
			this.MonthBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MonthBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.MonthBox.FormattingEnabled = true;
			this.MonthBox.Location = new Point(70, 65);
			this.MonthBox.Name = "MonthBox";
			this.MonthBox.Size = new Size(197, 21);
			this.MonthBox.TabIndex = 5;
			this.MonthBox.SelectedIndexChanged += new EventHandler(this.MonthBox_SelectedIndexChanged);
			this.MonthLbl.AutoSize = true;
			this.MonthLbl.Location = new Point(12, 68);
			this.MonthLbl.Name = "MonthLbl";
			this.MonthLbl.Size = new Size(40, 13);
			this.MonthLbl.TabIndex = 4;
			this.MonthLbl.Text = "Month:";
			this.YearBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.YearBox.Location = new Point(70, 39);
			NumericUpDown arg_3FF_0 = this.YearBox;
			int[] array = new int[4];
			array[0] = 10000;
			arg_3FF_0.Maximum = new decimal(array);
			this.YearBox.Name = "YearBox";
			this.YearBox.Size = new Size(197, 20);
			this.YearBox.TabIndex = 3;
			this.YearBox.ValueChanged += new EventHandler(this.YearBox_ValueChanged);
			this.DayLbl.AutoSize = true;
			this.DayLbl.Location = new Point(12, 94);
			this.DayLbl.Name = "DayLbl";
			this.DayLbl.Size = new Size(29, 13);
			this.DayLbl.TabIndex = 6;
			this.DayLbl.Text = "Day:";
			this.DayBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DayBox.Location = new Point(70, 92);
			NumericUpDown arg_4E7_0 = this.DayBox;
			int[] array2 = new int[4];
			array2[0] = 30;
			arg_4E7_0.Maximum = new decimal(array2);
			NumericUpDown arg_503_0 = this.DayBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_503_0.Minimum = new decimal(array3);
			this.DayBox.Name = "DayBox";
			this.DayBox.Size = new Size(197, 20);
			this.DayBox.TabIndex = 7;
			NumericUpDown arg_552_0 = this.DayBox;
			int[] array4 = new int[4];
			array4[0] = 1;
			arg_552_0.Value = new decimal(array4);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(279, 164);
			base.Controls.Add(this.DayBox);
			base.Controls.Add(this.DayLbl);
			base.Controls.Add(this.MonthBox);
			base.Controls.Add(this.MonthLbl);
			base.Controls.Add(this.YearBox);
			base.Controls.Add(this.YearLbl);
			base.Controls.Add(this.CalendarBox);
			base.Controls.Add(this.CalendarLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DateForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Date";
			((ISupportInitialize)this.YearBox).EndInit();
			((ISupportInitialize)this.DayBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public DateForm(CalendarDate date)
		{
			this.InitializeComponent();
			foreach (Calendar current in Session.Project.Calendars)
			{
				this.CalendarBox.Items.Add(current);
			}
			this.fDate = date.Copy();
			Calendar calendar = Session.Project.FindCalendar(this.fDate.CalendarID);
			if (calendar != null)
			{
				this.CalendarBox.SelectedItem = calendar;
			}
			else
			{
				this.CalendarBox.SelectedIndex = 0;
			}
			this.YearBox.Value = this.fDate.Year;
			MonthInfo monthInfo = this.SelectedCalendar.FindMonth(this.fDate.MonthID);
			if (monthInfo != null)
			{
				this.MonthBox.SelectedItem = monthInfo;
			}
			else
			{
				this.MonthBox.SelectedIndex = 0;
			}
			this.DayBox.Value = this.fDate.DayIndex + 1;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			Calendar calendar = this.CalendarBox.SelectedItem as Calendar;
			MonthInfo monthInfo = this.MonthBox.SelectedItem as MonthInfo;
			this.fDate.CalendarID = calendar.ID;
			this.fDate.Year = (int)this.YearBox.Value;
			this.fDate.MonthID = monthInfo.ID;
			this.fDate.DayIndex = (int)this.DayBox.Value - 1;
		}

		private void CalendarBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.MonthBox.Items.Clear();
			foreach (MonthInfo current in this.SelectedCalendar.Months)
			{
				this.MonthBox.Items.Add(current);
			}
			this.YearBox.Value = this.SelectedCalendar.CampaignYear;
			this.MonthBox.SelectedIndex = 0;
		}

		private void YearBox_ValueChanged(object sender, EventArgs e)
		{
			this.set_days();
		}

		private void MonthBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.set_days();
		}

		private void set_days()
		{
			if (this.SelectedMonth == null)
			{
				return;
			}
			int num = this.SelectedMonth.DayCount;
			int num2 = (int)this.YearBox.Value;
			if (num2 % this.SelectedMonth.LeapPeriod == 0)
			{
				num += this.SelectedMonth.LeapModifier;
			}
			this.DayBox.Maximum = num;
		}
	}
}
