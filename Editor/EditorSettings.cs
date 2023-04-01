using System.Collections.Generic;

namespace Grabli.Labeling.Editor
{
	public interface EditorSettings
	{
		public void GetAllBindings(IList<LabelContentPieceEditorBinding> types);
		public bool TryGetBinding(LabelContentPiece implementation, out LabelContentPieceEditorBinding binding);
	}
}
