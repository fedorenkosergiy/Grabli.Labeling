using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Grabli.Labeling.Editor
{
	public abstract class LabelContentPieceDrawer<T> : LabelContentPieceInstanceDrawer where T : Component
	{
		private const string sizeFieldName = "size";
		private const string dataFieldName = "data";
		private string[] allLabels;

		public void Init(LabelContentPiece piece)
		{
			List<string> labels = new List<string>();
			GrabliLabelingApi.Manager.Settings.GetAllLabels(labels);
			allLabels = labels.ToArray();
		}

		public void Draw(Rect position, SerializedProperty property)
		{
			SerializedProperty copy = property.Copy();
			IEnumerator enumerator = copy.GetEnumerator();
			while (enumerator.MoveNext())
			{
				SerializedProperty child = enumerator.Current as SerializedProperty;
				if (child == null)
				{
					continue;
				}

				switch (child.name)
				{
					case LabelContentPiece<T>.LabelHashFieldName:
						position = DrawLabel(position, child);
						break;
					case LabelContentPiece<T>.ComponentFieldName:
						EditorGUI.PropertyField(position, child, true);
						break;
					case sizeFieldName:
						break;
					case dataFieldName:
						break;
					default:
						Debug.LogWarning("Not covered child: " + child.name);
						EditorGUI.PropertyField(position, child, true);
						break;
				}
			}
		}

		private Rect DrawLabel(Rect position, SerializedProperty property)
		{
			Rect localPosition = position;
			float height = EditorGUI.GetPropertyHeight(property);
			localPosition.height = height;
			property.intValue = EditorGUI.Popup(localPosition, "Label", property.intValue, allLabels);
			position.y += localPosition.height;
			position.height -= localPosition.height;
			return position;
		}

		public float GetHeight(SerializedProperty property)
		{
			float result = 0;
			SerializedProperty copy = property.Copy();
			IEnumerator enumerator = copy.GetEnumerator();
			while (enumerator.MoveNext())
			{
				SerializedProperty child = (SerializedProperty)enumerator.Current;
				if (child.name == dataFieldName || child.name == sizeFieldName)
				{
					continue;
				}
				result += EditorGUI.GetPropertyHeight(child, true);
			}

			return result;
		}
	}
}
