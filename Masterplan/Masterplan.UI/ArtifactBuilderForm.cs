using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class ArtifactBuilderForm : Form
	{
		private IContainer components;

		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private WebBrowser StatBlockBrowser;

		private ToolStrip Toolbar;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileImport;

		private ToolStripMenuItem FileExport;

		private Artifact fArtifact;

		public Artifact Artifact
		{
			get
			{
				return this.fArtifact;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(ArtifactBuilderForm));
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.StatBlockBrowser = new WebBrowser();
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileImport = new ToolStripMenuItem();
			this.FileExport = new ToolStripMenuItem();
			this.BtnPnl.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.BtnPnl.Controls.Add(this.CancelBtn);
			this.BtnPnl.Controls.Add(this.OKBtn);
			this.BtnPnl.Dock = DockStyle.Bottom;
			this.BtnPnl.Location = new Point(0, 443);
			this.BtnPnl.Name = "BtnPnl";
			this.BtnPnl.Size = new Size(384, 35);
			this.BtnPnl.TabIndex = 2;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(297, 6);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(216, 6);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.StatBlockBrowser.AllowWebBrowserDrop = false;
			this.StatBlockBrowser.Dock = DockStyle.Fill;
			this.StatBlockBrowser.IsWebBrowserContextMenuEnabled = false;
			this.StatBlockBrowser.Location = new Point(0, 25);
			this.StatBlockBrowser.MinimumSize = new Size(20, 20);
			this.StatBlockBrowser.Name = "StatBlockBrowser";
			this.StatBlockBrowser.ScriptErrorsSuppressed = true;
			this.StatBlockBrowser.Size = new Size(384, 418);
			this.StatBlockBrowser.TabIndex = 2;
			this.StatBlockBrowser.WebBrowserShortcutsEnabled = false;
			this.StatBlockBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.FileMenu
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(384, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FileMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.FileImport,
				this.FileExport
			});
			this.FileMenu.Image = (Image)resources.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileImport.Name = "FileImport";
			this.FileImport.Size = new Size(119, 22);
			this.FileImport.Text = "Import...";
			this.FileImport.Click += new EventHandler(this.FileImport_Click);
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new Size(119, 22);
			this.FileExport.Text = "Export...";
			this.FileExport.Click += new EventHandler(this.FileExport_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(384, 478);
			base.Controls.Add(this.StatBlockBrowser);
			base.Controls.Add(this.BtnPnl);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ArtifactBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Artifact Builder";
			this.BtnPnl.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public ArtifactBuilderForm(Artifact artifact)
		{
			this.InitializeComponent();
			this.fArtifact = artifact.Copy();
			this.update_statblock();
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "build")
			{
				if (e.Url.LocalPath == "profile")
				{
					e.Cancel = true;
					ArtifactProfileForm artifactProfileForm = new ArtifactProfileForm(this.fArtifact);
					if (artifactProfileForm.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.Name = artifactProfileForm.Artifact.Name;
						this.fArtifact.Tier = artifactProfileForm.Artifact.Tier;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "description")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fArtifact.Description, "Description", null);
					if (detailsForm.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.Description = detailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "details")
				{
					e.Cancel = true;
					DetailsForm detailsForm2 = new DetailsForm(this.fArtifact.Details, "Details", null);
					if (detailsForm2.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.Details = detailsForm2.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "goals")
				{
					e.Cancel = true;
					DetailsForm detailsForm3 = new DetailsForm(this.fArtifact.Goals, "Goals", null);
					if (detailsForm3.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.Goals = detailsForm3.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "rp")
				{
					e.Cancel = true;
					DetailsForm detailsForm4 = new DetailsForm(this.fArtifact.RoleplayingTips, "Roleplaying", null);
					if (detailsForm4.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.RoleplayingTips = detailsForm4.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "section")
			{
				if (e.Url.LocalPath == "new")
				{
					e.Cancel = true;
					MagicItemSection section = new MagicItemSection();
					MagicItemSectionForm magicItemSectionForm = new MagicItemSectionForm(section);
					if (magicItemSectionForm.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.Sections.Add(magicItemSectionForm.Section);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath.Contains(",new"))
				{
					e.Cancel = true;
					try
					{
						string s = e.Url.LocalPath.Substring(0, e.Url.LocalPath.IndexOf(","));
						int index = int.Parse(s);
						ArtifactConcordance artifactConcordance = this.fArtifact.ConcordanceLevels[index];
						MagicItemSection section2 = new MagicItemSection();
						MagicItemSectionForm magicItemSectionForm2 = new MagicItemSectionForm(section2);
						if (magicItemSectionForm2.ShowDialog() == DialogResult.OK)
						{
							artifactConcordance.Sections.Add(magicItemSectionForm2.Section);
							this.update_statblock();
						}
					}
					catch
					{
					}
				}
			}
			if (e.Url.Scheme == "sectionedit")
			{
				if (e.Url.LocalPath.Contains(","))
				{
					e.Cancel = true;
					int num = e.Url.LocalPath.IndexOf(",");
					string s2 = e.Url.LocalPath.Substring(0, num);
					string s3 = e.Url.LocalPath.Substring(num);
					try
					{
						int index2 = int.Parse(s2);
						int index3 = int.Parse(s3);
						ArtifactConcordance artifactConcordance2 = this.fArtifact.ConcordanceLevels[index2];
						MagicItemSection section3 = artifactConcordance2.Sections[index3];
						MagicItemSectionForm magicItemSectionForm3 = new MagicItemSectionForm(section3);
						if (magicItemSectionForm3.ShowDialog() == DialogResult.OK)
						{
							artifactConcordance2.Sections[index3] = magicItemSectionForm3.Section;
							this.update_statblock();
						}
						goto IL_42B;
					}
					catch
					{
						goto IL_42B;
					}
				}
				e.Cancel = true;
				try
				{
					int index4 = int.Parse(e.Url.LocalPath);
					MagicItemSection section4 = this.fArtifact.Sections[index4];
					MagicItemSectionForm magicItemSectionForm4 = new MagicItemSectionForm(section4);
					if (magicItemSectionForm4.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.Sections[index4] = magicItemSectionForm4.Section;
						this.update_statblock();
					}
				}
				catch
				{
				}
			}
			IL_42B:
			if (e.Url.Scheme == "sectionremove")
			{
				if (e.Url.LocalPath.Contains(","))
				{
					e.Cancel = true;
					int num2 = e.Url.LocalPath.IndexOf(",");
					string s4 = e.Url.LocalPath.Substring(0, num2);
					string s5 = e.Url.LocalPath.Substring(num2);
					try
					{
						int index5 = int.Parse(s4);
						int index6 = int.Parse(s5);
						ArtifactConcordance artifactConcordance3 = this.fArtifact.ConcordanceLevels[index5];
						artifactConcordance3.Sections.RemoveAt(index6);
						this.update_statblock();
						goto IL_51B;
					}
					catch
					{
						goto IL_51B;
					}
				}
				e.Cancel = true;
				try
				{
					int index7 = int.Parse(e.Url.LocalPath);
					this.fArtifact.Sections.RemoveAt(index7);
					this.update_statblock();
				}
				catch
				{
				}
			}
			IL_51B:
			if (e.Url.Scheme == "rule")
			{
				e.Cancel = true;
				if (e.Url.LocalPath == "new")
				{
					Pair<string, string> concordance = new Pair<string, string>("", "");
					ArtifactConcordanceForm artifactConcordanceForm = new ArtifactConcordanceForm(concordance);
					if (artifactConcordanceForm.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.ConcordanceRules.Add(artifactConcordanceForm.Concordance);
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "ruleedit")
			{
				e.Cancel = true;
				try
				{
					int index8 = int.Parse(e.Url.LocalPath);
					Pair<string, string> concordance2 = this.fArtifact.ConcordanceRules[index8];
					ArtifactConcordanceForm artifactConcordanceForm2 = new ArtifactConcordanceForm(concordance2);
					if (artifactConcordanceForm2.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.ConcordanceRules[index8] = artifactConcordanceForm2.Concordance;
						this.update_statblock();
					}
				}
				catch
				{
				}
			}
			if (e.Url.Scheme == "ruleremove")
			{
				e.Cancel = true;
				try
				{
					int index9 = int.Parse(e.Url.LocalPath);
					this.fArtifact.ConcordanceRules.RemoveAt(index9);
					this.update_statblock();
				}
				catch
				{
				}
			}
			if (e.Url.Scheme == "quote")
			{
				e.Cancel = true;
				try
				{
					int index10 = int.Parse(e.Url.LocalPath);
					ArtifactConcordance artifactConcordance4 = this.fArtifact.ConcordanceLevels[index10];
					DetailsForm detailsForm5 = new DetailsForm(artifactConcordance4.Quote, "Concordance Quote", null);
					if (detailsForm5.ShowDialog() == DialogResult.OK)
					{
						artifactConcordance4.Quote = detailsForm5.Details;
						this.update_statblock();
					}
				}
				catch
				{
				}
			}
			if (e.Url.Scheme == "desc")
			{
				e.Cancel = true;
				try
				{
					int index11 = int.Parse(e.Url.LocalPath);
					ArtifactConcordance artifactConcordance5 = this.fArtifact.ConcordanceLevels[index11];
					DetailsForm detailsForm6 = new DetailsForm(artifactConcordance5.Description, "Concordance Description", null);
					if (detailsForm6.ShowDialog() == DialogResult.OK)
					{
						artifactConcordance5.Description = detailsForm6.Details;
						this.update_statblock();
					}
				}
				catch
				{
				}
			}
		}

		private void update_statblock()
		{
			this.StatBlockBrowser.DocumentText = HTML.Artifact(this.fArtifact, DisplaySize.Small, true, true);
		}

		private void FileImport_Click(object sender, EventArgs e)
		{
			string text = "Importing an artifact file will clear any changes you have made to the item shown.";
			if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
			{
				return;
			}
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Import Artifact";
			openFileDialog.Filter = Program.ArtifactFilter;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Artifact artifact = Serialisation<Artifact>.Load(openFileDialog.FileName, SerialisationMode.Binary);
				if (artifact != null)
				{
					this.fArtifact = artifact;
					this.update_statblock();
					return;
				}
				string text2 = "The artifact could not be imported.";
				MessageBox.Show(text2, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Export Artifact";
			saveFileDialog.FileName = this.fArtifact.Name;
			saveFileDialog.Filter = Program.ArtifactFilter;
			if (saveFileDialog.ShowDialog() == DialogResult.OK && !Serialisation<Artifact>.Save(saveFileDialog.FileName, this.fArtifact, SerialisationMode.Binary))
			{
				string text = "The artifact could not be exported.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}
}
