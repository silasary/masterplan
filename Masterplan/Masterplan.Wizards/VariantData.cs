using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Wizards
{
	internal class VariantData
	{
		public ICreature BaseCreature;

		public List<CreatureTemplate> Templates = new List<CreatureTemplate>();

		public int SelectedRoleIndex;

		public List<RoleType> Roles
		{
			get
			{
				List<RoleType> list = new List<RoleType>();
				if (this.BaseCreature != null && this.BaseCreature.Role is ComplexRole)
				{
					ComplexRole complexRole = this.BaseCreature.Role as ComplexRole;
					list.Add(complexRole.Type);
				}
				foreach (CreatureTemplate current in this.Templates)
				{
					if (!list.Contains(current.Role))
					{
						list.Add(current.Role);
					}
				}
				list.Sort();
				return list;
			}
		}
	}
}
