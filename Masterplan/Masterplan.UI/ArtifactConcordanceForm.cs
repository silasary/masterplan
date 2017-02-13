using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class ArtifactConcordanceForm : Form
	{
		private IContainer components;

		private Label RuleLbl;

		private TextBox RuleBox;

		private Button OKBtn;

		private Button CancelBtn;

		private Label ValueLbl;

		private TextBox ValueBox;

		private Pair<string, string> fConcordance;

		public Pair<string, string> Concordance
		{
			get
			{
				return this.fConcordance;
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
			this.RuleLbl = new Label();
			this.RuleBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.ValueLbl = new Label();
			this.ValueBox = new TextBox();
			base.SuspendLayout();
			this.RuleLbl.AutoSize = true;
			this.RuleLbl.Location = new Point(12, 15);
			this.RuleLbl.Name = "RuleLbl";
			this.RuleLbl.Size = new Size(32, 13);
			this.RuleLbl.TabIndex = 0;
			this.RuleLbl.Text = "Rule:";
			this.RuleBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RuleBox.Location = new Point(56, 12);
			this.RuleBox.Name = "RuleBox";
			this.RuleBox.Size = new Size(260, 20);
			this.RuleBox.TabIndex = 1;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(160, 76);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(241, 76);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.ValueLbl.AutoSize = true;
			this.ValueLbl.Location = new Point(12, 41);
			this.ValueLbl.Name = "ValueLbl";
			this.ValueLbl.Size = new Size(37, 13);
			this.ValueLbl.TabIndex = 2;
			this.ValueLbl.Text = "Value:";
			this.ValueBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ValueBox.Location = new Point(56, 34);
			this.ValueBox.Name = "ValueBox";
			this.ValueBox.Size = new Size(260, 20);
			this.ValueBox.TabIndex = 3;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(328, 111);
			base.Controls.Add(this.ValueBox);
			base.Controls.Add(this.ValueLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.RuleBox);
			base.Controls.Add(this.RuleLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ArtifactConcordanceForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Artifact Concordance Rule";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public ArtifactConcordanceForm(Pair<string, string> concordance)
		{
			this.InitializeComponent();
			this.fConcordance = concordance;
			this.RuleBox.Text = this.fConcordance.First;
			this.ValueBox.Text = this.fConcordance.Second;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fConcordance.First = this.RuleBox.Text;
			this.fConcordance.Second = this.ValueBox.Text;
		}
	}
}
