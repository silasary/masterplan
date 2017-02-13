using System;
using System.Windows.Forms;

namespace Masterplan.Extensibility
{
	public interface IPage
	{
		string Name
		{
			get;
		}

		Control Control
		{
			get;
		}

		void UpdateView();
	}
}
