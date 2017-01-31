using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Utils;
using Utils.Controls;

namespace Masterplan.UI
{
	internal class PlotPointForm : Form
	{
		private PlotPoint fPoint;

		private Plot fPlot;

		private bool fStartAtElement;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Button OKBtn;

		private Button CancelBtn;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TabPage LinksPage;

		private ListView LinkList;

		private ColumnHeader LinkHdr;

		private ToolStrip LinkToolbar;

		private ToolStripButton RemoveBtn;

		private TabPage RPGPage;

		private Button RemoveElementBtn;

		private Panel RPGPanel;

		private TabPage ParcelsPage;

		private ListView ParcelList;

		private ColumnHeader ParcelHdr;

		private ToolStrip ParcelToolbar;

		private ToolStripButton ParcelRemoveBtn;

		private ToolStripButton ParcelEditBtn;

		private ToolStrip MainToolbar;

		private ToolStripButton DateBtn;

		private ToolStripLabel ClearDateLbl;

		private SplitContainer TextSplitter;

		private DefaultTextBox ReadAloudBox;

		private DefaultTextBox DetailsBox;

		private TabPage EncyclopediaPage;

		private ListView EncyclopediaList;

		private ToolStrip EncyclopediaToolbar;

		private ColumnHeader EncHdr;

		private ToolStripButton EncyclopediaAddBtn;

		private ToolStripButton EncyclopediaRemoveBtn;

		private SplitContainer splitContainer1;

		private WebBrowser EncBrowser;

		private Panel EncBrowserPanel;

		private ColumnHeader ParcelDetailsHdr;

		private ToolStrip EncBrowserToolbar;

		private ToolStripButton EncPlayerViewBtn;

		private ColumnHeader LinkDetailsHdr;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton ChangeItemBtn;

		private ToolStripButton ItemStatBlockBtn;

		private ToolStripButton LocationBtn;

		private ToolStripLabel ClearLocationLbl;

		private NumericUpDown XPBox;

		private Label XPLbl;

		private Panel panel1;

		private ToolStripDropDownButton ParcelAddBtn;

		private ToolStripMenuItem ParcelAddPredefined;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem ParcelAddParcel;

		private ToolStripMenuItem ParcelAddItem;

		private ToolStripLabel StartXPLbl;

		private ColumnHeader ParcelHeroHdr;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripDropDownButton AllocateBtn;

		private ToolStripMenuItem heroesToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripSeparator XPSeparator;

		private ToolStripDropDownButton SettingsMenu;

		private ToolStripMenuItem SettingsColour;

		private ToolStripMenuItem SettingsState;

		private ToolStripSeparator toolStripSeparator2;

		private Button InfoBtn;

		private Button DieRollerBtn;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem ParcelAddArtifact;

		private Button CopyElementBtn;

		private Button CutElementBtn;

		public PlotPoint PlotPoint
		{
			get
			{
				return this.fPoint;
			}
			set
			{
				this.fPoint = value;
			}
		}

		public Parcel SelectedParcel
		{
			get
			{
				if (this.ParcelList.SelectedItems.Count != 0)
				{
					return this.ParcelList.SelectedItems[0].Tag as Parcel;
				}
				return null;
			}
		}

		private EncyclopediaEntry SelectedEntry
		{
			get
			{
				if (this.EncyclopediaList.SelectedItems.Count != 0)
				{
					return this.EncyclopediaList.SelectedItems[0].Tag as EncyclopediaEntry;
				}
				return null;
			}
		}

		private PlotPoint SelectedLink
		{
			get
			{
				if (this.LinkList.SelectedItems.Count != 0)
				{
					return this.LinkList.SelectedItems[0].Tag as PlotPoint;
				}
				return null;
			}
		}

		public PlotPointForm(PlotPoint pp, Plot p, bool start_at_element_page)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.ParcelList_SizeChanged(null, null);
			this.LinkList_SizeChanged(null, null);
			this.EncBrowser.DocumentText = "";
			this.fPoint = pp.Copy();
			this.fPlot = p;
			this.fStartAtElement = start_at_element_page;
			this.NameBox.Text = this.fPoint.Name;
			this.DetailsBox.Text = this.fPoint.Details;
			this.ReadAloudBox.Text = this.fPoint.ReadAloud;
			this.XPBox.Value = this.fPoint.AdditionalXP;
			if (this.fPlot.FindPoint(this.fPoint.ID) != null)
			{
				this.StartXPLbl.Text = "Start at: " + Workspace.GetTotalXP(this.fPoint) + " XP";
			}
			else
			{
				this.StartXPLbl.Visible = false;
				this.XPSeparator.Visible = false;
			}
			this.update_element();
			this.update_parcels();
			this.update_encyclopedia_entries();
			this.update_links();
			if (Session.Project.Encyclopedia.Entries.Count == 0)
			{
				this.Pages.TabPages.Remove(this.EncyclopediaPage);
			}
			else
			{
				this.EncyclopediaList_SelectedIndexChanged(null, null);
			}
			if (start_at_element_page)
			{
				this.Pages.SelectedTab = this.RPGPage;
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			MapLocation mapLocation = null;
			if (this.fPoint.RegionalMapID != Guid.Empty)
			{
				RegionalMap regionalMap = Session.Project.FindRegionalMap(this.fPoint.RegionalMapID);
				if (regionalMap != null && this.fPoint.MapLocationID != Guid.Empty)
				{
					mapLocation = regionalMap.FindLocation(this.fPoint.MapLocationID);
				}
			}
			int num = 0;
			foreach (RegionalMap current in Session.Project.RegionalMaps)
			{
				num += current.Locations.Count;
			}
			this.LocationBtn.Enabled = (num != 0);
			this.LocationBtn.Text = ((mapLocation != null) ? mapLocation.Name : "Set Location");
			this.ClearLocationLbl.Visible = (mapLocation != null);
			this.DateBtn.Enabled = (Session.Project.Calendars.Count != 0);
			this.DateBtn.Text = ((this.fPoint.Date != null) ? this.fPoint.Date.ToString() : "Set Date");
			this.ClearDateLbl.Visible = (this.fPoint.Date != null);
			this.CopyElementBtn.Visible = (this.fPoint.Element != null);
			this.RemoveElementBtn.Visible = (this.fPoint.Element != null);
			this.ParcelAddPredefined.Enabled = (Session.Project.TreasureParcels.Count != 0);
			this.ParcelAddItem.Enabled = (Session.MagicItems.Count != 0);
			this.ParcelAddArtifact.Enabled = (Session.Artifacts.Count != 0);
			this.ParcelRemoveBtn.Enabled = (this.SelectedParcel != null);
			this.ParcelEditBtn.Enabled = (this.SelectedParcel != null);
			if (this.SelectedParcel != null)
			{
				bool flag = this.SelectedParcel.MagicItemID != Guid.Empty;
				bool flag2 = flag && !Treasure.PlaceholderIDs.Contains(this.SelectedParcel.MagicItemID);
				bool flag3 = this.SelectedParcel.ArtifactID != Guid.Empty;
				bool flag4 = flag3 && !Treasure.PlaceholderIDs.Contains(this.SelectedParcel.ArtifactID);
				this.ChangeItemBtn.Enabled = (flag || flag3);
				this.ItemStatBlockBtn.Enabled = (flag2 || flag4);
				if (flag)
				{
					this.ChangeItemBtn.Text = "Select Magic Item";
				}
				else if (flag3)
				{
					this.ChangeItemBtn.Text = "Select Artifact";
				}
				else
				{
					this.ChangeItemBtn.Text = "Select";
				}
			}
			else
			{
				this.ChangeItemBtn.Enabled = false;
				this.ItemStatBlockBtn.Enabled = false;
			}
			this.EncyclopediaRemoveBtn.Enabled = (this.SelectedEntry != null);
			this.EncPlayerViewBtn.Enabled = (this.SelectedEntry != null);
			this.RemoveBtn.Enabled = (this.SelectedLink != null);
		}

