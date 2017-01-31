using System;

namespace Masterplan.Data
{
	public interface IPlayerOption
	{
		Guid ID
		{
			get;
			set;
		}

		string Name
		{
			get;
			set;
		}
	}
}
