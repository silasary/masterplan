using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Utils.Wizards
{
	public abstract class Wizard
	{
		private string fTitle = "Wizard";

		private List<IWizardPage> fPages = new List<IWizardPage>();

		public string Title
		{
			get
			{
				return this.fTitle;
			}
			set
			{
				this.fTitle = value;
			}
		}

		public List<IWizardPage> Pages
		{
			get
			{
				return this.fPages;
			}
		}

		public abstract object Data
		{
			get;
			set;
		}

		public Size MaxSize
		{
			get
			{
				Size empty = Size.Empty;
				foreach (IWizardPage current in this.fPages)
				{
					Control control = current as Control;
					if (control != null)
					{
						empty.Height = Math.Max(empty.Height, control.Height);
						empty.Width = Math.Max(empty.Width, control.Width);
					}
				}
				return empty;
			}
		}

		public Wizard()
		{
		}

		public Wizard(string title) : this()
		{
			this.fTitle = title;
		}

		public abstract void AddPages();

		public abstract void OnFinish();

		public abstract void OnCancel();

		public virtual int NextPageIndex(int current_page)
		{
			return -1;
		}

		public virtual int BackPageIndex(int current_page)
		{
			return -1;
		}

		public DialogResult Show()
		{
			this.AddPages();
			WizardForm wizardForm = new WizardForm(this);
			return wizardForm.ShowDialog();
		}
	}
}
