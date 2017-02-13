using Masterplan.Data;
using Masterplan.Tools.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class AutoBuildForm : Form
	{
		public enum Mode
		{
			Encounter,
			Delve,
			Deck
		}

		private const string RANDOM = "Random";

		private AutoBuildData fData;

		private AutoBuildForm.Mode fMode;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private ComboBox TemplateBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Label DiffLbl;

		private ComboBox DiffBox;

		private Label TemplateLbl;

		private Label CatLbl;

		private Button CatBtn;

		private Label KeywordLbl;

		private ComboBox KeywordBox;

		public AutoBuildData Data
		{
			get
			{
				return this.fData;
			}
		}

		public AutoBuildForm(AutoBuildForm.Mode mode)
		{
			this.InitializeComponent();
			this.fData = new AutoBuildData();
			this.fMode = mode;
			this.init_options();
			switch (this.fMode)
			{
			case AutoBuildForm.Mode.Encounter:
			{
				this.TemplateBox.Items.Add("Random");
				List<string> list = EncounterBuilder.FindTemplateNames();
				foreach (string current in list)
				{
					this.TemplateBox.Items.Add(current);
				}
				this.TemplateBox.SelectedItem = ((this.fData.Type != "") ? this.fData.Type : "Random");
				this.DiffBox.Items.Add(Difficulty.Random);
				this.DiffBox.Items.Add(Difficulty.Easy);
				this.DiffBox.Items.Add(Difficulty.Moderate);
				this.DiffBox.Items.Add(Difficulty.Hard);
				this.DiffBox.SelectedItem = this.fData.Difficulty;
				this.LevelBox.Value = this.fData.Level;
				this.update_cats();
				return;
			}
			case AutoBuildForm.Mode.Delve:
				this.TemplateLbl.Enabled = false;
				this.TemplateBox.Enabled = false;
				this.TemplateBox.Items.Add("(not applicable)");
				this.TemplateBox.SelectedIndex = 0;
				this.DiffLbl.Enabled = false;
				this.DiffBox.Enabled = false;
				this.DiffBox.Items.Add("(not applicable)");
				this.DiffBox.SelectedIndex = 0;
				this.LevelBox.Value = this.fData.Level;
				this.update_cats();
				return;
			case AutoBuildForm.Mode.Deck:
				this.TemplateLbl.Enabled = false;
				this.TemplateBox.Enabled = false;
				this.TemplateBox.Items.Add("(not applicable)");
				this.TemplateBox.SelectedIndex = 0;
				this.DiffLbl.Enabled = false;
				this.DiffBox.Enabled = false;
				this.DiffBox.Items.Add("(not applicable)");
				this.DiffBox.SelectedIndex = 0;
				this.LevelBox.Value = this.fData.Level;
				this.update_cats();
				return;
			default:
				return;
			}
		}

		private void init_options()
		{
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			BinarySearchTree<string> binarySearchTree2 = new BinarySearchTree<string>();
			foreach (Creature current in Session.Creatures)
			{
				if (current.Category != null && current.Category != "")
				{
					binarySearchTree.Add(current.Category);
				}
				if (current.Keywords != null && current.Keywords != "")
				{
					string[] array = current.Keywords.Split(new string[]
					{
						",",
						";"
					}, StringSplitOptions.RemoveEmptyEntries);
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text = array2[i];
						binarySearchTree2.Add(text.Trim().ToLower());
					}
				}
			}
			if (binarySearchTree.Count == 0)
			{
				this.CatLbl.Enabled = false;
				this.CatBtn.Enabled = false;
			}
			List<string> sortedList = binarySearchTree2.SortedList;
			foreach (string current2 in sortedList)
			{
				this.KeywordBox.Items.Add(current2);
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			string[] array = this.KeywordBox.Text.ToLower().Split(null);
			this.fData.Keywords.Clear();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (text != "")
				{
					this.fData.Keywords.Add(text);
				}
			}
			switch (this.fMode)
			{
			case AutoBuildForm.Mode.Encounter:
				this.fData.Type = ((this.TemplateBox.Text != "Random") ? this.TemplateBox.Text : "");
				this.fData.Difficulty = (Difficulty)this.DiffBox.SelectedItem;
				this.fData.Level = (int)this.LevelBox.Value;
				return;
			case AutoBuildForm.Mode.Delve:
				this.fData.Type = "";
				this.fData.Difficulty = Difficulty.Random;
				this.fData.Level = (int)this.LevelBox.Value;
				return;
			case AutoBuildForm.Mode.Deck:
				this.fData.Type = "";
				this.fData.Difficulty = Difficulty.Random;
				this.fData.Level = (int)this.LevelBox.Value;
				return;
			default:
				return;
			}
		}

		private void CatBtn_Click(object sender, EventArgs e)
		{
			CategoryListForm categoryListForm = new CategoryListForm(this.fData.Categories);
			if (categoryListForm.ShowDialog() == DialogResult.OK)
			{
				this.fData.Categories = categoryListForm.Categories;
				this.update_cats();
			}
		}

		private void update_cats()
		{
			this.CatBtn.Text = ((this.fData.Categories == null) ? "All Categories" : (this.fData.Categories.Count + " Categories"));
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
			this.TemplateBox = new ComboBox();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.DiffLbl = new Label();
			this.DiffBox = new ComboBox();
			this.TemplateLbl = new Label();
			this.CatLbl = new Label();
			this.CatBtn = new Button();
			this.KeywordLbl = new Label();
			this.KeywordBox = new ComboBox();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(196, 155);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 12;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(277, 155);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 13;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TemplateBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TemplateBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TemplateBox.FormattingEnabled = true;
			this.TemplateBox.Location = new Point(74, 12);
			this.TemplateBox.Name = "TemplateBox";
			this.TemplateBox.Size = new Size(278, 21);
			this.TemplateBox.TabIndex = 1;
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 68);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(36, 13);
			this.LevelLbl.TabIndex = 4;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(74, 66);
			NumericUpDown arg_2B1_0 = this.LevelBox;
			int[] array = new int[4];
			array[0] = 40;
			arg_2B1_0.Maximum = new decimal(array);
			NumericUpDown arg_2CD_0 = this.LevelBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_2CD_0.Minimum = new decimal(array2);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(278, 20);
			this.LevelBox.TabIndex = 5;
			NumericUpDown arg_31C_0 = this.LevelBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_31C_0.Value = new decimal(array3);
			this.DiffLbl.AutoSize = true;
			this.DiffLbl.Location = new Point(12, 42);
			this.DiffLbl.Name = "DiffLbl";
			this.DiffLbl.Size = new Size(50, 13);
			this.DiffLbl.TabIndex = 2;
			this.DiffLbl.Text = "Difficulty:";
			this.DiffBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DiffBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DiffBox.FormattingEnabled = true;
			this.DiffBox.Location = new Point(74, 39);
			this.DiffBox.Name = "DiffBox";
			this.DiffBox.Size = new Size(278, 21);
			this.DiffBox.TabIndex = 3;
			this.TemplateLbl.AutoSize = true;
			this.TemplateLbl.Location = new Point(12, 15);
			this.TemplateLbl.Name = "TemplateLbl";
			this.TemplateLbl.Size = new Size(54, 13);
			this.TemplateLbl.TabIndex = 0;
			this.TemplateLbl.Text = "Template:";
			this.CatLbl.AutoSize = true;
			this.CatLbl.Location = new Point(12, 97);
			this.CatLbl.Name = "CatLbl";
			this.CatLbl.Size = new Size(55, 13);
			this.CatLbl.TabIndex = 6;
			this.CatLbl.Text = "Creatures:";
			this.CatBtn.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.CatBtn.Location = new Point(74, 92);
			this.CatBtn.Name = "CatBtn";
			this.CatBtn.Size = new Size(278, 23);
			this.CatBtn.TabIndex = 7;
			this.CatBtn.Text = "All Categories";
			this.CatBtn.UseVisualStyleBackColor = true;
			this.CatBtn.Click += new EventHandler(this.CatBtn_Click);
			this.KeywordLbl.AutoSize = true;
			this.KeywordLbl.Location = new Point(12, 124);
			this.KeywordLbl.Name = "KeywordLbl";
			this.KeywordLbl.Size = new Size(56, 13);
			this.KeywordLbl.TabIndex = 8;
			this.KeywordLbl.Text = "Keywords:";
			this.KeywordBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.KeywordBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.KeywordBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.KeywordBox.FormattingEnabled = true;
			this.KeywordBox.Location = new Point(74, 121);
			this.KeywordBox.Name = "KeywordBox";
			this.KeywordBox.Size = new Size(278, 21);
			this.KeywordBox.TabIndex = 9;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(364, 190);
			base.Controls.Add(this.KeywordBox);
			base.Controls.Add(this.KeywordLbl);
			base.Controls.Add(this.CatBtn);
			base.Controls.Add(this.CatLbl);
			base.Controls.Add(this.TemplateLbl);
			base.Controls.Add(this.DiffBox);
			base.Controls.Add(this.DiffLbl);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.TemplateBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AutoBuildForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "AutoBuild Options";
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
