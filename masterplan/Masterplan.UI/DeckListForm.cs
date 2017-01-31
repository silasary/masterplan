using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DeckListForm : Form
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private ListView DeckList;

		private ColumnHeader NameHdr;

		private ToolStripButton AddBtn;

		private ToolStripButton RemoveBtn;

		private ColumnHeader LevelHdr;

		private ColumnHeader CardsHdr;

		private ToolStripButton EditBtn;

		private Panel MainPanel;

		private Button CloseBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton ViewBtn;

		private ToolStripSplitButton RunBtn;

		private ToolStripMenuItem RunMap;

		public EncounterDeck SelectedDeck
		{
			get
			{
				if (this.DeckList.SelectedItems.Count != 0)
				{
					return this.DeckList.SelectedItems[0].Tag as EncounterDeck;
				}
				return null;
			}
		}

		public DeckListForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_decks();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.SelectedDeck != null);
			this.EditBtn.Enabled = (this.SelectedDeck != null);
			this.ViewBtn.Enabled = (this.SelectedDeck != null && this.SelectedDeck.Cards.Count != 0);
			this.RunBtn.Enabled = (this.SelectedDeck != null && this.SelectedDeck.Cards.Count != 0);
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			DeckBuilderForm deckBuilderForm = new DeckBuilderForm(new EncounterDeck
			{
				Name = "New Deck",
				Level = Session.Project.Party.Level
			});
			if (deckBuilderForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.Decks.Add(deckBuilderForm.Deck);
				Session.Modified = true;
				this.update_decks();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDeck != null)
			{
				Session.Project.Decks.Remove(this.SelectedDeck);
				Session.Modified = true;
				this.update_decks();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDeck != null)
			{
				int index = Session.Project.Decks.IndexOf(this.SelectedDeck);
				DeckBuilderForm deckBuilderForm = new DeckBuilderForm(this.SelectedDeck);
				if (deckBuilderForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.Decks[index] = deckBuilderForm.Deck;
					Session.Modified = true;
					this.update_decks();
				}
			}
		}

		private void ViewBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDeck != null)
			{
				DeckViewForm deckViewForm = new DeckViewForm(this.SelectedDeck.Cards);
				deckViewForm.ShowDialog();
			}
		}

		private void RunBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedDeck != null)
			{
				this.run_encounter(this.SelectedDeck, false);
			}
		}

		private void RunMap_Click(object sender, EventArgs e)
		{
			if (this.SelectedDeck != null)
			{
				this.run_encounter(this.SelectedDeck, true);
			}
		}

		private void run_encounter(EncounterDeck deck, bool choose_map)
		{
			MapAreaSelectForm mapAreaSelectForm = null;
			if (choose_map)
			{
				mapAreaSelectForm = new MapAreaSelectForm(Guid.Empty, Guid.Empty);
				if (mapAreaSelectForm.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
			Encounter encounter = new Encounter();
			bool flag = deck.DrawEncounter(encounter);
			this.update_decks();
			if (flag)
			{
				CombatState combatState = new CombatState();
				combatState.Encounter = encounter;
				combatState.PartyLevel = Session.Project.Party.Level;
				if (mapAreaSelectForm != null && mapAreaSelectForm.Map != null)
				{
					combatState.Encounter.MapID = mapAreaSelectForm.Map.ID;
					if (mapAreaSelectForm.MapArea != null)
					{
						combatState.Encounter.MapAreaID = mapAreaSelectForm.MapArea.ID;
					}
				}
				CombatForm combatForm = new CombatForm(combatState);
				combatForm.Show();
				return;
			}
			string text = "An encounter could not be built from this deck; check that there are enough cards remaining.";
			MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void update_decks()
		{
			this.DeckList.Items.Clear();
			foreach (EncounterDeck current in Session.Project.Decks)
			{
				int num = 0;
				foreach (EncounterCard current2 in current.Cards)
				{
					if (!current2.Drawn)
					{
						num++;
					}
				}
				string text = current.Cards.Count.ToString();
				if (num != current.Cards.Count)
				{
					text = num + " / " + current.Cards.Count;
				}
				ListViewItem listViewItem = this.DeckList.Items.Add(current.Name);
				listViewItem.SubItems.Add(current.Level.ToString());
				listViewItem.SubItems.Add(text);
				listViewItem.Tag = current;
			}
			if (this.DeckList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.DeckList.Items.Add("(no decks)");
				listViewItem2.ForeColor = SystemColors.GrayText;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(DeckListForm));
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.ViewBtn = new ToolStripButton();
			this.RunBtn = new ToolStripSplitButton();
			this.RunMap = new ToolStripMenuItem();
			this.DeckList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.LevelHdr = new ColumnHeader();
			this.CardsHdr = new ColumnHeader();
			this.MainPanel = new Panel();
			this.CloseBtn = new Button();
			this.Toolbar.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddBtn,
				this.RemoveBtn,
				this.EditBtn,
				this.toolStripSeparator1,
				this.ViewBtn,
				this.RunBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(378, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)resources.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new Size(33, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)resources.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.ViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ViewBtn.Image = (Image)resources.GetObject("ViewBtn.Image");
			this.ViewBtn.ImageTransparentColor = Color.Magenta;
			this.ViewBtn.Name = "ViewBtn";
			this.ViewBtn.Size = new Size(69, 22);
			this.ViewBtn.Text = "View Cards";
			this.ViewBtn.Click += new EventHandler(this.ViewBtn_Click);
			this.RunBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RunBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.RunMap
			});
			this.RunBtn.Image = (Image)resources.GetObject("RunBtn.Image");
			this.RunBtn.ImageTransparentColor = Color.Magenta;
			this.RunBtn.Name = "RunBtn";
			this.RunBtn.Size = new Size(101, 22);
			this.RunBtn.Text = "Run Encounter";
			this.RunBtn.ButtonClick += new EventHandler(this.RunBtn_Click);
			this.RunMap.Name = "RunMap";
			this.RunMap.Size = new Size(150, 22);
			this.RunMap.Text = "Choose Map...";
			this.RunMap.Click += new EventHandler(this.RunMap_Click);
			this.DeckList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.LevelHdr,
				this.CardsHdr
			});
			this.DeckList.Dock = DockStyle.Fill;
			this.DeckList.FullRowSelect = true;
			this.DeckList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DeckList.HideSelection = false;
			this.DeckList.Location = new Point(0, 25);
			this.DeckList.MultiSelect = false;
			this.DeckList.Name = "DeckList";
			this.DeckList.Size = new Size(378, 255);
			this.DeckList.TabIndex = 1;
			this.DeckList.UseCompatibleStateImageBehavior = false;
			this.DeckList.View = View.Details;
			this.DeckList.DoubleClick += new EventHandler(this.ViewBtn_Click);
			this.NameHdr.Text = "Deck";
			this.NameHdr.Width = 225;
			this.LevelHdr.Text = "Level";
			this.LevelHdr.TextAlign = HorizontalAlignment.Right;
			this.CardsHdr.Text = "Cards";
			this.CardsHdr.TextAlign = HorizontalAlignment.Right;
			this.MainPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.MainPanel.Controls.Add(this.DeckList);
			this.MainPanel.Controls.Add(this.Toolbar);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new Size(378, 280);
			this.MainPanel.TabIndex = 2;
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(315, 298);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(75, 23);
			this.CloseBtn.TabIndex = 3;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(402, 333);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.MainPanel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DeckListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encounter Decks";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
