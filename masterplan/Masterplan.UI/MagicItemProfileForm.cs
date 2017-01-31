using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class MagicItemProfileForm : Form
	{
		private MagicItem fItem;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Label TypeLbl;

		private ComboBox TypeBox;

		private ComboBox RarityBox;

		private Label RarityLbl;

		public MagicItem MagicItem
		{
			get
			{
				return this.fItem;
			}
		}

		public MagicItemProfileForm(MagicItem item)
		{
			this.InitializeComponent();
			this.TypeBox.Items.Add("Armour");
			this.TypeBox.Items.Add("Weapon");
			this.TypeBox.Items.Add("Ammunition");
			this.TypeBox.Items.Add("Item Slot (head)");
			this.TypeBox.Items.Add("Item Slot (neck)");
			this.TypeBox.Items.Add("Item Slot (waist)");
			this.TypeBox.Items.Add("Item Slot (arms)");
			this.TypeBox.Items.Add("Item Slot (hands)");
			this.TypeBox.Items.Add("Item Slot (feet)");
			this.TypeBox.Items.Add("Item Slot (ring)");
			this.TypeBox.Items.Add("Implement");
			this.TypeBox.Items.Add("Alchemical Item");
			this.TypeBox.Items.Add("Divine Boon");
			this.TypeBox.Items.Add("Grandmaster Training");
			this.TypeBox.Items.Add("Potion");
			this.TypeBox.Items.Add("Reagent");
			this.TypeBox.Items.Add("Whetstone");
			this.TypeBox.Items.Add("Wondrous Item");
			Array values = Enum.GetValues(typeof(MagicItemRarity));
			foreach (MagicItemRarity magicItemRarity in values)
			{
				this.RarityBox.Items.Add(magicItemRarity);
			}
			this.fItem = item.Copy();
			this.NameBox.Text = this.fItem.Name;
			this.LevelBox.Value = this.fItem.Level;
			this.TypeBox.Text = this.fItem.Type;
			this.RarityBox.SelectedItem = this.fItem.Rarity;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fItem.Name = this.NameBox.Text;
			this.fItem.Level = (int)this.LevelBox.Value;
			this.fItem.Type = this.TypeBox.Text;
			this.fItem.Rarity = (MagicItemRarity)this.RarityBox.SelectedItem;
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
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.TypeLbl = new Label();
			this.TypeBox = new ComboBox();
			this.RarityBox = new ComboBox();
			this.RarityLbl = new Label();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(119, 124);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 8;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(200, 124);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 9;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(77, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(198, 20);
			this.NameBox.TabIndex = 1;
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 40);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(36, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(77, 38);
			NumericUpDown arg_2D9_0 = this.LevelBox;
			int[] array = new int[4];
			array[0] = 30;
			arg_2D9_0.Maximum = new decimal(array);
			NumericUpDown arg_2F5_0 = this.LevelBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_2F5_0.Minimum = new decimal(array2);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(198, 20);
			this.LevelBox.TabIndex = 3;
			NumericUpDown arg_344_0 = this.LevelBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_344_0.Value = new decimal(array3);
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(12, 67);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new Size(34, 13);
			this.TypeLbl.TabIndex = 4;
			this.TypeLbl.Text = "Type:";
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.TypeBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(77, 64);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new Size(198, 21);
			this.TypeBox.Sorted = true;
			this.TypeBox.TabIndex = 5;
			this.RarityBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RarityBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.RarityBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.RarityBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.RarityBox.FormattingEnabled = true;
			this.RarityBox.Location = new Point(77, 91);
			this.RarityBox.Name = "RarityBox";
			this.RarityBox.Size = new Size(198, 21);
			this.RarityBox.Sorted = true;
			this.RarityBox.TabIndex = 7;
			this.RarityLbl.AutoSize = true;
			this.RarityLbl.Location = new Point(12, 94);
			this.RarityLbl.Name = "RarityLbl";
			this.RarityLbl.Size = new Size(59, 13);
			this.RarityLbl.TabIndex = 6;
			this.RarityLbl.Text = "Availability:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(287, 159);
			base.Controls.Add(this.RarityBox);
			base.Controls.Add(this.RarityLbl);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.TypeLbl);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MagicItemProfileForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Magic Item";
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
