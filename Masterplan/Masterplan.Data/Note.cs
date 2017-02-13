using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Note : IComparable<Note>
	{
		private Guid fID = Guid.NewGuid();

		private string fContent = "";

		private string fCategory = "";

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

		public string Content
		{
			get
			{
				return this.fContent;
			}
			set
			{
				this.fContent = value;
			}
		}

		public string Category
		{
			get
			{
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
			}
		}

		public string Name
		{
			get
			{
				string[] separator = new string[]
				{
					Environment.NewLine
				};
				string[] array = this.fContent.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 0)
				{
					return "(blank note)";
				}
				return array[0];
			}
		}

		public override string ToString()
		{
			return this.fContent;
		}

		public Note Copy()
		{
			return new Note
			{
				ID = this.fID,
				Content = this.fContent,
				Category = this.fCategory
			};
		}

		public int CompareTo(Note rhs)
		{
			return this.Name.CompareTo(rhs.Name);
		}
	}
}