		private void RemoveElementBtn_Click(object sender, EventArgs e)
		{
			if (this.fPoint.Element != null)
			{
				this.fPoint.Element = null;
				this.update_element();
			}
		}

		private void CopyElementBtn_Click(object sender, EventArgs e)
		{
			if (this.fPoint.Element != null)
			{
				string format = this.fPoint.Element.GetType().ToString();
				Clipboard.SetData(format, this.fPoint.Element);
			}
		}

		private void CutElementBtn_Click(object sender, EventArgs e)
		{
			if (this.fPoint.Element != null)
			{
				string format = this.fPoint.Element.GetType().ToString();
				Clipboard.SetData(format, this.fPoint.Element);
				this.fPoint.Element = null;
				this.update_element();
			}
		}

		private void update_element()
		{
			this.RPGPanel.Controls.Clear();
			Control control = null;
			int party_level = this.get_party_level();
			if (this.fPoint.Element is Encounter)
			{
				control = new EncounterPanel
				{
					Encounter = (this.fPoint.Element as Encounter),
					PartyLevel = party_level
				};
			}
			if (this.fPoint.Element is SkillChallenge)
			{
				control = new SkillChallengePanel
				{
					Challenge = (this.fPoint.Element as SkillChallenge),
					PartyLevel = party_level
				};
			}
			if (this.fPoint.Element is TrapElement)
			{
				control = new TrapElementPanel
				{
					Trap = (this.fPoint.Element as TrapElement)
				};
			}
			if (this.fPoint.Element is Quest)
			{
				control = new QuestPanel
				{
					Quest = (this.fPoint.Element as Quest)
				};
			}
			if (this.fPoint.Element is MapElement)
			{
				control = new MapElementPanel
				{
					MapElement = (this.fPoint.Element as MapElement)
				};
			}
			if (control == null)
			{
				WebBrowser webBrowser = new WebBrowser();
				webBrowser.IsWebBrowserContextMenuEnabled = false;
				webBrowser.ScriptErrorsSuppressed = true;
				webBrowser.WebBrowserShortcutsEnabled = false;
				webBrowser.ScrollBarsEnabled = false;
				webBrowser.DocumentText = this.get_element_html();
				webBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.element_select);
				control = webBrowser;
			}
			if (control != null)
			{
				control.Dock = DockStyle.Fill;
				this.RPGPanel.Controls.Add(control);
			}
		}

