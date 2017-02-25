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
			List<string> conditions = new List<string>(condition.ToLower().Split(null));
			List<string> descriptions = new List<string>();
			if (conditions.Contains("blinded"))
			{
				descriptions.Add("Grant CA; targets have total concealment; -10 to Perception; can't flank");
			}
			if (conditions.Contains("dazed"))
			{
				descriptions.Add("Grant CA; one std / move / minor action per turn; no immediate / opportunity actions; can't flank");
			}
			if (conditions.Contains("deafened"))
			{
				descriptions.Add("Can't hear; -10 to Perception");
			}
			if (conditions.Contains("dominated"))
			{
				descriptions.Add("Grant CA; can't flank; can't take actions; dominating creature chooses one action on your turn, can make you use at-will powers");
			}
			if (conditions.Contains("dying"))
			{
				descriptions.Add("Grant CA; can be targeted by coup de grace; -5 to defences; can't take actions; can't flank; make death save each round");
			}
			if (conditions.Contains("grabbed"))
			{
				descriptions.Add("Can't move (can teleport, can be forced to move)");
			}
			if (conditions.Contains("helpless"))
			{
				descriptions.Add("Grant CA; can be targeted by coup de grace");
			}
			if (conditions.Contains("immobilised"))
			{
				descriptions.Add("Can't move (can teleport, can be forced to move)");
			}
			if (conditions.Contains("marked"))
			{
				descriptions.Add("-2 to attack when your attack doesn't include the marker");
			}
			if (conditions.Contains("petrified"))
			{
				descriptions.Add("Can't take actions; resist 20 to all damage; unaware of surroundings; don't age");
			}
			if (conditions.Contains("prone"))
			{
				descriptions.Add("Grant CA to enemies making melee attacks; can't move (can teleport, crawl or be forced to move); +2 defences vs ranged attacks from non-adjacent enemies; -2 to attacks");
			}
			if (conditions.Contains("removed"))
			{
				descriptions.Add("Can't take actions; no line of sight or effect");
			}
			if (conditions.Contains("restrained"))
			{
				descriptions.Add("Grant CA; can't move (can teleport); -2 to attacks");
			}
			if (conditions.Contains("slowed"))
			{
				descriptions.Add("Speed is 2");
			}
			if (conditions.Contains("stunned"))
			{
				descriptions.Add("Grant CA; can't take actions; can't flank");
			}
			if (conditions.Contains("surprised"))
			{
				descriptions.Add("Grant CA; can't take actions; can't flank");
			}
			if (conditions.Contains("unconscious"))
			{
				descriptions.Add("Grant CA; can be targeted by coup de grace; -5 to defences; can't take actions; can't flank");
			}
			if (conditions.Contains("weakened"))
			{
				descriptions.Add("Attacks deal half damage");
			}
			return descriptions;
		}
	}
}
