using System;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterNote
	{
		private Guid fID = Guid.NewGuid();

		private string fTitle = "";

		private string fContents = "";

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public string Title
		{
			get
			{
				return this.fTitle;
			}
			set
			{
				this.fTitle = value;
			}
		}

		public string Contents
		{
			get
			{
				return this.fContents;
			}
			set
			{
				this.fContents = value;
			}
		}

		public EncounterNote()
		{
		}

		public EncounterNote(string title)
		{
			this.fTitle = title;
		}

		public EncounterNote Copy()
		{
			return new EncounterNote
			{
				ID = this.fID,
				Title = this.fTitle,
				Contents = this.fContents
			};
		}

		public override string ToString()
		{
			return this.fTitle;
		}
	}
}
