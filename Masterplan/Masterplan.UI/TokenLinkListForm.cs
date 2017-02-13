using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TokenLinkListForm : Form
	{
		private List<TokenLink> fLinks;

		private IContainer components;

		private ListView EffectList;

		private ColumnHeader LinkHdr;

		private ToolStrip Toolbar;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ColumnHeader TokenHdr;

		public TokenLink SelectedLink
		{
			get
			{
				if (this.EffectList.SelectedItems.Count != 0)
				{
					return this.EffectList.SelectedItems[0].Tag as TokenLink;
				}
				return null;
			}
		}

		public TokenLinkListForm(List<TokenLink> links)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fLinks = links;
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.SelectedLink != null);
			this.EditBtn.Enabled = (this.SelectedLink != null);
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLink != null)
			{
				this.fLinks.Remove(this.SelectedLink);
				this.update_list();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLink != null)
			{
				int index = this.fLinks.IndexOf(this.SelectedLink);
				TokenLinkForm tokenLinkForm = new TokenLinkForm(this.SelectedLink);
				if (tokenLinkForm.ShowDialog() == DialogResult.OK)
				{
					this.fLinks[index] = tokenLinkForm.Link;
					this.update_list();
				}
			}
		}

		private void update_list()
		{
			this.EffectList.Items.Clear();
			foreach (TokenLink current in this.fLinks)
			{
				string text = "";
				foreach (IToken current2 in current.Tokens)
				{
					string text2 = "";
					if (current2 is CreatureToken)
					{
						CreatureToken creatureToken = current2 as CreatureToken;
						text2 = creatureToken.Data.DisplayName;
					}
					if (current2 is Hero)
					{
						Hero hero = current2 as Hero;
						text2 = hero.Name;
					}
					if (current2 is CustomToken)
					{
						CustomToken customToken = current2 as CustomToken;
						text2 = customToken.Name;
					}
					if (text2 == "")
					{
						text2 = "(unknown map token)";
					}
					if (text != "")
					{
						text += ", ";
					}
					text += text2;
				}
				ListViewItem listViewItem = this.EffectList.Items.Add(text);
				listViewItem.SubItems.Add(current.Text);
				listViewItem.Tag = current;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(TokenLinkListForm));
			this.Toolbar = new ToolStrip();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.EffectList = new ListView();
			this.LinkHdr = new ColumnHeader();
			this.TokenHdr = new ColumnHeader();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.RemoveBtn,
				this.EditBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(429, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
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
			this.EffectList.Columns.AddRange(new ColumnHeader[]
			{
				this.TokenHdr,
				this.LinkHdr
			});
			this.EffectList.Dock = DockStyle.Fill;
			this.EffectList.FullRowSelect = true;
			this.EffectList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EffectList.HideSelection = false;
			this.EffectList.Location = new Point(0, 25);
			this.EffectList.MultiSelect = false;
			this.EffectList.Name = "EffectList";
			this.EffectList.Size = new Size(429, 172);
			this.EffectList.TabIndex = 1;
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Details;
			this.EffectList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.LinkHdr.Text = "Link";
			this.LinkHdr.Width = 150;
			this.TokenHdr.Text = "Tokens";
			this.TokenHdr.Width = 250;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(429, 197);
			base.Controls.Add(this.EffectList);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TokenLinkListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Token Links";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
