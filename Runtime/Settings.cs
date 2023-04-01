using System.Collections.Generic;

namespace Grabli.Labeling
{
	public interface Settings
	{
		int GetLabelHash(string label);

		string GetLabel(int labelHash);

		void GetAllLabels(IList<string> allLabels);
	}
}
