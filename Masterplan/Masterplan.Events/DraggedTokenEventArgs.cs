using Masterplan.Data;
using System;
using System.Drawing;

namespace Masterplan.Events
{
	public class DraggedTokenEventArgs : EventArgs
	{
		private Point fOldLocation = CombatData.NoPoint;

		private Point fNewLocation = CombatData.NoPoint;

		public Point OldLocation
		{
			get
			{
				return this.fOldLocation;
			}
		}

		public Point NewLocation
		{
			get
			{
				return this.fNewLocation;
			}
		}

		public DraggedTokenEventArgs(Point old_location, Point new_location)
		{
			this.fOldLocation = old_location;
			this.fNewLocation = new_location;
		}
	}
}
