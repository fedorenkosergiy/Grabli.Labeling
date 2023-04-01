using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Grabli.Labeling.Editor
{
	[CustomPropertyDrawer(typeof(LabelHashFieldAttribute))]
	public class LabelHashFieldPropertyDrawer : PropertyDrawer
	{
		private LabelHashFieldAttribute Attribute => (LabelHashFieldAttribute)attribute;
		private static List<string> allLabels = new List<string>();

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);


			if (fieldInfo.FieldType == typeof(int))
			{
				Settings settings = GetSettings();

				settings.GetAllLabels(allLabels);
				string[] labels = allLabels.ToArray();
				string selectedLabel = settings.GetLabel(property.intValue);
				int selectedIndex = allLabels.IndexOf(selectedLabel);
				int nextIndex = EditorGUI.Popup(position, selectedIndex, labels);
				if (nextIndex != selectedIndex)
				{
					selectedLabel = allLabels[nextIndex];
					property.intValue = settings.GetLabelHash(selectedLabel);
				}
			}
			else
			{
				string message = "Not supported property type " + property.managedReferenceFieldTypename;
				EditorGUI.HelpBox(position, message, MessageType.Error);
			}

			EditorGUI.EndProperty();
		}

		private Settings GetSettings()
		{
			string path = AssetDatabase.GUIDToAssetPath(Attribute.LabelingSettingsAssetGuid);
			Object asset = AssetDatabase.LoadAssetAtPath(path, typeof(ScriptableObject));
			return (Settings)asset;
		}
	}
}
