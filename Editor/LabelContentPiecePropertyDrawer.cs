using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets.GUI;
using UnityEngine;
using System;

namespace Grabli.Labeling.Editor
{
	[CustomPropertyDrawer(typeof(LabelContentPiece), true)]
	public class LabelContentPiecePropertyDrawer : PropertyDrawer
	{
		private static GUIContent createButton = new GUIContent("Create");
		private static IList<LabelContentPieceEditorBinding> labelEditorBindings;

		private static IDictionary<Type, LabelContentPieceInstanceDrawer> drawers =
			new Dictionary<Type, LabelContentPieceInstanceDrawer>();

		private GenericMenu createMenu;
		private SerializedProperty currentProperty;
		private float currentHeight;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			TryInitializeBindings();
			TryInitializeCreateMenu();
			EditorGUI.BeginProperty(position, label, property);
			currentProperty = property;
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			string labelText = label.text;


			LabelContentPiece piece =
				property.GetActualObjectForSerializedProperty<LabelContentPiece>(fieldInfo, ref labelText);
			if (piece == null)
			{
				if (EditorGUI.DropdownButton(position, createButton, FocusType.Keyboard))
				{
					createMenu.ShowAsContext();
				}

				currentHeight = EditorGUI.GetPropertyHeight(property);
			}
			else
			{
				DrawLabelContentPiece(position, piece);
			}

			EditorGUI.EndProperty();
		}

		private static void TryInitializeBindings()
		{
			if (labelEditorBindings == null)
			{
				labelEditorBindings = new List<LabelContentPieceEditorBinding>();
				LabelingEditorApi.Settings.GetAllBindings(labelEditorBindings);
			}
		}

		private void TryInitializeCreateMenu()
		{
			if (createMenu != null)
			{
				return;
			}

			createMenu = new GenericMenu();
			for (int i = 0; i < labelEditorBindings.Count; ++i)
			{
				LabelContentPieceEditorBinding binding = labelEditorBindings[i];

				GUIContent content = new GUIContent(binding.CreateMenuPath);

				createMenu.AddItem(content, false, InvokeCreateMenuItem, binding);
			}
		}

		private void InvokeCreateMenuItem(object obj)
		{
			Undo.RecordObject(currentProperty.serializedObject.targetObject, "add label content piece");
			LabelContentPieceEditorBinding binding = (LabelContentPieceEditorBinding)obj;
			object labelContentPiece = Activator.CreateInstance(binding.Implementation);
			currentProperty.managedReferenceValue = labelContentPiece;
			currentProperty.serializedObject.ApplyModifiedProperties();
		}

		private void DrawLabelContentPiece(Rect position, LabelContentPiece piece)
		{
			LabelContentPieceInstanceDrawer drawer = GetDrawer(piece);
			drawer.Init(piece);
			currentHeight = drawer.GetHeight(currentProperty);
			drawer.Draw(position, currentProperty);
		}

		private LabelContentPieceInstanceDrawer GetDrawer(LabelContentPiece piece)
		{
			Type type = piece.GetType();
			if (drawers.TryGetValue(type, out LabelContentPieceInstanceDrawer drawer))
			{
				return drawer;
			}

			for (int i = 0; i < labelEditorBindings.Count; ++i)
			{
				LabelContentPieceEditorBinding binding = labelEditorBindings[i];
				if (binding.Implementation == type)
				{
					drawer =
						(LabelContentPieceInstanceDrawer)Activator.CreateInstance(binding.Drawer);
					drawers.Add(binding.Implementation, drawer);
					return drawer;
				}
			}

			return null;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return currentHeight;
		}
	}
}
