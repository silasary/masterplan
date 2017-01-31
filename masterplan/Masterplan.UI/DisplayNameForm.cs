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
	internal class DisplayNameForm : Form
	{
		private List<CombatData> fCombatants;

		private Encounter fEncounter;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label InfoLbl;

		private ListView CombatantList;

		private ColumnHeader CombatantHdr;

		private TextBox NameBox;

		private Button SetNameBtn;

		private TabControl Pages;

		private TabPage StatBlockPage;

		private TabPage MapPage;

		private WebBrowser Browser;

		private MapView Map;

		public List<CombatData> Combatants
		{
			get
			{
				return this.fCombatants;
			}
		}

		public CombatData SelectedCombatant
		{
			get
			{
				if (this.CombatantList.SelectedItems.Count != 0)
				{
					return this.CombatantList.SelectedItems[0].Tag as CombatData;
				}
				return null;
			}
		}

		public DisplayNameForm(List<CombatData> combatants, Encounter enc)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fCombatants = combatants;
			this.fEncounter = enc;
			Map map = null;
			if (this.fEncounter.MapID != Guid.Empty)
			{
				map = Session.Project.FindTacticalMap(this.fEncounter.MapID);
			}
			if (map == null)
			{
				this.Pages.TabPages.Remove(this.MapPage);
			}
			else
			{
				this.Map.Map = map;
				this.Map.Encounter = this.fEncounter;
			}
			this.update_list();
			this.update_stat_block();
			this.update_map_area();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.NameBox.Enabled = (this.SelectedCombatant != null);
			this.SetNameBtn.Enabled = (this.NameBox.Text != "" && this.SelectedCombatant != null && this.NameBox.Text != this.SelectedCombatant.DisplayName);
		}

		private void CombatantList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NameBox.Text = ((this.SelectedCombatant != null) ? this.SelectedCombatant.DisplayName : "");
			this.update_stat_block();
			this.update_map_area();
		}

		private void update_list()
		{
			this.CombatantList.Items.Clear();
			foreach (CombatData current in this.fCombatants)
			{
				ListViewItem listViewItem = this.CombatantList.Items.Add(current.DisplayName);
				listViewItem.Tag = current;
			}
		}

		private void update_stat_block()
		{
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.SelectedCombatant);
			EncounterCard card = (encounterSlot != null) ? encounterSlot.Card : null;
			this.Browser.DocumentText = HTML.StatBlock(card, this.SelectedCombatant, this.fEncounter, true, false, true, CardMode.View, DisplaySize.Small);
		}

		private void update_map_area()
		{
			Rectangle viewpoint = Rectangle.Empty;
			this.Map.BoxedTokens.Clear();
			if (this.SelectedCombatant != null && this.SelectedCombatant.Location != CombatData.NoPoint)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.SelectedCombatant);
				this.Map.BoxedTokens.Add(new CreatureToken(encounterSlot.ID, this.SelectedCombatant));
				this.Map.MapChanged();
				ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
				int size = Creature.GetSize(creature.Size);
				int num = 7;
				int num2 = 4;
				int x = this.SelectedCombatant.Location.X - num;
				int y = this.SelectedCombatant.Location.Y - num2;
				int width = num + size + num;
				int height = num2 + size + num2;
				viewpoint = new Rectangle(x, y, width, height);
			}
			else if (this.fEncounter.MapAreaID != Guid.Empty)
			{
				MapArea mapArea = this.Map.Map.FindArea(this.fEncounter.MapAreaID);
				if (mapArea != null)
				{
					viewpoint = mapArea.Region;
				}
			}
			this.Map.Viewpoint = viewpoint;
		}

		private void SetNameBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCombatant != null)
			{
				this.SelectedCombatant.DisplayName = this.NameBox.Text;
				this.update_list();
				this.update_stat_block();
				this.update_map_area();
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
			this.InfoLbl = new Label();
			this.CombatantList = new ListView();
			this.CombatantHdr = new ColumnHeader();
			this.NameBox = new TextBox();
			this.SetNameBtn = new Button();
			this.Pages = new TabControl();
			this.StatBlockPage = new TabPage();
			this.Browser = new WebBrowser();
			this.MapPage = new TabPage();
			this.Map = new MapView();
			this.Pages.SuspendLayout();
			this.StatBlockPage.SuspendLayout();
			this.MapPage.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(444, 333);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(525, 333);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.InfoLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InfoLbl.Location = new Point(12, 9);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(588, 35);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "Click on a creature in the list on the left to change its display name. This can be useful if, for example, you need to identify which miniature represents which creature.";
			this.CombatantList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.CombatantList.Columns.AddRange(new ColumnHeader[]
			{
				this.CombatantHdr
			});
			this.CombatantList.FullRowSelect = true;
			this.CombatantList.HideSelection = false;
			this.CombatantList.Location = new Point(12, 47);
			this.CombatantList.MultiSelect = false;
			this.CombatantList.Name = "CombatantList";
			this.CombatantList.Size = new Size(222, 280);
			this.CombatantList.TabIndex = 1;
			this.CombatantList.UseCompatibleStateImageBehavior = false;
			this.CombatantList.View = View.Details;
			this.CombatantList.SelectedIndexChanged += new EventHandler(this.CombatantList_SelectedIndexChanged);
			this.CombatantHdr.Text = "Creature";
			this.CombatantHdr.Width = 187;
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(240, 47);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(279, 20);
			this.NameBox.TabIndex = 2;
			this.SetNameBtn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.SetNameBtn.Location = new Point(525, 45);
			this.SetNameBtn.Name = "SetNameBtn";
			this.SetNameBtn.Size = new Size(75, 23);
			this.SetNameBtn.TabIndex = 3;
			this.SetNameBtn.Text = "Set Name";
			this.SetNameBtn.UseVisualStyleBackColor = true;
			this.SetNameBtn.Click += new EventHandler(this.SetNameBtn_Click);
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.StatBlockPage);
			this.Pages.Controls.Add(this.MapPage);
			this.Pages.Location = new Point(240, 73);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(360, 254);
			this.Pages.TabIndex = 4;
			this.StatBlockPage.Controls.Add(this.Browser);
			this.StatBlockPage.Location = new Point(4, 22);
			this.StatBlockPage.Name = "StatBlockPage";
			this.StatBlockPage.Padding = new Padding(3);
			this.StatBlockPage.Size = new Size(352, 228);
			this.StatBlockPage.TabIndex = 0;
			this.StatBlockPage.Text = "Stat Block";
			this.StatBlockPage.UseVisualStyleBackColor = true;
			this.Browser.AllowWebBrowserDrop = false;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(3, 3);
			this.Browser.MinimumSize = new Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new Size(346, 222);
			this.Browser.TabIndex = 0;
			this.Browser.WebBrowserShortcutsEnabled = false;
			this.MapPage.Controls.Add(this.Map);
			this.MapPage.Location = new Point(4, 22);
			this.MapPage.Name = "MapPage";
			this.MapPage.Padding = new Padding(3);
			this.MapPage.Size = new Size(352, 228);
			this.MapPage.TabIndex = 1;
			this.MapPage.Text = "Map Location";
			this.MapPage.UseVisualStyleBackColor = true;
			this.Map.AllowDrawing = false;
			this.Map.AllowDrop = true;
			this.Map.AllowLinkCreation = false;
			this.Map.AllowScrolling = false;
			this.Map.BackgroundMap = null;
			this.Map.BorderSize = 0;
			this.Map.Caption = "";
			this.Map.Cursor = Cursors.Default;
			this.Map.Dock = DockStyle.Fill;
			this.Map.Encounter = null;
			this.Map.FrameType = MapDisplayType.Dimmed;
			this.Map.HighlightAreas = false;
			this.Map.HoverToken = null;
			this.Map.LineOfSight = false;
			this.Map.Location = new Point(3, 3);
			this.Map.Map = null;
			this.Map.Mode = MapViewMode.Thumbnail;
			this.Map.Name = "Map";
			this.Map.Plot = null;
			this.Map.ScalingFactor = 1.0;
			this.Map.SelectedArea = null;
			this.Map.SelectedTiles = null;
			this.Map.Selection = new Rectangle(0, 0, 0, 0);
			this.Map.ShowAuras = false;
			this.Map.ShowConditions = false;
			this.Map.ShowCreatureLabels = true;
			this.Map.ShowCreatures = CreatureViewMode.All;
			this.Map.ShowGrid = MapGridMode.None;
			this.Map.ShowGridLabels = false;
			this.Map.ShowHealthBars = false;
			this.Map.ShowPictureTokens = true;
			this.Map.Size = new Size(346, 222);
			this.Map.TabIndex = 0;
			this.Map.Tactical = false;
			this.Map.TokenLinks = null;
			this.Map.Viewpoint = new Rectangle(0, 0, 0, 0);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(612, 368);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.SetNameBtn);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.CombatantList);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DisplayNameForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Display Names";
			this.Pages.ResumeLayout(false);
			this.StatBlockPage.ResumeLayout(false);
			this.MapPage.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
