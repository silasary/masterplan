using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class MagicItemBuilderForm : Form
	{
		private MagicItem fMagicItem;

		private IContainer components;

		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private WebBrowser StatBlockBrowser;

		private ToolStripDropDownButton OptionsMenu;

		private ToolStripMenuItem OptionsVariant;

		private ToolStrip Toolbar;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileExport;

		public MagicItem MagicItem
		{
			get
			{
				return this.fMagicItem;
			}
		}

		public MagicItemBuilderForm(MagicItem item)
		{
			this.InitializeComponent();
			this.fMagicItem = item.Copy();
			this.update_statblock();
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "build")
			{
				if (e.Url.LocalPath == "profile")
				{
					e.Cancel = true;
					MagicItemProfileForm magicItemProfileForm = new MagicItemProfileForm(this.fMagicItem);
					if (magicItemProfileForm.ShowDialog() == DialogResult.OK)
					{
						this.fMagicItem.Name = magicItemProfileForm.MagicItem.Name;
						this.fMagicItem.Level = magicItemProfileForm.MagicItem.Level;
						this.fMagicItem.Type = magicItemProfileForm.MagicItem.Type;
						this.fMagicItem.Rarity = magicItemProfileForm.MagicItem.Rarity;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "desc")
				{
					e.Cancel = true;
					DetailsForm detailsForm = new DetailsForm(this.fMagicItem.Description, "Description", null);
					if (detailsForm.ShowDialog() == DialogResult.OK)
					{
						this.fMagicItem.Description = detailsForm.Details;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "section")
			{
				e.Cancel = true;
				if (e.Url.LocalPath == "new")
				{
					MagicItemSectionForm magicItemSectionForm = new MagicItemSectionForm(new MagicItemSection
					{
						Header = "New Section"
					});
					if (magicItemSectionForm.ShowDialog() == DialogResult.OK)
					{
						this.fMagicItem.Sections.Add(magicItemSectionForm.Section);
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "edit")
			{
				e.Cancel = true;
				int index = int.Parse(e.Url.LocalPath);
				MagicItemSectionForm magicItemSectionForm2 = new MagicItemSectionForm(this.fMagicItem.Sections[index]);
				if (magicItemSectionForm2.ShowDialog() == DialogResult.OK)
				{
					this.fMagicItem.Sections[index] = magicItemSectionForm2.Section;
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "remove")
			{
				e.Cancel = true;
				int index2 = int.Parse(e.Url.LocalPath);
				this.fMagicItem.Sections.RemoveAt(index2);
				this.update_statblock();
			}
			if (e.Url.Scheme == "moveup")
			{
				e.Cancel = true;
				int num = int.Parse(e.Url.LocalPath);
				MagicItemSection value = this.fMagicItem.Sections[num - 1];
				this.fMagicItem.Sections[num - 1] = this.fMagicItem.Sections[num];
				this.fMagicItem.Sections[num] = value;
				this.update_statblock();
			}
			if (e.Url.Scheme == "movedown")
			{
				e.Cancel = true;
				int num2 = int.Parse(e.Url.LocalPath);
				MagicItemSection value2 = this.fMagicItem.Sections[num2 + 1];
				this.fMagicItem.Sections[num2 + 1] = this.fMagicItem.Sections[num2];
				this.fMagicItem.Sections[num2] = value2;
				this.update_statblock();
			}
		}

		private void OptionsVariant_Click(object sender, EventArgs e)
		{
			MagicItemSelectForm magicItemSelectForm = new MagicItemSelectForm(this.fMagicItem.Level);
			if (magicItemSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fMagicItem = magicItemSelectForm.MagicItem.Copy();
				this.fMagicItem.ID = Guid.NewGuid();
				this.update_statblock();
			}
		}

		private void update_statblock()
		{
			this.StatBlockBrowser.DocumentText = HTML.MagicItem(this.fMagicItem, DisplaySize.Small, true, true);
		}

		private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Export Magic Item";
			saveFileDialog.FileName = this.fMagicItem.Name;
			saveFileDialog.Filter = Program.MagicItemFilter;
			if (saveFileDialog.ShowDialog() == DialogResult.OK && !Serialisation<MagicItem>.Save(saveFileDialog.FileName, this.fMagicItem, SerialisationMode.Binary))
			{
				string text = "The magic item could not be exported.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(MagicItemBuilderForm));
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.StatBlockBrowser = new WebBrowser();
			this.OptionsMenu = new ToolStripDropDownButton();
			this.OptionsVariant = new ToolStripMenuItem();
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
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
			this.OptionsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OptionsMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.OptionsVariant
			});
			this.OptionsMenu.Image = (Image)resources.GetObject("OptionsMenu.Image");
			this.OptionsMenu.ImageTransparentColor = Color.Magenta;
			this.OptionsMenu.Name = "OptionsMenu";
			this.OptionsMenu.Size = new Size(62, 22);
			this.OptionsMenu.Text = "Options";
			this.OptionsVariant.Name = "OptionsVariant";
			this.OptionsVariant.Size = new Size(197, 22);
			this.OptionsVariant.Text = "Copy an Existing Item...";
			this.OptionsVariant.Click += new EventHandler(this.OptionsVariant_Click);
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
			this.FileMenu.Image = (Image)resources.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new Size(152, 22);
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
			base.Name = "MagicItemBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Magic Item Builder";
			this.BtnPnl.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
