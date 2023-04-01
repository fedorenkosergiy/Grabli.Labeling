using System.Collections.Generic;
using UnityEngine;

namespace Grabli.Labeling
{
	public class LabelComponent : MonoBehaviour, ManagerInitializationListener
	{
		[SerializeReference] private List<LabelContentPiece> labels;

		private void Start() => TryRegisterLabels();

		private void TryRegisterLabels()
		{
			if (GrabliLabelingApi.IsInitialized)
			{
				RegisterLabels(GrabliLabelingApi.Manager);
				return;
			}

			StartListeningToInitialization();
		}

		private void RegisterLabels(LabelingManager manager)
		{
			for (int i = 0; i < labels.Count; ++i)
			{
				manager.RegisterLabel(labels[i]);
			}
		}

		private void StartListeningToInitialization()
		{
			GrabliLabelingApi.AddListener(this);
		}

		void ManagerInitializationListener.OnInitialized(LabelingManager manager) => RegisterLabels(manager);

		private void OnDestroy() => TryUnregisterLabels();

		private void TryUnregisterLabels()
		{
			if (GrabliLabelingApi.IsInitialized)
			{
				UnregisterLabels(GrabliLabelingApi.Manager);
				return;
			}

			GrabliLabelingApi.RemoveListener(this);
		}

		private void UnregisterLabels(LabelingManager manager)
		{
			for (int i = 0; i < labels.Count; ++i)
			{
				manager.UnregisterLabel(labels[i]);
			}
		}
	}
}
