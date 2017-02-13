using Masterplan.Data;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Wizards
{
	internal class AdviceData
	{
		public bool TabulaRasa = true;

		public int PartyLevel = Session.Project.Party.Level;

		public List<Pair<EncounterTemplateGroup, EncounterTemplate>> Templates = new List<Pair<EncounterTemplateGroup, EncounterTemplate>>();

		public EncounterTemplate SelectedTemplate;

		public Dictionary<EncounterTemplateSlot, EncounterCard> FilledSlots = new Dictionary<EncounterTemplateSlot, EncounterCard>();
	}
}
