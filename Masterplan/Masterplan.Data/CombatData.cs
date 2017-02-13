using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class CombatData : IComparable<CombatData>
	{
		public static Point NoPoint = new Point(int.MinValue, int.MinValue);

		private Guid fID = Guid.NewGuid();

		private string fDisplayName = "";

		private Point fLocation = CombatData.NoPoint;

		private bool fVisible = true;

		private int fInitiative = int.MinValue;

		private bool fDelaying;

		private int fDamage;

		private int fTempHP;

		private int fAltitude;

		private List<Guid> fUsedPowers = new List<Guid>();

		private List<OngoingCondition> fConditions = new List<OngoingCondition>();

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

		public string DisplayName
		{
			get
			{
				return this.fDisplayName;
			}
			set
			{
				this.fDisplayName = value;
			}
		}

		public Point Location
		{
			get
			{
				return this.fLocation;
			}
			set
			{
				this.fLocation = value;
			}
		}

		public bool Visible
		{
			get
			{
				return this.fVisible;
			}
			set
			{
				this.fVisible = value;
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

		public bool Delaying
		{
			get
			{
				return this.fDelaying;
			}
			set
			{
				this.fDelaying = value;
			}
		}

		public int Damage
		{
			get
			{
				return this.fDamage;
			}
			set
			{
				this.fDamage = value;
			}
		}

		public int TempHP
		{
			get
			{
				return this.fTempHP;
			}
			set
			{
				this.fTempHP = value;
			}
		}

		public int Altitude
		{
			get
			{
				return this.fAltitude;
			}
			set
			{
				this.fAltitude = value;
			}
		}

		public List<Guid> UsedPowers
		{
			get
			{
				return this.fUsedPowers;
			}
			set
			{
				this.fUsedPowers = value;
			}
		}

		public List<OngoingCondition> Conditions
		{
			get
			{
				return this.fConditions;
			}
			set
			{
				this.fConditions = value;
			}
		}

		public void Reset(bool reset_damage)
		{
			this.fLocation = CombatData.NoPoint;
			this.fVisible = true;
			this.fInitiative = int.MinValue;
			this.fDelaying = false;
			this.fTempHP = 0;
			this.fAltitude = 0;
			this.fUsedPowers.Clear();
			this.fConditions.Clear();
			if (reset_damage)
			{
				this.fDamage = 0;
			}
		}

		public CombatData Copy()
		{
			CombatData combatData = new CombatData();
			combatData.ID = this.fID;
			combatData.DisplayName = this.fDisplayName;
			combatData.Location = new Point(this.fLocation.X, this.fLocation.Y);
			combatData.Visible = this.fVisible;
			combatData.Initiative = this.fInitiative;
			combatData.Delaying = this.fDelaying;
			combatData.Damage = this.fDamage;
			combatData.TempHP = this.fTempHP;
			combatData.Altitude = this.fAltitude;
			foreach (Guid current in this.fUsedPowers)
			{
				combatData.UsedPowers.Add(current);
			}
			foreach (OngoingCondition current2 in this.fConditions)
			{
				combatData.Conditions.Add(current2.Copy());
			}
			return combatData;
		}

		public override string ToString()
		{
			return this.fDisplayName;
		}

		public int CompareTo(CombatData rhs)
		{
			return this.fDisplayName.CompareTo(rhs.DisplayName);
		}
	}
}
