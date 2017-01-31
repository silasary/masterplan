using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CalendarEventForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label MonthLbl;

		private ComboBox MonthBox;

		private Label DayLbl;

		private NumericUpDown DayBox;

		private CalendarEvent fEvent;

		private Calendar fCalendar;

		public CalendarEvent Event
		{
			get
			{
				return this.fEvent;
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
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.MonthLbl = new Label();
			this.MonthBox = new ComboBox();
			this.DayLbl = new Label();
			this.DayBox = new NumericUpDown();
			((ISupportInitialize)this.DayBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(98, 100);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 6;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(179, 100);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 7;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(58, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(196, 20);
			this.NameBox.TabIndex = 1;
			this.MonthLbl.AutoSize = true;
			this.MonthLbl.Location = new Point(12, 41);
			this.MonthLbl.Name = "MonthLbl";
			this.MonthLbl.Size = new Size(40, 13);
			this.MonthLbl.TabIndex = 2;
			this.MonthLbl.Text = "Month:";
			this.MonthBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MonthBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.MonthBox.FormattingEnabled = true;
			this.MonthBox.Location = new Point(58, 38);
			this.MonthBox.Name = "MonthBox";
			this.MonthBox.Size = new Size(196, 21);
			this.MonthBox.TabIndex = 3;
			this.MonthBox.SelectedIndexChanged += new EventHandler(this.MonthBox_SelectedIndexChanged);
			this.DayLbl.AutoSize = true;
			this.DayLbl.Location = new Point(12, 67);
			this.DayLbl.Name = "DayLbl";
			this.DayLbl.Size = new Size(29, 13);
			this.DayLbl.TabIndex = 4;
			this.DayLbl.Text = "Day:";
			this.DayBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DayBox.Location = new Point(58, 65);
			NumericUpDown arg_3A4_0 = this.DayBox;
			int[] array = new int[4];
			array[0] = 1;
			arg_3A4_0.Minimum = new decimal(array);
			this.DayBox.Name = "DayBox";
			this.DayBox.Size = new Size(196, 20);
			this.DayBox.TabIndex = 5;
			NumericUpDown arg_3F3_0 = this.DayBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_3F3_0.Value = new decimal(array2);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(266, 135);
			base.Controls.Add(this.DayBox);
			base.Controls.Add(this.DayLbl);
			base.Controls.Add(this.MonthBox);
			base.Controls.Add(this.MonthLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CalendarEventForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Event";
			((ISupportInitialize)this.DayBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public CalendarEventForm(CalendarEvent ce, Calendar calendar)
		{
			this.InitializeComponent();
			this.fEvent = ce.Copy();
			this.fCalendar = calendar;
			foreach (MonthInfo current in this.fCalendar.Months)
			{
				this.MonthBox.Items.Add(current);
			}
			this.NameBox.Text = this.fEvent.Name;
			this.DayBox.Value = this.fEvent.DayIndex + 1;
			MonthInfo selectedItem = this.fCalendar.FindMonth(this.fEvent.MonthID);
			this.MonthBox.SelectedItem = selectedItem;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fEvent.Name = this.NameBox.Text;
			this.fEvent.DayIndex = (int)this.DayBox.Value - 1;
			MonthInfo monthInfo = this.MonthBox.SelectedItem as MonthInfo;
			this.fEvent.MonthID = monthInfo.ID;
		}

		private void MonthBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			MonthInfo monthInfo = this.MonthBox.SelectedItem as MonthInfo;
			this.DayBox.Maximum = monthInfo.DayCount;
		}
	}
}
