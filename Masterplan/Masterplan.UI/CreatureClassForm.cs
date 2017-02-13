using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CreatureClassForm : Form
	{
		private IContainer components;

		private Label OriginLbl;

		private ComboBox OriginBox;

		private Label TypeLbl;

		private ComboBox TypeBox;

		private Label KeywordLbl;

		private TextBox KeywordBox;

		private Button OKBtn;

		private Button CancelBtn;

		private ComboBox SizeBox;

		private Label SizeLbl;

		private ICreature fCreature;

		public CreatureSize CreatureSize
		{
			get
			{
				return (CreatureSize)this.SizeBox.SelectedItem;
			}
		}

		public CreatureOrigin Origin
		{
			get
			{
				return (CreatureOrigin)this.OriginBox.SelectedItem;
			}
		}

		public CreatureType Type
		{
			get
			{
				return (CreatureType)this.TypeBox.SelectedItem;
			}
		}

		public string Keywords
		{
			get
			{
				return this.KeywordBox.Text;
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
			this.OriginLbl = new Label();
			this.OriginBox = new ComboBox();
			this.TypeLbl = new Label();
			this.TypeBox = new ComboBox();
			this.KeywordLbl = new Label();
			this.KeywordBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.SizeBox = new ComboBox();
			this.SizeLbl = new Label();
			base.SuspendLayout();
			this.OriginLbl.AutoSize = true;
			this.OriginLbl.Location = new Point(12, 42);
			this.OriginLbl.Name = "OriginLbl";
			this.OriginLbl.Size = new Size(37, 13);
			this.OriginLbl.TabIndex = 2;
			this.OriginLbl.Text = "Origin:";
			this.OriginBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.OriginBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.OriginBox.FormattingEnabled = true;
			this.OriginBox.Location = new Point(74, 39);
			this.OriginBox.Name = "OriginBox";
			this.OriginBox.Size = new Size(259, 21);
			this.OriginBox.TabIndex = 3;
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(12, 69);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new Size(34, 13);
			this.TypeLbl.TabIndex = 4;
			this.TypeLbl.Text = "Type:";
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(74, 66);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new Size(259, 21);
			this.TypeBox.TabIndex = 5;
			this.KeywordLbl.AutoSize = true;
			this.KeywordLbl.Location = new Point(12, 96);
			this.KeywordLbl.Name = "KeywordLbl";
			this.KeywordLbl.Size = new Size(56, 13);
			this.KeywordLbl.TabIndex = 6;
			this.KeywordLbl.Text = "Keywords:";
			this.KeywordBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.KeywordBox.Location = new Point(74, 93);
			this.KeywordBox.Name = "KeywordBox";
			this.KeywordBox.Size = new Size(259, 20);
			this.KeywordBox.TabIndex = 7;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(177, 127);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 8;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(258, 127);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 9;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.SizeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SizeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.SizeBox.Location = new Point(74, 12);
			this.SizeBox.Name = "SizeBox";
			this.SizeBox.Size = new Size(259, 21);
			this.SizeBox.TabIndex = 1;
			this.SizeLbl.AutoSize = true;
			this.SizeLbl.Location = new Point(12, 15);
			this.SizeLbl.Name = "SizeLbl";
			this.SizeLbl.Size = new Size(30, 13);
			this.SizeLbl.TabIndex = 0;
			this.SizeLbl.Text = "Size:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(345, 162);
			base.Controls.Add(this.SizeBox);
			base.Controls.Add(this.SizeLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.KeywordBox);
			base.Controls.Add(this.KeywordLbl);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.TypeLbl);
			base.Controls.Add(this.OriginBox);
			base.Controls.Add(this.OriginLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureClassForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Information";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public CreatureClassForm(ICreature c)
		{
			this.InitializeComponent();
			foreach (CreatureSize creatureSize in Enum.GetValues(typeof(CreatureSize)))
			{
				this.SizeBox.Items.Add(creatureSize);
			}
			Array values = Enum.GetValues(typeof(CreatureOrigin));
			foreach (CreatureOrigin creatureOrigin in values)
			{
				this.OriginBox.Items.Add(creatureOrigin);
			}
			Array values2 = Enum.GetValues(typeof(CreatureType));
			foreach (CreatureType creatureType in values2)
			{
				this.TypeBox.Items.Add(creatureType);
			}
			this.fCreature = c;
			this.SizeBox.SelectedItem = this.fCreature.Size;
			this.OriginBox.SelectedItem = this.fCreature.Origin;
			this.TypeBox.SelectedItem = this.fCreature.Type;
			this.KeywordBox.Text = this.fCreature.Keywords;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}
	}
}
