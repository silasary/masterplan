using System;
using System.Drawing;
using System.Windows.Forms;

using Utils;

using Masterplan.Data;
using Masterplan.Tools;

namespace Masterplan.UI
{
	partial class EncounterReportListForm : Form
	{
		public EncounterReportListForm()
		{
			InitializeComponent();

			Application.Idle += new EventHandler(Application_Idle);

			update_list();
			set_map();
		}

		public Pair<Encounter, EncounterLog> SelectedReport
		{
			get
			{
				if (EncounterList.SelectedItems.Count != 0)
					return EncounterList.SelectedItems[0].Tag as Pair<Encounter, EncounterLog>;

				return null;
			}
		}

		void Application_Idle(object sender, EventArgs e)
		{
			ReportBtn.Enabled = (SelectedReport != null);
			RemoveBtn.Enabled = (SelectedReport != null);
		}

		private void RunBtn_Click(object sender, EventArgs e)
		{
			if (SelectedReport != null)
			{
				EncounterReportForm dlg = new EncounterReportForm(SelectedReport.Second, SelectedReport.First);
				dlg.ShowDialog();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (SelectedReport != null)
			{
				Session.Project.EncounterReports.Remove(SelectedReport);
				Session.Modified = true;

				update_list();
				set_map();
			}
		}

		private void EncounterList_SelectedIndexChanged(object sender, EventArgs e)
		{
			set_map();
		}

		void update_list()
		{
			EncounterList.Items.Clear();

			foreach (var report in Session.Project.EncounterReports)
			{
				if (report.Second.Entries.Count == 0)
					continue;

				DateTime timestamp = report.Second.Entries[0].Timestamp;
				string str = timestamp.ToShortTimeString() + " " + timestamp.ToShortDateString();

				ListViewItem lvi = EncounterList.Items.Add(str);
				lvi.Tag = report;
			}

			if (Session.Project.EncounterReports.Count == 0)
			{
				ListViewItem lvi = EncounterList.Items.Add("(none)");
				lvi.ForeColor = SystemColors.GrayText;
			}
		}

		void set_map()
		{
			if (SelectedReport != null)
				ReportBrowser.DocumentText = HTML.EncounterLog(SelectedReport.Second, SelectedReport.First, DisplaySize.Small);
			else
				ReportBrowser.DocumentText = HTML.EncounterLog(null, null, DisplaySize.Small);
		}
	}
}
