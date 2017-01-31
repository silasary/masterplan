using System;

namespace Masterplan.Data
{
	public interface IElement
	{
		int GetXP();

		Difficulty GetDifficulty(int party_level, int party_size);

		IElement Copy();
	}
}
