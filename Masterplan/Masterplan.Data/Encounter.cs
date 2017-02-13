using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Encounter : IElement
	{
		private List<EncounterSlot> fSlots = new List<EncounterSlot>();

		private List<Trap> fTraps = new List<Trap>();

		private List<SkillChallenge> fSkillChallenges = new List<SkillChallenge>();

		private List<CustomToken> fCustomTokens = new List<CustomToken>();

		private Guid fMapID = Guid.Empty;

		private Guid fMapAreaID = Guid.Empty;

		private List<EncounterNote> fNotes = new List<EncounterNote>();

		private List<EncounterWave> fWaves = new List<EncounterWave>();

		public List<EncounterSlot> Slots
		{
			get
			{
				return this.fSlots;
			}
			set
			{
				this.fSlots = value;
			}
		}

		public List<Trap> Traps
		{
			get
			{
				return this.fTraps;
			}
			set
			{
				this.fTraps = value;
			}
		}

		public List<SkillChallenge> SkillChallenges
		{
			get
			{
				return this.fSkillChallenges;
			}
			set
			{
				this.fSkillChallenges = value;
			}
		}

		public List<CustomToken> CustomTokens
		{
			get
			{
				return this.fCustomTokens;
			}
			set
			{
				this.fCustomTokens = value;
			}
		}

		public Guid MapID
		{
			get
			{
				return this.fMapID;
			}
			set
			{
				this.fMapID = value;
			}
		}

		public Guid MapAreaID
		{
			get
			{
				return this.fMapAreaID;
			}
			set
			{
				this.fMapAreaID = value;
			}
		}

		public List<EncounterNote> Notes
		{
			get
			{
				return this.fNotes;
			}
			set
			{
				this.fNotes = value;
			}
		}

		public List<EncounterWave> Waves
		{
			get
			{
				return this.fWaves;
			}
			set
			{
				this.fWaves = value;
			}
		}

		public int Count
		{
			get
			{
				int num = 0;
				foreach (EncounterSlot current in this.AllSlots)
				{
					num += current.CombatData.Count;
				}
				return num;
			}
		}

		public List<EncounterSlot> AllSlots
		{
			get
			{
				List<EncounterSlot> list = new List<EncounterSlot>();
				list.AddRange(this.fSlots);
				if (this.fWaves != null)
				{
					foreach (EncounterWave current in this.fWaves)
					{
						list.AddRange(current.Slots);
					}
				}
				return list;
			}
		}

		public EncounterSlot FindSlot(Guid slot_id)
		{
			foreach (EncounterSlot current in this.AllSlots)
			{
				if (current.ID == slot_id)
				{
					return current;
				}
			}
			return null;
		}

		public EncounterSlot FindSlot(CombatData data)
		{
			foreach (EncounterSlot current in this.AllSlots)
			{
				if (current.CombatData.Contains(data))
				{
					return current;
				}
			}
			return null;
		}

		public EncounterWave FindWave(EncounterSlot slot)
		{
			foreach (EncounterWave current in this.fWaves)
			{
				if (current.Slots.Contains(slot))
				{
					return current;
				}
			}
			return null;
		}

		public CombatData FindCombatData(Guid id)
		{
			foreach (EncounterSlot current in this.AllSlots)
			{
				foreach (CombatData current2 in current.CombatData)
				{
					if (current2.ID == id)
					{
						return current2;
					}
				}
			}
			return null;
		}

		public Trap FindTrap(Guid trap_id)
		{
			foreach (Trap current in this.fTraps)
			{
				if (current.ID == trap_id)
				{
					return current;
				}
			}
			return null;
		}

		public SkillChallenge FindSkillChallenge(Guid challenge_id)
		{
			foreach (SkillChallenge current in this.fSkillChallenges)
			{
				if (current.ID == challenge_id)
				{
					return current;
				}
			}
			return null;
		}

		public EncounterNote FindNote(string note_title)
		{
			foreach (EncounterNote current in this.fNotes)
			{
				if (current.Title == note_title)
				{
					return current;
				}
			}
			return null;
		}

		public bool Contains(Guid combatant_id)
		{
			foreach (EncounterSlot current in this.AllSlots)
			{
				if (current.Card.CreatureID == combatant_id)
				{
					return true;
				}
			}
			return false;
		}

		public string WhoIs(Guid id)
		{
			foreach (EncounterSlot current in this.AllSlots)
			{
				foreach (CombatData current2 in current.CombatData)
				{
					if (current2.ID == id)
					{
						string result = current2.DisplayName;
						return result;
					}
				}
			}
			foreach (Hero current3 in Session.Project.Heroes)
			{
				if (current3.ID == id)
				{
					string result = current3.Name;
					return result;
				}
			}
			foreach (Trap current4 in this.fTraps)
			{
				if (current4.ID == id)
				{
					string result = current4.Name;
					return result;
				}
			}
			return "";
		}

		public int GetXP()
		{
			int num = 0;
			foreach (EncounterSlot current in this.AllSlots)
			{
				num += current.XP;
			}
			foreach (Trap current2 in this.fTraps)
			{
				num += current2.XP;
			}
			foreach (SkillChallenge current3 in this.fSkillChallenges)
			{
				num += current3.GetXP();
			}
			num = Math.Max(0, num);
			return num;
		}

		public int GetLevel(int party_size)
		{
			if (party_size == 0)
			{
				return -1;
			}
			int num = this.GetXP();
			if (Session.Project != null)
			{
				num = (int)((double)num / Session.Project.CampaignSettings.XP);
			}
			num /= party_size;
			int result = 0;
			int num2 = 2147483647;
			for (int i = 0; i <= 40; i++)
			{
				int creatureXP = Experience.GetCreatureXP(i);
				int num3 = Math.Abs(num - creatureXP);
				if (num3 < num2)
				{
					result = i;
					num2 = num3;
				}
			}
			return result;
		}

		public Difficulty GetDifficulty(int party_level, int party_size)
		{
			List<Difficulty> list = new List<Difficulty>();
			foreach (EncounterSlot current in this.AllSlots)
			{
				if (current.Type == EncounterSlotType.Opponent)
				{
					ICreature creature = Session.FindCreature(current.Card.CreatureID, SearchType.Global);
					if (creature != null)
					{
						list.Add(AI.GetThreatDifficulty(creature.Level + current.Card.LevelAdjustment, party_level));
					}
				}
			}
			foreach (Trap current2 in this.fTraps)
			{
				list.Add(AI.GetThreatDifficulty(current2.Level, party_level));
			}
			foreach (SkillChallenge current3 in this.fSkillChallenges)
			{
				list.Add(current3.GetDifficulty(party_level, party_size));
			}
			list.Add(this.get_diff(party_level, party_size));
			if (list.Contains(Difficulty.Extreme))
			{
				return Difficulty.Extreme;
			}
			if (list.Contains(Difficulty.Hard))
			{
				return Difficulty.Hard;
			}
			if (list.Contains(Difficulty.Moderate))
			{
				return Difficulty.Moderate;
			}
			if (list.Contains(Difficulty.Easy))
			{
				return Difficulty.Easy;
			}
			return Difficulty.Trivial;
		}

		public void SetStandardEncounterNotes()
		{
			this.fNotes.Add(new EncounterNote("Illumination"));
			this.fNotes.Add(new EncounterNote("Features of the Area"));
			this.fNotes.Add(new EncounterNote("Setup"));
			this.fNotes.Add(new EncounterNote("Tactics"));
			this.fNotes.Add(new EncounterNote("Victory Conditions"));
		}

		public IElement Copy()
		{
			Encounter encounter = new Encounter();
			foreach (EncounterSlot current in this.fSlots)
			{
				encounter.Slots.Add(current.Copy());
			}
			foreach (Trap current2 in this.fTraps)
			{
				encounter.Traps.Add(current2.Copy());
			}
			foreach (SkillChallenge current3 in this.fSkillChallenges)
			{
				encounter.SkillChallenges.Add(current3.Copy() as SkillChallenge);
			}
			foreach (CustomToken current4 in this.fCustomTokens)
			{
				encounter.CustomTokens.Add(current4.Copy());
			}
			encounter.MapID = this.fMapID;
			encounter.MapAreaID = this.fMapAreaID;
			foreach (EncounterNote current5 in this.fNotes)
			{
				encounter.Notes.Add(current5.Copy());
			}
			foreach (EncounterWave current6 in this.fWaves)
			{
				encounter.Waves.Add(current6.Copy());
			}
			return encounter;
		}

		private Difficulty get_diff(int party_level, int party_size)
		{
			if (this.GetXP() <= 0)
			{
				return Difficulty.Trivial;
			}
			int level = this.GetLevel(party_size);
			int num = level - party_level;
			if (num < -2)
			{
				return Difficulty.Trivial;
			}
			if (num == -2 || num == -1)
			{
				return Difficulty.Easy;
			}
			if (num == 0 || num == 1)
			{
				return Difficulty.Moderate;
			}
			if (num == 2 || num == 3 || num == 4)
			{
				return Difficulty.Hard;
			}
			return Difficulty.Extreme;
		}
	}
}
