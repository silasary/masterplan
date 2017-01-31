using System;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class CustomToken : IToken
	{
		private Guid fID = Guid.NewGuid();

		private CustomTokenType fType;

		private string fName = "";

		private string fDetails = "";

		private CreatureSize fTokenSize = CreatureSize.Medium;

		private Size fOverlaySize = new Size(3, 3);

		private OverlayStyle fOverlayStyle;

		private Color fColour = Color.DarkBlue;

		private Image fImage;

		private bool fDifficultTerrain;

		private bool fOpaque;

		private CombatData fData = new CombatData();

		private TerrainPower fTerrainPower;

		private Guid fCreatureID = Guid.Empty;

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

		public CustomTokenType Type
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

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
			}
		}

		public CreatureSize TokenSize
		{
			get
			{
				return this.fTokenSize;
			}
			set
			{
				this.fTokenSize = value;
			}
		}

		public Size OverlaySize
		{
			get
			{
				return this.fOverlaySize;
			}
			set
			{
				this.fOverlaySize = value;
			}
		}

		public OverlayStyle OverlayStyle
		{
			get
			{
				return this.fOverlayStyle;
			}
			set
			{
				this.fOverlayStyle = value;
			}
		}

		public Color Colour
		{
			get
			{
				return this.fColour;
			}
			set
			{
				this.fColour = value;
			}
		}

		public Image Image
		{
			get
			{
				return this.fImage;
			}
			set
			{
				this.fImage = value;
			}
		}

		public bool DifficultTerrain
		{
			get
			{
				return this.fDifficultTerrain;
			}
			set
			{
				this.fDifficultTerrain = value;
			}
		}

		public bool Opaque
		{
			get
			{
				return this.fOpaque;
			}
			set
			{
				this.fOpaque = value;
			}
		}

		public CombatData Data
		{
			get
			{
				return this.fData;
			}
			set
			{
				this.fData = value;
			}
		}

		public TerrainPower TerrainPower
		{
			get
			{
				return this.fTerrainPower;
			}
			set
			{
				this.fTerrainPower = value;
			}
		}

		public Guid CreatureID
		{
			get
			{
				return this.fCreatureID;
			}
			set
			{
				this.fCreatureID = value;
			}
		}

		public CustomToken Copy()
		{
			return new CustomToken
			{
				ID = this.fID,
				Type = this.fType,
				Name = this.fName,
				Details = this.fDetails,
				TokenSize = this.fTokenSize,
				OverlaySize = this.fOverlaySize,
				OverlayStyle = this.fOverlayStyle,
				Colour = this.fColour,
				Image = this.fImage,
				DifficultTerrain = this.fDifficultTerrain,
				Opaque = this.fOpaque,
				Data = this.fData.Copy(),
				TerrainPower = ((this.fTerrainPower != null) ? this.fTerrainPower.Copy() : null),
				CreatureID = this.fCreatureID
			};
		}
	}
}
