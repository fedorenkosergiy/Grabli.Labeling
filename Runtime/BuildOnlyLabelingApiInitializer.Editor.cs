#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Grabli.Labeling
{
	public partial class BuildOnlyLabelingApiInitializer
	{
		private const string findAssetRequest = "t:" + nameof(BuildOnlyLabelingApiInitializer);

		[InitializeOnLoadMethod]
		private static void AddInitializerToPreloadedAssets()
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
			BuildOnlyLabelingApiInitializer instance =
				AssetDatabase.LoadAssetAtPath<BuildOnlyLabelingApiInitializer>(path);
			List<Object> preloadedAssets = new List<Object>(PlayerSettings.GetPreloadedAssets());
			if (preloadedAssets.Contains(instance))
			{
				return;
			}

			preloadedAssets.Add(instance);
			PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
			AssetDatabase.SaveAssets();
		}
	}
}
#endif
