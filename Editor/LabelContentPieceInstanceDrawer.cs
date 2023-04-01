using UnityEditor;
using UnityEngine;

namespace Grabli.Labeling.Editor
{
	public interface LabelContentPieceInstanceDrawer
	{
		void Init(LabelContentPiece piece);
		void Draw(Rect position, SerializedProperty property);
		float GetHeight(SerializedProperty property);
	}
}
