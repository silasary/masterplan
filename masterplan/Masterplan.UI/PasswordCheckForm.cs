using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PasswordCheckForm : Form
	{
		private IContainer components;

		private Label PasswordLbl;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox PasswordBox;

		private Label label1;

		private Button HintBtn;

		private string fPassword = "";

		private string fHint = "";

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
			this.label1 = new Label();
			this.HintBtn = new Button();
			base.SuspendLayout();
			this.PasswordLbl.AutoSize = true;
			this.PasswordLbl.Location = new Point(12, 43);
			this.PasswordLbl.Name = "PasswordLbl";
			this.PasswordLbl.Size = new Size(56, 13);
			this.PasswordLbl.TabIndex = 1;
			this.PasswordLbl.Text = "Password:";
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(149, 85);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(230, 85);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.PasswordBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.PasswordBox.Location = new Point(74, 40);
			this.PasswordBox.Name = "PasswordBox";
			this.PasswordBox.PasswordChar = '*';
			this.PasswordBox.Size = new Size(231, 20);
			this.PasswordBox.TabIndex = 2;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(206, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "This project is password-protected.";
			this.HintBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.HintBtn.Location = new Point(12, 85);
			this.HintBtn.Name = "HintBtn";
			this.HintBtn.Size = new Size(75, 23);
			this.HintBtn.TabIndex = 3;
			this.HintBtn.Text = "Hint";
			this.HintBtn.UseVisualStyleBackColor = true;
			this.HintBtn.Click += new EventHandler(this.HintBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(317, 120);
			base.Controls.Add(this.HintBtn);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.PasswordBox);
			base.Controls.Add(this.PasswordLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PasswordCheckForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Enter Password";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public PasswordCheckForm(string password, string hint)
		{
			this.InitializeComponent();
			this.fPassword = password;
			this.fHint = hint;
			this.HintBtn.Visible = (this.fHint != "");
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.PasswordBox.Text.ToLower() == this.fPassword);
		}

		private void HintBtn_Click(object sender, EventArgs e)
		{
			string text = "Password hint: " + this.fHint;
			MessageBox.Show(this, text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}
}
