using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Utils.Wizards
{
    partial class WizardForm
    {
        private IContainer components;

        private Button BackBtn;

        private Button NextBtn;

        private Button CancelBtn;

        private Panel ContentPnl;

        private Button FinishBtn;

        private PictureBox ImageBox;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(WizardForm));
            this.BackBtn = new Button();
            this.NextBtn = new Button();
            this.CancelBtn = new Button();
            this.ContentPnl = new Panel();
            this.FinishBtn = new Button();
            this.ImageBox = new PictureBox();
            ((ISupportInitialize)this.ImageBox).BeginInit();
            base.SuspendLayout();
            this.BackBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.BackBtn.Location = new Point(148, 277);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new Size(75, 23);
            this.BackBtn.TabIndex = 1;
            this.BackBtn.Text = "< Back";
            this.BackBtn.UseVisualStyleBackColor = true;
            this.BackBtn.Click += new EventHandler(this.BackBtn_Click);
            this.NextBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.NextBtn.Location = new Point(229, 277);
            this.NextBtn.Name = "NextBtn";
            this.NextBtn.Size = new Size(75, 23);
            this.NextBtn.TabIndex = 2;
            this.NextBtn.Text = "Next >";
            this.NextBtn.UseVisualStyleBackColor = true;
            this.NextBtn.Click += new EventHandler(this.NextBtn_Click);
            this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.CancelBtn.DialogResult = DialogResult.Cancel;
            this.CancelBtn.Location = new Point(391, 277);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new Size(75, 23);
            this.CancelBtn.TabIndex = 4;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new EventHandler(this.CancelBtn_Click);
            this.ContentPnl.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.ContentPnl.Location = new Point(149, 12);
            this.ContentPnl.Name = "ContentPnl";
            this.ContentPnl.Size = new Size(317, 259);
            this.ContentPnl.TabIndex = 0;
            this.FinishBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.FinishBtn.DialogResult = DialogResult.OK;
            this.FinishBtn.Location = new Point(310, 277);
            this.FinishBtn.Name = "FinishBtn";
            this.FinishBtn.Size = new Size(75, 23);
            this.FinishBtn.TabIndex = 3;
            this.FinishBtn.Text = "Finish";
            this.FinishBtn.UseVisualStyleBackColor = true;
            this.FinishBtn.Click += new EventHandler(this.FinishBtn_Click);
            this.ImageBox.Image = (Image)resources.GetObject("ImageBox.Image");
            this.ImageBox.Location = new Point(12, 12);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new Size(131, 259);
            this.ImageBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.ImageBox.TabIndex = 13;
            this.ImageBox.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.CancelBtn;
            base.ClientSize = new Size(478, 312);
            base.Controls.Add(this.ImageBox);
            base.Controls.Add(this.FinishBtn);
            base.Controls.Add(this.ContentPnl);
            base.Controls.Add(this.CancelBtn);
            base.Controls.Add(this.NextBtn);
            base.Controls.Add(this.BackBtn);
            this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "WizardForm";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.SizeGripStyle = SizeGripStyle.Hide;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Wizard";
            ((ISupportInitialize)this.ImageBox).EndInit();
            base.ResumeLayout(false);
        }

    }
}
