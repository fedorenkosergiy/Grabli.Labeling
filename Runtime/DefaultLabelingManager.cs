using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grabli.Labeling
{
	[CreateAssetMenu(fileName = "LabelingManager.asset", menuName = "Labeling/Manager")]
	public class DefaultLabelingManager : ScriptableObject, LabelingManager
	{
		[SerializeField] [InterfaceRestriction(typeof(Settings))]
		private ScriptableObject settings;

		private readonly ISet<LabelContentPiece> content = new HashSet<LabelContentPiece>();
		private readonly IDictionary<int, IList<LabelContentPiece>> data = new Dictionary<int, IList<LabelContentPiece>>();

		public event Action<LabelContentPiece> OnLabelRegistered;
		public event Action<LabelContentPiece> OnLabelUnregistered;

		public Settings Settings { get; private set; }

		private void OnEnable()
		{
			Settings = (Settings)settings;
		}

		public void RegisterLabel(LabelContentPiece label)
		{
			if (content.Contains(label))
			{
				return;
			}

			IList<LabelContentPiece> list = GetLabelRelatedList(label.LabelHash);
			list.Add(label);
			content.Add(label);
			OnLabelRegistered?.Invoke(label);
		}

		private IList<LabelContentPiece> GetLabelRelatedList(int hash)
		{
			if (data.TryGetValue(hash, out IList<LabelContentPiece> list))
			{
				return list;
			}

			list = new List<LabelContentPiece>();
			data.Add(hash, list);

			return list;
		}

		public void UnregisterLabel(LabelContentPiece label)
		{
			if (!content.Contains(label))
			{
				return;
			}

			IList<LabelContentPiece> list = GetLabelRelatedList(label.LabelHash);
			list.Remove(label);
			content.Remove(label);
			OnLabelUnregistered?.Invoke(label);
		}

		public void GetLabelContent(string label, IList<LabelContentPiece> labels)
		{
			int labelHash = Settings.GetLabelHash(label);
			GetLabelContent(labelHash, labels);
		}

		public void GetLabelContent(int labelHash, IList<LabelContentPiece> labels)
		{
			IList<LabelContentPiece> internalList = GetLabelRelatedList(labelHash);
			for (int i = 0; i < internalList.Count; ++i)
			{
				labels.Add(internalList[i]);
			}
		}
	}
}
