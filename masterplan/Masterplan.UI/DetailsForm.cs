using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DetailsForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox DetailsBox;

		private StatusStrip HintStatusbar;

		private ToolStripStatusLabel HintLbl;

		private Panel panel1;

		private ICreature fCreature;

		private DetailsField fField;

		public string Details
		{
			get
			{
				return this.DetailsBox.Text;
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
			this.DetailsBox = new TextBox();
			this.HintStatusbar = new StatusStrip();
			this.HintLbl = new ToolStripStatusLabel();
			this.panel1 = new Panel();
			this.HintStatusbar.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(226, 231);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(307, 231);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(0, 0);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(370, 191);
			this.DetailsBox.TabIndex = 0;
			this.HintStatusbar.Items.AddRange(new ToolStripItem[]
			{
				this.HintLbl
			});
			this.HintStatusbar.Location = new Point(0, 191);
			this.HintStatusbar.Name = "HintStatusbar";
			this.HintStatusbar.Size = new Size(370, 22);
			this.HintStatusbar.SizingGrip = false;
			this.HintStatusbar.TabIndex = 1;
			this.HintStatusbar.Text = "statusStrip1";
			this.HintLbl.Name = "HintLbl";
			this.HintLbl.Size = new Size(36, 17);
			this.HintLbl.Text = "(info)";
			this.panel1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel1.Controls.Add(this.DetailsBox);
			this.panel1.Controls.Add(this.HintStatusbar);
			this.panel1.Location = new Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(370, 213);
			this.panel1.TabIndex = 0;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(394, 266);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DetailsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Details";
			this.HintStatusbar.ResumeLayout(false);
			this.HintStatusbar.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		public DetailsForm(ICreature c, DetailsField field, string hint)
		{
			this.InitializeComponent();
			this.fCreature = c;
			this.fField = field;
			if (hint != null && hint != "")
			{
				this.HintLbl.Text = hint;
			}
			else
			{
				this.HintStatusbar.Visible = false;
			}
			switch (this.fField)
			{
			case DetailsField.Senses:
				this.Text = "Senses";
				this.DetailsBox.Text = this.fCreature.Senses;
				return;
			case DetailsField.Movement:
				this.Text = "Movement";
				this.DetailsBox.Text = this.fCreature.Movement;
				return;
			case DetailsField.Resist:
				this.Text = "Resist";
				this.DetailsBox.Text = this.fCreature.Resist;
				return;
			case DetailsField.Vulnerable:
				this.Text = "Vulnerable";
				this.DetailsBox.Text = this.fCreature.Vulnerable;
				return;
			case DetailsField.Immune:
				this.Text = "Immune";
				this.DetailsBox.Text = this.fCreature.Immune;
				return;
			case DetailsField.Alignment:
				this.Text = "Alignment";
				this.DetailsBox.Text = this.fCreature.Alignment;
				return;
			case DetailsField.Languages:
				this.Text = "Languages";
				this.DetailsBox.Text = this.fCreature.Languages;
				return;
			case DetailsField.Skills:
				this.Text = "Skills";
				this.DetailsBox.Text = this.fCreature.Skills;
				return;
			case DetailsField.Equipment:
				this.Text = "Equipment";
				this.DetailsBox.Text = this.fCreature.Equipment;
				return;
			case DetailsField.Description:
				this.Text = "Description";
				this.DetailsBox.Text = this.fCreature.Details;
				return;
			case DetailsField.Tactics:
				this.Text = "Tactics";
				this.DetailsBox.Text = this.fCreature.Tactics;
				return;
			default:
				return;
			}
		}

		public DetailsForm(string str, string title, string hint)
		{
			this.InitializeComponent();
			this.Text = title;
			this.DetailsBox.Text = str;
			if (hint != null && hint != "")
			{
				this.HintLbl.Text = hint;
				return;
			}
			this.HintStatusbar.Visible = false;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			switch (this.fField)
			{
			case DetailsField.Senses:
				this.fCreature.Senses = this.DetailsBox.Text;
				return;
			case DetailsField.Movement:
				this.fCreature.Movement = this.DetailsBox.Text;
				return;
			case DetailsField.Resist:
				this.fCreature.Resist = this.DetailsBox.Text;
				return;
			case DetailsField.Vulnerable:
				this.fCreature.Vulnerable = this.DetailsBox.Text;
				return;
			case DetailsField.Immune:
				this.fCreature.Immune = this.DetailsBox.Text;
				return;
			case DetailsField.Alignment:
				this.fCreature.Alignment = this.DetailsBox.Text;
				return;
			case DetailsField.Languages:
				this.fCreature.Languages = this.DetailsBox.Text;
				return;
			case DetailsField.Skills:
				this.fCreature.Skills = this.DetailsBox.Text;
				return;
			case DetailsField.Equipment:
				this.fCreature.Equipment = this.DetailsBox.Text;
				return;
			case DetailsField.Description:
				this.fCreature.Details = this.DetailsBox.Text;
				return;
			case DetailsField.Tactics:
				this.fCreature.Tactics = this.DetailsBox.Text;
				return;
			default:
				return;
			}
		}
	}
}
