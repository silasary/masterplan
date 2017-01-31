using System;

namespace Masterplan.Data
{
	[Serializable]
	public class TerrainPower
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private TerrainPowerType fType = TerrainPowerType.SingleUse;

		private string fFlavourText = "";

		private ActionType fAction = ActionType.Standard;

		private string fRequirement = "";

		private string fCheck = "";

		private string fSuccess = "";

		private string fFailure = "";

		private string fTarget = "";

		private string fAttack = "";

		private string fHit = "";

		private string fMiss = "";

		private string fEffect = "";

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

		public TerrainPowerType Type
		{
			get
			{
				return this.fType;
			}
			set
			{
				this.fType = value;
			}
		}

		public string FlavourText
		{
			get
			{
				return this.fFlavourText;
			}
			set
			{
				this.fFlavourText = value;
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

		public string Requirement
		{
			get
			{
				return this.fRequirement;
			}
			set
			{
				this.fRequirement = value;
			}
		}

		public string Check
		{
			get
			{
				return this.fCheck;
			}
			set
			{
				this.fCheck = value;
			}
		}

		public string Success
		{
			get
			{
				return this.fSuccess;
			}
			set
			{
				this.fSuccess = value;
			}
		}

		public string Failure
		{
			get
			{
				return this.fFailure;
			}
			set
			{
				this.fFailure = value;
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

		public string Attack
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

		public string Hit
		{
			get
			{
				return this.fHit;
			}
			set
			{
				this.fHit = value;
			}
		}

		public string Miss
		{
			get
			{
				return this.fMiss;
			}
			set
			{
				this.fMiss = value;
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

		public TerrainPower Copy()
		{
			return new TerrainPower
			{
				ID = this.fID,
				Name = this.fName,
				Type = this.fType,
				FlavourText = this.fFlavourText,
				Action = this.fAction,
				Requirement = this.fRequirement,
				Check = this.fCheck,
				Success = this.fSuccess,
				Failure = this.fFailure,
				Target = this.fTarget,
				Attack = this.fAttack,
				Hit = this.fHit,
				Miss = this.fMiss,
				Effect = this.fEffect
			};
		}
	}
}
