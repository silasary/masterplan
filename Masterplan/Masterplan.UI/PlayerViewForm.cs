using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal partial class PlayerViewForm : Form
	{
		public static bool UseOtherDisplay = true;

		public static DisplaySize DisplaySize = DisplaySize.Small;

		private PlayerViewMode fMode;

		public PlayerViewMode Mode
		{
			get
			{
				return this.fMode;
			}
			set
			{
				this.fMode = value;
			}
		}

		public MapView ParentMap
		{
			get
			{
				return this.fParentMap;
			}
			set
			{
				this.fParentMap = value;
			}
		}

		public PlayerViewForm(Form parent)
		{
			this.InitializeComponent();
			this.set_location(parent);
		}

		private void set_location(Form parent)
		{
			if (!PlayerViewForm.UseOtherDisplay)
			{
				return;
			}
			if (Screen.AllScreens.Length < 2)
			{
				return;
			}
			List<Screen> list = new List<Screen>();
			Screen[] allScreens = Screen.AllScreens;
			for (int i = 0; i < allScreens.Length; i++)
			{
				Screen screen = allScreens[i];
				if (!screen.Bounds.Contains(parent.ClientRectangle))
				{
					list.Add(screen);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			base.StartPosition = FormStartPosition.Manual;
			base.Location = list[0].WorkingArea.Location;
			base.WindowState = FormWindowState.Maximized;
			base.FormBorderStyle = FormBorderStyle.None;
		}

		private void PlayerViewForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Session.PlayerView = null;
		}

		public void ShowDefault()
		{
			TitlePanel titlePanel = new TitlePanel();
			titlePanel.Title = "Masterplan";
			titlePanel.Zooming = true;
			titlePanel.Mode = TitlePanel.TitlePanelMode.PlayerView;
			titlePanel.BackColor = Color.Black;
			titlePanel.ForeColor = Color.White;
			titlePanel.MouseMove += new MouseEventHandler(this.mouse_move);
			base.Controls.Clear();
			base.Controls.Add(titlePanel);
			titlePanel.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.Blank;
			base.Show();
		}

		private void mouse_move(object sender, MouseEventArgs e)
		{
			TitlePanel titlePanel = base.Controls[0] as TitlePanel;
			titlePanel.Wake();
		}

		public void ShowMessage(string message)
		{
			string html = HTML.Text(message, true, true, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowPlainText(Attachment att)
		{
			string @string = new ASCIIEncoding().GetString(att.Contents);
			string html = HTML.Text(@string, true, false, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowRichText(Attachment att)
		{
			string @string = new ASCIIEncoding().GetString(att.Contents);
			RichTextBox richTextBox = new RichTextBox();
			richTextBox.Rtf = @string;
			richTextBox.ReadOnly = true;
			richTextBox.Multiline = true;
			richTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
			base.Controls.Clear();
			base.Controls.Add(richTextBox);
			richTextBox.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.RichText;
			base.Show();
		}

		public void ShowWebPage(Attachment att)
		{
			WebBrowser webBrowser = new WebBrowser();
			webBrowser.IsWebBrowserContextMenuEnabled = false;
			webBrowser.ScriptErrorsSuppressed = true;
			webBrowser.WebBrowserShortcutsEnabled = false;
			switch (att.Type)
			{
			case AttachmentType.URL:
			{
				string @string = new ASCIIEncoding().GetString(att.Contents);
				string[] array = @string.Split(new string[]
				{
					Environment.NewLine
				}, StringSplitOptions.RemoveEmptyEntries);
				string text = "";
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text2 = array2[i];
					if (text2.StartsWith("URL="))
					{
						text = text2.Substring(4);
						break;
					}
				}
				if (text != "")
				{
					webBrowser.Navigate(text);
				}
				break;
			}
			case AttachmentType.HTML:
			{
				string string2 = new ASCIIEncoding().GetString(att.Contents);
				webBrowser.DocumentText = string2;
				break;
			}
			}
			base.Controls.Clear();
			base.Controls.Add(webBrowser);
			webBrowser.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.HTML;
			base.Show();
		}

		public void ShowImage(Attachment att)
		{
			Image image = Image.FromStream(new MemoryStream(att.Contents));
			PictureBox pictureBox = new PictureBox();
			pictureBox.Image = image;
			pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			base.Controls.Clear();
			base.Controls.Add(pictureBox);
			pictureBox.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.Image;
			base.Show();
		}

		public void ShowImage(Image img)
		{
			PictureBox pictureBox = new PictureBox();
			pictureBox.Image = img;
			pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			base.Controls.Clear();
			base.Controls.Add(pictureBox);
			pictureBox.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.Image;
			base.Show();
		}

		public void ShowPlotPoint(PlotPoint pp)
		{
			string html = HTML.Text(pp.ReadAloud, false, false, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowBackground(Background background)
		{
			string html = HTML.Background(background, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowBackground(List<Background> backgrounds)
		{
			string html = HTML.Background(backgrounds, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowEncyclopediaItem(IEncyclopediaItem item)
		{
			if (item is EncyclopediaEntry)
			{
				string html = HTML.EncyclopediaEntry(item as EncyclopediaEntry, Session.Project.Encyclopedia, PlayerViewForm.DisplaySize, false, false, false, false);
				this.set_html(html);
			}
			if (item is EncyclopediaGroup)
			{
				string html2 = HTML.EncyclopediaGroup(item as EncyclopediaGroup, Session.Project.Encyclopedia, PlayerViewForm.DisplaySize, false, false);
				this.set_html(html2);
			}
			base.Show();
		}

		public void ShowEncyclopediaGroup(EncyclopediaGroup group)
		{
			string html = HTML.EncyclopediaGroup(group, Session.Project.Encyclopedia, PlayerViewForm.DisplaySize, false, false);
			this.set_html(html);
			base.Show();
		}

		public void ShowTacticalMap(MapView mapview, string initiative)
		{
			this.fParentMap = mapview;
			MapView mapView = null;
			if (this.fParentMap != null)
			{
				mapView = new MapView();
				mapView.Map = this.fParentMap.Map;
				mapView.Viewpoint = this.fParentMap.Viewpoint;
				mapView.BorderSize = this.fParentMap.BorderSize;
				mapView.ScalingFactor = this.fParentMap.ScalingFactor;
				mapView.Encounter = this.fParentMap.Encounter;
				mapView.Plot = this.fParentMap.Plot;
				mapView.TokenLinks = this.fParentMap.TokenLinks;
				mapView.AllowDrawing = this.fParentMap.AllowDrawing;
				mapView.Mode = MapViewMode.PlayerView;
				mapView.Tactical = true;
				mapView.HighlightAreas = false;
				mapView.FrameType = MapDisplayType.Opaque;
				mapView.ShowCreatures = Session.Preferences.PlayerViewFog;
				mapView.ShowHealthBars = Session.Preferences.PlayerViewHealthBars;
				mapView.ShowCreatureLabels = Session.Preferences.PlayerViewCreatureLabels;
				mapView.ShowGrid = (Session.Preferences.PlayerViewGrid ? MapGridMode.Overlay : MapGridMode.None);
				mapView.ShowGridLabels = Session.Preferences.PlayerViewGridLabels;
				mapView.ShowAuras = false;
				mapView.ShowGrid = MapGridMode.None;
				foreach (MapSketch current in mapview.Sketches)
				{
					mapView.Sketches.Add(current.Copy());
				}
				mapView.SelectedTokensChanged += new EventHandler(this.selected_tokens_changed);
				mapView.HoverTokenChanged += new EventHandler(this.hover_token_changed);
				mapView.ItemMoved += new MovementEventHandler(this.item_moved);
				mapView.TokenDragged += new DraggedTokenEventHandler(this.token_dragged);
				mapView.SketchCreated += new MapSketchEventHandler(this.sketch_created);
				mapView.Dock = DockStyle.Fill;
			}
			Button button = new Button();
			button.Text = "Die Roller";
			button.BackColor = SystemColors.Control;
			button.Dock = DockStyle.Bottom;
			button.Click += new EventHandler(this.dicebtn_click);
			WebBrowser webBrowser = new WebBrowser();
			webBrowser.IsWebBrowserContextMenuEnabled = false;
			webBrowser.ScriptErrorsSuppressed = true;
			webBrowser.WebBrowserShortcutsEnabled = false;
			webBrowser.Dock = DockStyle.Fill;
			webBrowser.DocumentText = initiative;
			SplitContainer splitContainer = new SplitContainer();
			splitContainer.Panel1.Controls.Add(mapView);
			splitContainer.Panel2.Controls.Add(webBrowser);
			splitContainer.Panel2.Controls.Add(button);
			base.Controls.Clear();
			base.Controls.Add(splitContainer);
			splitContainer.Dock = DockStyle.Fill;
			if (mapview == null)
			{
				splitContainer.Panel1Collapsed = true;
			}
			else if (initiative == null)
			{
				splitContainer.Panel2Collapsed = true;
			}
			else
			{
				splitContainer.BackColor = Color.FromArgb(10, 10, 10);
				splitContainer.SplitterDistance = (int)((double)base.Width * 0.65);
				splitContainer.FixedPanel = FixedPanel.Panel2;
				splitContainer.Panel2Collapsed = !Session.Preferences.PlayerViewInitiative;
			}
			this.fMode = PlayerViewMode.Combat;
			base.Show();
		}

		private void selected_tokens_changed(object sender, EventArgs e)
		{
			SplitContainer splitContainer = base.Controls[0] as SplitContainer;
			MapView mapView = splitContainer.Panel1.Controls[0] as MapView;
			this.fParentMap.SelectTokens(mapView.SelectedTokens, true);
		}

		private void hover_token_changed(object sender, EventArgs e)
		{
			SplitContainer splitContainer = base.Controls[0] as SplitContainer;
			MapView mapView = splitContainer.Panel1.Controls[0] as MapView;
			this.fParentMap.HoverToken = mapView.HoverToken;
			string text = "";
			string text2 = null;
			if (mapView.HoverToken is CreatureToken)
			{
				CreatureToken creatureToken = mapView.HoverToken as CreatureToken;
				EncounterSlot encounterSlot = mapView.Encounter.FindSlot(creatureToken.SlotID);
				ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
				int hP = encounterSlot.Card.HP;
				int num = hP - creatureToken.Data.Damage;
				int num2 = hP / 2;
				if (mapView.ShowCreatureLabels)
				{
					text = creatureToken.Data.DisplayName;
				}
				else
				{
					text = creature.Category;
					if (text == "")
					{
						text = "Creature";
					}
				}
				if (creatureToken.Data.Damage == 0)
				{
					text2 = "Not wounded";
				}
				if (num < hP)
				{
					text2 = "Wounded";
				}
				if (num < num2)
				{
					text2 = "Bloodied";
				}
				if (num <= 0)
				{
					text2 = "Dead";
				}
				if (creatureToken.Data.Conditions.Count != 0)
				{
					text2 += Environment.NewLine;
					foreach (OngoingCondition current in creatureToken.Data.Conditions)
					{
						text2 += Environment.NewLine;
						text2 += current.ToString(this.fParentMap.Encounter, false);
					}
				}
			}
			if (mapView.HoverToken is Hero)
			{
				Hero hero = mapView.HoverToken as Hero;
				text = hero.Name;
				text2 = hero.Race + " " + hero.Class;
				text2 += Environment.NewLine;
				text2 = text2 + "Player: " + hero.Player;
			}
			if (mapView.HoverToken is CustomToken)
			{
				CustomToken customToken = mapView.HoverToken as CustomToken;
				if (mapView.ShowCreatureLabels)
				{
					text = customToken.Name;
					text2 = "(custom token)";
				}
			}
			this.Tooltip.ToolTipTitle = text;
			this.Tooltip.ToolTipIcon = ToolTipIcon.Info;
			this.Tooltip.SetToolTip(mapView, text2);
		}

		private void item_moved(object sender, MovementEventArgs e)
		{
			this.fParentMap.Invalidate();
		}

		private void token_dragged(object sender, DraggedTokenEventArgs e)
		{
			this.fParentMap.SetDragInfo(e.OldLocation, e.NewLocation);
		}

		private void sketch_created(object sender, MapSketchEventArgs e)
		{
			this.fParentMap.Sketches.Add(e.Sketch.Copy());
			this.fParentMap.Invalidate();
		}

		private void dicebtn_click(object sender, EventArgs e)
		{
			DieRollerForm dieRollerForm = new DieRollerForm();
			dieRollerForm.ShowDialog();
		}

		public void ShowRegionalMap(RegionalMapPanel panel)
		{
			RegionalMapPanel regionalMapPanel = new RegionalMapPanel();
			regionalMapPanel.Map = panel.Map;
			regionalMapPanel.Mode = MapViewMode.PlayerView;
			if (panel.SelectedLocation == null)
			{
				regionalMapPanel.ShowLocations = false;
			}
			else
			{
				regionalMapPanel.ShowLocations = true;
				regionalMapPanel.HighlightedLocation = panel.SelectedLocation;
			}
			base.Controls.Clear();
			base.Controls.Add(regionalMapPanel);
			regionalMapPanel.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.RegionalMap;
			base.Show();
		}

		public void ShowHandout(List<object> items, bool include_dm_info)
		{
			string html = HTML.Handout(items, PlayerViewForm.DisplaySize, include_dm_info);
			this.set_html(html);
			base.Show();
		}

		public void ShowPCs()
		{
			string html = HTML.PartyBreakdown(PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowPlayerOption(IPlayerOption option)
		{
			string html = HTML.PlayerOption(option, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowCalendar(Calendar calendar, int month_index, int year)
		{
			CalendarPanel calendarPanel = new CalendarPanel();
			calendarPanel.Calendar = calendar;
			calendarPanel.MonthIndex = month_index;
			calendarPanel.Year = year;
			base.Controls.Clear();
			base.Controls.Add(calendarPanel);
			calendarPanel.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.Calendar;
			base.Show();
		}

		public void ShowHero(Hero h)
		{
			string html = HTML.StatBlock(h, null, true, false, false, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowEncounterCard(EncounterCard card)
		{
			string html = HTML.StatBlock(card, null, null, true, false, true, CardMode.View, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowCreatureTemplate(CreatureTemplate template)
		{
			string html = HTML.CreatureTemplate(template, PlayerViewForm.DisplaySize, false);
			this.set_html(html);
			base.Show();
		}

		public void ShowTrap(Trap trap)
		{
			string html = HTML.Trap(trap, null, true, false, false, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowSkillChallenge(SkillChallenge sc)
		{
			string html = HTML.SkillChallenge(sc, false, true, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowMagicItem(MagicItem item)
		{
			string html = HTML.MagicItem(item, PlayerViewForm.DisplaySize, false, true);
			this.set_html(html);
			base.Show();
		}

		public void ShowArtifact(Artifact artifact)
		{
			string html = HTML.Artifact(artifact, PlayerViewForm.DisplaySize, false, true);
			this.set_html(html);
			base.Show();
		}

		public void ShowTerrainPower(TerrainPower tp)
		{
			string html = HTML.TerrainPower(tp, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		public void ShowEncounterReportTable(ReportTable table)
		{
			string html = HTML.EncounterReportTable(table, PlayerViewForm.DisplaySize);
			this.set_html(html);
			base.Show();
		}

		private void set_html(string html)
		{
			WebBrowser webBrowser = new WebBrowser();
			webBrowser.IsWebBrowserContextMenuEnabled = false;
			webBrowser.ScriptErrorsSuppressed = true;
			webBrowser.WebBrowserShortcutsEnabled = false;
			webBrowser.DocumentText = html;
			base.Controls.Clear();
			base.Controls.Add(webBrowser);
			webBrowser.Dock = DockStyle.Fill;
			this.fMode = PlayerViewMode.HTML;
		}


	}
}