		private string get_element_html()
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead("Plot Point", "Plot Point", DisplaySize.Small));
			list.Add("<BODY>");
			list.Add("<P>");
			list.Add("This plot point does not contain a game element (such as an encounter or a skill challenge).");
			list.Add("The list of available game elements is below.");
			list.Add("You can add a game element to this plot point by clicking on one of the links.");
			list.Add("</P>");
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD><B>Select a Game Element</B></TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Add a <A href=\"element:encounter\">combat encounter</A></TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Add a <A href=\"element:challenge\">skill challenge</A></TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Add a <A href=\"element:trap\">trap or hazard</A></TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Add a <A href=\"element:quest\">quest</A></TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>Add a <A href=\"element:map\">tactical map</A></TD>");
			list.Add("</TR>");
			if (Clipboard.ContainsData(typeof(Encounter).ToString()))
			{
				list.Add("<TR>");
				list.Add("<TD><A href=\"element:pasteencounter\">Paste the encounter from the clipboard</A></TD>");
				list.Add("</TR>");
			}
			if (Clipboard.ContainsData(typeof(SkillChallenge).ToString()))
			{
				list.Add("<TR>");
				list.Add("<TD><A href=\"element:pastechallenge\">Paste the skill challenge from the clipboard</A></TD>");
				list.Add("</TR>");
			}
			if (Clipboard.ContainsData(typeof(TrapElement).ToString()))
			{
				list.Add("<TR>");
				list.Add("<TD><A href=\"element:pastetrap\">Paste the trap from the clipboard</A></TD>");
				list.Add("</TR>");
			}
			if (Clipboard.ContainsData(typeof(Quest).ToString()))
			{
				list.Add("<TR>");
				list.Add("<TD><A href=\"element:pastequest\">Paste the quest from the clipboard</A></TD>");
				list.Add("</TR>");
			}
			if (Clipboard.ContainsData(typeof(MapElement).ToString()))
			{
				list.Add("<TR>");
				list.Add("<TD><A href=\"element:pastemap\">Paste the map from the clipboard</A></TD>");
				list.Add("</TR>");
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		private void element_select(object sender, WebBrowserNavigatingEventArgs e)
		{
			int party_level = this.get_party_level();
			if (e.Url.LocalPath == "encounter")
			{
				Encounter encounter = new Encounter();
				encounter.SetStandardEncounterNotes();
				this.fPoint.Element = encounter;
				this.update_element();
			}
			if (e.Url.LocalPath == "pasteencounter")
			{
				Encounter element = Clipboard.GetData(typeof(Encounter).ToString()) as Encounter;
				this.fPoint.Element = element;
				this.update_element();
			}
			if (e.Url.LocalPath == "challenge")
			{
				SkillChallenge skillChallenge = new SkillChallenge();
				skillChallenge.Name = "Unnamed Skill Challenge";
				skillChallenge.Level = party_level;
				this.fPoint.Element = skillChallenge;
				this.update_element();
			}
			if (e.Url.LocalPath == "pastechallenge")
			{
				SkillChallenge skillChallenge2 = Clipboard.GetData(typeof(SkillChallenge).ToString()) as SkillChallenge;
				skillChallenge2.Level = party_level;
				this.fPoint.Element = skillChallenge2;
				this.update_element();
			}
			if (e.Url.LocalPath == "trap")
			{
				TrapElement trapElement = new TrapElement();
				trapElement.Trap.Name = "Unnamed Trap";
				trapElement.Trap.Level = party_level;
				this.fPoint.Element = trapElement;
				this.update_element();
			}
			if (e.Url.LocalPath == "pastetrap")
			{
				TrapElement trapElement2 = Clipboard.GetData(typeof(TrapElement).ToString()) as TrapElement;
				trapElement2.Trap.Level = party_level;
				this.fPoint.Element = trapElement2;
				this.update_element();
			}
			if (e.Url.LocalPath == "quest")
			{
				Quest quest = new Quest();
				quest.Level = party_level;
				this.fPoint.Element = quest;
				this.update_element();
			}
			if (e.Url.LocalPath == "pastequest")
			{
				Quest quest2 = Clipboard.GetData(typeof(Quest).ToString()) as Quest;
				quest2.Level = party_level;
				this.fPoint.Element = quest2;
				this.update_element();
			}
			if (e.Url.LocalPath == "map")
			{
				MapElement element2 = new MapElement();
				this.fPoint.Element = element2;
				this.update_element();
			}
			if (e.Url.LocalPath == "pastemap")
			{
				MapElement element3 = Clipboard.GetData(typeof(MapElement).ToString()) as MapElement;
				this.fPoint.Element = element3;
				this.update_element();
			}
		}

		private void ParcelAddPredefined_Click(object sender, EventArgs e)
		{
			ParcelSelectForm parcelSelectForm = new ParcelSelectForm();
			if (parcelSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fPoint.Parcels.Add(parcelSelectForm.Parcel);
				Session.Project.TreasureParcels.Remove(parcelSelectForm.Parcel);
				this.update_parcels();
			}
		}

		private void ParcelAddParcel_Click(object sender, EventArgs e)
		{
			ParcelForm parcelForm = new ParcelForm(new Parcel
			{
				Name = "New Treasure Parcel"
			});
			if (parcelForm.ShowDialog() == DialogResult.OK)
			{
				this.fPoint.Parcels.Add(parcelForm.Parcel);
				this.update_parcels();
			}
		}

		private void ParcelAddItem_Click(object sender, EventArgs e)
		{
			MagicItemSelectForm magicItemSelectForm = new MagicItemSelectForm(Session.Project.Party.Level);
			if (magicItemSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fPoint.Parcels.Add(new Parcel(magicItemSelectForm.MagicItem));
				this.update_parcels();
			}
		}

		private void ParcelAddArtifact_Click(object sender, EventArgs e)
		{
			ArtifactSelectForm artifactSelectForm = new ArtifactSelectForm();
			if (artifactSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fPoint.Parcels.Add(new Parcel(artifactSelectForm.Artifact));
				this.update_parcels();
			}
		}

		private void ParcelRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedParcel != null)
			{
				this.fPoint.Parcels.Remove(this.SelectedParcel);
				Session.Project.TreasureParcels.Add(this.SelectedParcel);
				this.update_parcels();
			}
		}

		private void ParcelEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedParcel != null)
			{
				int index = this.fPoint.Parcels.IndexOf(this.SelectedParcel);
				ParcelForm parcelForm = new ParcelForm(this.SelectedParcel);
				if (parcelForm.ShowDialog() == DialogResult.OK)
				{
					this.fPoint.Parcels[index] = parcelForm.Parcel;
					this.update_parcels();
				}
			}
		}

		private void ChangeItemBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedParcel != null)
			{
				if (this.SelectedParcel.MagicItemID != Guid.Empty)
				{
					int num = this.SelectedParcel.FindItemLevel();
					if (num != -1)
					{
						MagicItemSelectForm magicItemSelectForm = new MagicItemSelectForm(num);
						if (magicItemSelectForm.ShowDialog() == DialogResult.OK)
						{
							this.SelectedParcel.SetAsMagicItem(magicItemSelectForm.MagicItem);
						}
					}
				}
				else if (this.SelectedParcel.ArtifactID != Guid.Empty)
				{
					ArtifactSelectForm artifactSelectForm = new ArtifactSelectForm();
					if (artifactSelectForm.ShowDialog() == DialogResult.OK)
					{
						this.SelectedParcel.SetAsArtifact(artifactSelectForm.Artifact);
					}
				}
				this.update_parcels();
			}
		}

		private void ItemStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedParcel != null)
			{
				if (this.SelectedParcel.MagicItemID != Guid.Empty)
				{
					MagicItem magicItem = Session.FindMagicItem(this.SelectedParcel.MagicItemID, SearchType.Global);
					if (magicItem != null)
					{
						MagicItemDetailsForm magicItemDetailsForm = new MagicItemDetailsForm(magicItem);
						magicItemDetailsForm.ShowDialog();
						return;
					}
				}
				else if (this.SelectedParcel.ArtifactID != Guid.Empty)
				{
					Artifact artifact = Session.FindArtifact(this.SelectedParcel.ArtifactID, SearchType.Global);
					if (artifact != null)
					{
						ArtifactDetailsForm artifactDetailsForm = new ArtifactDetailsForm(artifact);
						artifactDetailsForm.ShowDialog();
					}
				}
			}
		}

		private void ParcelList_SizeChanged(object sender, EventArgs e)
		{
			int width = this.ParcelList.Width - (SystemInformation.VerticalScrollBarWidth + 6);
			this.ParcelList.TileSize = new Size(width, this.ParcelList.TileSize.Height);
		}

		private void update_parcels()
		{
			this.ParcelList.Items.Clear();
			foreach (Parcel current in this.fPoint.Parcels)
			{
				string text = current.Name;
				if (text == "")
				{
					text = "(undefined parcel)";
				}
				if (current.MagicItemID != Guid.Empty && !Treasure.PlaceholderIDs.Contains(current.MagicItemID))
				{
					MagicItem magicItem = Session.FindMagicItem(current.MagicItemID, SearchType.Global);
					if (magicItem != null)
					{
						text = text + " (" + magicItem.Info.ToLower() + ")";
					}
				}
				if (current.ArtifactID != Guid.Empty && !Treasure.PlaceholderIDs.Contains(current.ArtifactID))
				{
					Artifact artifact = Session.FindArtifact(current.ArtifactID, SearchType.Global);
					if (artifact != null)
					{
						text = text + " (" + artifact.Tier.ToString().ToLower() + " tier)";
					}
				}
				ListViewItem listViewItem = this.ParcelList.Items.Add(text);
				listViewItem.Tag = current;
				if (current.Details != "")
				{
					listViewItem.SubItems.Add(current.Details);
				}
				else
				{
					listViewItem.SubItems.Add("(no details)");
				}
				Hero hero = null;
				if (current.HeroID != Guid.Empty)
				{
					hero = Session.Project.FindHero(current.HeroID);
				}
				if (hero != null)
				{
					listViewItem.SubItems.Add("Allocated to " + hero.Name);
				}
				else
				{
					listViewItem.SubItems.Add("(not allocated to a PC)");
				}
			}
			if (this.ParcelList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.ParcelList.Items.Add("(no parcels)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
		}

		private void EncyclopediaAddBtn_Click(object sender, EventArgs e)
		{
			EncyclopediaEntrySelectForm encyclopediaEntrySelectForm = new EncyclopediaEntrySelectForm(this.fPoint.EncyclopediaEntryIDs);
			if (encyclopediaEntrySelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fPoint.EncyclopediaEntryIDs.Add(encyclopediaEntrySelectForm.EncyclopediaEntry.ID);
				this.update_encyclopedia_entries();
			}
		}

		private void EncyclopediaRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEntry != null)
			{
				this.fPoint.EncyclopediaEntryIDs.Remove(this.SelectedEntry.ID);
				this.update_encyclopedia_entries();
			}
		}

		private void EncPlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEntry != null)
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
				}
				Session.PlayerView.ShowEncyclopediaItem(this.SelectedEntry);
			}
		}

		private void EncyclopediaList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = HTML.EncyclopediaEntry(this.SelectedEntry, Session.Project.Encyclopedia, DisplaySize.Small, true, false, false, true);
			this.EncBrowser.Document.OpenNew(true);
			this.EncBrowser.Document.Write(text);
		}

		private void update_encyclopedia_entries()
		{
			this.EncyclopediaList.BeginUpdate();
			this.EncyclopediaList.Items.Clear();
			foreach (Guid current in this.fPoint.EncyclopediaEntryIDs)
			{
				EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntry(current);
				if (encyclopediaEntry != null)
				{
					ListViewItem listViewItem = this.EncyclopediaList.Items.Add(encyclopediaEntry.Name);
					listViewItem.Tag = encyclopediaEntry;
				}
			}
			if (this.EncyclopediaList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.EncyclopediaList.Items.Add("(no encyclopedia entries)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			this.EncyclopediaList.EndUpdate();
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLink != null)
			{
				if (this.SelectedLink.Links.Contains(this.fPoint.ID))
				{
					while (this.SelectedLink.Links.Contains(this.fPoint.ID))
					{
						this.SelectedLink.Links.Remove(this.fPoint.ID);
					}
				}
				else if (this.fPoint.Links.Contains(this.SelectedLink.ID))
				{
					while (this.fPoint.Links.Contains(this.SelectedLink.ID))
					{
						this.fPoint.Links.Remove(this.SelectedLink.ID);
					}
				}
				this.update_links();
			}
		}

		private void LinkList_SizeChanged(object sender, EventArgs e)
		{
			int width = this.LinkList.Width - (SystemInformation.VerticalScrollBarWidth + 6);
			this.LinkList.TileSize = new Size(width, this.LinkList.TileSize.Height);
		}

		private void update_links()
		{
			this.LinkList.Items.Clear();
			foreach (PlotPoint current in this.fPlot.Points)
			{
				if (current.Links.Contains(this.fPoint.ID))
				{
					ListViewItem listViewItem = this.LinkList.Items.Add(current.Name);
					listViewItem.SubItems.Add((current.Details != "") ? current.Details : "(no details)");
					listViewItem.Tag = current;
					listViewItem.Group = this.LinkList.Groups[0];
				}
			}
			foreach (Guid current2 in this.fPoint.Links)
			{
				PlotPoint plotPoint = this.fPlot.FindPoint(current2);
				if (plotPoint != null)
				{
					ListViewItem listViewItem2 = this.LinkList.Items.Add(plotPoint.Name);
					listViewItem2.SubItems.Add((plotPoint.Details != "") ? plotPoint.Details : "(no details)");
					listViewItem2.Tag = plotPoint;
					listViewItem2.Group = this.LinkList.Groups[1];
				}
			}
			foreach (ListViewGroup listViewGroup in this.LinkList.Groups)
			{
				if (listViewGroup.Items.Count == 0)
				{
					ListViewItem listViewItem3 = this.LinkList.Items.Add("(none)");
					listViewItem3.ForeColor = SystemColors.GrayText;
					listViewItem3.Group = listViewGroup;
				}
			}
		}

		private void LocationBtn_Click(object sender, EventArgs e)
		{
			MapLocationSelectForm mapLocationSelectForm = new MapLocationSelectForm(this.fPoint.RegionalMapID, this.fPoint.MapLocationID);
			if (mapLocationSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fPoint.RegionalMapID = ((mapLocationSelectForm.Map != null) ? mapLocationSelectForm.Map.ID : Guid.Empty);
				this.fPoint.MapLocationID = ((mapLocationSelectForm.MapLocation != null) ? mapLocationSelectForm.MapLocation.ID : Guid.Empty);
			}
		}

		private void ClearLocationLbl_Click(object sender, EventArgs e)
		{
			this.fPoint.RegionalMapID = Guid.Empty;
			this.fPoint.MapLocationID = Guid.Empty;
		}

		private void DateBtn_Click(object sender, EventArgs e)
		{
			CalendarDate calendarDate = this.fPoint.Date;
			if (calendarDate == null)
			{
				calendarDate = new CalendarDate();
				Calendar calendar = Session.Project.Calendars[0];
				calendarDate.CalendarID = calendar.ID;
				calendarDate.Year = calendar.CampaignYear;
			}
			DateForm dateForm = new DateForm(calendarDate);
			if (dateForm.ShowDialog() == DialogResult.OK)
			{
				this.fPoint.Date = dateForm.Date;
			}
		}

		private void ClearDateLbl_Click(object sender, EventArgs e)
		{
			this.fPoint.Date = null;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fPoint.Name = this.NameBox.Text;
			this.fPoint.Details = ((this.DetailsBox.Text != this.DetailsBox.DefaultText) ? this.DetailsBox.Text : "");
			this.fPoint.ReadAloud = ((this.ReadAloudBox.Text != this.ReadAloudBox.DefaultText) ? this.ReadAloudBox.Text : "");
			this.fPoint.AdditionalXP = (int)this.XPBox.Value;
		}

		private void AllocateBtn_DropDownOpening(object sender, EventArgs e)
		{
			this.AllocateBtn.DropDownItems.Clear();
			foreach (Hero current in Session.Project.Heroes)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(current.Name);
				toolStripMenuItem.Tag = current;
				toolStripMenuItem.Click += new EventHandler(this.assign_to_hero);
				if (this.SelectedParcel != null)
				{
					toolStripMenuItem.Checked = (this.SelectedParcel.HeroID == current.ID);
				}
				this.AllocateBtn.DropDownItems.Add(toolStripMenuItem);
			}
			if (Session.Project.Heroes.Count != 0)
			{
				this.AllocateBtn.DropDownItems.Add(new ToolStripSeparator());
			}
			ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Not Allocated");
			toolStripMenuItem2.Tag = null;
			toolStripMenuItem2.Click += new EventHandler(this.assign_to_hero);
			if (this.SelectedParcel != null)
			{
				toolStripMenuItem2.Checked = (this.SelectedParcel.HeroID == Guid.Empty);
			}
			this.AllocateBtn.DropDownItems.Add(toolStripMenuItem2);
		}

		private void assign_to_hero(object sender, EventArgs e)
		{
			if (this.SelectedParcel == null)
			{
				return;
			}
			ToolStripItem toolStripItem = sender as ToolStripItem;
			if (toolStripItem == null)
			{
				return;
			}
			Hero hero = toolStripItem.Tag as Hero;
			this.SelectedParcel.HeroID = ((hero != null) ? hero.ID : Guid.Empty);
			this.update_parcels();
		}

		private void PlotPointForm_Shown(object sender, EventArgs e)
		{
			if (this.fStartAtElement)
			{
				if (this.RPGPanel.Controls.Count == 0)
				{
					return;
				}
				EncounterPanel encounterPanel = this.RPGPanel.Controls[0] as EncounterPanel;
				if (encounterPanel != null)
				{
					encounterPanel.Edit();
				}
				SkillChallengePanel skillChallengePanel = this.RPGPanel.Controls[0] as SkillChallengePanel;
				if (skillChallengePanel != null)
				{
					skillChallengePanel.Edit();
				}
			}
		}

		private int get_party_level()
		{
			int result = Session.Project.Party.Level;
			if (this.fPlot.FindPoint(this.fPoint.ID) != null)
			{
				result = Workspace.GetPartyLevel(this.fPoint);
			}
			else if (this.fPlot.Points.Count > 0)
			{
				List<List<PlotPoint>> list = Workspace.FindLayers(this.fPlot);
				result = Workspace.GetPartyLevel(list[0][0]);
			}
			return result;
		}

		private void SettingsMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.SettingsMenu.DropDownItems.Clear();
			Array values = Enum.GetValues(typeof(PlotPointState));
			foreach (PlotPointState plotPointState in values)
			{
				ToolStripMenuItem toolStripMenuItem = this.SettingsMenu.DropDownItems.Add(plotPointState.ToString()) as ToolStripMenuItem;
				toolStripMenuItem.Tag = plotPointState;
				toolStripMenuItem.Checked = (this.fPoint.State == plotPointState);
				toolStripMenuItem.Click += new EventHandler(this.select_state);
			}
			this.SettingsMenu.DropDownItems.Add(new ToolStripSeparator());
			Array values2 = Enum.GetValues(typeof(PlotPointColour));
			foreach (PlotPointColour plotPointColour in values2)
			{
				string text = plotPointColour.ToString();
				if (plotPointColour == PlotPointColour.Yellow)
				{
					text += " (default)";
				}
				Bitmap image = new Bitmap(16, 16);
				Rectangle rect = new Rectangle(0, 0, 16, 16);
				Pair<Color, Color> colourGradient = PlotView.GetColourGradient(plotPointColour, 255);
				Graphics graphics = Graphics.FromImage(image);
				graphics.FillRectangle(new LinearGradientBrush(rect, colourGradient.First, colourGradient.Second, LinearGradientMode.Vertical), rect);
				ToolStripMenuItem toolStripMenuItem2 = this.SettingsMenu.DropDownItems.Add(text) as ToolStripMenuItem;
				toolStripMenuItem2.Image = image;
				toolStripMenuItem2.Tag = plotPointColour;
				toolStripMenuItem2.Checked = (this.fPoint.Colour == plotPointColour);
				toolStripMenuItem2.Click += new EventHandler(this.select_colour);
			}
		}

		private void select_state(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				PlotPointState state = (PlotPointState)toolStripMenuItem.Tag;
				this.fPoint.State = state;
			}
		}

		private void select_colour(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				PlotPointColour colour = (PlotPointColour)toolStripMenuItem.Tag;
				this.fPoint.Colour = colour;
			}
		}

		private void InfoBtn_Click(object sender, EventArgs e)
		{
			new InfoForm
			{
				Level = this.get_party_level()
			}.ShowDialog();
		}

		private void DieRollerBtn_Click(object sender, EventArgs e)
		{
			DieRollerForm dieRollerForm = new DieRollerForm();
			dieRollerForm.ShowDialog();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PlotPointForm));
			ListViewGroup listViewGroup = new ListViewGroup("Links To This Point", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Links From This Point", HorizontalAlignment.Left);
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.TextSplitter = new SplitContainer();
			this.DetailsBox = new DefaultTextBox();
			this.ReadAloudBox = new DefaultTextBox();
			this.MainToolbar = new ToolStrip();
			this.SettingsMenu = new ToolStripDropDownButton();
			this.SettingsColour = new ToolStripMenuItem();
			this.SettingsState = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.StartXPLbl = new ToolStripLabel();
			this.XPSeparator = new ToolStripSeparator();
			this.LocationBtn = new ToolStripButton();
			this.ClearLocationLbl = new ToolStripLabel();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.DateBtn = new ToolStripButton();
			this.ClearDateLbl = new ToolStripLabel();
			this.RPGPage = new TabPage();
			this.RPGPanel = new Panel();
			this.panel1 = new Panel();
			this.CutElementBtn = new Button();
			this.CopyElementBtn = new Button();
			this.XPLbl = new Label();
			this.XPBox = new NumericUpDown();
			this.RemoveElementBtn = new Button();
			this.ParcelsPage = new TabPage();
			this.ParcelList = new ListView();
			this.ParcelHdr = new ColumnHeader();
			this.ParcelDetailsHdr = new ColumnHeader();
			this.ParcelHeroHdr = new ColumnHeader();
			this.ParcelToolbar = new ToolStrip();
			this.ParcelAddBtn = new ToolStripDropDownButton();
			this.ParcelAddParcel = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.ParcelAddPredefined = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.ParcelAddItem = new ToolStripMenuItem();
			this.ParcelAddArtifact = new ToolStripMenuItem();
			this.ParcelRemoveBtn = new ToolStripButton();
			this.ParcelEditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.ChangeItemBtn = new ToolStripButton();
			this.ItemStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.AllocateBtn = new ToolStripDropDownButton();
			this.heroesToolStripMenuItem = new ToolStripMenuItem();
			this.EncyclopediaPage = new TabPage();
			this.splitContainer1 = new SplitContainer();
			this.EncyclopediaList = new ListView();
			this.EncHdr = new ColumnHeader();
			this.EncyclopediaToolbar = new ToolStrip();
			this.EncyclopediaAddBtn = new ToolStripButton();
			this.EncyclopediaRemoveBtn = new ToolStripButton();
			this.EncBrowserPanel = new Panel();
			this.EncBrowser = new WebBrowser();
			this.EncBrowserToolbar = new ToolStrip();
			this.EncPlayerViewBtn = new ToolStripButton();
			this.LinksPage = new TabPage();
			this.LinkList = new ListView();
			this.LinkHdr = new ColumnHeader();
			this.LinkDetailsHdr = new ColumnHeader();
			this.LinkToolbar = new ToolStrip();
			this.RemoveBtn = new ToolStripButton();
			this.InfoBtn = new Button();
			this.DieRollerBtn = new Button();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.TextSplitter.Panel1.SuspendLayout();
			this.TextSplitter.Panel2.SuspendLayout();
			this.TextSplitter.SuspendLayout();
			this.MainToolbar.SuspendLayout();
			this.RPGPage.SuspendLayout();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.XPBox).BeginInit();
			this.ParcelsPage.SuspendLayout();
			this.ParcelToolbar.SuspendLayout();
			this.EncyclopediaPage.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.EncyclopediaToolbar.SuspendLayout();
			this.EncBrowserPanel.SuspendLayout();
			this.EncBrowserToolbar.SuspendLayout();
			this.LinksPage.SuspendLayout();
			this.LinkToolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(9, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(53, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(584, 20);
			this.NameBox.TabIndex = 1;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(481, 338);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(562, 338);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.RPGPage);
			this.Pages.Controls.Add(this.ParcelsPage);
			this.Pages.Controls.Add(this.EncyclopediaPage);
			this.Pages.Controls.Add(this.LinksPage);
			this.Pages.Location = new Point(12, 38);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(625, 294);
			this.Pages.TabIndex = 2;
			this.DetailsPage.Controls.Add(this.TextSplitter);
			this.DetailsPage.Controls.Add(this.MainToolbar);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(617, 268);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.TextSplitter.Dock = DockStyle.Fill;
			this.TextSplitter.Location = new Point(3, 28);
			this.TextSplitter.Name = "TextSplitter";
			this.TextSplitter.Panel1.Controls.Add(this.DetailsBox);
			this.TextSplitter.Panel2.Controls.Add(this.ReadAloudBox);
			this.TextSplitter.Size = new Size(611, 237);
			this.TextSplitter.SplitterDistance = 374;
			this.TextSplitter.TabIndex = 1;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.DefaultText = "(no details)";
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(0, 0);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(374, 237);
			this.DetailsBox.TabIndex = 0;
			this.DetailsBox.Text = "(no details)";
			this.ReadAloudBox.AcceptsReturn = true;
			this.ReadAloudBox.AcceptsTab = true;
			this.ReadAloudBox.DefaultText = "(no read-aloud text)";
			this.ReadAloudBox.Dock = DockStyle.Fill;
			this.ReadAloudBox.Location = new Point(0, 0);
			this.ReadAloudBox.Multiline = true;
			this.ReadAloudBox.Name = "ReadAloudBox";
			this.ReadAloudBox.ScrollBars = ScrollBars.Vertical;
			this.ReadAloudBox.Size = new Size(233, 237);
			this.ReadAloudBox.TabIndex = 0;
			this.ReadAloudBox.Text = "(no read-aloud text)";
			this.MainToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.SettingsMenu,
				this.toolStripSeparator2,
				this.StartXPLbl,
				this.XPSeparator,
				this.LocationBtn,
				this.ClearLocationLbl,
				this.toolStripSeparator6,
				this.DateBtn,
				this.ClearDateLbl
			});
			this.MainToolbar.Location = new Point(3, 3);
			this.MainToolbar.Name = "MainToolbar";
			this.MainToolbar.Size = new Size(611, 25);
			this.MainToolbar.TabIndex = 0;
			this.MainToolbar.Text = "toolStrip1";
			this.SettingsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SettingsMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.SettingsColour,
				this.SettingsState
			});
			this.SettingsMenu.Image = (Image)componentResourceManager.GetObject("SettingsMenu.Image");
			this.SettingsMenu.ImageTransparentColor = Color.Magenta;
			this.SettingsMenu.Name = "SettingsMenu";
			this.SettingsMenu.Size = new Size(62, 22);
			this.SettingsMenu.Text = "Settings";
			this.SettingsMenu.DropDownOpening += new EventHandler(this.SettingsMenu_DropDownOpening);
			this.SettingsColour.Name = "SettingsColour";
			this.SettingsColour.Size = new Size(110, 22);
			this.SettingsColour.Text = "Colour";
			this.SettingsState.Name = "SettingsState";
			this.SettingsState.Size = new Size(110, 22);
			this.SettingsState.Text = "State";
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.StartXPLbl.Name = "StartXPLbl";
			this.StartXPLbl.Size = new Size(27, 22);
			this.StartXPLbl.Text = "[xp]";
			this.XPSeparator.Name = "XPSeparator";
			this.XPSeparator.Size = new Size(6, 25);
			this.LocationBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LocationBtn.Image = (Image)componentResourceManager.GetObject("LocationBtn.Image");
			this.LocationBtn.ImageTransparentColor = Color.Magenta;
			this.LocationBtn.Name = "LocationBtn";
			this.LocationBtn.Size = new Size(76, 22);
			this.LocationBtn.Text = "Set Location";
			this.LocationBtn.Click += new EventHandler(this.LocationBtn_Click);
			this.ClearLocationLbl.IsLink = true;
			this.ClearLocationLbl.Name = "ClearLocationLbl";
			this.ClearLocationLbl.Size = new Size(34, 22);
			this.ClearLocationLbl.Text = "Clear";
			this.ClearLocationLbl.Click += new EventHandler(this.ClearLocationLbl_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new Size(6, 25);
			this.DateBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DateBtn.Image = (Image)componentResourceManager.GetObject("DateBtn.Image");
			this.DateBtn.ImageTransparentColor = Color.Magenta;
			this.DateBtn.Name = "DateBtn";
			this.DateBtn.Size = new Size(54, 22);
			this.DateBtn.Text = "Set Date";
			this.DateBtn.Click += new EventHandler(this.DateBtn_Click);
			this.ClearDateLbl.IsLink = true;
			this.ClearDateLbl.Name = "ClearDateLbl";
			this.ClearDateLbl.Size = new Size(34, 22);
			this.ClearDateLbl.Text = "Clear";
			this.ClearDateLbl.Click += new EventHandler(this.ClearDateLbl_Click);
			this.RPGPage.Controls.Add(this.RPGPanel);
			this.RPGPage.Controls.Add(this.panel1);
			this.RPGPage.Location = new Point(4, 22);
			this.RPGPage.Name = "RPGPage";
			this.RPGPage.Padding = new Padding(3);
			this.RPGPage.Size = new Size(617, 268);
			this.RPGPage.TabIndex = 2;
			this.RPGPage.Text = "Game Element";
			this.RPGPage.UseVisualStyleBackColor = true;
			this.RPGPanel.Dock = DockStyle.Fill;
			this.RPGPanel.Location = new Point(3, 3);
			this.RPGPanel.Name = "RPGPanel";
			this.RPGPanel.Size = new Size(611, 231);
			this.RPGPanel.TabIndex = 0;
			this.panel1.Controls.Add(this.CutElementBtn);
			this.panel1.Controls.Add(this.CopyElementBtn);
			this.panel1.Controls.Add(this.XPLbl);
			this.panel1.Controls.Add(this.XPBox);
			this.panel1.Controls.Add(this.RemoveElementBtn);
			this.panel1.Dock = DockStyle.Bottom;
			this.panel1.Location = new Point(3, 234);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(611, 31);
			this.panel1.TabIndex = 1;
			this.CutElementBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CutElementBtn.Location = new Point(543, 5);
			this.CutElementBtn.Name = "CutElementBtn";
			this.CutElementBtn.Size = new Size(65, 23);
			this.CutElementBtn.TabIndex = 4;
			this.CutElementBtn.Text = "Cut";
			this.CutElementBtn.UseVisualStyleBackColor = true;
			this.CutElementBtn.Click += new EventHandler(this.CutElementBtn_Click);
			this.CopyElementBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CopyElementBtn.Location = new Point(472, 5);
			this.CopyElementBtn.Name = "CopyElementBtn";
			this.CopyElementBtn.Size = new Size(65, 23);
			this.CopyElementBtn.TabIndex = 3;
			this.CopyElementBtn.Text = "Copy";
			this.CopyElementBtn.UseVisualStyleBackColor = true;
			this.CopyElementBtn.Click += new EventHandler(this.CopyElementBtn_Click);
			this.XPLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.XPLbl.AutoSize = true;
			this.XPLbl.Location = new Point(3, 10);
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new Size(112, 13);
			this.XPLbl.TabIndex = 0;
			this.XPLbl.Text = "Additional XP granted:";
			this.XPBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			NumericUpDown arg_10B1_0 = this.XPBox;
			int[] array = new int[4];
			array[0] = 50;
			arg_10B1_0.Increment = new decimal(array);
			this.XPBox.Location = new Point(121, 8);
			NumericUpDown arg_10E7_0 = this.XPBox;
			int[] array2 = new int[4];
			array2[0] = 100000;
			arg_10E7_0.Maximum = new decimal(array2);
			this.XPBox.Minimum = new decimal(new int[]
			{
				100000,
				0,
				0,
				-2147483648
			});
			this.XPBox.Name = "XPBox";
			this.XPBox.Size = new Size(155, 20);
			this.XPBox.TabIndex = 1;
			this.RemoveElementBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.RemoveElementBtn.Location = new Point(330, 5);
			this.RemoveElementBtn.Name = "RemoveElementBtn";
			this.RemoveElementBtn.Size = new Size(136, 23);
			this.RemoveElementBtn.TabIndex = 2;
			this.RemoveElementBtn.Text = "Remove Game Element";
			this.RemoveElementBtn.UseVisualStyleBackColor = true;
			this.RemoveElementBtn.Click += new EventHandler(this.RemoveElementBtn_Click);
			this.ParcelsPage.Controls.Add(this.ParcelList);
			this.ParcelsPage.Controls.Add(this.ParcelToolbar);
			this.ParcelsPage.Location = new Point(4, 22);
			this.ParcelsPage.Name = "ParcelsPage";
			this.ParcelsPage.Padding = new Padding(3);
			this.ParcelsPage.Size = new Size(617, 268);
			this.ParcelsPage.TabIndex = 3;
			this.ParcelsPage.Text = "Treasure Parcels";
			this.ParcelsPage.UseVisualStyleBackColor = true;
			this.ParcelList.Columns.AddRange(new ColumnHeader[]
			{
				this.ParcelHdr,
				this.ParcelDetailsHdr,
				this.ParcelHeroHdr
			});
			this.ParcelList.Dock = DockStyle.Fill;
			this.ParcelList.FullRowSelect = true;
			this.ParcelList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ParcelList.HideSelection = false;
			this.ParcelList.Location = new Point(3, 28);
			this.ParcelList.MultiSelect = false;
			this.ParcelList.Name = "ParcelList";
			this.ParcelList.Size = new Size(611, 237);
			this.ParcelList.Sorting = SortOrder.Ascending;
			this.ParcelList.TabIndex = 1;
			this.ParcelList.TileSize = new Size(200, 50);
			this.ParcelList.UseCompatibleStateImageBehavior = false;
			this.ParcelList.View = View.Tile;
			this.ParcelList.SizeChanged += new EventHandler(this.ParcelList_SizeChanged);
			this.ParcelList.DoubleClick += new EventHandler(this.ParcelEditBtn_Click);
			this.ParcelHdr.Text = "Parcel";
			this.ParcelHdr.Width = 200;
			this.ParcelDetailsHdr.Text = "Details";
			this.ParcelDetailsHdr.Width = 250;
			this.ParcelHeroHdr.Text = "PC";
			this.ParcelHeroHdr.Width = 150;
			this.ParcelToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.ParcelAddBtn,
				this.ParcelRemoveBtn,
				this.ParcelEditBtn,
				this.toolStripSeparator1,
				this.ChangeItemBtn,
				this.ItemStatBlockBtn,
				this.toolStripSeparator5,
				this.AllocateBtn
			});
			this.ParcelToolbar.Location = new Point(3, 3);
			this.ParcelToolbar.Name = "ParcelToolbar";
			this.ParcelToolbar.Size = new Size(611, 25);
			this.ParcelToolbar.TabIndex = 0;
			this.ParcelToolbar.Text = "toolStrip1";
			this.ParcelAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ParcelAddBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ParcelAddParcel,
				this.toolStripSeparator3,
				this.ParcelAddPredefined,
				this.toolStripSeparator4,
				this.ParcelAddItem,
				this.ParcelAddArtifact
			});
			this.ParcelAddBtn.Image = (Image)componentResourceManager.GetObject("ParcelAddBtn.Image");
			this.ParcelAddBtn.ImageTransparentColor = Color.Magenta;
			this.ParcelAddBtn.Name = "ParcelAddBtn";
			this.ParcelAddBtn.Size = new Size(42, 22);
			this.ParcelAddBtn.Text = "Add";
			this.ParcelAddParcel.Name = "ParcelAddParcel";
			this.ParcelAddParcel.Size = new Size(186, 22);
			this.ParcelAddParcel.Text = "Parcel";
			this.ParcelAddParcel.Click += new EventHandler(this.ParcelAddParcel_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(183, 6);
			this.ParcelAddPredefined.Name = "ParcelAddPredefined";
			this.ParcelAddPredefined.Size = new Size(186, 22);
			this.ParcelAddPredefined.Text = "Predefined Parcel";
			this.ParcelAddPredefined.Click += new EventHandler(this.ParcelAddPredefined_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new Size(183, 6);
			this.ParcelAddItem.Name = "ParcelAddItem";
			this.ParcelAddItem.Size = new Size(186, 22);
			this.ParcelAddItem.Text = "Select a Magic Item...";
			this.ParcelAddItem.Click += new EventHandler(this.ParcelAddItem_Click);
			this.ParcelAddArtifact.Name = "ParcelAddArtifact";
			this.ParcelAddArtifact.Size = new Size(186, 22);
			this.ParcelAddArtifact.Text = "Select an Artifact...";
			this.ParcelAddArtifact.Click += new EventHandler(this.ParcelAddArtifact_Click);
			this.ParcelRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ParcelRemoveBtn.Image = (Image)componentResourceManager.GetObject("ParcelRemoveBtn.Image");
			this.ParcelRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.ParcelRemoveBtn.Name = "ParcelRemoveBtn";
			this.ParcelRemoveBtn.Size = new Size(54, 22);
			this.ParcelRemoveBtn.Text = "Remove";
			this.ParcelRemoveBtn.Click += new EventHandler(this.ParcelRemoveBtn_Click);
			this.ParcelEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ParcelEditBtn.Image = (Image)componentResourceManager.GetObject("ParcelEditBtn.Image");
			this.ParcelEditBtn.ImageTransparentColor = Color.Magenta;
			this.ParcelEditBtn.Name = "ParcelEditBtn";
			this.ParcelEditBtn.Size = new Size(31, 22);
			this.ParcelEditBtn.Text = "Edit";
			this.ParcelEditBtn.Click += new EventHandler(this.ParcelEditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.ChangeItemBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChangeItemBtn.Image = (Image)componentResourceManager.GetObject("ChangeItemBtn.Image");
			this.ChangeItemBtn.ImageTransparentColor = Color.Magenta;
			this.ChangeItemBtn.Name = "ChangeItemBtn";
			this.ChangeItemBtn.Size = new Size(115, 22);
			this.ChangeItemBtn.Text = "Change Magic Item";
			this.ChangeItemBtn.Click += new EventHandler(this.ChangeItemBtn_Click);
			this.ItemStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ItemStatBlockBtn.Image = (Image)componentResourceManager.GetObject("ItemStatBlockBtn.Image");
			this.ItemStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.ItemStatBlockBtn.Name = "ItemStatBlockBtn";
			this.ItemStatBlockBtn.Size = new Size(63, 22);
			this.ItemStatBlockBtn.Text = "Stat Block";
			this.ItemStatBlockBtn.Click += new EventHandler(this.ItemStatBlockBtn_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new Size(6, 25);
			this.AllocateBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AllocateBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.heroesToolStripMenuItem
			});
			this.AllocateBtn.Image = (Image)componentResourceManager.GetObject("AllocateBtn.Image");
			this.AllocateBtn.ImageTransparentColor = Color.Magenta;
			this.AllocateBtn.Name = "AllocateBtn";
			this.AllocateBtn.Size = new Size(95, 22);
			this.AllocateBtn.Text = "Allocate to PC";
			this.AllocateBtn.DropDownOpening += new EventHandler(this.AllocateBtn_DropDownOpening);
			this.heroesToolStripMenuItem.Name = "heroesToolStripMenuItem";
			this.heroesToolStripMenuItem.Size = new Size(117, 22);
			this.heroesToolStripMenuItem.Text = "(heroes)";
			this.EncyclopediaPage.Controls.Add(this.splitContainer1);
			this.EncyclopediaPage.Location = new Point(4, 22);
			this.EncyclopediaPage.Name = "EncyclopediaPage";
			this.EncyclopediaPage.Padding = new Padding(3);
			this.EncyclopediaPage.Size = new Size(617, 268);
			this.EncyclopediaPage.TabIndex = 4;
			this.EncyclopediaPage.Text = "Encyclopedia Entries";
			this.EncyclopediaPage.UseVisualStyleBackColor = true;
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.EncyclopediaList);
			this.splitContainer1.Panel1.Controls.Add(this.EncyclopediaToolbar);
			this.splitContainer1.Panel2.Controls.Add(this.EncBrowserPanel);
			this.splitContainer1.Panel2.Controls.Add(this.EncBrowserToolbar);
			this.splitContainer1.Size = new Size(611, 262);
			this.splitContainer1.SplitterDistance = 331;
			this.splitContainer1.TabIndex = 2;
			this.EncyclopediaList.Columns.AddRange(new ColumnHeader[]
			{
				this.EncHdr
			});
			this.EncyclopediaList.Dock = DockStyle.Fill;
			this.EncyclopediaList.FullRowSelect = true;
			this.EncyclopediaList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EncyclopediaList.HideSelection = false;
			this.EncyclopediaList.Location = new Point(0, 25);
			this.EncyclopediaList.MultiSelect = false;
			this.EncyclopediaList.Name = "EncyclopediaList";
			this.EncyclopediaList.Size = new Size(331, 237);
			this.EncyclopediaList.Sorting = SortOrder.Ascending;
			this.EncyclopediaList.TabIndex = 1;
			this.EncyclopediaList.UseCompatibleStateImageBehavior = false;
			this.EncyclopediaList.View = View.Details;
			this.EncyclopediaList.SelectedIndexChanged += new EventHandler(this.EncyclopediaList_SelectedIndexChanged);
			this.EncHdr.Text = "Entry";
			this.EncHdr.Width = 300;
			this.EncyclopediaToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.EncyclopediaAddBtn,
				this.EncyclopediaRemoveBtn
			});
			this.EncyclopediaToolbar.Location = new Point(0, 0);
			this.EncyclopediaToolbar.Name = "EncyclopediaToolbar";
			this.EncyclopediaToolbar.Size = new Size(331, 25);
			this.EncyclopediaToolbar.TabIndex = 0;
			this.EncyclopediaToolbar.Text = "toolStrip1";
			this.EncyclopediaAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncyclopediaAddBtn.Image = (Image)componentResourceManager.GetObject("EncyclopediaAddBtn.Image");
			this.EncyclopediaAddBtn.ImageTransparentColor = Color.Magenta;
			this.EncyclopediaAddBtn.Name = "EncyclopediaAddBtn";
			this.EncyclopediaAddBtn.Size = new Size(33, 22);
			this.EncyclopediaAddBtn.Text = "Add";
			this.EncyclopediaAddBtn.Click += new EventHandler(this.EncyclopediaAddBtn_Click);
			this.EncyclopediaRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncyclopediaRemoveBtn.Image = (Image)componentResourceManager.GetObject("EncyclopediaRemoveBtn.Image");
			this.EncyclopediaRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.EncyclopediaRemoveBtn.Name = "EncyclopediaRemoveBtn";
			this.EncyclopediaRemoveBtn.Size = new Size(54, 22);
			this.EncyclopediaRemoveBtn.Text = "Remove";
			this.EncyclopediaRemoveBtn.Click += new EventHandler(this.EncyclopediaRemoveBtn_Click);
			this.EncBrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.EncBrowserPanel.Controls.Add(this.EncBrowser);
			this.EncBrowserPanel.Dock = DockStyle.Fill;
			this.EncBrowserPanel.Location = new Point(0, 25);
			this.EncBrowserPanel.Name = "EncBrowserPanel";
			this.EncBrowserPanel.Size = new Size(276, 237);
			this.EncBrowserPanel.TabIndex = 1;
			this.EncBrowser.AllowNavigation = false;
			this.EncBrowser.AllowWebBrowserDrop = false;
			this.EncBrowser.Dock = DockStyle.Fill;
			this.EncBrowser.IsWebBrowserContextMenuEnabled = false;
			this.EncBrowser.Location = new Point(0, 0);
			this.EncBrowser.MinimumSize = new Size(20, 20);
			this.EncBrowser.Name = "EncBrowser";
			this.EncBrowser.ScriptErrorsSuppressed = true;
			this.EncBrowser.Size = new Size(274, 235);
			this.EncBrowser.TabIndex = 0;
			this.EncBrowser.WebBrowserShortcutsEnabled = false;
			this.EncBrowserToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.EncPlayerViewBtn
			});
			this.EncBrowserToolbar.Location = new Point(0, 0);
			this.EncBrowserToolbar.Name = "EncBrowserToolbar";
			this.EncBrowserToolbar.Size = new Size(276, 25);
			this.EncBrowserToolbar.TabIndex = 1;
			this.EncBrowserToolbar.Text = "toolStrip1";
			this.EncPlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncPlayerViewBtn.Image = (Image)componentResourceManager.GetObject("EncPlayerViewBtn.Image");
			this.EncPlayerViewBtn.ImageTransparentColor = Color.Magenta;
			this.EncPlayerViewBtn.Name = "EncPlayerViewBtn";
			this.EncPlayerViewBtn.Size = new Size(114, 22);
			this.EncPlayerViewBtn.Text = "Send to Player View";
			this.EncPlayerViewBtn.Click += new EventHandler(this.EncPlayerViewBtn_Click);
			this.LinksPage.Controls.Add(this.LinkList);
			this.LinksPage.Controls.Add(this.LinkToolbar);
			this.LinksPage.Location = new Point(4, 22);
			this.LinksPage.Name = "LinksPage";
			this.LinksPage.Padding = new Padding(3);
			this.LinksPage.Size = new Size(617, 268);
			this.LinksPage.TabIndex = 1;
			this.LinksPage.Text = "Plot Connections";
			this.LinksPage.UseVisualStyleBackColor = true;
			this.LinkList.Columns.AddRange(new ColumnHeader[]
			{
				this.LinkHdr,
				this.LinkDetailsHdr
			});
			this.LinkList.Dock = DockStyle.Fill;
			this.LinkList.FullRowSelect = true;
			listViewGroup.Header = "Links To This Point";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Links From This Point";
			listViewGroup2.Name = "listViewGroup2";
			this.LinkList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2
			});
			this.LinkList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.LinkList.HideSelection = false;
			this.LinkList.Location = new Point(3, 28);
			this.LinkList.MultiSelect = false;
			this.LinkList.Name = "LinkList";
			this.LinkList.Size = new Size(611, 237);
			this.LinkList.TabIndex = 1;
			this.LinkList.TileSize = new Size(500, 30);
			this.LinkList.UseCompatibleStateImageBehavior = false;
			this.LinkList.View = View.Tile;
			this.LinkList.SizeChanged += new EventHandler(this.LinkList_SizeChanged);
			this.LinkHdr.Text = "Plot Point";
			this.LinkHdr.Width = 200;
			this.LinkDetailsHdr.Text = "Details";
			this.LinkDetailsHdr.Width = 300;
			this.LinkToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.RemoveBtn
			});
			this.LinkToolbar.Location = new Point(3, 3);
			this.LinkToolbar.Name = "LinkToolbar";
			this.LinkToolbar.Size = new Size(611, 25);
			this.LinkToolbar.TabIndex = 0;
			this.LinkToolbar.Text = "toolStrip1";
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.InfoBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.InfoBtn.Location = new Point(12, 338);
			this.InfoBtn.Name = "InfoBtn";
			this.InfoBtn.Size = new Size(75, 23);
			this.InfoBtn.TabIndex = 3;
			this.InfoBtn.Text = "Information";
			this.InfoBtn.UseVisualStyleBackColor = true;
			this.InfoBtn.Click += new EventHandler(this.InfoBtn_Click);
			this.DieRollerBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.DieRollerBtn.Location = new Point(93, 338);
			this.DieRollerBtn.Name = "DieRollerBtn";
			this.DieRollerBtn.Size = new Size(75, 23);
			this.DieRollerBtn.TabIndex = 4;
			this.DieRollerBtn.Text = "Die Roller";
			this.DieRollerBtn.UseVisualStyleBackColor = true;
			this.DieRollerBtn.Click += new EventHandler(this.DieRollerBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(649, 373);
			base.Controls.Add(this.DieRollerBtn);
			base.Controls.Add(this.InfoBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MinimizeBox = false;
			base.Name = "PlotPointForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Plot Point";
			base.Shown += new EventHandler(this.PlotPointForm_Shown);
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.TextSplitter.Panel1.ResumeLayout(false);
			this.TextSplitter.Panel1.PerformLayout();
			this.TextSplitter.Panel2.ResumeLayout(false);
			this.TextSplitter.Panel2.PerformLayout();
			this.TextSplitter.ResumeLayout(false);
			this.MainToolbar.ResumeLayout(false);
			this.MainToolbar.PerformLayout();
			this.RPGPage.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((ISupportInitialize)this.XPBox).EndInit();
			this.ParcelsPage.ResumeLayout(false);
			this.ParcelsPage.PerformLayout();
			this.ParcelToolbar.ResumeLayout(false);
			this.ParcelToolbar.PerformLayout();
			this.EncyclopediaPage.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.EncyclopediaToolbar.ResumeLayout(false);
			this.EncyclopediaToolbar.PerformLayout();
			this.EncBrowserPanel.ResumeLayout(false);
			this.EncBrowserToolbar.ResumeLayout(false);
			this.EncBrowserToolbar.PerformLayout();
			this.LinksPage.ResumeLayout(false);
			this.LinksPage.PerformLayout();
			this.LinkToolbar.ResumeLayout(false);
			this.LinkToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
