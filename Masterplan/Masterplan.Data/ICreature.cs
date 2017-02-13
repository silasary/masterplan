using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	public interface ICreature
	{
		Guid ID
		{
			get;
			set;
		}

		string Name
		{
			get;
			set;
		}

		CreatureSize Size
		{
			get;
			set;
		}

		CreatureOrigin Origin
		{
			get;
			set;
		}

		CreatureType Type
		{
			get;
			set;
		}

		string Keywords
		{
			get;
			set;
		}

		int Level
		{
			get;
			set;
		}

		IRole Role
		{
			get;
			set;
		}

		string Senses
		{
			get;
			set;
		}

		string Movement
		{
			get;
			set;
		}

		string Alignment
		{
			get;
			set;
		}

		string Languages
		{
			get;
			set;
		}

		string Skills
		{
			get;
			set;
		}

		string Equipment
		{
			get;
			set;
		}

		string Details
		{
			get;
			set;
		}

		string Category
		{
			get;
			set;
		}

		Ability Strength
		{
			get;
			set;
		}

		Ability Constitution
		{
			get;
			set;
		}

		Ability Dexterity
		{
			get;
			set;
		}

		Ability Intelligence
		{
			get;
			set;
		}

		Ability Wisdom
		{
			get;
			set;
		}

		Ability Charisma
		{
			get;
			set;
		}

		int HP
		{
			get;
			set;
		}

		int Initiative
		{
			get;
			set;
		}

		int AC
		{
			get;
			set;
		}

		int Fortitude
		{
			get;
			set;
		}

		int Reflex
		{
			get;
			set;
		}

		int Will
		{
			get;
			set;
		}

		Regeneration Regeneration
		{
			get;
			set;
		}

		List<Aura> Auras
		{
			get;
			set;
		}

		List<CreaturePower> CreaturePowers
		{
			get;
			set;
		}

		List<DamageModifier> DamageModifiers
		{
			get;
			set;
		}

		string Resist
		{
			get;
			set;
		}

		string Vulnerable
		{
			get;
			set;
		}

		string Immune
		{
			get;
			set;
		}

		string Tactics
		{
			get;
			set;
		}

		Image Image
		{
			get;
			set;
		}

		string Info
		{
			get;
		}

		string Phenotype
		{
			get;
		}
	}
}
