using System;
using System.Collections.Generic;

namespace Masterplan.Tools
{
	internal class Conditions
	{
		public static List<string> GetConditions()
		{
			return new List<string>
			{
				"Blinded",
				"Dazed",
				"Deafened",
				"Dominated",
				"Dying",
				"Grabbed",
				"Helpless",
				"Immobilised",
				"Marked",
				"Petrified",
				"Prone",
				"Removed from play",
				"Restrained",
				"Slowed",
				"Stunned",
				"Surprised",
				"Unconscious",
				"Weakened"
			};
		}

		public static List<string> GetConditionInfo(string condition)
		{
			List<string> list = new List<string>(condition.ToLower().Split(null));
			List<string> list2 = new List<string>();
			if (list.Contains("blinded"))
			{
				list2.Add("Grant CA; targets have total concealment; -10 to Perception; can't flank");
			}
			if (list.Contains("dazed"))
			{
				list2.Add("Grant CA; one std / move / minor action per turn; no immediate / opportunity actions; can't flank");
			}
			if (list.Contains("deafened"))
			{
				list2.Add("Can't hear; -10 to Perception");
			}
			if (list.Contains("dominated"))
			{
				list2.Add("Grant CA; can't flank; can't take actions; dominating creature chooses one action on your turn, can make you use at-will powers");
			}
			if (list.Contains("dying"))
			{
				list2.Add("Grant CA; can be targeted by coup de grace; -5 to defences; can't take actions; can't flank; make death save each round");
			}
			if (list.Contains("grabbed"))
			{
				list2.Add("Can't move (can teleport, can be forced to move)");
			}
			if (list.Contains("helpless"))
			{
				list2.Add("Grant CA; can be targeted by coup de grace");
			}
			if (list.Contains("immobilised"))
			{
				list2.Add("Can't move (can teleport, can be forced to move)");
			}
			if (list.Contains("marked"))
			{
				list2.Add("-2 to attack when your attack doesn't include the marker");
			}
			if (list.Contains("petrified"))
			{
				list2.Add("Can't take actions; resist 20 to all damage; unaware of surroundings; don't age");
			}
			if (list.Contains("prone"))
			{
				list2.Add("Grant CA to enemies making melee attacks; can't move (can teleport, crawl or be forced to move); +2 defences vs ranged attacks from non-adjacent enemies; -2 to attacks");
			}
			if (list.Contains("removed"))
			{
				list2.Add("Can't take actions; no line of sight or effect");
			}
			if (list.Contains("restrained"))
			{
				list2.Add("Grant CA; can't move (can teleport); -2 to attacks");
			}
			if (list.Contains("slowed"))
			{
				list2.Add("Speed is 2");
			}
			if (list.Contains("stunned"))
			{
				list2.Add("Grant CA; can't take actions; can't flank");
			}
			if (list.Contains("surprised"))
			{
				list2.Add("Grant CA; can't take actions; can't flank");
			}
			if (list.Contains("unconscious"))
			{
				list2.Add("Grant CA; can be targeted by coup de grace; -5 to defences; can't take actions; can't flank");
			}
			if (list.Contains("weakened"))
			{
				list2.Add("Attacks deal half damage");
			}
			return list2;
		}
	}
}
