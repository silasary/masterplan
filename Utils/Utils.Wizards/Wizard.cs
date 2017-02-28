using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Utils.Wizards
{
    ///<summary>
    ///Abstract class which can be inherited to define a custom wizard.
    ///</summary>
    public abstract class Wizard
	{
		private string fTitle = "Wizard";

		private List<IWizardPage> fPages = new List<IWizardPage>();

        ///<summary>
        ///The text to be shown in the wizard title bar.
        ///</summary>
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

        ///<summary>
        ///The list of IWizardPage objects that make up the wizard.
        ///This list should not be edited directly outside the AddPages() method.
        ///</summary>
        public List<IWizardPage> Pages
		{
			get
			{
				return this.fPages;
			}
		}

        ///<summary>
        ///The user-defined data that contains whatever data the wizard pages display.
        ///</summary>
        public abstract object Data { get; set; }

        ///<summary>
        ///Gets the required size of the display area of the wizard, being the largest width and largest height of each IWizardPage contained in Pages.
        ///</summary>
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

        ///<summary>
        ///Default constructor.
        ///</summary>
        public Wizard()
		{
		}

        ///<summary>
        ///Constructor which sets the title of the wizard.
        ///</summary>
        ///<param name="title">The text to be shown in the wizard title bar.</param>
        public Wizard(string title) : this()
		{
			this.fTitle = title;
		}

        ///<summary>
        ///This method should be overridden to add IWizardPage objects to the Pages list.
        ///It is called before the wizard is shown.
        ///</summary>
        public abstract void AddPages();

        ///<summary>
        ///This method is called when the user presses the Finish button.
        ///</summary>
        public abstract void OnFinish();

        ///<summary>
        ///This method is called when the user presses the Cancel button.
        ///</summary>
        public abstract void OnCancel();

        ///<summary>
        ///Returns the index of the IWizardPage which will be displayed if the user presses the Next button.
        ///If this method returns -1, the wizard will display Pages[current_page + 1].
        ///</summary>
        ///<param name="current_page">The index of the current page.</param>
        ///<returns>The index of the next page to be shown.</returns>
        public virtual int NextPageIndex(int current_page)
		{
			return -1;
		}

        ///<summary>
        ///Returns the index of the IWizardPage which will be displayed if the user presses the Back button.
        ///If this method returns -1, the wizard will display Pages[current_page - 1].
        ///</summary>
        ///<param name="current_page">The index of the current page.</param>
        ///<returns>The index of the next page to be shown.</returns>
        public virtual int BackPageIndex(int current_page)
		{
			return -1;
		}

        ///<summary>
        ///Calls the AddPages() method and displays the wizard dialog, showing the IWizardPage at Pages[0].
        ///</summary>
        ///<returns>DialogResult.OK if the user pressed Finish to exit the wizard; DialogResult.Cancel if the user instead pressed Cancel.</returns>
        public DialogResult Show()
		{
            AddPages();
			WizardForm wizardForm = new WizardForm(this);
			return wizardForm.ShowDialog();
		}
	}
}
