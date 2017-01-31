using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class MapBuilderData
	{
		public MapAutoBuildType Type;

		public bool DelveOnly;

		public List<Library> Libraries = new List<Library>();

		public int MaxAreaCount = 10;

		public int MinAreaCount = 4;

		public int Width = 20;

		public int Height = 15;
	}
}
