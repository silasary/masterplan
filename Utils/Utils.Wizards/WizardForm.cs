using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Utils.Wizards
{
	internal class WizardForm : Form
	{
		private IContainer components;

		private Button BackBtn;

		private Button NextBtn;

		private Button CancelBtn;

		private Panel ContentPnl;

		private Button FinishBtn;

		private PictureBox ImageBox;

		private Wizard fWizard;

		public IWizardPage CurrentPage
		{
			get
			{
				if (this.ContentPnl.Controls.Count != 0)
				{
					return this.ContentPnl.Controls[0] as IWizardPage;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WizardForm));
			this.BackBtn = new Button();
			this.NextBtn = new Button();
			this.CancelBtn = new Button();
			this.ContentPnl = new Panel();
			this.FinishBtn = new Button();
			this.ImageBox = new PictureBox();
			((ISupportInitialize)this.ImageBox).BeginInit();
			base.SuspendLayout();
			this.BackBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.BackBtn.Location = new Point(148, 277);
			this.BackBtn.Name = "BackBtn";
			this.BackBtn.Size = new Size(75, 23);
			this.BackBtn.TabIndex = 1;
			this.BackBtn.Text = "< Back";
			this.BackBtn.UseVisualStyleBackColor = true;
			this.BackBtn.Click += new EventHandler(this.BackBtn_Click);
			this.NextBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.NextBtn.Location = new Point(229, 277);
			this.NextBtn.Name = "NextBtn";
			this.NextBtn.Size = new Size(75, 23);
			this.NextBtn.TabIndex = 2;
			this.NextBtn.Text = "Next >";
			this.NextBtn.UseVisualStyleBackColor = true;
			this.NextBtn.Click += new EventHandler(this.NextBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(391, 277);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Click += new EventHandler(this.CancelBtn_Click);
			this.ContentPnl.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ContentPnl.Location = new Point(149, 12);
			this.ContentPnl.Name = "ContentPnl";
			this.ContentPnl.Size = new Size(317, 259);
			this.ContentPnl.TabIndex = 0;
			this.FinishBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.FinishBtn.DialogResult = DialogResult.OK;
			this.FinishBtn.Location = new Point(310, 277);
			this.FinishBtn.Name = "FinishBtn";
			this.FinishBtn.Size = new Size(75, 23);
			this.FinishBtn.TabIndex = 3;
			this.FinishBtn.Text = "Finish";
			this.FinishBtn.UseVisualStyleBackColor = true;
			this.FinishBtn.Click += new EventHandler(this.FinishBtn_Click);
			this.ImageBox.Image = (Image)componentResourceManager.GetObject("ImageBox.Image");
			this.ImageBox.Location = new Point(12, 12);
			this.ImageBox.Name = "ImageBox";
			this.ImageBox.Size = new Size(131, 259);
			this.ImageBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.ImageBox.TabIndex = 13;
			this.ImageBox.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(478, 312);
			base.Controls.Add(this.ImageBox);
			base.Controls.Add(this.FinishBtn);
			base.Controls.Add(this.ContentPnl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.NextBtn);
			base.Controls.Add(this.BackBtn);
			this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "WizardForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Wizard";
			((ISupportInitialize)this.ImageBox).EndInit();
			base.ResumeLayout(false);
		}

		public WizardForm(Wizard wiz)
		{
			this.InitializeComponent();
			this.fWizard = wiz;
			this.Text = this.fWizard.Title;
			if (!this.fWizard.MaxSize.IsEmpty)
			{
				base.Width += Math.Max(this.fWizard.MaxSize.Width, this.ContentPnl.Width) - this.ContentPnl.Width;
				base.Height += Math.Max(this.fWizard.MaxSize.Height, this.ContentPnl.Height) - this.ContentPnl.Height;
				this.ImageBox.Height = this.ContentPnl.Height;
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			if (this.fWizard.Pages.Count != 0)
			{
				this.set_page(0);
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			IWizardPage currentPage = this.CurrentPage;
			if (currentPage != null)
			{
				this.BackBtn.Enabled = currentPage.AllowBack;
				this.NextBtn.Enabled = currentPage.AllowNext;
				this.FinishBtn.Enabled = currentPage.AllowFinish;
				if (currentPage.AllowFinish)
				{
					base.AcceptButton = this.FinishBtn;
					return;
				}
				if (currentPage.AllowNext)
				{
					base.AcceptButton = this.NextBtn;
					return;
				}
				base.AcceptButton = null;
			}
		}

		private void BackBtn_Click(object sender, EventArgs e)
		{
			if (this.CurrentPage != null)
			{
				if (!this.CurrentPage.AllowBack)
				{
					return;
				}
				if (!this.CurrentPage.OnBack())
				{
					return;
				}
				int num = this.fWizard.Pages.IndexOf(this.CurrentPage);
				int num2 = this.fWizard.BackPageIndex(num);
				if (num2 == -1)
				{
					num2 = num - 1;
				}
				this.set_page(num2);
			}
		}

		private void NextBtn_Click(object sender, EventArgs e)
		{
			if (this.CurrentPage != null)
			{
				if (!this.CurrentPage.AllowNext)
				{
					return;
				}
				if (!this.CurrentPage.OnNext())
				{
					return;
				}
				int num = this.fWizard.Pages.IndexOf(this.CurrentPage);
				int num2 = this.fWizard.NextPageIndex(num);
				if (num2 == -1)
				{
					num2 = num + 1;
				}
				this.set_page(num2);
			}
		}

		private void FinishBtn_Click(object sender, EventArgs e)
		{
			if (this.CurrentPage != null)
			{
				if (!this.CurrentPage.AllowFinish)
				{
					return;
				}
				if (!this.CurrentPage.OnFinish())
				{
					return;
				}
				this.fWizard.OnFinish();
				base.Close();
			}
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			if (this.CurrentPage != null)
			{
				this.fWizard.OnCancel();
			}
			base.Close();
		}

		private void set_page(int pageindex)
		{
			IWizardPage wizardPage = this.fWizard.Pages[pageindex];
			Control control = wizardPage as Control;
			if (control != null)
			{
				this.ContentPnl.Controls.Clear();
				this.ContentPnl.Controls.Add(control);
				control.Dock = DockStyle.Fill;
				wizardPage.OnShown(this.fWizard.Data);
			}
		}
	}
}
