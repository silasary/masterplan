using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Weapon : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private WeaponCategory fCategory;

		private WeaponType fType;

		private bool fTwoHanded;

		private int fProficiency = 2;

		private string fDamage = "";

		private string fRange = "";

		private string fPrice = "";

		private string fWeight = "";

		private string fGroup = "";

		private string fProperties = "";

		private string fDescription = "";

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

		public WeaponCategory Category
		{
			get
			{
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
			}
		}

		public WeaponType Type
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

		public bool TwoHanded
		{
			get
			{
				return this.fTwoHanded;
			}
			set
			{
				this.fTwoHanded = value;
			}
		}

		public int Proficiency
		{
			get
			{
				return this.fProficiency;
			}
			set
			{
				this.fProficiency = value;
			}
		}

		public string Damage
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

		public string Price
		{
			get
			{
				return this.fPrice;
			}
			set
			{
				this.fPrice = value;
			}
		}

		public string Weight
		{
			get
			{
				return this.fWeight;
			}
			set
			{
				this.fWeight = value;
			}
		}

		public string Group
		{
			get
			{
				return this.fGroup;
			}
			set
			{
				this.fGroup = value;
			}
		}

		public string Properties
		{
			get
			{
				return this.fProperties;
			}
			set
			{
				this.fProperties = value;
			}
		}

		public string Description
		{
			get
			{
				return this.fDescription;
			}
			set
			{
				this.fDescription = value;
			}
		}

		public Weapon Copy()
		{
			return new Weapon
			{
				ID = this.fID,
				Name = this.fName,
				Category = this.fCategory,
				Type = this.fType,
				TwoHanded = this.fTwoHanded,
				Proficiency = this.fProficiency,
				Damage = this.fDamage,
				Range = this.fRange,
				Price = this.fPrice,
				Weight = this.fWeight,
				Group = this.fGroup,
				Properties = this.fProperties,
				Description = this.fDescription
			};
		}
	}
}
