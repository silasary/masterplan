using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class CreatureTemplateBuilderForm : Form
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private WebBrowser StatBlockBrowser;

		private ToolStripDropDownButton OptionsMenu;

		private ToolStripMenuItem OptionsVariant;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileExport;

		private CreatureTemplate fTemplate;

		public CreatureTemplate Template
		{
			get
			{
				return this.fTemplate;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CreatureTemplateBuilderForm));
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileExport = new ToolStripMenuItem();
			this.OptionsMenu = new ToolStripDropDownButton();
			this.OptionsVariant = new ToolStripMenuItem();
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.StatBlockBrowser = new WebBrowser();
			this.Toolbar.SuspendLayout();
			this.BtnPnl.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.FileMenu,
				this.OptionsMenu
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(384, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FileMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.FileExport
			});
			this.FileMenu.Image = (Image)componentResourceManager.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new Size(152, 22);
			this.FileExport.Text = "Export...";
			this.FileExport.Click += new EventHandler(this.FileExport_Click);
			this.OptionsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OptionsMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.OptionsVariant
			});
			this.OptionsMenu.Image = (Image)componentResourceManager.GetObject("OptionsMenu.Image");
			this.OptionsMenu.ImageTransparentColor = Color.Magenta;
			this.OptionsMenu.Name = "OptionsMenu";
			this.OptionsMenu.Size = new Size(62, 22);
			this.OptionsMenu.Text = "Options";
			this.OptionsVariant.Name = "OptionsVariant";
			this.OptionsVariant.Size = new Size(218, 22);
			this.OptionsVariant.Text = "Copy an Existing Creature...";
			this.OptionsVariant.Click += new EventHandler(this.OptionsVariant_Click);
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
			base.Name = "CreatureTemplateBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Template Builder";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.BtnPnl.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public CreatureTemplateBuilderForm(CreatureTemplate template)
		{
			this.InitializeComponent();
			this.fTemplate = template.Copy();
			this.update_statblock();
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "build")
			{
				if (e.Url.LocalPath == "profile")
				{
					e.Cancel = true;
					CreatureTemplateProfileForm creatureTemplateProfileForm = new CreatureTemplateProfileForm(this.fTemplate);
					if (creatureTemplateProfileForm.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.Name = creatureTemplateProfileForm.Template.Name;
						this.fTemplate.Type = creatureTemplateProfileForm.Template.Type;
						this.fTemplate.Role = creatureTemplateProfileForm.Template.Role;
						this.fTemplate.Leader = creatureTemplateProfileForm.Template.Leader;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "combat")
				{
					e.Cancel = true;
					CreatureTemplateStatsForm creatureTemplateStatsForm = new CreatureTemplateStatsForm(this.fTemplate);
					if (creatureTemplateStatsForm.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.HP = creatureTemplateStatsForm.Template.HP;
						this.fTemplate.Initiative = creatureTemplateStatsForm.Template.Initiative;
						this.fTemplate.AC = creatureTemplateStatsForm.Template.AC;
						this.fTemplate.Fortitude = creatureTemplateStatsForm.Template.Fortitude;
						this.fTemplate.Reflex = creatureTemplateStatsForm.Template.Reflex;
						this.fTemplate.Will = creatureTemplateStatsForm.Template.Will;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "damage")
				{
					e.Cancel = true;
					DamageModListForm damageModListForm = new DamageModListForm(this.fTemplate);
					if (damageModListForm.ShowDialog() == DialogResult.OK)
					{
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "senses")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fTemplate.Senses, "Senses", "");
					if (detailsForm.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.Senses = detailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "movement")
				{
					e.Cancel = true;
					DetailsForm detailsForm2 = new DetailsForm(this.fTemplate.Movement, "Movement", "");
					if (detailsForm2.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.Movement = detailsForm2.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "tactics")
				{
					e.Cancel = true;
					DetailsForm detailsForm3 = new DetailsForm(this.fTemplate.Tactics, "Tactics", "");
					if (detailsForm3.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.Tactics = detailsForm3.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "power")
			{
				if (e.Url.LocalPath == "addpower")
				{
					e.Cancel = true;
					CreaturePower creaturePower = new CreaturePower();
					creaturePower.Name = "New Power";
					creaturePower.Action = new PowerAction();
					bool functional_template = this.fTemplate.Type == CreatureTemplateType.Functional;
					PowerBuilderForm powerBuilderForm = new PowerBuilderForm(creaturePower, null, functional_template);
					if (powerBuilderForm.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.CreaturePowers.Add(powerBuilderForm.Power);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addtrait")
				{
					e.Cancel = true;
					CreaturePower creaturePower2 = new CreaturePower();
					creaturePower2.Name = "New Trait";
					creaturePower2.Action = null;
					bool functional_template2 = this.fTemplate.Type == CreatureTemplateType.Functional;
					PowerBuilderForm powerBuilderForm2 = new PowerBuilderForm(creaturePower2, null, functional_template2);
					if (powerBuilderForm2.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.CreaturePowers.Add(powerBuilderForm2.Power);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addaura")
				{
					e.Cancel = true;
					AuraForm auraForm = new AuraForm(new Aura
					{
						Name = "New Aura",
						Details = "1"
					});
					if (auraForm.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.Auras.Add(auraForm.Aura);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "regenedit")
				{
					e.Cancel = true;
					Regeneration regeneration = this.fTemplate.Regeneration;
					if (regeneration == null)
					{
						regeneration = new Regeneration(5, "");
					}
					RegenerationForm regenerationForm = new RegenerationForm(regeneration);
					if (regenerationForm.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.Regeneration = regenerationForm.Regeneration;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "regenremove")
				{
					e.Cancel = true;
					this.fTemplate.Regeneration = null;
					this.update_statblock();
				}
			}
			//e.Url.Scheme == "powerup";
			//e.Url.Scheme == "powerdown";
			if (e.Url.Scheme == "poweredit")
			{
				CreaturePower creaturePower3 = this.find_power(new Guid(e.Url.LocalPath));
				if (creaturePower3 != null)
				{
					e.Cancel = true;
					int index = this.fTemplate.CreaturePowers.IndexOf(creaturePower3);
					bool functional_template3 = this.fTemplate.Type == CreatureTemplateType.Functional;
					PowerBuilderForm powerBuilderForm3 = new PowerBuilderForm(creaturePower3, null, functional_template3);
					if (powerBuilderForm3.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.CreaturePowers[index] = powerBuilderForm3.Power;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "powerremove")
			{
				CreaturePower creaturePower4 = this.find_power(new Guid(e.Url.LocalPath));
				if (creaturePower4 != null)
				{
					e.Cancel = true;
					this.fTemplate.CreaturePowers.Remove(creaturePower4);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "auraedit")
			{
				Aura aura = this.find_aura(new Guid(e.Url.LocalPath));
				if (aura != null)
				{
					e.Cancel = true;
					int index2 = this.fTemplate.Auras.IndexOf(aura);
					AuraForm auraForm2 = new AuraForm(aura);
					if (auraForm2.ShowDialog() == DialogResult.OK)
					{
						this.fTemplate.Auras[index2] = auraForm2.Aura;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "auraremove")
			{
				Aura aura2 = this.find_aura(new Guid(e.Url.LocalPath));
				if (aura2 != null)
				{
					e.Cancel = true;
					this.fTemplate.Auras.Remove(aura2);
					this.update_statblock();
				}
			}
		}

		private CreaturePower find_power(Guid id)
		{
			foreach (CreaturePower current in this.fTemplate.CreaturePowers)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}

		private Aura find_aura(Guid id)
		{
			foreach (Aura current in this.fTemplate.Auras)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}

		private void add_power(CreaturePower power)
		{
			this.fTemplate.CreaturePowers.Add(power);
			this.update_statblock();
		}

		private void OptionsVariant_Click(object sender, EventArgs e)
		{
		}

		private void update_statblock()
		{
			this.StatBlockBrowser.DocumentText = HTML.CreatureTemplate(this.fTemplate, DisplaySize.Small, true);
		}

		private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Export Creature Template";
			saveFileDialog.FileName = this.fTemplate.Name;
			saveFileDialog.Filter = Program.CreatureTemplateFilter;
			if (saveFileDialog.ShowDialog() == DialogResult.OK && !Serialisation<CreatureTemplate>.Save(saveFileDialog.FileName, this.fTemplate, SerialisationMode.Binary))
			{
				string text = "The creature template could not be exported.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}
}
