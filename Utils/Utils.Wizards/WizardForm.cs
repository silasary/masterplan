using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Utils.Wizards
{
	internal partial class WizardForm : Form
	{
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
			Application.Idle += Application_Idle;
			if (this.fWizard.Pages.Count != 0)
			{
				this.set_page(0);
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
            if (this.IsDisposed) // Unhook, and allow this to be garbage collected.
                Application.Idle -= Application_Idle; 

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
