using System.Collections.Generic;
using UnityEngine;

namespace Grabli.Labeling
{
	[CreateAssetMenu(fileName = "LabelingSettings.asset", menuName = "Labeling/Settings/Runtime")]
	public class DefaultSettings : ScriptableObject, Settings
	{
		[NonReorderable] [SerializeField] private List<string> labels;

		public int GetLabelHash(string label) => labels.IndexOf(label);

		public string GetLabel(int labelHash) => labels[labelHash];

		public void GetAllLabels(IList<string> allLabels)
		{
			for (int i = 0; i < labels.Count; ++i)
			{
				allLabels.Add(labels[i]);
			}
		}
	}
}
