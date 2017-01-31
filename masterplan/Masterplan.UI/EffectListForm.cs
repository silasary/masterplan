using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class EffectListForm : Form
	{
		private Encounter fEncounter;

		private CombatData fCurrentActor;

		private int fCurrentRound = -2147483648;

		private IContainer components;

		private ListView EffectList;

		private ColumnHeader EffectHdr;

		private ToolStrip Toolbar;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		public Pair<CombatData, OngoingCondition> SelectedEffect
		{
			get
			{
				if (this.EffectList.SelectedItems.Count != 0)
				{
					return this.EffectList.SelectedItems[0].Tag as Pair<CombatData, OngoingCondition>;
				}
				return null;
			}
		}

		public EffectListForm(Encounter enc, CombatData current_actor, int current_round)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fEncounter = enc;
			this.fCurrentActor = current_actor;
			this.fCurrentRound = current_round;
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.SelectedEffect != null);
			this.EditBtn.Enabled = (this.SelectedEffect != null);
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				CombatData first = this.SelectedEffect.First;
				OngoingCondition second = this.SelectedEffect.Second;
				first.Conditions.Remove(second);
				this.update_list();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				CombatData first = this.SelectedEffect.First;
				OngoingCondition second = this.SelectedEffect.Second;
				int index = first.Conditions.IndexOf(second);
				EffectForm effectForm = new EffectForm(second, this.fEncounter, this.fCurrentActor, this.fCurrentRound);
				if (effectForm.ShowDialog() == DialogResult.OK)
				{
					first.Conditions[index] = effectForm.Effect;
					this.update_list();
				}
			}
		}

		private void update_list()
		{
			this.EffectList.Groups.Clear();
			this.EffectList.Items.Clear();
			foreach (Hero current in Session.Project.Heroes)
			{
				CombatData combatData = current.CombatData;
				if (combatData.Conditions.Count > 0)
				{
					this.add_conditions(combatData);
				}
			}
			foreach (EncounterSlot current2 in this.fEncounter.Slots)
			{
				foreach (CombatData current3 in current2.CombatData)
				{
					if (current3.Conditions.Count > 0)
					{
						this.add_conditions(current3);
					}
				}
			}
		}

		private void add_conditions(CombatData cd)
		{
			ListViewGroup group = this.EffectList.Groups.Add(cd.DisplayName, cd.DisplayName);
			foreach (OngoingCondition current in cd.Conditions)
			{
				ListViewItem listViewItem = this.EffectList.Items.Add(current.ToString(this.fEncounter, false));
				listViewItem.Tag = new Pair<CombatData, OngoingCondition>(cd, current);
				listViewItem.Group = group;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(EffectListForm));
			this.Toolbar = new ToolStrip();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.EffectList = new ListView();
			this.EffectHdr = new ColumnHeader();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.RemoveBtn,
				this.EditBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(398, 25);
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
				this.EffectHdr
			});
			this.EffectList.Dock = DockStyle.Fill;
			this.EffectList.FullRowSelect = true;
			this.EffectList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EffectList.HideSelection = false;
			this.EffectList.Location = new Point(0, 25);
			this.EffectList.MultiSelect = false;
			this.EffectList.Name = "EffectList";
			this.EffectList.Size = new Size(398, 289);
			this.EffectList.TabIndex = 1;
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Details;
			this.EffectList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.EffectHdr.Text = "Effect";
			this.EffectHdr.Width = 363;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(398, 314);
			base.Controls.Add(this.EffectList);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EffectListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Ongoing Effects";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
