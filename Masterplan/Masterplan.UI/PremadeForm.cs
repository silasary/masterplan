using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using Utils;
using Utils.Forms;

namespace Masterplan.UI
{
	internal class PremadeForm : Form
	{
		public class Adventure
		{
			public string Name = "";

			public int PartyLevel;

			public int PartySize;

			public string URL = "";
		}

		private IContainer components;

		private ListView AdventureList;

		private Button OKBtn;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private ColumnHeader SizeHdr;

		private ColumnHeader LevelHdr;

		private string fDownloadedFileName = "";

		private List<PremadeForm.Adventure> fAdventures;

		private ProgressScreen fProgressScreen;

		public PremadeForm.Adventure SelectedAdventure
		{
			get
			{
				if (this.AdventureList.SelectedItems.Count != 0)
				{
					return this.AdventureList.SelectedItems[0].Tag as PremadeForm.Adventure;
				}
				return null;
			}
		}

		public string DownloadedFileName
		{
			get
			{
				return this.fDownloadedFileName;
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
			this.AdventureList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.LevelHdr = new ColumnHeader();
			this.SizeHdr = new ColumnHeader();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			base.SuspendLayout();
			this.AdventureList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.AdventureList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.LevelHdr,
				this.SizeHdr
			});
			this.AdventureList.FullRowSelect = true;
			this.AdventureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.AdventureList.HideSelection = false;
			this.AdventureList.Location = new Point(12, 12);
			this.AdventureList.MultiSelect = false;
			this.AdventureList.Name = "AdventureList";
			this.AdventureList.Size = new Size(452, 188);
			this.AdventureList.TabIndex = 0;
			this.AdventureList.UseCompatibleStateImageBehavior = false;
			this.AdventureList.View = View.Details;
			this.AdventureList.DoubleClick += new EventHandler(this.AdventureList_DoubleClick);
			this.NameHdr.Text = "Adventure Name";
			this.NameHdr.Width = 219;
			this.LevelHdr.Text = "Party Level";
			this.LevelHdr.TextAlign = HorizontalAlignment.Right;
			this.LevelHdr.Width = 100;
			this.SizeHdr.Text = "Party Size";
			this.SizeHdr.TextAlign = HorizontalAlignment.Right;
			this.SizeHdr.Width = 100;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(308, 206);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(389, 206);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(476, 241);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.AdventureList);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PremadeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Premade Adventures";
			base.FormClosing += new FormClosingEventHandler(this.PremadeForm_FormClosing);
			base.ResumeLayout(false);
		}

		public PremadeForm()
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			WebClient webClient = new WebClient();
			webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.downloaded_html);
			webClient.DownloadStringAsync(new Uri("http://web.archive.org/web/20100405210245/http://masterplan.habitualindolence.net/adventures.htm"));
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.SelectedAdventure != null);
		}

		private void downloaded_html(object sender, DownloadStringCompletedEventArgs e)
		{
			try
			{
				this.fAdventures = new List<PremadeForm.Adventure>();
				string text = e.Result.ToLower();
				int startIndex = 0;
				while (true)
				{
					int num = text.IndexOf("<tr>", startIndex);
					if (num == -1)
					{
						break;
					}
					int num2 = text.IndexOf("</tr>", num);
					if (num2 == -1)
					{
						break;
					}
					num2 += "</tr>".Length;
					string html = e.Result.Substring(num, num2 - num);
					startIndex = num2;
					PremadeForm.Adventure adventure = this.get_adventure(html);
					if (adventure != null)
					{
						this.fAdventures.Add(adventure);
					}
				}
			}
			catch
			{
			}
			this.update_list();
		}

		private Adventure get_adventure(string html)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(html);
				XmlNode documentElement = xmlDocument.DocumentElement;
				PremadeForm.Adventure result;
				if (documentElement == null)
				{
					result = null;
					return result;
				}
                Adventure adventure = new Adventure();
				XmlNode firstChild = documentElement.FirstChild;
				adventure.Name = firstChild.InnerText;
				XmlNode firstChild2 = firstChild.FirstChild;
				adventure.URL = XMLHelper.GetAttribute(firstChild2, "href");
				adventure.URL = "http://web.archive.org/web/20100405210245/http://www.habitualindolence.net/masterplan/" + adventure.URL;
				XmlNode nextSibling = firstChild.NextSibling;
				string text = nextSibling.InnerText;
				text = text.Replace("Level", "");
				text = text.Replace("level", "");
				text = text.Replace(" ", "");
				adventure.PartyLevel = int.Parse(text);
				XmlNode nextSibling2 = nextSibling.NextSibling;
				string text2 = nextSibling2.InnerText;
				text2 = text2.Replace("PCs", "");
				text2 = text2.Replace("pcs", "");
				text2 = text2.Replace("heroes", "");
				text2 = text2.Replace(" ", "");
				adventure.PartySize = int.Parse(text2);
				result = adventure;
				return result;
			}
			catch
			{
			}
			return null;
		}

		private void update_list()
		{
			this.AdventureList.Items.Clear();
			this.AdventureList.Enabled = false;
			if (this.fAdventures == null)
			{
				ListViewItem listViewItem = this.AdventureList.Items.Add("Downloading adventure list...");
				listViewItem.ForeColor = SystemColors.GrayText;
				return;
			}
			if (this.fAdventures.Count != 0)
			{
				this.AdventureList.Enabled = true;
				using (List<PremadeForm.Adventure>.Enumerator enumerator = this.fAdventures.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PremadeForm.Adventure current = enumerator.Current;
						ListViewItem listViewItem2 = this.AdventureList.Items.Add(current.Name);
						listViewItem2.SubItems.Add("Level " + current.PartyLevel);
						listViewItem2.SubItems.Add(current.PartySize + " PCs");
						listViewItem2.Tag = current;
					}
					return;
				}
			}
			ListViewItem listViewItem3 = this.AdventureList.Items.Add("(could not download adventures)");
			listViewItem3.ForeColor = SystemColors.GrayText;
		}

		private void PremadeForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.fProgressScreen != null)
			{
				e.Cancel = true;
			}
		}

		private void AdventureList_DoubleClick(object sender, EventArgs e)
		{
			this.OKBtn_Click(sender, e);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedAdventure != null)
			{
				this.get_file_name(this.SelectedAdventure);
			}
		}

		private void get_file_name(PremadeForm.Adventure adv)
		{
			string text = FileName.Name(adv.Name);
			text = FileName.TrimInvalidCharacters(text);
			text += ".masterplan";
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = Program.ProjectFilter;
			saveFileDialog.FileName = text;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.start_download(adv, saveFileDialog.FileName);
			}
		}

		private void start_download(PremadeForm.Adventure adv, string filename)
		{
			this.fProgressScreen = new ProgressScreen("Downloading Adventure...", 100);
			this.fProgressScreen.CurrentSubAction = adv.Name;
			this.fProgressScreen.Show();
			WebClient webClient = new WebClient();
			webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.progress_changed);
			webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.download_completed);
			webClient.DownloadFileAsync(new Uri(adv.URL), filename);
			this.fDownloadedFileName = filename;
		}

		private void progress_changed(object sender, DownloadProgressChangedEventArgs e)
		{
			this.fProgressScreen.Progress = e.ProgressPercentage;
		}

		private void download_completed(object sender, AsyncCompletedEventArgs e)
		{
            
			this.fProgressScreen.Hide();
			this.fProgressScreen = null;
			base.DialogResult = DialogResult.OK;
			base.Close();

		}
	}
}
