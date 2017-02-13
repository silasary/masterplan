using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Encyclopedia
	{
		private List<EncyclopediaEntry> fEntries = new List<EncyclopediaEntry>();

		private List<EncyclopediaLink> fLinks = new List<EncyclopediaLink>();

		private List<EncyclopediaGroup> fGroups = new List<EncyclopediaGroup>();

		public List<EncyclopediaEntry> Entries
		{
			get
			{
				return this.fEntries;
			}
			set
			{
				this.fEntries = value;
			}
		}

		public List<EncyclopediaLink> Links
		{
			get
			{
				return this.fLinks;
			}
			set
			{
				this.fLinks = value;
			}
		}

		public List<EncyclopediaGroup> Groups
		{
			get
			{
				return this.fGroups;
			}
			set
			{
				this.fGroups = value;
			}
		}

		public EncyclopediaEntry FindEntry(Guid entry_id)
		{
			foreach (EncyclopediaEntry current in this.fEntries)
			{
				if (current.ID == entry_id)
				{
					return current;
				}
			}
			return null;
		}

		public EncyclopediaEntry FindEntry(string name)
		{
			foreach (EncyclopediaEntry current in this.fEntries)
			{
				if (current.Name.ToLower() == name.ToLower())
				{
					return current;
				}
			}
			return null;
		}

		public EncyclopediaGroup FindGroup(Guid entry_id)
		{
			foreach (EncyclopediaGroup current in this.fGroups)
			{
				if (current.ID == entry_id)
				{
					return current;
				}
			}
			return null;
		}

		public EncyclopediaLink FindLink(Guid entry_id_1, Guid entry_id_2)
		{
			foreach (EncyclopediaLink current in this.fLinks)
			{
				if (current.EntryIDs.Contains(entry_id_1) && current.EntryIDs.Contains(entry_id_2))
				{
					return current;
				}
			}
			return null;
		}

		public EncyclopediaEntry FindEntryForAttachment(Guid attachment_id)
		{
			foreach (EncyclopediaEntry current in Session.Project.Encyclopedia.Entries)
			{
				if (current.AttachmentID == attachment_id)
				{
					return current;
				}
			}
			return null;
		}

		public Encyclopedia Copy()
		{
			Encyclopedia encyclopedia = new Encyclopedia();
			foreach (EncyclopediaEntry current in this.fEntries)
			{
				encyclopedia.Entries.Add(current.Copy());
			}
			foreach (EncyclopediaLink current2 in this.fLinks)
			{
				encyclopedia.Links.Add(current2.Copy());
			}
			foreach (EncyclopediaGroup current3 in this.fGroups)
			{
				encyclopedia.Groups.Add(current3.Copy());
			}
			return encyclopedia;
		}

		public void Import(Encyclopedia enc)
		{
			if (enc == null)
			{
				return;
			}
			foreach (EncyclopediaEntry current in enc.Entries)
			{
				EncyclopediaEntry encyclopediaEntry = this.FindEntry(current.ID);
				if (encyclopediaEntry != null)
				{
					this.Entries.Remove(encyclopediaEntry);
				}
				this.Entries.Add(current);
			}
			foreach (EncyclopediaGroup current2 in enc.Groups)
			{
				EncyclopediaGroup encyclopediaGroup = this.FindGroup(current2.ID);
				if (encyclopediaGroup != null)
				{
					this.Groups.Remove(encyclopediaGroup);
				}
				this.Groups.Add(current2);
			}
			foreach (EncyclopediaLink current3 in enc.Links)
			{
				EncyclopediaLink encyclopediaLink = this.FindLink(current3.EntryIDs[0], current3.EntryIDs[1]);
				if (encyclopediaLink != null)
				{
					this.Links.Remove(encyclopediaLink);
				}
				this.Links.Add(current3);
			}
		}
	}
}
