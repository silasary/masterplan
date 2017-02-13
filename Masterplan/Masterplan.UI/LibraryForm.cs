using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class LibraryForm : Form
	{
		private Library fLibrary;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label InfoLbl;

		public Library Library
		{
			get
			{
				return this.fLibrary;
			}
		}

		public LibraryForm(Library lib)
		{
			this.InitializeComponent();
			this.fLibrary = lib;
			string userName = SystemInformation.UserName;
			string computerName = SystemInformation.ComputerName;
			this.InfoLbl.Text = string.Concat(new string[]
			{
				"Note that when you create a library it will be usable only by this user (",
				userName,
				") on this computer (",
				computerName,
				")."
			});
			this.NameBox.Text = this.fLibrary.Name;
			this.NameBox_TextChanged(null, null);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fLibrary.Name = this.NameBox.Text;
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			if (this.NameBox.Text == "")
			{
				this.OKBtn.Enabled = false;
				return;
			}
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			string path = FileName.Directory(entryAssembly.FullName);
			DirectoryInfo arg = new DirectoryInfo(path);
			string path2 = arg + this.NameBox.Text + ".library";
			this.OKBtn.Enabled = !File.Exists(path2);
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
			this.InfoLbl = new Label();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(226, 69);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(307, 69);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 3;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(56, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(326, 20);
			this.NameBox.TabIndex = 1;
			this.NameBox.TextChanged += new EventHandler(this.NameBox_TextChanged);
			this.InfoLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.InfoLbl.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.InfoLbl.Location = new Point(12, 35);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(370, 31);
			this.InfoLbl.TabIndex = 4;
			this.InfoLbl.Text = "Note that when you create a library it will be usable only by this user (xxx) on this computer (xxx).";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(394, 104);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LibraryForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Library";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
