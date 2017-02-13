using System;

namespace Masterplan.Events
{
	public class MovementEventArgs : EventArgs
	{
		private int fDistance;

		public int Distance
		{
			get
			{
				return this.fDistance;
			}
		}

		public MovementEventArgs(int distance)
		{
			this.fDistance = distance;
		}
	}
}
