using Grabli.Labeling.Editor;
using System;
using UnityEditor;
using UnityEngine;
using EditorSettings = Grabli.Labeling.Editor.EditorSettings;

namespace Grabli.Labeling
{
	[CreateAssetMenu(fileName = "EditorOnlyLabelingApiInitializer.asset", menuName = "Labeling/Initializer/EditorOnly")]
	public class EditorOnlyLabelingApiInitializer : ScriptableObject
	{
		private const string findAssetRequest = "t:" + nameof(EditorOnlyLabelingApiInitializer);

		[SerializeField] [InterfaceRestriction(typeof(LabelingManager))]
		private ScriptableObject manager;

		[SerializeField] [InterfaceRestriction(typeof(EditorSettings))]
		private ScriptableObject editorSettings;

		[InitializeOnLoadMethod]
		private static void Initialize()
		{
			if (Application.isBatchMode)
			{
				return;
			}

			string[] guids = AssetDatabase.FindAssets(findAssetRequest);
			if (guids == null || guids.Length == 0)
			{
				return;
			}

			if (guids.Length != 1)
			{
				throw new Exception("expected 1");
			}

			string path = AssetDatabase.GUIDToAssetPath(guids[0]);
			EditorOnlyLabelingApiInitializer instance =
				AssetDatabase.LoadAssetAtPath<EditorOnlyLabelingApiInitializer>(path);
			GrabliLabelingApi.Init((LabelingManager)instance.manager);
			LabelingEditorApi.Init((EditorSettings)instance.editorSettings);
		}
	}
}
