using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Masterplan.UI
{
   	internal partial class EncounterReportForm : Form
	{
		private EncounterReport fReport;

		private Encounter fEncounter;

		private ReportType fReportType;

		private BreakdownType fBreakdownType;

		private WebBrowser Browser;

		private ToolStrip Toolbar;

		private ToolStripButton ExportBtn;

		private ToolStripDropDownButton ReportBtn;

		private ToolStripDropDownButton BreakdownBtn;

		private ToolStripMenuItem ReportTime;

		private ToolStripMenuItem ReportDamageEnemies;

		private ToolStripMenuItem ReportDamageAllies;

		private ToolStripMenuItem ReportMovement;

		private ToolStripMenuItem BreakdownIndividually;

		private ToolStripMenuItem BreakdownByController;

		private ToolStripMenuItem BreakdownByFaction;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton PlayerViewBtn;

		private SplitContainer Splitter;

		private DemographicsPanel Graph;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripLabel MVPLbl;

		public EncounterReportForm(EncounterLog log, Encounter enc)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fReport = log.CreateReport(enc, true);
			this.fEncounter = enc;
			if (this.fEncounter.MapID == Guid.Empty)
			{
				this.ReportBtn.DropDownItems.Remove(this.ReportMovement);
			}
			this.update_report();
			this.update_mvp();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ReportTime.Checked = (this.fReportType == ReportType.Time);
			this.ReportDamageEnemies.Checked = (this.fReportType == ReportType.DamageToEnemies);
			this.ReportDamageAllies.Checked = (this.fReportType == ReportType.DamageToAllies);
			this.ReportMovement.Checked = (this.fReportType == ReportType.Movement);
			this.BreakdownIndividually.Checked = (this.fBreakdownType == BreakdownType.Individual);
			this.BreakdownByController.Checked = (this.fBreakdownType == BreakdownType.Controller);
			this.BreakdownByFaction.Checked = (this.fBreakdownType == BreakdownType.Faction);
		}

		private void ReportTime_Click(object sender, EventArgs e)
		{
			this.fReportType = ReportType.Time;
			this.update_report();
		}

		private void ReportDamageEnemies_Click(object sender, EventArgs e)
		{
			this.fReportType = ReportType.DamageToEnemies;
			this.update_report();
		}

		private void ReportDamageAllies_Click(object sender, EventArgs e)
		{
			this.fReportType = ReportType.DamageToAllies;
			this.update_report();
		}

		private void ReportMovement_Click(object sender, EventArgs e)
		{
			this.fReportType = ReportType.Movement;
			this.update_report();
		}

		private void BreakdownIndividually_Click(object sender, EventArgs e)
		{
			this.fBreakdownType = BreakdownType.Individual;
			this.update_report();
		}

		private void BreakdownByController_Click(object sender, EventArgs e)
		{
			this.fBreakdownType = BreakdownType.Controller;
			this.update_report();
		}

		private void BreakdownByFaction_Click(object sender, EventArgs e)
		{
			this.fBreakdownType = BreakdownType.Faction;
			this.update_report();
		}

		private void ExportBtn_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = "Encounter Report";
			saveFileDialog.Filter = Program.HTMLFilter;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				File.WriteAllText(saveFileDialog.FileName, this.Browser.DocumentText);
			}
		}

		private void update_report()
		{
			ReportTable table = this.fReport.CreateTable(this.fReportType, this.fBreakdownType, this.fEncounter);
			this.Browser.DocumentText = HTML.EncounterReportTable(table, DisplaySize.Small);
			this.Graph.ShowTable(table);
		}

		private void update_mvp()
		{
			List<Guid> list = this.fReport.MVPs(this.fEncounter);
			string text = "";
			foreach (Guid current in list)
			{
				Hero hero = Session.Project.FindHero(current);
				if (hero != null)
				{
					if (text != "")
					{
						text += ", ";
					}
					text += hero.Name;
				}
			}
			if (text != "")
			{
				this.MVPLbl.Text = "MVP: " + text;
				return;
			}
			this.MVPLbl.Text = "(no MVP for this encounter)";
			this.MVPLbl.Enabled = false;
		}

		private void PlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			ReportTable table = this.fReport.CreateTable(this.fReportType, this.fBreakdownType, this.fEncounter);
			Session.PlayerView.ShowEncounterReportTable(table);
		}

	}
}
