using System;
using System.Collections.Generic;

namespace Masterplan.Extensibility
{
	public interface IAddIn
	{
		string Name
		{
			get;
		}

		string Description
		{
			get;
		}

		Version Version
		{
			get;
		}

		List<ICommand> Commands
		{
			get;
		}

		List<ICommand> CombatCommands
		{
			get;
		}

		List<IPage> Pages
		{
			get;
		}

		List<IPage> QuickReferencePages
		{
			get;
		}

		bool Initialise(IApplication app);
	}
}
