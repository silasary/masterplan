using System;

namespace Masterplan.Extensibility
{
	public interface ICommand
	{
		string Name
		{
			get;
		}

		string Description
		{
			get;
		}

		bool Available
		{
			get;
		}

		bool Active
		{
			get;
		}

		void Execute();
	}
}
