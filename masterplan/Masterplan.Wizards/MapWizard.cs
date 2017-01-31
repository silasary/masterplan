using Masterplan.Tools.Generators;
using System;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class MapWizard : Wizard
	{
		private static MapBuilderData fData = new MapBuilderData();

		public override object Data
		{
			get
			{
				return MapWizard.fData;
			}
			set
			{
				MapWizard.fData = (value as MapBuilderData);
			}
		}

		public MapWizard(bool delve_only) : base("AutoBuild Map")
		{
			MapWizard.fData.DelveOnly = delve_only;
		}

		public override void AddPages()
		{
			if (!MapWizard.fData.DelveOnly)
			{
				base.Pages.Add(new MapTypePage());
			}
			base.Pages.Add(new MapLibrariesPage());
			base.Pages.Add(new MapAreasPage());
			base.Pages.Add(new MapSizePage());
		}

		public override int NextPageIndex(int current_page)
		{
			if (current_page == 1)
			{
				switch (MapWizard.fData.Type)
				{
				case MapAutoBuildType.Warren:
					return 2;
				case MapAutoBuildType.FilledArea:
				case MapAutoBuildType.Freeform:
					return 3;
				}
			}
			return base.NextPageIndex(current_page);
		}

		public override int BackPageIndex(int current_page)
		{
			if (current_page == 2 || current_page == 3)
			{
				return 1;
			}
			return base.BackPageIndex(current_page);
		}

		public override void OnFinish()
		{
		}

		public override void OnCancel()
		{
		}
	}
}
