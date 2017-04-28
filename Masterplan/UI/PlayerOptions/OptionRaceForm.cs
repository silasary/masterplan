﻿using System;
using System.Drawing;
using System.Windows.Forms;

using Masterplan.Data;

namespace Masterplan.UI
{
	partial class OptionRaceForm : Form
	{
		public OptionRaceForm(Race race)
		{
			InitializeComponent();

			Array sizes = Enum.GetValues(typeof(CreatureSize));
			foreach (CreatureSize size in sizes)
				SizeBox.Items.Add(size);

			StaticEventDispatcher.ApplicationIdle += new EventHandler(Application_Idle);

			fRace = race.Copy();

			NameBox.Text = fRace.Name;

			HeightBox.Text = fRace.HeightRange;
			WeightBox.Text = fRace.WeightRange;
			AbilityScoreBox.Text = fRace.AbilityScores;
			SizeBox.SelectedItem = fRace.Size;
			SpeedBox.Text = fRace.Speed;
			VisionBox.Text = fRace.Vision;
			LanguageBox.Text = fRace.Languages;
			SkillBonusBox.Text = fRace.SkillBonuses;

			DetailsBox.Text = fRace.Details;
			QuoteBox.Text = fRace.Quote;

			update_features();
			update_powers();
		}

		void Application_Idle(object sender, EventArgs e)
		{
			FeatureRemoveBtn.Enabled = (SelectedFeature != null);
			FeatureEditBtn.Enabled = (SelectedFeature != null);

			PowerRemoveBtn.Enabled = (SelectedPower != null);
			PowerEditBtn.Enabled = (SelectedPower != null);
		}

		public Race Race
		{
			get { return fRace; }
		}
		Race fRace = null;

		private void OKBtn_Click(object sender, EventArgs e)
		{
			fRace.Name = NameBox.Text;
			fRace.HeightRange = HeightBox.Text;
			fRace.WeightRange = WeightBox.Text;
			fRace.AbilityScores = AbilityScoreBox.Text;
			fRace.Size = (CreatureSize)SizeBox.SelectedItem;
			fRace.Speed = SpeedBox.Text;
			fRace.Vision = VisionBox.Text;
			fRace.Languages = LanguageBox.Text;
			fRace.SkillBonuses = SkillBonusBox.Text;
			fRace.Details = DetailsBox.Text;
			fRace.Quote = QuoteBox.Text;
		}

		public Feature SelectedFeature
		{
			get
			{
				if (FeatureList.SelectedItems.Count != 0)
					return FeatureList.SelectedItems[0].Tag as Feature;

				return null;
			}
		}

		private void FeatureAddBtn_Click(object sender, EventArgs e)
		{
			Feature ft = new Feature();
			ft.Name = "New Feature";

			OptionFeatureForm dlg = new OptionFeatureForm(ft);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				fRace.Features.Add(dlg.Feature);
				update_features();
			}
		}

		private void FeatureRemoveBtn_Click(object sender, EventArgs e)
		{
			if (SelectedFeature != null)
			{
				fRace.Features.Remove(SelectedFeature);
				update_features();
			}
		}

		private void FeatureEditBtn_Click(object sender, EventArgs e)
		{
			if (SelectedFeature != null)
			{
				int index = fRace.Features.IndexOf(SelectedFeature);

				OptionFeatureForm dlg = new OptionFeatureForm(SelectedFeature);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					fRace.Features[index] = dlg.Feature;
					update_features();
				}
			}
		}

		void update_features()
		{
			FeatureList.Items.Clear();
			foreach (Feature ft in fRace.Features)
			{
				ListViewItem lvi = FeatureList.Items.Add(ft.Name);
				lvi.Tag = ft;
			}

			if (fRace.Features.Count == 0)
			{
				ListViewItem lvi = FeatureList.Items.Add("(none)");
				lvi.ForeColor = SystemColors.GrayText;
			}
		}

		public PlayerPower SelectedPower
		{
			get
			{
				if (PowerList.SelectedItems.Count != 0)
					return PowerList.SelectedItems[0].Tag as PlayerPower;

				return null;
			}
		}

		private void PowerAddBtn_Click(object sender, EventArgs e)
		{
			PlayerPower power = new PlayerPower();
			power.Name = "New Power";

			OptionPowerForm dlg = new OptionPowerForm(power);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				fRace.Powers.Add(dlg.Power);
				update_powers();
			}
		}

		private void PowerRemoveBtn_Click(object sender, EventArgs e)
		{
			if (SelectedPower != null)
			{
				fRace.Powers.Remove(SelectedPower);
				update_powers();
			}
		}

		private void PowerEditBtn_Click(object sender, EventArgs e)
		{
			if (SelectedPower != null)
			{
				int index = fRace.Powers.IndexOf(SelectedPower);

				OptionPowerForm dlg = new OptionPowerForm(SelectedPower);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					fRace.Powers[index] = dlg.Power;
					update_powers();
				}
			}
		}

		void update_powers()
		{
			PowerList.Items.Clear();
			foreach (PlayerPower power in fRace.Powers)
			{
				ListViewItem lvi = PowerList.Items.Add(power.Name);
				lvi.Tag = power;
			}

			if (fRace.Powers.Count == 0)
			{
				ListViewItem lvi = PowerList.Items.Add("(none)");
				lvi.ForeColor = SystemColors.GrayText;
			}
		}
	}
}
