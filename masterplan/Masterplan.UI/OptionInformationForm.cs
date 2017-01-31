using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class OptionInformationForm : Form
	{
		private IContainer components;

		private Label DCLbl;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private Button OKBtn;

		private Button CancelBtn;

		private NumericUpDown DCBox;

		private Pair<int, string> fInfo;

		public Pair<int, string> Information
		{
			get
			{
				return this.fInfo;
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
			this.DCLbl = new Label();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.DCBox = new NumericUpDown();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			((ISupportInitialize)this.DCBox).BeginInit();
			base.SuspendLayout();
			this.DCLbl.AutoSize = true;
			this.DCLbl.Location = new Point(12, 14);
			this.DCLbl.Name = "DCLbl";
			this.DCLbl.Size = new Size(47, 13);
			this.DCLbl.TabIndex = 0;
			this.DCLbl.Text = "Skill DC:";
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Location = new Point(12, 38);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(291, 137);
			this.Pages.TabIndex = 2;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(283, 111);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(277, 105);
			this.DetailsBox.TabIndex = 0;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(147, 181);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(228, 181);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.DCBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			NumericUpDown arg_390_0 = this.DCBox;
			int[] array = new int[4];
			array[0] = 5;
			arg_390_0.Increment = new decimal(array);
			this.DCBox.Location = new Point(65, 12);
			NumericUpDown arg_3C1_0 = this.DCBox;
			int[] array2 = new int[4];
			array2[0] = 10;
			arg_3C1_0.Minimum = new decimal(array2);
			this.DCBox.Name = "DCBox";
			this.DCBox.Size = new Size(238, 20);
			this.DCBox.TabIndex = 1;
			NumericUpDown arg_411_0 = this.DCBox;
			int[] array3 = new int[4];
			array3[0] = 10;
			arg_411_0.Value = new decimal(array3);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(315, 216);
			base.Controls.Add(this.DCBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.DCLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionInformationForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Feature";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			((ISupportInitialize)this.DCBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public OptionInformationForm(Pair<int, string> info)
		{
			this.InitializeComponent();
			this.fInfo = info;
			this.DCBox.Value = this.fInfo.First;
			this.DetailsBox.Text = this.fInfo.Second;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fInfo.First = (int)this.DCBox.Value;
			this.fInfo.Second = this.DetailsBox.Text;
		}
	}
}
