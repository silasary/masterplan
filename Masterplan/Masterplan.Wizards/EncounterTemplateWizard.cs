using Masterplan.Data;
using System;
using System.Collections.Generic;
using Utils;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class EncounterTemplateWizard : Wizard
	{
		private AdviceData fData = new AdviceData();

		private Encounter fEncounter;

		public override object Data
		{
			get
			{
				return this.fData;
			}
			set
			{
				this.fData = (value as AdviceData);
			}
		}

		public EncounterTemplateWizard(List<Pair<EncounterTemplateGroup, EncounterTemplate>> templates, Encounter enc, int party_level) : base("Encounter Templates")
		{
			this.fData.Templates = templates;
			this.fData.PartyLevel = party_level;
			this.fEncounter = enc;
			this.fData.TabulaRasa = (this.fEncounter.Count == 0);
		}

		public override void AddPages()
		{
			base.Pages.Add(new EncounterTemplatePage());
			base.Pages.Add(new EncounterSelectionPage());
		}

		public override int NextPageIndex(int current_page)
		{
			return base.NextPageIndex(current_page);
		}

		public override int BackPageIndex(int current_page)
		{
			return base.BackPageIndex(current_page);
		}

		public override void OnFinish()
		{
			foreach (EncounterTemplateSlot current in this.fData.SelectedTemplate.Slots)
			{
				if (this.fData.FilledSlots.ContainsKey(current))
				{
					EncounterSlot encounterSlot = new EncounterSlot();
					encounterSlot.Card = this.fData.FilledSlots[current];
					for (int num = 0; num != current.Count; num++)
					{
						encounterSlot.CombatData.Add(new CombatData());
					}
					this.fEncounter.Slots.Add(encounterSlot);
				}
			}
		}

		public override void OnCancel()
		{
		}
	}
}
