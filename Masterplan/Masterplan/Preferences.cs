using Masterplan.Controls;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan
{
	[Serializable]
	public class Preferences
	{
		private string fLastFile = "";

		private bool fShowHeadlines = true;

		private bool fMaximised;

		private Size fSize = Size.Empty;

		private Point fLocation = Point.Empty;

		private bool fNavigation;

		private bool fPreview = true;

		private PlotViewLinkStyle fLinkStyle;

		private bool fAllXP = true;

		private TileView fTileView = TileView.Size;

		private TileSize fTileSize = TileSize.Medium;

		private List<Guid> fTileLibraries = new List<Guid>();

		private InitiativeMode fInitiativeMode;

		private InitiativeMode fHeroInitiativeMode = InitiativeMode.ManualIndividual;

		private InitiativeMode fTrapInitiativeMode = InitiativeMode.AutoIndividual;

		private bool fCreatureAutoRemove = true;

		private bool fiPlay4E = true;

		private bool fCombatTwoColumns;

		private bool fCombatTwoColumnsNoMap = true;

		private bool fCombatMapRight = true;

		private CreatureViewMode fCombatFog;

		private CreatureViewMode fPlayerViewFog = CreatureViewMode.Visible;

		private bool fCombatHealthBars;

		private bool fPlayerViewConditionBadges = true;

		private bool fCombatConditionBadges = true;

		private bool fPlayerViewHealthBars;

		private bool fCreatureLabels;

		private bool fCombatPictureTokens = true;

		private bool fPlayerViewPictureTokens = true;

		private bool fPlayerViewMap = true;

		private bool fPlayerViewInitiative = true;

		private bool fCombatGrid;

		private bool fPlayerViewGrid;

		private bool fCombatGridLabels;

		private bool fPlayerViewGridLabels;

		private bool fCombatColumnInitiative = true;

		private bool fCombatColumnHP = true;

		private bool fCombatColumnDefences;

		private bool fCombatColumnEffects;

		public string LastFile
		{
			get
			{
				return this.fLastFile;
			}
			set
			{
				this.fLastFile = value;
			}
		}

		public bool ShowHeadlines
		{
			get
			{
				return this.fShowHeadlines;
			}
			set
			{
				this.fShowHeadlines = value;
			}
		}

		public bool Maximised
		{
			get
			{
				return this.fMaximised;
			}
			set
			{
				this.fMaximised = value;
			}
		}

		public Size Size
		{
			get
			{
				return this.fSize;
			}
			set
			{
				this.fSize = value;
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

		public bool ShowNavigation
		{
			get
			{
				return this.fNavigation;
			}
			set
			{
				this.fNavigation = value;
			}
		}

		public bool ShowPreview
		{
			get
			{
				return this.fPreview;
			}
			set
			{
				this.fPreview = value;
			}
		}

		public PlotViewLinkStyle LinkStyle
		{
			get
			{
				return this.fLinkStyle;
			}
			set
			{
				this.fLinkStyle = value;
			}
		}

		public bool AllXP
		{
			get
			{
				return this.fAllXP;
			}
			set
			{
				this.fAllXP = value;
			}
		}

		public TileView TileView
		{
			get
			{
				return this.fTileView;
			}
			set
			{
				this.fTileView = value;
			}
		}

		public TileSize TileSize
		{
			get
			{
				return this.fTileSize;
			}
			set
			{
				this.fTileSize = value;
			}
		}

		public List<Guid> TileLibraries
		{
			get
			{
				return this.fTileLibraries;
			}
			set
			{
				this.fTileLibraries = value;
			}
		}

		public InitiativeMode InitiativeMode
		{
			get
			{
				return this.fInitiativeMode;
			}
			set
			{
				this.fInitiativeMode = value;
			}
		}

		public InitiativeMode HeroInitiativeMode
		{
			get
			{
				return this.fHeroInitiativeMode;
			}
			set
			{
				this.fHeroInitiativeMode = value;
			}
		}

		public InitiativeMode TrapInitiativeMode
		{
			get
			{
				return this.fTrapInitiativeMode;
			}
			set
			{
				this.fTrapInitiativeMode = value;
			}
		}

		public bool CreatureAutoRemove
		{
			get
			{
				return this.fCreatureAutoRemove;
			}
			set
			{
				this.fCreatureAutoRemove = value;
			}
		}

		public bool iPlay4E
		{
			get
			{
				return this.fiPlay4E;
			}
			set
			{
				this.fiPlay4E = value;
			}
		}

		public bool CombatTwoColumns
		{
			get
			{
				return this.fCombatTwoColumns;
			}
			set
			{
				this.fCombatTwoColumns = value;
			}
		}

		public bool CombatTwoColumnsNoMap
		{
			get
			{
				return this.fCombatTwoColumnsNoMap;
			}
			set
			{
				this.fCombatTwoColumnsNoMap = value;
			}
		}

		public bool CombatMapRight
		{
			get
			{
				return this.fCombatMapRight;
			}
			set
			{
				this.fCombatMapRight = value;
			}
		}

		public CreatureViewMode CombatFog
		{
			get
			{
				return this.fCombatFog;
			}
			set
			{
				this.fCombatFog = value;
			}
		}

		public CreatureViewMode PlayerViewFog
		{
			get
			{
				return this.fPlayerViewFog;
			}
			set
			{
				this.fPlayerViewFog = value;
			}
		}

		public bool CombatHealthBars
		{
			get
			{
				return this.fCombatHealthBars;
			}
			set
			{
				this.fCombatHealthBars = value;
			}
		}

		public bool PlayerViewConditionBadges
		{
			get
			{
				return this.fPlayerViewConditionBadges;
			}
			set
			{
				this.fPlayerViewConditionBadges = value;
			}
		}

		public bool CombatConditionBadges
		{
			get
			{
				return this.fCombatConditionBadges;
			}
			set
			{
				this.fCombatConditionBadges = value;
			}
		}

		public bool PlayerViewHealthBars
		{
			get
			{
				return this.fPlayerViewHealthBars;
			}
			set
			{
				this.fPlayerViewHealthBars = value;
			}
		}

		public bool PlayerViewCreatureLabels
		{
			get
			{
				return this.fCreatureLabels;
			}
			set
			{
				this.fCreatureLabels = value;
			}
		}

		public bool CombatPictureTokens
		{
			get
			{
				return this.fCombatPictureTokens;
			}
			set
			{
				this.fCombatPictureTokens = value;
			}
		}

		public bool PlayerViewPictureTokens
		{
			get
			{
				return this.fPlayerViewPictureTokens;
			}
			set
			{
				this.fPlayerViewPictureTokens = value;
			}
		}

		public bool PlayerViewMap
		{
			get
			{
				return this.fPlayerViewMap;
			}
			set
			{
				this.fPlayerViewMap = value;
			}
		}

		public bool PlayerViewInitiative
		{
			get
			{
				return this.fPlayerViewInitiative;
			}
			set
			{
				this.fPlayerViewInitiative = value;
			}
		}

		public bool CombatGrid
		{
			get
			{
				return this.fCombatGrid;
			}
			set
			{
				this.fCombatGrid = value;
			}
		}

		public bool PlayerViewGrid
		{
			get
			{
				return this.fPlayerViewGrid;
			}
			set
			{
				this.fPlayerViewGrid = value;
			}
		}

		public bool CombatGridLabels
		{
			get
			{
				return this.fCombatGridLabels;
			}
			set
			{
				this.fCombatGridLabels = value;
			}
		}

		public bool PlayerViewGridLabels
		{
			get
			{
				return this.fPlayerViewGridLabels;
			}
			set
			{
				this.fPlayerViewGridLabels = value;
			}
		}

		public bool CombatColumnInitiative
		{
			get
			{
				return this.fCombatColumnInitiative;
			}
			set
			{
				this.fCombatColumnInitiative = value;
			}
		}

		public bool CombatColumnHP
		{
			get
			{
				return this.fCombatColumnHP;
			}
			set
			{
				this.fCombatColumnHP = value;
			}
		}

		public bool CombatColumnDefences
		{
			get
			{
				return this.fCombatColumnDefences;
			}
			set
			{
				this.fCombatColumnDefences = value;
			}
		}

		public bool CombatColumnEffects
		{
			get
			{
				return this.fCombatColumnEffects;
			}
			set
			{
				this.fCombatColumnEffects = value;
			}
		}
	}
}
