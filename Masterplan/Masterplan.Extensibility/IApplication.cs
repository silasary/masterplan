using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Extensibility
{
	public interface IApplication
	{
		Project Project
		{
			get;
			set;
		}

		PlotPoint SelectedPoint
		{
			get;
		}

		Encounter CurrentEncounter
		{
			get;
		}

		string ProjectFile
		{
			get;
			set;
		}

		bool ProjectModified
		{
			get;
			set;
		}

		List<Library> Libraries
		{
			get;
		}

		List<IAddIn> AddIns
		{
			get;
		}

		void UpdateView();

		void SaveLibrary(Library lib);
	}
}
