using System;

namespace Grabli.Labeling
{
	public interface LabelContentPiece
	{
		int LabelHash { get; }
		Type ComponentType { get; }

		public int Count { get; }

		object this[int index] { get; }
	}
}
