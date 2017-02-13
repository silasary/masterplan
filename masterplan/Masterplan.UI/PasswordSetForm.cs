using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PasswordSetForm : Form
	{
		private IContainer components;

		private Label PasswordLbl;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox PasswordBox;

		private TextBox RetypeBox;

		private Label RetypeLbl;

		private TextBox HintBox;

		private Label HintLbl;

		private Button ClearBtn;

		public string Password
		{
			get
			{
				return this.PasswordBox.Text.ToLower();
			}
		}

		public string PasswordHint
		{
			get
			{
				return this.HintBox.Text;
			}
		}

		public PasswordSetForm()
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.PasswordBox.Text = Session.Project.Password;
			this.RetypeBox.Text = Session.Project.Password;
			this.HintBox.Text = Session.Project.PasswordHint;
			this.ClearBtn.Visible = (Session.Project.Password != "");
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.PasswordBox.Text.ToLower() == this.RetypeBox.Text.ToLower());
		}

		private void ClearBtn_Click(object sender, EventArgs e)
		{
			Session.Project.Password = "";
			Session.Project.PasswordHint = "";
			Session.Modified = true;
			base.DialogResult = DialogResult.Ignore;
			base.Close();
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
			this.PasswordLbl = new Label();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.PasswordBox = new TextBox();
			this.RetypeBox = new TextBox();
			this.RetypeLbl = new Label();
			this.HintBox = new TextBox();
			this.HintLbl = new Label();
			this.ClearBtn = new Button();
			base.SuspendLayout();
			this.PasswordLbl.AutoSize = true;
			this.PasswordLbl.Location = new Point(12, 15);
			this.PasswordLbl.Name = "PasswordLbl";
			this.PasswordLbl.Size = new Size(83, 13);
			this.PasswordLbl.TabIndex = 0;
			this.PasswordLbl.Text = "Enter password:";
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(206, 128);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 7;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(287, 128);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 8;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.PasswordBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.PasswordBox.Location = new Point(110, 12);
			this.PasswordBox.Name = "PasswordBox";
			this.PasswordBox.PasswordChar = '*';
			this.PasswordBox.Size = new Size(252, 20);
			this.PasswordBox.TabIndex = 1;
			this.RetypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RetypeBox.Location = new Point(110, 38);
			this.RetypeBox.Name = "RetypeBox";
			this.RetypeBox.PasswordChar = '*';
			this.RetypeBox.Size = new Size(252, 20);
			this.RetypeBox.TabIndex = 3;
			this.RetypeLbl.AutoSize = true;
			this.RetypeLbl.Location = new Point(12, 41);
			this.RetypeLbl.Name = "RetypeLbl";
			this.RetypeLbl.Size = new Size(92, 13);
			this.RetypeLbl.TabIndex = 2;
			this.RetypeLbl.Text = "Retype password:";
			this.HintBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HintBox.Location = new Point(110, 87);
			this.HintBox.Name = "HintBox";
			this.HintBox.Size = new Size(252, 20);
			this.HintBox.TabIndex = 5;
			this.HintLbl.AutoSize = true;
			this.HintLbl.Location = new Point(12, 90);
			this.HintLbl.Name = "HintLbl";
			this.HintLbl.Size = new Size(76, 13);
			this.HintLbl.TabIndex = 4;
			this.HintLbl.Text = "Password hint:";
			this.ClearBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.ClearBtn.Location = new Point(12, 128);
			this.ClearBtn.Name = "ClearBtn";
			this.ClearBtn.Size = new Size(117, 23);
			this.ClearBtn.TabIndex = 6;
			this.ClearBtn.Text = "Remove Password";
			this.ClearBtn.UseVisualStyleBackColor = true;
			this.ClearBtn.Click += new EventHandler(this.ClearBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(374, 163);
			base.Controls.Add(this.ClearBtn);
			base.Controls.Add(this.HintBox);
			base.Controls.Add(this.HintLbl);
			base.Controls.Add(this.RetypeBox);
			base.Controls.Add(this.RetypeLbl);
			base.Controls.Add(this.PasswordBox);
			base.Controls.Add(this.PasswordLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PasswordSetForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Set Password";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
