using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class MapLocationSelectForm : Form
	{
		private IContainer components;

		private Label MapLbl;

		private ComboBox MapBox;

		private Label LocationLbl;

		private ComboBox LocationBox;

		private Button OKBtn;

		private Button CancelBtn;

		private RegionalMapPanel MapPanel;

		public RegionalMap Map
		{
			get
			{
				return this.MapBox.SelectedItem as RegionalMap;
			}
		}

		public MapLocation MapLocation
		{
			get
			{
				return this.LocationBox.SelectedItem as MapLocation;
			}
		}

		public MapLocationSelectForm(Guid map_id, Guid map_location_id)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.MapBox.Items.Add("(no map)");
			foreach (RegionalMap current in Session.Project.RegionalMaps)
			{
				this.MapBox.Items.Add(current);
			}
			RegionalMap regionalMap = Session.Project.FindRegionalMap(map_id);
			if (regionalMap == null)
			{
				this.MapBox.SelectedIndex = 0;
				this.LocationBox.Items.Add("(no map)");
				this.LocationBox.SelectedIndex = 0;
				return;
			}
			this.MapBox.SelectedItem = regionalMap;
			MapLocation mapLocation = regionalMap.FindLocation(map_location_id);
			if (mapLocation != null)
			{
				this.LocationBox.SelectedItem = mapLocation;
				return;
			}
			this.LocationBox.SelectedIndex = 0;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.MapLbl.Enabled = (Session.Project.RegionalMaps.Count != 0);
			this.MapBox.Enabled = (Session.Project.RegionalMaps.Count != 0);
			RegionalMap regionalMap = this.MapBox.SelectedItem as RegionalMap;
			bool enabled = regionalMap != null && regionalMap.Locations.Count != 0;
			this.LocationLbl.Enabled = enabled;
			this.LocationBox.Enabled = enabled;
			this.OKBtn.Enabled = (this.MapLocation != null);
		}

		private void MapBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.LocationBox.Items.Clear();
			RegionalMap regionalMap = this.MapBox.SelectedItem as RegionalMap;
			if (regionalMap != null)
			{
				this.LocationBox.Items.Add("(entire map)");
				foreach (MapLocation current in regionalMap.Locations)
				{
					this.LocationBox.Items.Add(current);
				}
				this.LocationBox.SelectedIndex = 0;
			}
			this.show_map();
		}

		private void AreaBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.show_map();
		}

		private void show_map()
		{
			RegionalMap regionalMap = this.MapBox.SelectedItem as RegionalMap;
			if (regionalMap != null)
			{
				this.MapPanel.Map = regionalMap;
				MapLocation highlightedLocation = this.LocationBox.SelectedItem as MapLocation;
				this.MapPanel.HighlightedLocation = highlightedLocation;
				return;
			}
			this.MapPanel.Map = null;
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
			this.MapLbl = new Label();
			this.MapBox = new ComboBox();
			this.LocationLbl = new Label();
			this.LocationBox = new ComboBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.MapPanel = new RegionalMapPanel();
			base.SuspendLayout();
			this.MapLbl.AutoSize = true;
			this.MapLbl.Location = new Point(12, 15);
			this.MapLbl.Name = "MapLbl";
			this.MapLbl.Size = new Size(31, 13);
			this.MapLbl.TabIndex = 0;
			this.MapLbl.Text = "Map:";
			this.MapBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MapBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.MapBox.FormattingEnabled = true;
			this.MapBox.Location = new Point(69, 12);
			this.MapBox.Name = "MapBox";
			this.MapBox.Size = new Size(478, 21);
			this.MapBox.Sorted = true;
			this.MapBox.TabIndex = 1;
			this.MapBox.SelectedIndexChanged += new EventHandler(this.MapBox_SelectedIndexChanged);
			this.LocationLbl.AutoSize = true;
			this.LocationLbl.Location = new Point(12, 42);
			this.LocationLbl.Name = "LocationLbl";
			this.LocationLbl.Size = new Size(51, 13);
			this.LocationLbl.TabIndex = 2;
			this.LocationLbl.Text = "Location:";
			this.LocationBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LocationBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LocationBox.FormattingEnabled = true;
			this.LocationBox.Location = new Point(69, 39);
			this.LocationBox.Name = "LocationBox";
			this.LocationBox.Size = new Size(478, 21);
			this.LocationBox.Sorted = true;
			this.LocationBox.TabIndex = 3;
			this.LocationBox.SelectedIndexChanged += new EventHandler(this.AreaBox_SelectedIndexChanged);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(391, 357);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(472, 357);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.MapPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.MapPanel.BorderStyle = BorderStyle.FixedSingle;
			this.MapPanel.HighlightedLocation = null;
			this.MapPanel.Location = new Point(12, 66);
			this.MapPanel.Map = null;
			this.MapPanel.Mode = MapViewMode.Plain;
			this.MapPanel.Name = "MapPanel";
			this.MapPanel.SelectedLocation = null;
			this.MapPanel.ShowLocations = true;
			this.MapPanel.Size = new Size(535, 285);
			this.MapPanel.TabIndex = 4;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(559, 392);
			base.Controls.Add(this.MapPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.LocationBox);
			base.Controls.Add(this.LocationLbl);
			base.Controls.Add(this.MapBox);
			base.Controls.Add(this.MapLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MapLocationSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Location";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
