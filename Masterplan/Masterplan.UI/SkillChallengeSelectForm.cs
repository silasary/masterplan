using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class SkillChallengeSelectForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView ChallengeList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private SplitContainer Splitter;

		private Panel BrowserPanel;

		private WebBrowser Browser;

		public SkillChallenge SkillChallenge
		{
			get
			{
				if (this.ChallengeList.SelectedItems.Count != 0)
				{
					return this.ChallengeList.SelectedItems[0].Tag as SkillChallenge;
				}
				return null;
			}
		}

		public SkillChallengeSelectForm()
		{
			this.InitializeComponent();
			List<SkillChallenge> skillChallenges = Session.SkillChallenges;
			foreach (SkillChallenge current in skillChallenges)
			{
				ListViewItem listViewItem = this.ChallengeList.Items.Add(current.Name);
				listViewItem.SubItems.Add(current.Info);
				listViewItem.Tag = current;
			}
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.Browser.DocumentText = "";
			this.ChallengeList_SelectedIndexChanged(null, null);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.SkillChallenge != null);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SkillChallenge != null)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		private void ChallengeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = HTML.SkillChallenge(this.SkillChallenge, false, true, DisplaySize.Small);
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(text);
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
			this.ChallengeList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.BrowserPanel = new Panel();
			this.Browser = new WebBrowser();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.BrowserPanel.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(549, 354);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.ChallengeList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.InfoHdr
			});
			this.ChallengeList.Dock = DockStyle.Fill;
			this.ChallengeList.FullRowSelect = true;
			this.ChallengeList.HideSelection = false;
			this.ChallengeList.Location = new Point(0, 0);
			this.ChallengeList.MultiSelect = false;
			this.ChallengeList.Name = "ChallengeList";
			this.ChallengeList.Size = new Size(330, 336);
			this.ChallengeList.Sorting = SortOrder.Ascending;
			this.ChallengeList.TabIndex = 0;
			this.ChallengeList.UseCompatibleStateImageBehavior = false;
			this.ChallengeList.View = View.Details;
			this.ChallengeList.SelectedIndexChanged += new EventHandler(this.ChallengeList_SelectedIndexChanged);
			this.ChallengeList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.NameHdr.Text = "Skill Challenge";
			this.NameHdr.Width = 150;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 150;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(630, 354);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.ChallengeList);
			this.Splitter.Panel2.Controls.Add(this.BrowserPanel);
			this.Splitter.Size = new Size(693, 336);
			this.Splitter.SplitterDistance = 330;
			this.Splitter.TabIndex = 3;
			this.BrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BrowserPanel.Controls.Add(this.Browser);
			this.BrowserPanel.Dock = DockStyle.Fill;
			this.BrowserPanel.Location = new Point(0, 0);
			this.BrowserPanel.Name = "BrowserPanel";
			this.BrowserPanel.Size = new Size(359, 336);
			this.BrowserPanel.TabIndex = 0;
			this.Browser.AllowWebBrowserDrop = false;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.MinimumSize = new Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new Size(357, 334);
			this.Browser.TabIndex = 0;
			this.Browser.WebBrowserShortcutsEnabled = false;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(717, 389);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SkillChallengeSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a Skill Challenge";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.BrowserPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
