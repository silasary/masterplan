using System;

namespace Utils.Wizards
{
	public interface IWizardPage
	{
		bool AllowNext
		{
			get;
		}

		bool AllowBack
		{
			get;
		}

		bool AllowFinish
		{
			get;
		}

		void OnShown(object data);

		bool OnBack();

		bool OnNext();

		bool OnFinish();
	}
}
