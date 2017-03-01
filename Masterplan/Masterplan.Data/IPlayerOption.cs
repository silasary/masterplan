using System;

namespace Masterplan.Data
{
    ///<summary>
    ///Interface implemented by player option classes.
    ///</summary>
    public interface IPlayerOption
	{
        ///<summary>
        ///Gets or sets the unique ID of the option.
        ///</summary>
        Guid ID
		{
			get;
			set;
		}

        ///<summary>
        ///Gets or sets the name of the option.
        ///</summary>
        string Name
		{
			get;
			set;
		}
	}
}
