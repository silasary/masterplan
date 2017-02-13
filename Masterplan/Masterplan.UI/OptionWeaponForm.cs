using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionWeaponForm : Form
	{
		private Weapon fWeapon;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Label CatLbl;

		private ComboBox CatBox;

		private Label TypeLbl;

		private ComboBox TypeBox;

		private CheckBox TwoHandBox;

		private Label ProfLbl;

		private NumericUpDown ProfBox;

		private Label DamageLbl;

		private TextBox DamageBox;

		private Label RangeLbl;

		private TextBox RangeBox;

		private Label PriceLbl;

		private TextBox PriceBox;

		private Label WeightLbl;

		private TextBox WeightBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private Button OKBtn;

		private Button CancelBtn;

		private ComboBox PropertiesBox;

		private Label PropertiesLbl;

		private ComboBox GroupBox;

		private Label GroupLbl;

		public Weapon Weapon
		{
			get
			{
				return this.fWeapon;
			}
		}

		public OptionWeaponForm(Weapon weapon)
		{
			this.InitializeComponent();
			Array values = Enum.GetValues(typeof(WeaponCategory));
			foreach (WeaponCategory weaponCategory in values)
			{
				this.CatBox.Items.Add(weaponCategory);
			}
			Array values2 = Enum.GetValues(typeof(WeaponType));
			foreach (WeaponType weaponType in values2)
			{
				this.TypeBox.Items.Add(weaponType);
			}
			this.GroupBox.Items.Add("Axe");
			this.GroupBox.Items.Add("Box");
			this.GroupBox.Items.Add("Crossbow");
			this.GroupBox.Items.Add("Flail");
			this.GroupBox.Items.Add("Hammer");
			this.GroupBox.Items.Add("Heavy Blade");
			this.GroupBox.Items.Add("Light Blade");
			this.GroupBox.Items.Add("Mace");
			this.GroupBox.Items.Add("Pick");
			this.GroupBox.Items.Add("Polearm");
			this.GroupBox.Items.Add("Sling");
			this.GroupBox.Items.Add("Spear");
			this.GroupBox.Items.Add("Staff");
			this.GroupBox.Items.Add("Unarmed");
			this.PropertiesBox.Items.Add("Brutal 1");
			this.PropertiesBox.Items.Add("Brutal 2");
			this.PropertiesBox.Items.Add("Defensive");
			this.PropertiesBox.Items.Add("Heavy Thrown");
			this.PropertiesBox.Items.Add("High Crit");
			this.PropertiesBox.Items.Add("Light Thrown");
			this.PropertiesBox.Items.Add("Load Free");
			this.PropertiesBox.Items.Add("Load Minor");
			this.PropertiesBox.Items.Add("Off-Hand");
			this.PropertiesBox.Items.Add("Reach");
			this.PropertiesBox.Items.Add("Small");
			this.PropertiesBox.Items.Add("Stout");
			this.PropertiesBox.Items.Add("Versatile");
			this.fWeapon = weapon.Copy();
			this.NameBox.Text = this.fWeapon.Name;
			this.CatBox.SelectedItem = this.fWeapon.Category;
			this.TypeBox.SelectedItem = this.fWeapon.Type;
			this.TwoHandBox.Checked = this.fWeapon.TwoHanded;
			this.ProfBox.Value = this.fWeapon.Proficiency;
			this.DamageBox.Text = this.fWeapon.Damage;
			this.RangeBox.Text = this.fWeapon.Range;
			this.PriceBox.Text = this.fWeapon.Price;
			this.WeightBox.Text = this.fWeapon.Weight;
			this.GroupBox.Text = this.fWeapon.Group;
			this.PropertiesBox.Text = this.fWeapon.Properties;
			this.DetailsBox.Text = this.fWeapon.Description;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fWeapon.Name = this.NameBox.Text;
			this.fWeapon.Category = (WeaponCategory)this.CatBox.SelectedItem;
			this.fWeapon.Type = (WeaponType)this.TypeBox.SelectedItem;
			this.fWeapon.TwoHanded = this.TwoHandBox.Checked;
			this.fWeapon.Proficiency = (int)this.ProfBox.Value;
			this.fWeapon.Damage = this.DamageBox.Text;
			this.fWeapon.Range = this.RangeBox.Text;
			this.fWeapon.Price = this.PriceBox.Text;
			this.fWeapon.Weight = this.WeightBox.Text;
			this.fWeapon.Group = this.GroupBox.Text;
			this.fWeapon.Properties = this.PropertiesBox.Text;
			this.fWeapon.Description = this.DetailsBox.Text;
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
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.CatLbl = new Label();
			this.CatBox = new ComboBox();
			this.TypeLbl = new Label();
			this.TypeBox = new ComboBox();
			this.TwoHandBox = new CheckBox();
			this.ProfLbl = new Label();
			this.ProfBox = new NumericUpDown();
			this.DamageLbl = new Label();
			this.DamageBox = new TextBox();
			this.RangeLbl = new Label();
			this.RangeBox = new TextBox();
			this.PriceLbl = new Label();
			this.PriceBox = new TextBox();
			this.WeightLbl = new Label();
			this.WeightBox = new TextBox();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.PropertiesBox = new ComboBox();
			this.PropertiesLbl = new Label();
			this.GroupBox = new ComboBox();
			this.GroupLbl = new Label();
			((ISupportInitialize)this.ProfBox).BeginInit();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(80, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(230, 20);
			this.NameBox.TabIndex = 1;
			this.CatLbl.AutoSize = true;
			this.CatLbl.Location = new Point(12, 41);
			this.CatLbl.Name = "CatLbl";
			this.CatLbl.Size = new Size(52, 13);
			this.CatLbl.TabIndex = 2;
			this.CatLbl.Text = "Category:";
			this.CatBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.CatBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.CatBox.FormattingEnabled = true;
			this.CatBox.Location = new Point(80, 38);
			this.CatBox.Name = "CatBox";
			this.CatBox.Size = new Size(230, 21);
			this.CatBox.TabIndex = 3;
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(12, 68);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new Size(34, 13);
			this.TypeLbl.TabIndex = 4;
			this.TypeLbl.Text = "Type:";
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(80, 65);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new Size(230, 21);
			this.TypeBox.TabIndex = 5;
			this.TwoHandBox.AutoSize = true;
			this.TwoHandBox.Location = new Point(80, 92);
			this.TwoHandBox.Name = "TwoHandBox";
			this.TwoHandBox.Size = new Size(162, 17);
			this.TwoHandBox.TabIndex = 6;
			this.TwoHandBox.Text = "Must be wielded two-handed";
			this.TwoHandBox.UseVisualStyleBackColor = true;
			this.ProfLbl.AutoSize = true;
			this.ProfLbl.Location = new Point(12, 117);
			this.ProfLbl.Name = "ProfLbl";
			this.ProfLbl.Size = new Size(62, 13);
			this.ProfLbl.TabIndex = 7;
			this.ProfLbl.Text = "Proficiency:";
			this.ProfBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ProfBox.Location = new Point(80, 115);
			this.ProfBox.Name = "ProfBox";
			this.ProfBox.Size = new Size(230, 20);
			this.ProfBox.TabIndex = 8;
			this.DamageLbl.AutoSize = true;
			this.DamageLbl.Location = new Point(12, 144);
			this.DamageLbl.Name = "DamageLbl";
			this.DamageLbl.Size = new Size(50, 13);
			this.DamageLbl.TabIndex = 9;
			this.DamageLbl.Text = "Damage:";
			this.DamageBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageBox.Location = new Point(80, 141);
			this.DamageBox.Name = "DamageBox";
			this.DamageBox.Size = new Size(230, 20);
			this.DamageBox.TabIndex = 10;
			this.RangeLbl.AutoSize = true;
			this.RangeLbl.Location = new Point(12, 170);
			this.RangeLbl.Name = "RangeLbl";
			this.RangeLbl.Size = new Size(42, 13);
			this.RangeLbl.TabIndex = 11;
			this.RangeLbl.Text = "Range:";
			this.RangeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RangeBox.Location = new Point(80, 167);
			this.RangeBox.Name = "RangeBox";
			this.RangeBox.Size = new Size(230, 20);
			this.RangeBox.TabIndex = 12;
			this.PriceLbl.AutoSize = true;
			this.PriceLbl.Location = new Point(12, 196);
			this.PriceLbl.Name = "PriceLbl";
			this.PriceLbl.Size = new Size(34, 13);
			this.PriceLbl.TabIndex = 13;
			this.PriceLbl.Text = "Price:";
			this.PriceBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.PriceBox.Location = new Point(80, 193);
			this.PriceBox.Name = "PriceBox";
			this.PriceBox.Size = new Size(230, 20);
			this.PriceBox.TabIndex = 14;
			this.WeightLbl.AutoSize = true;
			this.WeightLbl.Location = new Point(12, 222);
			this.WeightLbl.Name = "WeightLbl";
			this.WeightLbl.Size = new Size(44, 13);
			this.WeightLbl.TabIndex = 15;
			this.WeightLbl.Text = "Weight:";
			this.WeightBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.WeightBox.Location = new Point(80, 219);
			this.WeightBox.Name = "WeightBox";
			this.WeightBox.Size = new Size(230, 20);
			this.WeightBox.TabIndex = 16;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Location = new Point(12, 299);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(298, 129);
			this.Pages.TabIndex = 21;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(290, 103);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(284, 97);
			this.DetailsBox.TabIndex = 0;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(154, 434);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 22;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(235, 434);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 23;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.PropertiesBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.PropertiesBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.PropertiesBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.PropertiesBox.FormattingEnabled = true;
			this.PropertiesBox.Location = new Point(80, 272);
			this.PropertiesBox.Name = "PropertiesBox";
			this.PropertiesBox.Size = new Size(230, 21);
			this.PropertiesBox.TabIndex = 20;
			this.PropertiesLbl.AutoSize = true;
			this.PropertiesLbl.Location = new Point(12, 275);
			this.PropertiesLbl.Name = "PropertiesLbl";
			this.PropertiesLbl.Size = new Size(57, 13);
			this.PropertiesLbl.TabIndex = 19;
			this.PropertiesLbl.Text = "Properties:";
			this.GroupBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.GroupBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.GroupBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.GroupBox.FormattingEnabled = true;
			this.GroupBox.Location = new Point(80, 245);
			this.GroupBox.Name = "GroupBox";
			this.GroupBox.Size = new Size(230, 21);
			this.GroupBox.TabIndex = 18;
			this.GroupLbl.AutoSize = true;
			this.GroupLbl.Location = new Point(12, 248);
			this.GroupLbl.Name = "GroupLbl";
			this.GroupLbl.Size = new Size(50, 13);
			this.GroupLbl.TabIndex = 17;
			this.GroupLbl.Text = "Group(s):";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(322, 469);
			base.Controls.Add(this.PropertiesBox);
			base.Controls.Add(this.PropertiesLbl);
			base.Controls.Add(this.GroupBox);
			base.Controls.Add(this.GroupLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.WeightBox);
			base.Controls.Add(this.WeightLbl);
			base.Controls.Add(this.PriceBox);
			base.Controls.Add(this.PriceLbl);
			base.Controls.Add(this.RangeBox);
			base.Controls.Add(this.RangeLbl);
			base.Controls.Add(this.DamageBox);
			base.Controls.Add(this.DamageLbl);
			base.Controls.Add(this.ProfBox);
			base.Controls.Add(this.ProfLbl);
			base.Controls.Add(this.TwoHandBox);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.TypeLbl);
			base.Controls.Add(this.CatBox);
			base.Controls.Add(this.CatLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionWeaponForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Weapon";
			((ISupportInitialize)this.ProfBox).EndInit();
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
