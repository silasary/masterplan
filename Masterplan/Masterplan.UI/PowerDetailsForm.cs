using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PowerDetailsForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox DetailsBox;

		private Panel panel1;

		private Panel panel2;

		private WebBrowser Browser;

		private ICreature fCreature;

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
			this.panel1 = new Panel();
			this.panel2 = new Panel();
			this.Browser = new WebBrowser();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(223, 279);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(304, 279);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 3;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.BorderStyle = BorderStyle.None;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(0, 0);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(365, 120);
			this.DetailsBox.TabIndex = 0;
			this.panel1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel1.BorderStyle = BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.DetailsBox);
			this.panel1.Location = new Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(367, 122);
			this.panel1.TabIndex = 0;
			this.panel2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel2.BorderStyle = BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.Browser);
			this.panel2.Location = new Point(12, 140);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(367, 133);
			this.panel2.TabIndex = 1;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.Location = new Point(0, 0);
			this.Browser.MinimumSize = new Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.Size = new Size(365, 131);
			this.Browser.TabIndex = 0;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(391, 314);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerDetailsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Details";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public PowerDetailsForm(string str, ICreature creature)
		{
			this.InitializeComponent();
			this.DetailsBox.Text = str;
			this.fCreature = creature;
			int level = (this.fCreature != null) ? this.fCreature.Level : 0;
			IRole role = (this.fCreature != null) ? this.fCreature.Role : null;
			string str2 = "1d8 + 2";
			if (role != null)
			{
				if (role is Minion)
				{
					str2 = Statistics.Damage(level, DamageExpressionType.Minion);
				}
				else
				{
					str2 = Statistics.Damage(level, DamageExpressionType.Normal);
				}
			}
			List<string> list = new List<string>();
			list.Add(str2 + " damage");
			list.Add(str2 + " damage, and the target is knocked prone");
			list.Add("The target is slowed (save ends)");
			list.Add("The target is immobilised until the start of your next turn");
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD><B>Examples</B></TD>");
			head.Add("</TR>");
			foreach (string current in list)
			{
				head.Add("<TR>");
				head.Add("<TD>" + current + "</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			head.Add("</HTML>");
			this.Browser.DocumentText = HTML.Concatenate(head);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}
	}
}
