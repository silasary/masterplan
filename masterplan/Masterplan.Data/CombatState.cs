using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class CombatState
	{
		private DateTime fTimestamp = DateTime.Now;

		private int fPartyLevel = Session.Project.Party.Level;

		private Encounter fEncounter;

		private int fCurrentRound = 1;

		private Dictionary<Guid, CombatData> fHeroData;

		private Dictionary<Guid, CombatData> fTrapData;

		private List<TokenLink> fTokenLinks;

		private int fRemovedCreatureXP;

		private Guid fCurrentActor = Guid.Empty;

		private Rectangle fViewpoint = Rectangle.Empty;

		private List<MapSketch> fSketches = new List<MapSketch>();

		private List<OngoingCondition> fQuickEffects = new List<OngoingCondition>();

		private EncounterLog fLog = new EncounterLog();

		public DateTime Timestamp
		{
			get
			{
				return this.fTimestamp;
			}
			set
			{
				this.fTimestamp = value;
			}
		}

		public int PartyLevel
		{
			get
			{
				return this.fPartyLevel;
			}
			set
			{
				this.fPartyLevel = value;
			}
		}

		public Encounter Encounter
		{
			get
			{
				return this.fEncounter;
			}
			set
			{
				this.fEncounter = value;
			}
		}

		public int CurrentRound
		{
			get
			{
				return this.fCurrentRound;
			}
			set
			{
				this.fCurrentRound = value;
			}
		}

		public Dictionary<Guid, CombatData> HeroData
		{
			get
			{
				return this.fHeroData;
			}
			set
			{
				this.fHeroData = value;
			}
		}

		public Dictionary<Guid, CombatData> TrapData
		{
			get
			{
				return this.fTrapData;
			}
			set
			{
				this.fTrapData = value;
			}
		}

		public List<TokenLink> TokenLinks
		{
			get
			{
				return this.fTokenLinks;
			}
			set
			{
				this.fTokenLinks = value;
			}
		}

		public int RemovedCreatureXP
		{
			get
			{
				return this.fRemovedCreatureXP;
			}
			set
			{
				this.fRemovedCreatureXP = value;
			}
		}

		public Guid CurrentActor
		{
			get
			{
				return this.fCurrentActor;
			}
			set
			{
				this.fCurrentActor = value;
			}
		}

		public Rectangle Viewpoint
		{
			get
			{
				return this.fViewpoint;
			}
			set
			{
				this.fViewpoint = value;
			}
		}

		public List<MapSketch> Sketches
		{
			get
			{
				return this.fSketches;
			}
			set
			{
				this.fSketches = value;
			}
		}

		public List<OngoingCondition> QuickEffects
		{
			get
			{
				return this.fQuickEffects;
			}
			set
			{
				this.fQuickEffects = value;
			}
		}

		public EncounterLog Log
		{
			get
			{
				return this.fLog;
			}
			set
			{
				this.fLog = value;
			}
		}

		public override string ToString()
		{
			return this.fTimestamp.ToShortDateString() + " at " + this.fTimestamp.ToShortTimeString();
		}
	}
}
