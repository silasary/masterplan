using Masterplan.Data;
using System;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class VariantWizard : Wizard
	{
		private VariantData fData = new VariantData();

		public override object Data
		{
			get
			{
				return this.fData;
			}
			set
			{
				this.fData = (value as VariantData);
			}
		}

		public VariantWizard() : base("Select Creature")
		{
		}

		public override void AddPages()
		{
			base.Pages.Add(new VariantBasePage());
			base.Pages.Add(new VariantTemplatesPage());
			base.Pages.Add(new VariantRolePage());
			base.Pages.Add(new VariantFinishPage());
		}

		public override int NextPageIndex(int current_page)
		{
			if (current_page == 0)
			{
				if (!(this.fData.BaseCreature.Role is Minion))
				{
					return 1;
				}
				return 3;
			}
			else
			{
				if (current_page != 1)
				{
					return base.NextPageIndex(current_page);
				}
				if (this.fData.Roles.Count != 1)
				{
					return 2;
				}
				return 3;
			}
		}

		public override int BackPageIndex(int current_page)
		{
			if (current_page != 3)
			{
				return base.BackPageIndex(current_page);
			}
			if (this.fData.BaseCreature.Role is Minion)
			{
				return 0;
			}
			if (this.fData.Roles.Count != 1)
			{
				return 2;
			}
			return 1;
		}

		public override void OnFinish()
		{
		}

		public override void OnCancel()
		{
		}
	}
}
