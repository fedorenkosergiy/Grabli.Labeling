using JetBrains.Annotations;

namespace Grabli.Labeling.Editor
{
	[PublicAPI]
	public static class LabelingEditorApi
	{
		public static EditorSettings Settings { get; private set; }

		public static bool IsInitialized => Settings != null;

		public static void Init(EditorSettings settings)
		{
			if (IsInitialized)
			{
				return;
			}

			Settings = settings;
		}
	}
}
