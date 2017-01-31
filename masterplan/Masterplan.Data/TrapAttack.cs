using System;

namespace Masterplan.Data
{
	[Serializable]
	public class TrapAttack
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fTrigger = "";

		private ActionType fAction = ActionType.Standard;

		private string fRange = "";

		private string fKeywords = "";

		private string fTarget = "";

		private bool fHasInitiative;

		private int fInitiative;

		private PowerAttack fAttack = new PowerAttack();

		private string fOnHit = "";

		private string fOnMiss = "";

		private string fEffect = "";

		private string fNotes = "";

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public string Trigger
		{
			get
			{
				return this.fTrigger;
			}
			set
			{
				this.fTrigger = value;
			}
		}

		public ActionType Action
		{
			get
			{
				return this.fAction;
			}
			set
			{
				this.fAction = value;
			}
		}

		public string Range
		{
			get
			{
				return this.fRange;
			}
			set
			{
				this.fRange = value;
			}
		}

		public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
			}
		}

		public string Target
		{
			get
			{
				return this.fTarget;
			}
			set
			{
				this.fTarget = value;
			}
		}

		public bool HasInitiative
		{
			get
			{
				return this.fHasInitiative;
			}
			set
			{
				this.fHasInitiative = value;
			}
		}

		public int Initiative
		{
			get
			{
				return this.fInitiative;
			}
			set
			{
				this.fInitiative = value;
			}
		}

		public PowerAttack Attack
		{
			get
			{
				return this.fAttack;
			}
			set
			{
				this.fAttack = value;
			}
		}

		public string OnHit
		{
			get
			{
				return this.fOnHit;
			}
			set
			{
				this.fOnHit = value;
			}
		}

		public string OnMiss
		{
			get
			{
				return this.fOnMiss;
			}
			set
			{
				this.fOnMiss = value;
			}
		}

		public string Effect
		{
			get
			{
				return this.fEffect;
			}
			set
			{
				this.fEffect = value;
			}
		}

		public string Notes
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

		public TrapAttack Copy()
		{
			return new TrapAttack
			{
				ID = this.fID,
				Name = this.fName,
				Trigger = this.fTrigger,
				Action = this.fAction,
				Keywords = this.fKeywords,
				Range = this.fRange,
				Target = this.fTarget,
				HasInitiative = this.fHasInitiative,
				Initiative = this.fInitiative,
				Attack = this.fAttack.Copy(),
				OnHit = this.fOnHit,
				OnMiss = this.fOnMiss,
				Effect = this.fEffect,
				Notes = this.fNotes
			};
		}
	}
}
