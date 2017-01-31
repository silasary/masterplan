using Masterplan.Data;
using System;

namespace Masterplan.Events
{
	public class HeroEventArgs : EventArgs
	{
		private Hero fHero;

		public Hero Hero
		{
			get
			{
				return this.fHero;
			}
		}

		public HeroEventArgs(Hero hero)
		{
			this.fHero = hero;
		}
	}
}
