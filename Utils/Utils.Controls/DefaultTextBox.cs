using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Utils.Controls
{
	public class DefaultTextBox : TextBox
	{
		private string fDefaultText = "";

		private bool fUpdating;

		private IContainer components;

		[Category("Appearance"), DefaultValue(""), Description("The default text to be shown in the text box.")]
		public string DefaultText
		{
			get
			{
				return this.fDefaultText;
			}
			set
			{
				if (this.Text == this.fDefaultText)
				{
					this.Text = "";
				}
				this.fDefaultText = value;
				if (this.Text == "")
				{
					this.Text = this.fDefaultText;
				}
			}
		}

		public DefaultTextBox()
		{
			this.InitializeComponent();
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			if (!this.fUpdating && !this.Focused && this.Text == "")
			{
				this.Text = this.fDefaultText;
			}
		}

		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (this.Text == this.fDefaultText)
			{
				this.fUpdating = true;
				this.Text = "";
				this.fUpdating = false;
			}
			base.SelectAll();
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			if (this.Text == "")
			{
				this.fUpdating = true;
				this.Text = this.fDefaultText;
				this.fUpdating = false;
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if ((e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.A)
			{
				base.SelectAll();
				return;
			}
			base.OnKeyDown(e);
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
			this.components = new Container();
		}
	}
}
