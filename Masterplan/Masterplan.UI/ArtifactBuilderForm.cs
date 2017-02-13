using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal partial class ArtifactBuilderForm : Form
	{
		private Artifact fArtifact;

		public Artifact Artifact
		{
			get
			{
				return this.fArtifact;
			}
		}

		public ArtifactBuilderForm(Artifact artifact)
		{
			this.InitializeComponent();
			this.fArtifact = artifact.Copy();
			this.UpdateStatblock();
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
						this.UpdateStatblock();
					}
				}
				if (e.Url.LocalPath == "description")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fArtifact.Description, "Description", null);
					if (detailsForm.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.Description = detailsForm.Details;
						this.UpdateStatblock();
					}
				}
				if (e.Url.LocalPath == "details")
				{
					e.Cancel = true;
					DetailsForm detailsForm2 = new DetailsForm(this.fArtifact.Details, "Details", null);
					if (detailsForm2.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.Details = detailsForm2.Details;
						this.UpdateStatblock();
					}
				}
				if (e.Url.LocalPath == "goals")
				{
					e.Cancel = true;
					DetailsForm detailsForm3 = new DetailsForm(this.fArtifact.Goals, "Goals", null);
					if (detailsForm3.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.Goals = detailsForm3.Details;
						this.UpdateStatblock();
					}
				}
				if (e.Url.LocalPath == "rp")
				{
					e.Cancel = true;
					DetailsForm detailsForm4 = new DetailsForm(this.fArtifact.RoleplayingTips, "Roleplaying", null);
					if (detailsForm4.ShowDialog() == DialogResult.OK)
					{
						this.fArtifact.RoleplayingTips = detailsForm4.Details;
						this.UpdateStatblock();
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
						this.UpdateStatblock();
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
							this.UpdateStatblock();
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
							this.UpdateStatblock();
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
						this.UpdateStatblock();
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
						this.UpdateStatblock();
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
					this.UpdateStatblock();
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
						this.UpdateStatblock();
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
						this.UpdateStatblock();
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
					this.UpdateStatblock();
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
						this.UpdateStatblock();
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
                        UpdateStatblock();
					}
				}
				catch
				{
				}
			}
		}

		private void UpdateStatblock()
		{
            StatBlockBrowser.DocumentText = HTML.Artifact(fArtifact, DisplaySize.Small, true, true);
		}

		private void FileImport_Click(object sender, EventArgs e)
		{
			string text = "Importing an artifact file will clear any changes you have made to the item shown.";
			if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
			{
				return;
			}
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Import Artifact",
                Filter = Program.ArtifactFilter
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Artifact artifact = Serialisation<Artifact>.Load(openFileDialog.FileName, SerialisationMode.Binary);
				if (artifact != null)
				{
					this.fArtifact = artifact;
					this.UpdateStatblock();
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
