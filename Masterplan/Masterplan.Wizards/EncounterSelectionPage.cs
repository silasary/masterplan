using Masterplan.Data;
using Masterplan.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class EncounterSelectionPage : UserControl, IWizardPage
	{
		private AdviceData fData;

		private IContainer components;

		private Label InfoLbl;

		private ListView SlotList;

		private ColumnHeader SlotHdr;

		private ColumnHeader CardHdr;

		public EncounterTemplateSlot SelectedSlot
		{
			get
			{
				if (this.SlotList.SelectedItems.Count != 0)
				{
					return this.SlotList.SelectedItems[0].Tag as EncounterTemplateSlot;
				}
				return null;
			}
		}

		public bool AllowNext
		{
			get
			{
				return false;
			}
		}

		public bool AllowBack
		{
			get
			{
				return true;
			}
		}

		public bool AllowFinish
		{
			get
			{
				foreach (EncounterTemplateSlot current in this.fData.SelectedTemplate.Slots)
				{
					if (!this.fData.FilledSlots.ContainsKey(current))
					{
						return false;
					}
				}
				return true;
			}
		}

		public EncounterSelectionPage()
		{
			this.InitializeComponent();
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = (data as AdviceData);
			}
			this.update_list();
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnNext()
		{
			return true;
		}

		public bool OnFinish()
		{
			return true;
		}

		private void SlotList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				CreatureSelectForm creatureSelectForm = new CreatureSelectForm(this.SelectedSlot, this.fData.PartyLevel);
				if (creatureSelectForm.ShowDialog() == DialogResult.OK)
				{
					this.fData.FilledSlots[this.SelectedSlot] = creatureSelectForm.Creature;
					this.update_list();
				}
			}
		}

		private void update_list()
		{
			this.SlotList.Items.Clear();
			foreach (EncounterTemplateSlot current in this.fData.SelectedTemplate.Slots)
			{
				ListViewItem listViewItem = this.SlotList.Items.Add(this.slot_info(current));
				if (this.fData.FilledSlots.ContainsKey(current))
				{
					listViewItem.SubItems.Add(this.fData.FilledSlots[current].Title);
				}
				else
				{
					listViewItem.SubItems.Add("(not filled)");
				}
				listViewItem.Tag = current;
			}
			if (this.SlotList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.SlotList.Items.Add("(no unused slots)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
		}

		private string slot_info(EncounterTemplateSlot slot)
		{
			int num = this.fData.PartyLevel + slot.LevelAdjustment;
			string text = (slot.Flag != RoleFlag.Standard) ? (" " + slot.Flag) : "";
			string text2 = "";
			foreach (RoleType current in slot.Roles)
			{
				if (text2 != "")
				{
					text2 += " / ";
				}
				text2 += current.ToString().ToLower();
			}
			if (text2 == "")
			{
				text2 = "any role";
			}
			if (slot.Minions)
			{
				text2 += ", minion";
			}
			string text3 = "";
			if (slot.Count != 1)
			{
				text3 = " (x" + slot.Count + ")";
			}
			return string.Concat(new object[]
			{
				"Level ",
				num,
				text,
				" ",
				text2,
				text3
			});
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
			this.InfoLbl = new Label();
			this.SlotList = new ListView();
			this.SlotHdr = new ColumnHeader();
			this.CardHdr = new ColumnHeader();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(372, 40);
			this.InfoLbl.TabIndex = 1;
			this.InfoLbl.Text = "Double-click on each of the empty slots in the list below to select creatures to fill them.";
			this.SlotList.Columns.AddRange(new ColumnHeader[]
			{
				this.SlotHdr,
				this.CardHdr
			});
			this.SlotList.Dock = DockStyle.Fill;
			this.SlotList.FullRowSelect = true;
			this.SlotList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SlotList.HideSelection = false;
			this.SlotList.Location = new Point(0, 40);
			this.SlotList.Name = "SlotList";
			this.SlotList.Size = new Size(372, 206);
			this.SlotList.TabIndex = 2;
			this.SlotList.UseCompatibleStateImageBehavior = false;
			this.SlotList.View = View.Details;
			this.SlotList.DoubleClick += new EventHandler(this.SlotList_DoubleClick);
			this.SlotHdr.Text = "Slot";
			this.SlotHdr.Width = 160;
			this.CardHdr.Text = "Selected Creature";
			this.CardHdr.Width = 160;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.SlotList);
			base.Controls.Add(this.InfoLbl);
			base.Name = "EncounterSelectionPage";
			base.Size = new Size(372, 246);
			base.ResumeLayout(false);
		}
	}
}
