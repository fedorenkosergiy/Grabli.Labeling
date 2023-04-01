using UnityEditor;
using UnityEditor.AddressableAssets.GUI;
using UnityEngine;

namespace Grabli.Labeling.Editor
{
	[CustomPropertyDrawer(typeof(InterfaceRestrictionAttribute))]
	public class InterfaceRestrictionPropertyDrawer : PropertyDrawer
	{
		private InterfaceRestrictionAttribute Attribute => (InterfaceRestrictionAttribute)attribute;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			string labelText = label.text;

			if (fieldInfo.FieldType == typeof(ScriptableObject))
			{
				Object prevValue =
					property.GetActualObjectForSerializedProperty<ScriptableObject>(fieldInfo, ref labelText);
				Object nextValue = EditorGUI.ObjectField(position, prevValue, Attribute.Type, false);
				if (nextValue != prevValue)
				{
					property.objectReferenceValue = nextValue;
				}
			}
			else
			{
				string message = "Not supported property type " + property.managedReferenceFieldTypename;
				EditorGUI.HelpBox(position, message, MessageType.Error);
			}

			EditorGUI.EndProperty();
		}
	}
}
