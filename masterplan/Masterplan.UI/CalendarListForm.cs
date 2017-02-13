using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CalendarListForm : Form
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private ListView CalendarList;

		private ColumnHeader NameHdr;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ColumnHeader MonthsHdr;

		private ColumnHeader DaysHdr;

		private ToolStripButton AddBtn;

		private SplitContainer Splitter;

		private CalendarPanel CalendarPnl;

		private ToolStrip NavigationToolbar;

		private ToolStripLabel MonthPrevBtn;

		private ToolStripLabel MonthNextBtn;

		private ToolStripLabel YearPrevBtn;

		private ToolStripLabel YearNextBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripComboBox MonthBox;

		private ToolStripTextBox YearBox;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton ExportBtn;

		private ToolStripButton PlayerViewBtn;

		private Button CloseBtn;

		public Calendar SelectedCalendar
		{
			get
			{
				if (this.CalendarList.SelectedItems.Count != 0)
				{
					return this.CalendarList.SelectedItems[0].Tag as Calendar;
				}
				return null;
			}
		}

		public CalendarListForm()
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.update_calendars();
			this.update_calendar_panel();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.SelectedCalendar != null);
			this.EditBtn.Enabled = (this.SelectedCalendar != null);
			this.ExportBtn.Enabled = (this.SelectedCalendar != null);
			this.PlayerViewBtn.Enabled = (this.SelectedCalendar != null);
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			CalendarForm calendarForm = new CalendarForm(new Calendar
			{
				Name = "New Calendar"
			});
			if (calendarForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.Calendars.Add(calendarForm.Calendar);
				Session.Modified = true;
				this.update_calendars();
				this.update_calendar_panel();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCalendar != null)
			{
				string text = "Are you sure you want to delete this calendar?";
				DialogResult dialogResult = MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.No)
				{
					return;
				}
				Session.Project.Calendars.Remove(this.SelectedCalendar);
				Session.Modified = true;
				this.update_calendars();
				this.update_calendar_panel();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCalendar != null)
			{
				int index = Session.Project.Calendars.IndexOf(this.SelectedCalendar);
				CalendarForm calendarForm = new CalendarForm(this.SelectedCalendar);
				if (calendarForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.Calendars[index] = calendarForm.Calendar;
					Session.Modified = true;
					this.update_calendars();
					this.update_calendar_panel();
				}
			}
		}

		private void ExportBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCalendar != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.FileName = this.SelectedCalendar.Name;
				saveFileDialog.Filter = "Bitmap Image |*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|PNG Image|*.png";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					ImageFormat format = ImageFormat.Bmp;
					switch (saveFileDialog.FilterIndex)
					{
					case 1:
						format = ImageFormat.Bmp;
						break;
					case 2:
						format = ImageFormat.Jpeg;
						break;
					case 3:
						format = ImageFormat.Gif;
						break;
					case 4:
						format = ImageFormat.Png;
						break;
					}
					Bitmap bitmap = Screenshot.Calendar(this.CalendarPnl.Calendar, this.CalendarPnl.MonthIndex, this.CalendarPnl.Year, new Size(800, 600));
					bitmap.Save(saveFileDialog.FileName, format);
				}
			}
		}

		private void PlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCalendar != null)
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
				}
				Session.PlayerView.ShowCalendar(this.CalendarPnl.Calendar, this.CalendarPnl.MonthIndex, this.CalendarPnl.Year);
			}
		}

		private void CalendarList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.update_calendar_panel();
		}

		private void MonthBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			MonthInfo item = this.MonthBox.SelectedItem as MonthInfo;
			int monthIndex = this.CalendarPnl.Calendar.Months.IndexOf(item);
			this.CalendarPnl.MonthIndex = monthIndex;
		}

		private void YearBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				int year = int.Parse(this.YearBox.Text);
				this.CalendarPnl.Year = year;
			}
			catch
			{
				this.YearBox.Text = this.CalendarPnl.Year.ToString();
			}
		}

		private void MonthPrevBtn_Click(object sender, EventArgs e)
		{
			this.CalendarPnl.MonthIndex--;
			if (this.CalendarPnl.MonthIndex == -1)
			{
				this.CalendarPnl.MonthIndex = this.CalendarPnl.Calendar.Months.Count - 1;
				this.CalendarPnl.Year--;
			}
			this.update_calendar_panel();
		}

		private void MonthNextBtn_Click(object sender, EventArgs e)
		{
			this.CalendarPnl.MonthIndex++;
			if (this.CalendarPnl.MonthIndex == this.CalendarPnl.Calendar.Months.Count)
			{
				this.CalendarPnl.MonthIndex = 0;
				this.CalendarPnl.Year++;
			}
			this.update_calendar_panel();
		}

		private void YearPrevBtn_Click(object sender, EventArgs e)
		{
			this.CalendarPnl.Year--;
			this.update_calendar_panel();
		}

		private void YearNextBtn_Click(object sender, EventArgs e)
		{
			this.CalendarPnl.Year++;
			this.update_calendar_panel();
		}

		private void update_calendars()
		{
			this.CalendarList.Items.Clear();
			foreach (Calendar current in Session.Project.Calendars)
			{
				ListViewItem listViewItem = this.CalendarList.Items.Add(current.Name);
				listViewItem.SubItems.Add(current.Months.Count.ToString());
				listViewItem.SubItems.Add(current.DayCount(current.CampaignYear).ToString());
				listViewItem.Tag = current;
			}
			if (this.CalendarList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.CalendarList.Items.Add("(no calendars)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_calendar_panel()
		{
			this.NavigationToolbar.Visible = (this.SelectedCalendar != null);
			if (this.CalendarPnl.Calendar != this.SelectedCalendar)
			{
				this.CalendarPnl.Calendar = this.SelectedCalendar;
				if (this.CalendarPnl.Calendar != null)
				{
					this.CalendarPnl.Year = this.SelectedCalendar.CampaignYear;
					this.CalendarPnl.MonthIndex = 0;
				}
				this.CalendarPnl.Invalidate();
			}
			this.MonthBox.Items.Clear();
			if (this.CalendarPnl.Calendar != null)
			{
				foreach (MonthInfo current in this.CalendarPnl.Calendar.Months)
				{
					this.MonthBox.Items.Add(current);
				}
				MonthInfo selectedItem = this.CalendarPnl.Calendar.Months[this.CalendarPnl.MonthIndex];
				this.MonthBox.SelectedItem = selectedItem;
				this.YearBox.Text = this.CalendarPnl.Year.ToString();
				return;
			}
			this.MonthBox.Text = "";
			this.YearBox.Text = "";
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(CalendarListForm));
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.ExportBtn = new ToolStripButton();
			this.PlayerViewBtn = new ToolStripButton();
			this.CalendarList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.MonthsHdr = new ColumnHeader();
			this.DaysHdr = new ColumnHeader();
			this.Splitter = new SplitContainer();
			this.CalendarPnl = new CalendarPanel();
			this.NavigationToolbar = new ToolStrip();
			this.YearPrevBtn = new ToolStripLabel();
			this.MonthPrevBtn = new ToolStripLabel();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.MonthBox = new ToolStripComboBox();
			this.YearBox = new ToolStripTextBox();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.MonthNextBtn = new ToolStripLabel();
			this.YearNextBtn = new ToolStripLabel();
			this.CloseBtn = new Button();
			this.Toolbar.SuspendLayout();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.NavigationToolbar.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddBtn,
				this.RemoveBtn,
				this.EditBtn,
				this.toolStripSeparator3,
				this.ExportBtn,
				this.PlayerViewBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(485, 25);
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
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 25);
			this.ExportBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ExportBtn.Image = (Image)resources.GetObject("ExportBtn.Image");
			this.ExportBtn.ImageTransparentColor = Color.Magenta;
			this.ExportBtn.Name = "ExportBtn";
			this.ExportBtn.Size = new Size(44, 22);
			this.ExportBtn.Text = "Export";
			this.ExportBtn.Click += new EventHandler(this.ExportBtn_Click);
			this.PlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlayerViewBtn.Image = (Image)resources.GetObject("PlayerViewBtn.Image");
			this.PlayerViewBtn.ImageTransparentColor = Color.Magenta;
			this.PlayerViewBtn.Name = "PlayerViewBtn";
			this.PlayerViewBtn.Size = new Size(114, 22);
			this.PlayerViewBtn.Text = "Send to Player View";
			this.PlayerViewBtn.Click += new EventHandler(this.PlayerViewBtn_Click);
			this.CalendarList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.MonthsHdr,
				this.DaysHdr
			});
			this.CalendarList.Dock = DockStyle.Fill;
			this.CalendarList.FullRowSelect = true;
			this.CalendarList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CalendarList.HideSelection = false;
			this.CalendarList.Location = new Point(0, 25);
			this.CalendarList.MultiSelect = false;
			this.CalendarList.Name = "CalendarList";
			this.CalendarList.Size = new Size(485, 72);
			this.CalendarList.TabIndex = 1;
			this.CalendarList.UseCompatibleStateImageBehavior = false;
			this.CalendarList.View = View.Details;
			this.CalendarList.SelectedIndexChanged += new EventHandler(this.CalendarList_SelectedIndexChanged);
			this.CalendarList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.NameHdr.Text = "Calendar";
			this.NameHdr.Width = 300;
			this.MonthsHdr.Text = "Months";
			this.MonthsHdr.TextAlign = HorizontalAlignment.Right;
			this.DaysHdr.Text = "Days";
			this.DaysHdr.TextAlign = HorizontalAlignment.Right;
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.FixedPanel = FixedPanel.Panel1;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Orientation = Orientation.Horizontal;
			this.Splitter.Panel1.Controls.Add(this.CalendarList);
			this.Splitter.Panel1.Controls.Add(this.Toolbar);
			this.Splitter.Panel2.Controls.Add(this.CalendarPnl);
			this.Splitter.Panel2.Controls.Add(this.NavigationToolbar);
			this.Splitter.Size = new Size(485, 364);
			this.Splitter.SplitterDistance = 97;
			this.Splitter.TabIndex = 2;
			this.CalendarPnl.BorderStyle = BorderStyle.FixedSingle;
			this.CalendarPnl.Calendar = null;
			this.CalendarPnl.Dock = DockStyle.Fill;
			this.CalendarPnl.Location = new Point(0, 25);
			this.CalendarPnl.MonthIndex = 0;
			this.CalendarPnl.Name = "CalendarPnl";
			this.CalendarPnl.Size = new Size(485, 238);
			this.CalendarPnl.TabIndex = 0;
			this.CalendarPnl.Year = 0;
			this.NavigationToolbar.GripStyle = ToolStripGripStyle.Hidden;
			this.NavigationToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.YearPrevBtn,
				this.MonthPrevBtn,
				this.toolStripSeparator1,
				this.MonthBox,
				this.YearBox,
				this.toolStripSeparator2,
				this.MonthNextBtn,
				this.YearNextBtn
			});
			this.NavigationToolbar.Location = new Point(0, 0);
			this.NavigationToolbar.Name = "NavigationToolbar";
			this.NavigationToolbar.Size = new Size(485, 25);
			this.NavigationToolbar.TabIndex = 1;
			this.NavigationToolbar.Text = "toolStrip1";
			this.YearPrevBtn.IsLink = true;
			this.YearPrevBtn.Name = "YearPrevBtn";
			this.YearPrevBtn.Size = new Size(49, 22);
			this.YearPrevBtn.Text = "<< Year";
			this.YearPrevBtn.Click += new EventHandler(this.YearPrevBtn_Click);
			this.MonthPrevBtn.IsLink = true;
			this.MonthPrevBtn.Name = "MonthPrevBtn";
			this.MonthPrevBtn.Size = new Size(62, 22);
			this.MonthPrevBtn.Text = "<< Month";
			this.MonthPrevBtn.Click += new EventHandler(this.MonthPrevBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.MonthBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.MonthBox.Name = "MonthBox";
			this.MonthBox.Size = new Size(121, 25);
			this.MonthBox.SelectedIndexChanged += new EventHandler(this.MonthBox_SelectedIndexChanged);
			this.YearBox.Name = "YearBox";
			this.YearBox.Size = new Size(100, 25);
			this.YearBox.TextChanged += new EventHandler(this.YearBox_TextChanged);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.MonthNextBtn.IsLink = true;
			this.MonthNextBtn.Name = "MonthNextBtn";
			this.MonthNextBtn.Size = new Size(62, 22);
			this.MonthNextBtn.Text = "Month >>";
			this.MonthNextBtn.Click += new EventHandler(this.MonthNextBtn_Click);
			this.YearNextBtn.IsLink = true;
			this.YearNextBtn.Name = "YearNextBtn";
			this.YearNextBtn.Size = new Size(49, 22);
			this.YearNextBtn.Text = "Year >>";
			this.YearNextBtn.Click += new EventHandler(this.YearNextBtn_Click);
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(422, 382);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(75, 23);
			this.CloseBtn.TabIndex = 3;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(509, 417);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.Splitter);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CalendarListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Calendars";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.Panel2.PerformLayout();
			this.Splitter.ResumeLayout(false);
			this.NavigationToolbar.ResumeLayout(false);
			this.NavigationToolbar.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
