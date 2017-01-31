using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DeckViewForm : Form
	{
		private IContainer components;

		private SplitContainer Splitter;

		private CardDeck DeckView;

		private WebBrowser Browser;

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
			this.Splitter = new SplitContainer();
			this.DeckView = new CardDeck();
			this.Browser = new WebBrowser();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.Splitter.BorderStyle = BorderStyle.FixedSingle;
			this.Splitter.Dock = DockStyle.Fill;
			this.Splitter.FixedPanel = FixedPanel.Panel2;
			this.Splitter.Location = new Point(0, 0);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.DeckView);
			this.Splitter.Panel2.Controls.Add(this.Browser);
			this.Splitter.Size = new Size(715, 327);
			this.Splitter.SplitterDistance = 367;
			this.Splitter.TabIndex = 1;
			this.DeckView.Dock = DockStyle.Fill;
			this.DeckView.Location = new Point(0, 0);
			this.DeckView.Name = "DeckView";
			this.DeckView.Size = new Size(365, 325);
			this.DeckView.TabIndex = 1;
			this.DeckView.DeckOrderChanged += new EventHandler(this.DeckView_DeckOrderChanged);
			this.Browser.AllowWebBrowserDrop = false;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.MinimumSize = new Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new Size(342, 325);
			this.Browser.TabIndex = 0;
			this.Browser.WebBrowserShortcutsEnabled = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(715, 327);
			base.Controls.Add(this.Splitter);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DeckViewForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encounter Cards";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public DeckViewForm(List<EncounterCard> cards)
		{
			this.InitializeComponent();
			this.DeckView.SetCards(cards);
			this.Browser.DocumentText = "";
			this.update_stats();
		}

		private void DeckView_DeckOrderChanged(object sender, EventArgs e)
		{
			this.update_stats();
			this.DeckView.Focus();
		}

		private void update_stats()
		{
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(HTML.StatBlock(this.DeckView.TopCard, null, null, true, false, true, CardMode.View, DisplaySize.Small));
		}
	}
}
