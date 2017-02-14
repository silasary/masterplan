using Masterplan.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Masterplan.UI
{
    partial class PlayerViewForm
    {
        private MapView fParentMap;

        private IContainer components;

        private ToolTip Tooltip;

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
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PlayerViewForm));
            this.Tooltip = new ToolTip(this.components);
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Black;
            base.ClientSize = new Size(534, 357);
            base.Icon = (Icon)resources.GetObject("$this.Icon");
            base.Name = "PlayerViewForm";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Player View";
            base.FormClosed += new FormClosedEventHandler(this.PlayerViewForm_FormClosed);
            base.ResumeLayout(false);
        }
    }
}
