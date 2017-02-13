using System;

namespace Masterplan.Data
{
	[Serializable]
	public class PowerAction
	{
		public const string RECHARGE_2 = "Recharges on 2-6";

		public const string RECHARGE_3 = "Recharges on 3-6";

		public const string RECHARGE_4 = "Recharges on 4-6";

		public const string RECHARGE_5 = "Recharges on 5-6";

		public const string RECHARGE_6 = "Recharges on 6";

		private ActionType fAction = ActionType.Standard;

		private string fTrigger = "";

		private ActionType fSustainAction;

		private PowerUseType fUse = PowerUseType.AtWill;

		private string fRecharge = "";

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

		public ActionType SustainAction
		{
			get
			{
				return this.fSustainAction;
			}
			set
			{
				this.fSustainAction = value;
			}
		}

		public PowerUseType Use
		{
			get
			{
				return this.fUse;
			}
			set
			{
				this.fUse = value;
			}
		}

		public string Recharge
		{
			get
			{
				return this.fRecharge;
			}
			set
			{
				this.fRecharge = value;
			}
		}

		public PowerAction Copy()
		{
			return new PowerAction
			{
				Action = this.fAction,
				Trigger = this.fTrigger,
				SustainAction = this.fSustainAction,
				Use = this.fUse,
				Recharge = this.fRecharge
			};
		}

		public override string ToString()
		{
			string text = "";
			if (this.fUse == PowerUseType.AtWill || this.fUse == PowerUseType.Basic)
			{
				text = "At-Will";
				if (this.fUse == PowerUseType.Basic)
				{
					text += " (basic attack)";
				}
			}
			if (this.fUse == PowerUseType.Encounter && this.fRecharge == "")
			{
				text = "Encounter";
			}
			if (this.fUse == PowerUseType.Daily)
			{
				text = "Daily";
			}
			if (this.fRecharge != "")
			{
				if (text != "")
				{
					text += "; ";
				}
				text += this.fRecharge;
			}
			return text;
		}
	}
}
