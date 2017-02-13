using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CompendiumItemForm : Form
	{
		private object fResult;

		private CompendiumHelper.CompendiumItem fItem;

		private IContainer components;

		private Button CancelBtn;

		private Panel Panel;

		private SplitContainer Splitter;

		private WebBrowser CompendiumBrowser;

		private WebBrowser ItemBrowser;

		private Button OKBtn;

		public object Result
		{
			get
			{
				return this.fResult;
			}
		}

		public CompendiumItemForm(CompendiumHelper.CompendiumItem item)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fItem = item;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.fResult != null);
		}

		private void CompendiumItemForm_Shown(object sender, EventArgs e)
		{
			this.CompendiumBrowser.Navigate(this.fItem.URL);
		}

		private void CompendiumBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (e.Url.ToString() != this.fItem.URL)
			{
				List<string> list = new List<string>();
				list.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
				list.Add("<BODY>");
				list.Add("<P>You need to log into the Compendium to see this item.</P>");
				list.Add("<P>When you do, the item will be imported and its details will be shown in this panel.</P>");
				list.Add("<P>The imported item can then be edited if its details are incorrect.</P>");
				list.Add("</BODY>");
				list.Add("</HTML>");
				this.ItemBrowser.DocumentText = HTML.Concatenate(list);
				return;
			}
			switch (this.fItem.Type)
			{
			case CompendiumHelper.ItemType.Creature:
				this.fResult = CompendiumImport.ImportCreatureFromHTML(this.CompendiumBrowser.DocumentText, this.fItem.URL);
				break;
			case CompendiumHelper.ItemType.Trap:
				this.fResult = CompendiumImport.ImportTrapFromHTML(this.CompendiumBrowser.DocumentText, this.fItem.URL);
				break;
			case CompendiumHelper.ItemType.MagicItem:
				this.fResult = CompendiumImport.ImportItemFromHTML(this.CompendiumBrowser.DocumentText, this.fItem.URL);
				break;
			}
			if (this.fResult != null)
			{
				this.display_result();
				return;
			}
			List<string> list2 = new List<string>();
			list2.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
			list2.Add("<BODY>");
			list2.Add("<P class=instruction>The item could not be imported.</P>");
			list2.Add("</BODY>");
			list2.Add("</HTML>");
			this.ItemBrowser.DocumentText = HTML.Concatenate(list2);
		}

		private void display_result()
		{
			Library library = new Library();
			library.Name = "Not yet added";
			Session.Libraries.Insert(0, library);
			switch (this.fItem.Type)
			{
			case CompendiumHelper.ItemType.Creature:
			{
				Creature creature = this.fResult as Creature;
				library.Creatures.Add(creature);
				EncounterCard encounterCard = new EncounterCard();
				encounterCard.CreatureID = creature.ID;
				this.ItemBrowser.DocumentText = HTML.StatBlock(encounterCard, null, null, true, false, true, CardMode.View, DisplaySize.Small);
				break;
			}
			case CompendiumHelper.ItemType.Trap:
			{
				Trap trap = this.fResult as Trap;
				library.Traps.Add(trap);
				this.ItemBrowser.DocumentText = HTML.Trap(trap, null, true, false, false, DisplaySize.Small);
				break;
			}
			case CompendiumHelper.ItemType.MagicItem:
			{
				MagicItem item = this.fResult as MagicItem;
				library.MagicItems.Add(item);
				this.ItemBrowser.DocumentText = HTML.MagicItem(item, DisplaySize.Small, false, true);
				break;
			}
			}
			Session.Libraries.Remove(library);
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
			this.CancelBtn = new Button();
			this.Panel = new Panel();
			this.Splitter = new SplitContainer();
			this.CompendiumBrowser = new WebBrowser();
			this.ItemBrowser = new WebBrowser();
			this.OKBtn = new Button();
			this.Panel.SuspendLayout();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(894, 365);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 0;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Panel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Panel.Controls.Add(this.Splitter);
			this.Panel.Location = new Point(12, 12);
			this.Panel.Name = "Panel";
			this.Panel.Size = new Size(957, 347);
			this.Panel.TabIndex = 1;
			this.Splitter.BorderStyle = BorderStyle.FixedSingle;
			this.Splitter.Dock = DockStyle.Fill;
			this.Splitter.Location = new Point(0, 0);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.CompendiumBrowser);
			this.Splitter.Panel2.Controls.Add(this.ItemBrowser);
			this.Splitter.Size = new Size(957, 347);
			this.Splitter.SplitterDistance = 614;
			this.Splitter.TabIndex = 0;
			this.CompendiumBrowser.Dock = DockStyle.Fill;
			this.CompendiumBrowser.IsWebBrowserContextMenuEnabled = false;
			this.CompendiumBrowser.Location = new Point(0, 0);
			this.CompendiumBrowser.MinimumSize = new Size(20, 20);
			this.CompendiumBrowser.Name = "CompendiumBrowser";
			this.CompendiumBrowser.ScriptErrorsSuppressed = true;
			this.CompendiumBrowser.Size = new Size(612, 345);
			this.CompendiumBrowser.TabIndex = 0;
			this.CompendiumBrowser.WebBrowserShortcutsEnabled = false;
			this.CompendiumBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.CompendiumBrowser_DocumentCompleted);
			this.ItemBrowser.Dock = DockStyle.Fill;
			this.ItemBrowser.IsWebBrowserContextMenuEnabled = false;
			this.ItemBrowser.Location = new Point(0, 0);
			this.ItemBrowser.MinimumSize = new Size(20, 20);
			this.ItemBrowser.Name = "ItemBrowser";
			this.ItemBrowser.ScriptErrorsSuppressed = true;
			this.ItemBrowser.Size = new Size(337, 345);
			this.ItemBrowser.TabIndex = 0;
			this.ItemBrowser.WebBrowserShortcutsEnabled = false;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(813, 365);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(981, 400);
			base.Controls.Add(this.Panel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CompendiumItemForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Compendium Item";
			base.Shown += new EventHandler(this.CompendiumItemForm_Shown);
			this.Panel.ResumeLayout(false);
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
