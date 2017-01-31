using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class HeroSelectForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label QuestionLbl;

		private RadioButton YesBtn;

		private ComboBox HeroBox;

		private RadioButton NoBtn;

		private Label InfoLbl;

		public Hero SelectedHero
		{
			get
			{
				if (this.YesBtn.Checked)
				{
					return this.HeroBox.SelectedItem as Hero;
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
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.QuestionLbl = new Label();
			this.YesBtn = new RadioButton();
			this.HeroBox = new ComboBox();
			this.NoBtn = new RadioButton();
			this.InfoLbl = new Label();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(165, 185);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(246, 185);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.QuestionLbl.AutoSize = true;
			this.QuestionLbl.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.QuestionLbl.Location = new Point(12, 9);
			this.QuestionLbl.Name = "QuestionLbl";
			this.QuestionLbl.Size = new Size(193, 13);
			this.QuestionLbl.TabIndex = 5;
			this.QuestionLbl.Text = "Was this effect applied by a PC?";
			this.YesBtn.AutoSize = true;
			this.YesBtn.Location = new Point(12, 40);
			this.YesBtn.Name = "YesBtn";
			this.YesBtn.Size = new Size(43, 17);
			this.YesBtn.TabIndex = 6;
			this.YesBtn.TabStop = true;
			this.YesBtn.Text = "Yes";
			this.YesBtn.UseVisualStyleBackColor = true;
			this.YesBtn.CheckedChanged += new EventHandler(this.option_changed);
			this.HeroBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HeroBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.HeroBox.FormattingEnabled = true;
			this.HeroBox.Location = new Point(61, 39);
			this.HeroBox.Name = "HeroBox";
			this.HeroBox.Size = new Size(260, 21);
			this.HeroBox.TabIndex = 7;
			this.HeroBox.SelectedIndexChanged += new EventHandler(this.option_changed);
			this.NoBtn.AutoSize = true;
			this.NoBtn.Location = new Point(12, 76);
			this.NoBtn.Name = "NoBtn";
			this.NoBtn.Size = new Size(39, 17);
			this.NoBtn.TabIndex = 8;
			this.NoBtn.TabStop = true;
			this.NoBtn.Text = "No";
			this.NoBtn.UseVisualStyleBackColor = true;
			this.NoBtn.CheckedChanged += new EventHandler(this.option_changed);
			this.InfoLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.InfoLbl.Location = new Point(12, 109);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(309, 73);
			this.InfoLbl.TabIndex = 9;
			this.InfoLbl.Text = "[info]";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(333, 220);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.NoBtn);
			base.Controls.Add(this.HeroBox);
			base.Controls.Add(this.YesBtn);
			base.Controls.Add(this.QuestionLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "HeroSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a PC";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public HeroSelectForm(Hero selected)
		{
			this.InitializeComponent();
			foreach (Hero current in Session.Project.Heroes)
			{
				this.HeroBox.Items.Add(current);
			}
			if (selected != null)
			{
				this.HeroBox.SelectedItem = selected;
				this.YesBtn.Checked = true;
				return;
			}
			this.HeroBox.SelectedIndex = 0;
			this.NoBtn.Checked = true;
		}

		private void option_changed(object sender, EventArgs e)
		{
			this.HeroBox.Enabled = this.YesBtn.Checked;
			if (this.YesBtn.Checked)
			{
				this.InfoLbl.Text = "The effect will be added to " + this.SelectedHero + "'s list.";
			}
			else
			{
				this.InfoLbl.Text = "The effect will be added to the list of predefined effects for this encounter only.";
			}
			Label expr_5B = this.InfoLbl;
			expr_5B.Text += Environment.NewLine;
			Label expr_76 = this.InfoLbl;
			expr_76.Text += "To apply the effect again, simply select it from the list.";
		}
	}
}
