using Masterplan.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Masterplan.UI
{
    partial class EncounterReportForm
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(EncounterReportForm));
            this.Browser = new WebBrowser();
            this.Toolbar = new ToolStrip();
            this.ReportBtn = new ToolStripDropDownButton();
            this.ReportTime = new ToolStripMenuItem();
            this.ReportDamageEnemies = new ToolStripMenuItem();
            this.ReportDamageAllies = new ToolStripMenuItem();
            this.ReportMovement = new ToolStripMenuItem();
            this.BreakdownBtn = new ToolStripDropDownButton();
            this.BreakdownIndividually = new ToolStripMenuItem();
            this.BreakdownByController = new ToolStripMenuItem();
            this.BreakdownByFaction = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.PlayerViewBtn = new ToolStripButton();
            this.ExportBtn = new ToolStripButton();
            this.Splitter = new SplitContainer();
            this.Graph = new DemographicsPanel();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.MVPLbl = new ToolStripLabel();
            this.Toolbar.SuspendLayout();
            this.Splitter.Panel1.SuspendLayout();
            this.Splitter.Panel2.SuspendLayout();
            this.Splitter.SuspendLayout();
            base.SuspendLayout();
            this.Browser.Dock = DockStyle.Fill;
            this.Browser.IsWebBrowserContextMenuEnabled = false;
            this.Browser.Location = new Point(0, 0);
            this.Browser.Name = "Browser";
            this.Browser.ScriptErrorsSuppressed = true;
            this.Browser.Size = new Size(404, 266);
            this.Browser.TabIndex = 2;
            this.Browser.WebBrowserShortcutsEnabled = false;
            this.Toolbar.Items.AddRange(new ToolStripItem[]
            {
                this.ReportBtn,
                this.BreakdownBtn,
                this.toolStripSeparator1,
                this.PlayerViewBtn,
                this.ExportBtn,
                this.toolStripSeparator2,
                this.MVPLbl
            });
            this.Toolbar.Location = new Point(0, 0);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Size = new Size(811, 25);
            this.Toolbar.TabIndex = 3;
            this.Toolbar.Text = "toolStrip1";
            this.ReportBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.ReportBtn.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.ReportTime,
                this.ReportDamageEnemies,
                this.ReportDamageAllies,
                this.ReportMovement
            });
            this.ReportBtn.Image = (Image)resources.GetObject("ReportBtn.Image");
            this.ReportBtn.ImageTransparentColor = Color.Magenta;
            this.ReportBtn.Name = "ReportBtn";
            this.ReportBtn.Size = new Size(84, 22);
            this.ReportBtn.Text = "Report Type";
            this.ReportTime.Name = "ReportTime";
            this.ReportTime.Size = new Size(218, 22);
            this.ReportTime.Text = "Time Taken";
            this.ReportTime.Click += new EventHandler(this.ReportTime_Click);
            this.ReportDamageEnemies.Name = "ReportDamageEnemies";
            this.ReportDamageEnemies.Size = new Size(218, 22);
            this.ReportDamageEnemies.Text = "Damage Done (to enemies)";
            this.ReportDamageEnemies.Click += new EventHandler(this.ReportDamageEnemies_Click);
            this.ReportDamageAllies.Name = "ReportDamageAllies";
            this.ReportDamageAllies.Size = new Size(218, 22);
            this.ReportDamageAllies.Text = "Damage Done (to allies)";
            this.ReportDamageAllies.Click += new EventHandler(this.ReportDamageAllies_Click);
            this.ReportMovement.Name = "ReportMovement";
            this.ReportMovement.Size = new Size(218, 22);
            this.ReportMovement.Text = "Movement";
            this.ReportMovement.Click += new EventHandler(this.ReportMovement_Click);
            this.BreakdownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.BreakdownBtn.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.BreakdownIndividually,
                this.BreakdownByController,
                this.BreakdownByFaction
            });
            this.BreakdownBtn.Image = (Image)resources.GetObject("BreakdownBtn.Image");
            this.BreakdownBtn.ImageTransparentColor = Color.Magenta;
            this.BreakdownBtn.Name = "BreakdownBtn";
            this.BreakdownBtn.Size = new Size(70, 22);
            this.BreakdownBtn.Text = "Grouping";
            this.BreakdownIndividually.Name = "BreakdownIndividually";
            this.BreakdownIndividually.Size = new Size(183, 22);
            this.BreakdownIndividually.Text = "Individually (default)";
            this.BreakdownIndividually.Click += new EventHandler(this.BreakdownIndividually_Click);
            this.BreakdownByController.Name = "BreakdownByController";
            this.BreakdownByController.Size = new Size(183, 22);
            this.BreakdownByController.Text = "By Controller";
            this.BreakdownByController.Click += new EventHandler(this.BreakdownByController_Click);
            this.BreakdownByFaction.Name = "BreakdownByFaction";
            this.BreakdownByFaction.Size = new Size(183, 22);
            this.BreakdownByFaction.Text = "By Faction";
            this.BreakdownByFaction.Click += new EventHandler(this.BreakdownByFaction_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 25);
            this.PlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.PlayerViewBtn.Image = (Image)resources.GetObject("PlayerViewBtn.Image");
            this.PlayerViewBtn.ImageTransparentColor = Color.Magenta;
            this.PlayerViewBtn.Name = "PlayerViewBtn";
            this.PlayerViewBtn.Size = new Size(114, 22);
            this.PlayerViewBtn.Text = "Send to Player View";
            this.PlayerViewBtn.Click += new EventHandler(this.PlayerViewBtn_Click);
            this.ExportBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.ExportBtn.Image = (Image)resources.GetObject("ExportBtn.Image");
            this.ExportBtn.ImageTransparentColor = Color.Magenta;
            this.ExportBtn.Name = "ExportBtn";
            this.ExportBtn.Size = new Size(44, 22);
            this.ExportBtn.Text = "Export";
            this.ExportBtn.Click += new EventHandler(this.ExportBtn_Click);
            this.Splitter.Dock = DockStyle.Fill;
            this.Splitter.Location = new Point(0, 25);
            this.Splitter.Name = "Splitter";
            this.Splitter.Panel1.Controls.Add(this.Browser);
            this.Splitter.Panel2.Controls.Add(this.Graph);
            this.Splitter.Size = new Size(811, 266);
            this.Splitter.SplitterDistance = 404;
            this.Splitter.TabIndex = 4;
            this.Graph.Dock = DockStyle.Fill;
            this.Graph.Library = null;
            this.Graph.Location = new Point(0, 0);
            this.Graph.Mode = DemographicsMode.Level;
            this.Graph.Name = "Graph";
            this.Graph.Size = new Size(403, 266);
            this.Graph.Source = DemographicsSource.Creatures;
            this.Graph.TabIndex = 0;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 25);
            this.MVPLbl.Name = "MVPLbl";
            this.MVPLbl.Size = new Size(39, 22);
            this.MVPLbl.Text = "[mvp]";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(811, 291);
            base.Controls.Add(this.Splitter);
            base.Controls.Add(this.Toolbar);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "EncounterReportForm";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.SizeGripStyle = SizeGripStyle.Hide;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Encounter Report";
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            this.Splitter.Panel1.ResumeLayout(false);
            this.Splitter.Panel2.ResumeLayout(false);
            this.Splitter.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }


        #endregion

    }
}

