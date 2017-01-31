using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;
using Utils;

namespace Masterplan.Controls
{
	internal class WelcomePanel : UserControl
	{
		private class Headline : IComparable<WelcomePanel.Headline>
		{
			public string Title = "";

			public string URL = "";

			public DateTime Date = DateTime.Now;

			public int CompareTo(WelcomePanel.Headline rhs)
			{
				return this.Date.CompareTo(rhs.Date) * -1;
			}
		}

		private const int MAX_HEADLINES = 10;

		private const int MAX_LENGTH = 45;

		private IContainer components;

		private TitlePanel TitlePanel;

		private WebBrowser MenuBrowser;

		private List<WelcomePanel.Headline> fHeadlines;

		private bool fShowHeadlines;

        [Category("Actions")]
        public event EventHandler NewProjectClicked;

        [Category("Actions")]
        public event EventHandler OpenProjectClicked;

        [Category("Actions")]
        public event EventHandler OpenLastProjectClicked;

        [Category("Actions")]
        public event EventHandler DelveClicked;

        [Category("Actions")]
        public event EventHandler PremadeClicked;

		public bool ShowHeadlines
		{
			get
			{
				return this.fShowHeadlines;
			}
			set
			{
				this.fShowHeadlines = value;
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
			this.MenuBrowser = new WebBrowser();
			this.TitlePanel = new TitlePanel();
			base.SuspendLayout();
			this.MenuBrowser.Dock = DockStyle.Right;
			this.MenuBrowser.IsWebBrowserContextMenuEnabled = false;
			this.MenuBrowser.Location = new Point(364, 0);
			this.MenuBrowser.MinimumSize = new Size(20, 20);
			this.MenuBrowser.Name = "MenuBrowser";
			this.MenuBrowser.ScriptErrorsSuppressed = true;
			this.MenuBrowser.Size = new Size(345, 429);
			this.MenuBrowser.TabIndex = 5;
			this.MenuBrowser.WebBrowserShortcutsEnabled = false;
			this.MenuBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.MenuBrowser_Navigating);
			this.TitlePanel.Dock = DockStyle.Fill;
			this.TitlePanel.Font = new Font("Calibri", 11f);
			this.TitlePanel.ForeColor = Color.MidnightBlue;
			this.TitlePanel.Location = new Point(0, 0);
			this.TitlePanel.Margin = new Padding(3, 4, 3, 4);
			this.TitlePanel.Mode = TitlePanel.TitlePanelMode.WelcomeScreen;
			this.TitlePanel.Name = "TitlePanel";
			this.TitlePanel.Size = new Size(364, 429);
			this.TitlePanel.TabIndex = 4;
			this.TitlePanel.Title = "Masterplan";
			this.TitlePanel.Zooming = false;
			this.TitlePanel.FadeFinished += new EventHandler(this.TitlePanel_FadeFinished);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.Controls.Add(this.TitlePanel);
			base.Controls.Add(this.MenuBrowser);
			base.Name = "WelcomePanel";
			base.Size = new Size(709, 429);
			base.ResumeLayout(false);
		}

		public WelcomePanel(bool show_headlines)
		{
			this.InitializeComponent();
			this.fShowHeadlines = show_headlines;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.MenuBrowser.DocumentText = "";
			this.set_options();
			if (this.fShowHeadlines)
			{
				this.DownloadHeadlines("http://www.habitualindolence.net/masterplanblog/feed/");
			}
		}

		private void TitlePanel_FadeFinished(object sender, EventArgs e)
		{
		}

