using System;
using UnityEngine;

namespace Grabli.Labeling
{
	[AttributeUsage(AttributeTargets.Field)]
	public class LabelHashFieldAttribute : PropertyAttribute
	{
		public readonly string LabelingSettingsAssetGuid;

		public LabelHashFieldAttribute(string labelingSettingsAssetGuid)
		{
			LabelingSettingsAssetGuid = labelingSettingsAssetGuid;
		}
	}
}
