using UnityEngine;

namespace Grabli.Labeling
{
	[CreateAssetMenu(fileName = "BuildOnlyLabelingApiInitializer.asset", menuName = "Labeling/Initializer/BuildOnly")]
	public partial class BuildOnlyLabelingApiInitializer : ScriptableObject
	{
		[SerializeField] [InterfaceRestriction(typeof(LabelingManager))]
		private ScriptableObject manager;

		private void OnEnable()
		{
			if (Application.isEditor)
			{
				return;
			}

			GrabliLabelingApi.Init((LabelingManager)manager);
		}
	}
}