		private void MenuBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "masterplan")
			{
				e.Cancel = true;
				if (e.Url.LocalPath == "new")
				{
					this.OnNewProjectClicked();
				}
				if (e.Url.LocalPath == "open")
				{
					this.OnOpenProjectClicked();
				}
				if (e.Url.LocalPath == "last")
				{
					this.OnOpenLastProjectClicked();
				}
				if (e.Url.LocalPath == "delve")
				{
					this.OnDelveClicked();
				}
				if (e.Url.LocalPath == "premade")
				{
					this.OnPremadeClicked();
				}
				if (e.Url.LocalPath == "genesis")
				{
					CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(new Creature
					{
						Name = "New Creature"
					});
					creatureBuilderForm.ShowDialog();
				}
				if (e.Url.LocalPath == "exodus")
				{
					CreatureTemplateBuilderForm creatureTemplateBuilderForm = new CreatureTemplateBuilderForm(new CreatureTemplate
					{
						Name = "New Template"
					});
					creatureTemplateBuilderForm.ShowDialog();
				}
				if (e.Url.LocalPath == "minos")
				{
					TrapBuilderForm trapBuilderForm = new TrapBuilderForm(new Trap
					{
						Name = "New Trap",
						Attacks = 
						{
							new TrapAttack()
						}
					});
					trapBuilderForm.ShowDialog();
				}
				if (e.Url.LocalPath == "excalibur")
				{
					MagicItemBuilderForm magicItemBuilderForm = new MagicItemBuilderForm(new MagicItem
					{
						Name = "New Magic Item"
					});
					magicItemBuilderForm.ShowDialog();
				}
				if (e.Url.LocalPath == "indiana")
				{
					ArtifactBuilderForm artifactBuilderForm = new ArtifactBuilderForm(new Artifact
					{
						Name = "New Artifact"
					});
					artifactBuilderForm.ShowDialog();
				}
				if (e.Url.LocalPath == "manual")
				{
					this.open_manual();
				}
			}
		}

		public void DownloadHeadlines(string address)
		{
			try
			{
				WebClient webClient = new WebClient();
				webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.downloaded_headlines);
				Uri address2 = new Uri(address);
				webClient.DownloadStringAsync(address2);
			}
			catch (WebException ex)
			{
				LogSystem.Trace(ex);
			}
			catch (Exception ex2)
			{
				LogSystem.Trace(ex2);
			}
		}

		private void downloaded_headlines(object sender, DownloadStringCompletedEventArgs e)
		{
			try
			{
				this.fHeadlines = new List<WelcomePanel.Headline>();
				if (e.Error == null)
				{
					string result = e.Result;
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(result);
					XmlNode documentElement = xmlDocument.DocumentElement;
					if (documentElement == null)
					{
						return;
					}
					XmlNode firstChild = documentElement.FirstChild;
					if (firstChild == null)
					{
						return;
					}
					foreach (XmlNode xmlNode in firstChild.ChildNodes)
					{
						if (!(xmlNode.Name.ToLower() != "item"))
						{
							WelcomePanel.Headline headline = new WelcomePanel.Headline();
							XmlNode xmlNode2 = XMLHelper.FindChild(xmlNode, "title");
							if (xmlNode2 != null)
							{
								headline.Title = xmlNode2.InnerText;
								XmlNode xmlNode3 = XMLHelper.FindChild(xmlNode, "link");
								if (xmlNode3 != null)
								{
									headline.URL = xmlNode3.InnerText;
									XmlNode xmlNode4 = XMLHelper.FindChild(xmlNode, "pubDate");
									if (xmlNode3 != null)
									{
										headline.Date = DateTime.Parse(xmlNode4.InnerText);
										if (headline.Title.Length > 45)
										{
											headline.Title = headline.Title.Substring(0, 45) + "...";
										}
										this.fHeadlines.Add(headline);
									}
								}
							}
						}
					}
				}
				this.set_options();
			}
			catch
			{
			}
		}

		protected void OnNewProjectClicked()
		{
			if (this.NewProjectClicked != null)
			{
				this.NewProjectClicked(this, new EventArgs());
			}
		}

		protected void OnOpenProjectClicked()
		{
			if (this.OpenProjectClicked != null)
			{
				this.OpenProjectClicked(this, new EventArgs());
			}
		}

		protected void OnOpenLastProjectClicked()
		{
			if (this.OpenLastProjectClicked != null)
			{
				this.OpenLastProjectClicked(this, new EventArgs());
			}
		}

		protected void OnDelveClicked()
		{
			if (this.DelveClicked != null)
			{
				this.DelveClicked(this, new EventArgs());
			}
		}

		protected void OnPremadeClicked()
		{
			if (this.PremadeClicked != null)
			{
				this.PremadeClicked(this, new EventArgs());
			}
		}

		private void set_options()
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead("Masterplan", "Main Menu", DisplaySize.Small));
			list.Add("<BODY>");
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD>");
			list.Add("<B>Getting Started</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			if (this.show_last_file_option())
			{
				string str = FileName.Name(Session.Preferences.LastFile);
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"masterplan:last\">Reopen <I>" + str + "</I></A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add("<A href=\"masterplan:new\">Create a new adventure project</A>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add("<A href=\"masterplan:open\">Open an existing project</A>");
			list.Add("</TD>");
			list.Add("</TR>");
			if (this.show_delve_option())
			{
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"masterplan:delve\">Generate a random dungeon delve</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add("<A href=\"masterplan:premade\">Download a premade adventure</A>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("</TABLE>");
			list.Add("</P>");
			if (Program.IsBeta)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>Development Links</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"masterplan:genesis\">Project Genesis</A>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"masterplan:exodus\">Project Exodus</A>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"masterplan:minos\">Project Minos</A>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"masterplan:excalibur\">Project Excalibur</A>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"masterplan:indiana\">Project Indiana</A>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD>");
			list.Add("<B>More Information</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			if (this.show_manual_option())
			{
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"masterplan:manual\">Read the Masterplan user manual</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add("<A href=\"http://www.habitualindolence.net/masterplan/tutorials.htm\" target=_new>Watch a tutorial video</A>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add("<A href=\"http://www.habitualindolence.net/masterplan/\" target=_new>Visit the Masterplan website</A>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("</TABLE>");
			list.Add("</P>");
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD>");
			list.Add("<B>Latest News</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			if (!this.fShowHeadlines)
			{
				list.Add("<TR>");
				list.Add("<TD class=dimmed>");
				list.Add("Headlines are disabled");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (this.fHeadlines == null)
			{
				list.Add("<TR>");
				list.Add("<TD class=dimmed>");
				list.Add("Retrieving headlines...");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (this.fHeadlines.Count == 0)
			{
				list.Add("<TR>");
				list.Add("<TD class=dimmed>");
				list.Add("Could not download headlines");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else
			{
				this.fHeadlines.Sort();
				int num = 0;
				foreach (WelcomePanel.Headline current in this.fHeadlines)
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add(current.Date.ToString("dd MMM") + ":");
					list.Add(string.Concat(new string[]
					{
						"<A href=\"",
						current.URL,
						"\" target=_new>",
						current.Title,
						"</A>"
					}));
					list.Add("</TD>");
					list.Add("</TR>");
					num++;
					if (num == 10)
					{
						break;
					}
				}
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			list.Add("</BODY>");
			list.Add("</HTML>");
			this.MenuBrowser.Document.OpenNew(true);
			this.MenuBrowser.Document.Write(HTML.Concatenate(list));
		}

		private bool show_last_file_option()
		{
			return Session.Preferences.LastFile != null && !(Session.Preferences.LastFile == "") && File.Exists(Session.Preferences.LastFile);
		}

		private bool show_delve_option()
		{
			foreach (Library current in Session.Libraries)
			{
				if (current.ShowInAutoBuild)
				{
					return true;
				}
			}
			return false;
		}

		private bool show_manual_option()
		{
			string manual_filename = this.get_manual_filename();
			return File.Exists(manual_filename);
		}

		private void open_manual()
		{
			string manual_filename = this.get_manual_filename();
			if (!File.Exists(manual_filename))
			{
				return;
			}
			Process.Start(manual_filename);
		}

		private string get_manual_filename()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			return FileName.Directory(entryAssembly.FullName) + "Manual.pdf";
		}
	}
}
