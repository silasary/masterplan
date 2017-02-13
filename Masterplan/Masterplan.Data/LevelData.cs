using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class LevelData
	{
		private int fLevel;

		private List<Feature> fFeatures = new List<Feature>();

		private List<PlayerPower> fPowers = new List<PlayerPower>();

		public int Level
		{
			get
			{
				return this.fLevel;
			}
			set
			{
				this.fLevel = value;
			}
		}

		public List<Feature> Features
		{
			get
			{
				return this.fFeatures;
			}
			set
			{
				this.fFeatures = value;
			}
		}

		public List<PlayerPower> Powers
		{
			get
			{
				return this.fPowers;
			}
			set
			{
				this.fPowers = value;
			}
		}

		public int Count
		{
			get
			{
				return this.fFeatures.Count + this.fPowers.Count;
			}
		}

		public LevelData Copy()
		{
			LevelData levelData = new LevelData();
			levelData.Level = this.fLevel;
			foreach (Feature current in this.fFeatures)
			{
				levelData.Features.Add(current.Copy());
			}
			foreach (PlayerPower current2 in this.fPowers)
			{
				levelData.Powers.Add(current2.Copy());
			}
			return levelData;
		}

		public override string ToString()
		{
			string text = "";
			foreach (Feature current in this.fFeatures)
			{
				if (text != "")
				{
					text += "; ";
				}
				text += current.Name;
			}
			foreach (PlayerPower current2 in this.fPowers)
			{
				if (text != "")
				{
					text += "; ";
				}
				text += current2.Name;
			}
			if (text == "")
			{
				text = "(empty)";
			}
			if (this.fLevel >= 1)
			{
				return string.Concat(new object[]
				{
					"Level ",
					this.fLevel,
					": ",
					text
				});
			}
			return text;
		}
	}
}
