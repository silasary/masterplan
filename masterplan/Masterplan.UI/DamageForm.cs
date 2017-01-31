using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class DamageForm : Form
	{
		public class Token
		{
			public CombatData Data;

			public EncounterCard Card;

			public int Modifier;

			public Token(CombatData data, EncounterCard card)
			{
				this.Data = data;
				this.Card = card;
			}
		}

		private List<DamageForm.Token> fData;

		private List<DamageType> fTypes = new List<DamageType>();

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label DmgLbl;

		private NumericUpDown DmgBox;

		private Label ModLbl;

		private TextBox ModBox;

		private Label ValLbl;

		private CheckBox HalveBtn;

		private Label TypeLbl;

		private TextBox ValBox;

		private ToolStrip AmountToolbar;

		private ToolStripButton Dmg1;

		private ToolStripButton Dmg2;

		private ToolStripButton Dmg5;

		private ToolStripButton Dmg10;

		private ToolStripButton Dmg20;

		private ToolStripButton Dmg50;

		private ToolStripButton Dmg100;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton ResetBtn;

		private ToolStrip TypeToolbar;

		private ToolStripButton FireBtn;

		private ToolStripButton ColdBtn;

		private ToolStripButton LightningBtn;

		private ToolStripButton ThunderBtn;

		private ToolStripButton PsychicBtn;

		private ToolStripButton ForceBtn;

		private ToolStripButton AcidBtn;

		private ToolStripButton PoisonBtn;

		private ToolStripButton NecroticBtn;

		private ToolStripButton RadiantBtn;

		private TextBox TypeBox;

		public List<DamageType> Types
		{
			get
			{
				return this.fTypes;
			}
		}

		public DamageForm(List<Pair<CombatData, EncounterCard>> tokens, int value)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fData = new List<DamageForm.Token>();
			foreach (Pair<CombatData, EncounterCard> current in tokens)
			{
				this.fData.Add(new DamageForm.Token(current.First, current.Second));
			}
			this.DmgBox.Value = value;
			if (this.fData.Count == 1 && this.fData[0].Card != null)
			{
				this.HalveBtn.Checked = this.fData[0].Card.Resist.ToLower().Contains("insubstantial");
			}
			this.update_type();
			this.update_modifier();
			this.update_value();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ResetBtn.Enabled = (this.DmgBox.Value != 0m);
			this.AcidBtn.Checked = this.fTypes.Contains(DamageType.Acid);
			this.ColdBtn.Checked = this.fTypes.Contains(DamageType.Cold);
			this.FireBtn.Checked = this.fTypes.Contains(DamageType.Fire);
			this.ForceBtn.Checked = this.fTypes.Contains(DamageType.Force);
			this.LightningBtn.Checked = this.fTypes.Contains(DamageType.Lightning);
			this.NecroticBtn.Checked = this.fTypes.Contains(DamageType.Necrotic);
			this.PoisonBtn.Checked = this.fTypes.Contains(DamageType.Poison);
			this.PsychicBtn.Checked = this.fTypes.Contains(DamageType.Psychic);
			this.RadiantBtn.Checked = this.fTypes.Contains(DamageType.Radiant);
			this.ThunderBtn.Checked = this.fTypes.Contains(DamageType.Thunder);
			this.TypeLbl.Enabled = (this.fTypes.Count != 0);
			this.TypeBox.Enabled = (this.fTypes.Count != 0);
			this.ModLbl.Enabled = (this.fTypes.Count != 0);
			this.ModBox.Enabled = (this.fTypes.Count != 0);
		}

		private void DamageForm_Shown(object sender, EventArgs e)
		{
			this.DmgBox.Select(0, 1);
		}

		private void DmgBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_value();
		}

		private void HalveBtn_CheckedChanged(object sender, EventArgs e)
		{
			this.update_value();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			foreach (DamageForm.Token current in this.fData)
			{
				DamageForm.DoDamage(current.Data, current.Card, (int)this.DmgBox.Value, this.fTypes, this.HalveBtn.Checked);
			}
		}

		internal static void DoDamage(CombatData data, EncounterCard card, int damage, List<DamageType> types, bool halve_damage)
		{
			int modifier = 0;
			if (card != null)
			{
				modifier = card.GetDamageModifier(types, data);
			}
			int num = DamageForm.get_value(damage, modifier, halve_damage);
			if (data.TempHP > 0)
			{
				int num2 = Math.Min(data.TempHP, num);
				data.TempHP -= num2;
				num -= num2;
			}
			data.Damage += num;
		}

		private void update_type()
		{
			string text = "";
			foreach (DamageType current in this.fTypes)
			{
				if (text != "")
				{
					text += ", ";
				}
				text += current.ToString();
			}
			if (text == "")
			{
				text = "(untyped)";
			}
			this.TypeBox.Text = text;
		}

		private void update_modifier()
		{
			foreach (DamageForm.Token current in this.fData)
			{
				if (current.Card != null)
				{
					current.Modifier = current.Card.GetDamageModifier(this.fTypes, current.Data);
				}
			}
			if (this.fData.Count != 1)
			{
				this.ModBox.Text = "(multiple tokens)";
				return;
			}
			DamageForm.Token token = this.fData[0];
			if (token.Modifier == -2147483648)
			{
				this.ModBox.Text = "Immune";
				return;
			}
			if (token.Modifier > 0)
			{
				this.ModBox.Text = "Vulnerable " + token.Modifier;
				return;
			}
			if (token.Modifier < 0)
			{
				this.ModBox.Text = "Resist " + Math.Abs(token.Modifier);
				return;
			}
			this.ModBox.Text = "(none)";
		}

		private void update_value()
		{
			if (this.fData.Count == 1)
			{
				int num = DamageForm.get_value((int)this.DmgBox.Value, this.fData[0].Modifier, this.HalveBtn.Checked);
				this.ValBox.Text = num.ToString();
				return;
			}
			this.ValBox.Text = "(multiple tokens)";
		}

		private static int get_value(int initial_value, int modifier, bool halve_damage)
		{
			int num = initial_value;
			if (modifier != 0)
			{
				if (modifier == -2147483648)
				{
					num = 0;
				}
				else
				{
					num += modifier;
					num = Math.Max(num, 0);
				}
			}
			if (halve_damage)
			{
				num /= 2;
			}
			return num;
		}

		private void Dmg1_Click(object sender, EventArgs e)
		{
			this.damage(1);
		}

		private void Dmg2_Click(object sender, EventArgs e)
		{
			this.damage(2);
		}

		private void Dmg5_Click(object sender, EventArgs e)
		{
			this.damage(5);
		}

		private void Dmg10_Click(object sender, EventArgs e)
		{
			this.damage(10);
		}

		private void Dmg20_Click(object sender, EventArgs e)
		{
			this.damage(20);
		}

		private void Dmg50_Click(object sender, EventArgs e)
		{
			this.damage(50);
		}

		private void Dmg100_Click(object sender, EventArgs e)
		{
			this.damage(100);
		}

		private void damage(int n)
		{
			this.DmgBox.Value += n;
		}

		private void ResetBtn_Click(object sender, EventArgs e)
		{
			this.DmgBox.Value = 0m;
		}

		private void AcidBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Acid);
		}

		private void ColdBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Cold);
		}

		private void FireBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Fire);
		}

		private void ForceBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Force);
		}

		private void LightningBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Lightning);
		}

		private void NecroticBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Necrotic);
		}

		private void PoisonBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Poison);
		}

		private void PsychicBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Psychic);
		}

		private void RadiantBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Radiant);
		}

		private void ThunderBtn_Click(object sender, EventArgs e)
		{
			this.add_type(DamageType.Thunder);
		}

		private void add_type(DamageType type)
		{
			if (this.fTypes.Contains(type))
			{
				this.fTypes.Remove(type);
			}
			else
			{
				this.fTypes.Add(type);
				this.fTypes.Sort();
			}
			this.update_type();
			this.update_modifier();
			this.update_value();
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(DamageForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.DmgLbl = new Label();
			this.DmgBox = new NumericUpDown();
			this.ModLbl = new Label();
			this.ModBox = new TextBox();
			this.ValLbl = new Label();
			this.HalveBtn = new CheckBox();
			this.TypeLbl = new Label();
			this.ValBox = new TextBox();
			this.AmountToolbar = new ToolStrip();
			this.Dmg1 = new ToolStripButton();
			this.Dmg2 = new ToolStripButton();
			this.Dmg5 = new ToolStripButton();
			this.Dmg10 = new ToolStripButton();
			this.Dmg20 = new ToolStripButton();
			this.Dmg50 = new ToolStripButton();
			this.Dmg100 = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.ResetBtn = new ToolStripButton();
			this.TypeToolbar = new ToolStrip();
			this.AcidBtn = new ToolStripButton();
			this.ColdBtn = new ToolStripButton();
			this.FireBtn = new ToolStripButton();
			this.ForceBtn = new ToolStripButton();
			this.LightningBtn = new ToolStripButton();
			this.NecroticBtn = new ToolStripButton();
			this.PoisonBtn = new ToolStripButton();
			this.PsychicBtn = new ToolStripButton();
			this.RadiantBtn = new ToolStripButton();
			this.ThunderBtn = new ToolStripButton();
			this.TypeBox = new TextBox();
			((ISupportInitialize)this.DmgBox).BeginInit();
			this.AmountToolbar.SuspendLayout();
			this.TypeToolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(172, 190);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 11;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(253, 190);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 12;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.DmgLbl.AutoSize = true;
			this.DmgLbl.Location = new Point(66, 30);
			this.DmgLbl.Name = "DmgLbl";
			this.DmgLbl.Size = new Size(50, 13);
			this.DmgLbl.TabIndex = 2;
			this.DmgLbl.Text = "Damage:";
			this.DmgBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DmgBox.Location = new Point(134, 28);
			NumericUpDown arg_34D_0 = this.DmgBox;
			int[] array = new int[4];
			array[0] = 1000;
			arg_34D_0.Maximum = new decimal(array);
			this.DmgBox.Name = "DmgBox";
			this.DmgBox.Size = new Size(194, 20);
			this.DmgBox.TabIndex = 3;
			this.DmgBox.ValueChanged += new EventHandler(this.DmgBox_ValueChanged);
			this.ModLbl.AutoSize = true;
			this.ModLbl.Location = new Point(66, 86);
			this.ModLbl.Name = "ModLbl";
			this.ModLbl.Size = new Size(47, 13);
			this.ModLbl.TabIndex = 6;
			this.ModLbl.Text = "Modifier:";
			this.ModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ModBox.Location = new Point(134, 83);
			this.ModBox.Name = "ModBox";
			this.ModBox.ReadOnly = true;
			this.ModBox.Size = new Size(194, 20);
			this.ModBox.TabIndex = 7;
			this.ValLbl.AutoSize = true;
			this.ValLbl.Location = new Point(66, 135);
			this.ValLbl.Name = "ValLbl";
			this.ValLbl.Size = new Size(62, 13);
			this.ValLbl.TabIndex = 9;
			this.ValLbl.Text = "Final Value:";
			this.HalveBtn.AutoSize = true;
			this.HalveBtn.Location = new Point(134, 109);
			this.HalveBtn.Name = "HalveBtn";
			this.HalveBtn.Size = new Size(95, 17);
			this.HalveBtn.TabIndex = 8;
			this.HalveBtn.Text = "Halve damage";
			this.HalveBtn.UseVisualStyleBackColor = true;
			this.HalveBtn.CheckedChanged += new EventHandler(this.HalveBtn_CheckedChanged);
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(66, 57);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new Size(34, 13);
			this.TypeLbl.TabIndex = 4;
			this.TypeLbl.Text = "Type:";
			this.ValBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ValBox.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.ValBox.Location = new Point(134, 132);
			this.ValBox.Name = "ValBox";
			this.ValBox.ReadOnly = true;
			this.ValBox.Size = new Size(194, 26);
			this.ValBox.TabIndex = 10;
			this.ValBox.Text = "[dmg]";
			this.ValBox.TextAlign = HorizontalAlignment.Center;
			this.AmountToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.Dmg1,
				this.Dmg2,
				this.Dmg5,
				this.Dmg10,
				this.Dmg20,
				this.Dmg50,
				this.Dmg100,
				this.toolStripSeparator1,
				this.ResetBtn
			});
			this.AmountToolbar.Location = new Point(63, 0);
			this.AmountToolbar.Name = "AmountToolbar";
			this.AmountToolbar.Size = new Size(277, 25);
			this.AmountToolbar.TabIndex = 0;
			this.AmountToolbar.Text = "toolStrip1";
			this.Dmg1.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg1.Image = (Image)resources.GetObject("Dmg1.Image");
			this.Dmg1.ImageTransparentColor = Color.Magenta;
			this.Dmg1.Name = "Dmg1";
			this.Dmg1.Size = new Size(25, 22);
			this.Dmg1.Text = "+1";
			this.Dmg1.Click += new EventHandler(this.Dmg1_Click);
			this.Dmg2.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg2.Image = (Image)resources.GetObject("Dmg2.Image");
			this.Dmg2.ImageTransparentColor = Color.Magenta;
			this.Dmg2.Name = "Dmg2";
			this.Dmg2.Size = new Size(25, 22);
			this.Dmg2.Text = "+2";
			this.Dmg2.Click += new EventHandler(this.Dmg2_Click);
			this.Dmg5.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg5.Image = (Image)resources.GetObject("Dmg5.Image");
			this.Dmg5.ImageTransparentColor = Color.Magenta;
			this.Dmg5.Name = "Dmg5";
			this.Dmg5.Size = new Size(25, 22);
			this.Dmg5.Text = "+5";
			this.Dmg5.Click += new EventHandler(this.Dmg5_Click);
			this.Dmg10.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg10.Image = (Image)resources.GetObject("Dmg10.Image");
			this.Dmg10.ImageTransparentColor = Color.Magenta;
			this.Dmg10.Name = "Dmg10";
			this.Dmg10.Size = new Size(31, 22);
			this.Dmg10.Text = "+10";
			this.Dmg10.Click += new EventHandler(this.Dmg10_Click);
			this.Dmg20.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg20.Image = (Image)resources.GetObject("Dmg20.Image");
			this.Dmg20.ImageTransparentColor = Color.Magenta;
			this.Dmg20.Name = "Dmg20";
			this.Dmg20.Size = new Size(31, 22);
			this.Dmg20.Text = "+20";
			this.Dmg20.Click += new EventHandler(this.Dmg20_Click);
			this.Dmg50.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg50.Image = (Image)resources.GetObject("Dmg50.Image");
			this.Dmg50.ImageTransparentColor = Color.Magenta;
			this.Dmg50.Name = "Dmg50";
			this.Dmg50.Size = new Size(31, 22);
			this.Dmg50.Text = "+50";
			this.Dmg50.Click += new EventHandler(this.Dmg50_Click);
			this.Dmg100.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.Dmg100.Image = (Image)resources.GetObject("Dmg100.Image");
			this.Dmg100.ImageTransparentColor = Color.Magenta;
			this.Dmg100.Name = "Dmg100";
			this.Dmg100.Size = new Size(37, 22);
			this.Dmg100.Text = "+100";
			this.Dmg100.Click += new EventHandler(this.Dmg100_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.ResetBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ResetBtn.Image = (Image)resources.GetObject("ResetBtn.Image");
			this.ResetBtn.ImageTransparentColor = Color.Magenta;
			this.ResetBtn.Name = "ResetBtn";
			this.ResetBtn.Size = new Size(39, 22);
			this.ResetBtn.Text = "Reset";
			this.ResetBtn.Click += new EventHandler(this.ResetBtn_Click);
			this.TypeToolbar.Dock = DockStyle.Left;
			this.TypeToolbar.GripStyle = ToolStripGripStyle.Hidden;
			this.TypeToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AcidBtn,
				this.ColdBtn,
				this.FireBtn,
				this.ForceBtn,
				this.LightningBtn,
				this.NecroticBtn,
				this.PoisonBtn,
				this.PsychicBtn,
				this.RadiantBtn,
				this.ThunderBtn
			});
			this.TypeToolbar.Location = new Point(0, 0);
			this.TypeToolbar.Name = "TypeToolbar";
			this.TypeToolbar.Size = new Size(63, 225);
			this.TypeToolbar.TabIndex = 1;
			this.TypeToolbar.Text = "toolStrip2";
			this.AcidBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AcidBtn.Image = (Image)resources.GetObject("AcidBtn.Image");
			this.AcidBtn.ImageTransparentColor = Color.Magenta;
			this.AcidBtn.Name = "AcidBtn";
			this.AcidBtn.Size = new Size(60, 19);
			this.AcidBtn.Text = "Acid";
			this.AcidBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.AcidBtn.Click += new EventHandler(this.AcidBtn_Click);
			this.ColdBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ColdBtn.Image = (Image)resources.GetObject("ColdBtn.Image");
			this.ColdBtn.ImageTransparentColor = Color.Magenta;
			this.ColdBtn.Name = "ColdBtn";
			this.ColdBtn.Size = new Size(60, 19);
			this.ColdBtn.Text = "Cold";
			this.ColdBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.ColdBtn.Click += new EventHandler(this.ColdBtn_Click);
			this.FireBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FireBtn.Image = (Image)resources.GetObject("FireBtn.Image");
			this.FireBtn.ImageTransparentColor = Color.Magenta;
			this.FireBtn.Name = "FireBtn";
			this.FireBtn.Size = new Size(60, 19);
			this.FireBtn.Text = "Fire";
			this.FireBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.FireBtn.Click += new EventHandler(this.FireBtn_Click);
			this.ForceBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ForceBtn.Image = (Image)resources.GetObject("ForceBtn.Image");
			this.ForceBtn.ImageTransparentColor = Color.Magenta;
			this.ForceBtn.Name = "ForceBtn";
			this.ForceBtn.Size = new Size(60, 19);
			this.ForceBtn.Text = "Force";
			this.ForceBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.ForceBtn.Click += new EventHandler(this.ForceBtn_Click);
			this.LightningBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LightningBtn.Image = (Image)resources.GetObject("LightningBtn.Image");
			this.LightningBtn.ImageTransparentColor = Color.Magenta;
			this.LightningBtn.Name = "LightningBtn";
			this.LightningBtn.Size = new Size(60, 19);
			this.LightningBtn.Text = "Lightning";
			this.LightningBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.LightningBtn.Click += new EventHandler(this.LightningBtn_Click);
			this.NecroticBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NecroticBtn.Image = (Image)resources.GetObject("NecroticBtn.Image");
			this.NecroticBtn.ImageTransparentColor = Color.Magenta;
			this.NecroticBtn.Name = "NecroticBtn";
			this.NecroticBtn.Size = new Size(60, 19);
			this.NecroticBtn.Text = "Necrotic";
			this.NecroticBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.NecroticBtn.Click += new EventHandler(this.NecroticBtn_Click);
			this.PoisonBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PoisonBtn.Image = (Image)resources.GetObject("PoisonBtn.Image");
			this.PoisonBtn.ImageTransparentColor = Color.Magenta;
			this.PoisonBtn.Name = "PoisonBtn";
			this.PoisonBtn.Size = new Size(60, 19);
			this.PoisonBtn.Text = "Poison";
			this.PoisonBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.PoisonBtn.Click += new EventHandler(this.PoisonBtn_Click);
			this.PsychicBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PsychicBtn.Image = (Image)resources.GetObject("PsychicBtn.Image");
			this.PsychicBtn.ImageTransparentColor = Color.Magenta;
			this.PsychicBtn.Name = "PsychicBtn";
			this.PsychicBtn.Size = new Size(60, 19);
			this.PsychicBtn.Text = "Psychic";
			this.PsychicBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.PsychicBtn.Click += new EventHandler(this.PsychicBtn_Click);
			this.RadiantBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RadiantBtn.Image = (Image)resources.GetObject("RadiantBtn.Image");
			this.RadiantBtn.ImageTransparentColor = Color.Magenta;
			this.RadiantBtn.Name = "RadiantBtn";
			this.RadiantBtn.Size = new Size(60, 19);
			this.RadiantBtn.Text = "Radiant";
			this.RadiantBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.RadiantBtn.Click += new EventHandler(this.RadiantBtn_Click);
			this.ThunderBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ThunderBtn.Image = (Image)resources.GetObject("ThunderBtn.Image");
			this.ThunderBtn.ImageTransparentColor = Color.Magenta;
			this.ThunderBtn.Name = "ThunderBtn";
			this.ThunderBtn.Size = new Size(60, 19);
			this.ThunderBtn.Text = "Thunder";
			this.ThunderBtn.TextAlign = ContentAlignment.MiddleLeft;
			this.ThunderBtn.Click += new EventHandler(this.ThunderBtn_Click);
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.Location = new Point(134, 54);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.ReadOnly = true;
			this.TypeBox.Size = new Size(194, 20);
			this.TypeBox.TabIndex = 5;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(340, 225);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.DmgBox);
			base.Controls.Add(this.DmgLbl);
			base.Controls.Add(this.ModLbl);
			base.Controls.Add(this.AmountToolbar);
			base.Controls.Add(this.TypeToolbar);
			base.Controls.Add(this.HalveBtn);
			base.Controls.Add(this.TypeLbl);
			base.Controls.Add(this.ModBox);
			base.Controls.Add(this.ValLbl);
			base.Controls.Add(this.ValBox);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DamageForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Damage";
			base.Shown += new EventHandler(this.DamageForm_Shown);
			((ISupportInitialize)this.DmgBox).EndInit();
			this.AmountToolbar.ResumeLayout(false);
			this.AmountToolbar.PerformLayout();
			this.TypeToolbar.ResumeLayout(false);
			this.TypeToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
