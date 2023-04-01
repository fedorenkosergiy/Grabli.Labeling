using System;
using System.Collections.Generic;

namespace Grabli.Labeling
{
	public interface LabelingManager
	{
		event Action<LabelContentPiece> OnLabelRegistered;
		event Action<LabelContentPiece> OnLabelUnregistered;

		public Settings Settings { get; }

		void RegisterLabel(LabelContentPiece label);

		void UnregisterLabel(LabelContentPiece label);

		void GetLabelContent(string label, IList<LabelContentPiece> labels);
		void GetLabelContent(int labelHash, IList<LabelContentPiece> labels);
	}
}
