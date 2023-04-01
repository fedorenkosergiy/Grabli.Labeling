using System;
using UnityEditor;
using UnityEngine;

namespace Grabli.Labeling.Editor
{
	[Serializable]
	public struct LabelContentPieceEditorBinding
	{
		[SerializeField] private MonoScript implementation;
		[SerializeField] private MonoScript drawer;
		[SerializeField] private string createMenuPath;

		public Type Implementation => implementation.GetClass();
		public Type Drawer => drawer.GetClass();
		public string CreateMenuPath => createMenuPath;
	}
}
