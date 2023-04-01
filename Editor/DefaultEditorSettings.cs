using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grabli.Labeling.Editor
{
	[CreateAssetMenu(fileName = "EditorLabelingSettings.asset", menuName = "Labeling/Settings/Editor")]
	public class DefaultEditorSettings : ScriptableObject, EditorSettings
	{
		[SerializeField] private List<LabelContentPieceEditorBinding> bindings;

		public void GetAllBindings(IList<LabelContentPieceEditorBinding> bindings)
		{
			for (int i = 0; i < this.bindings.Count; ++i)
			{
				bindings.Add(this.bindings[i]);
			}
		}

		public bool TryGetBinding(LabelContentPiece implementation, out LabelContentPieceEditorBinding binding)
		{
			Type type = implementation.GetType();
			for (int i = 0; i < bindings.Count; ++i)
			{
				if (bindings[i].Implementation == type)
				{
					binding = bindings[i];
					return true;
				}
			}

			binding = default;
			return false;
		}
	}
}
