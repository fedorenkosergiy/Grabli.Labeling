using JetBrains.Annotations;
using System.Collections.Generic;
using Grabli.Labeling;

[PublicAPI]
public static class GrabliLabelingApi
{
	private static ISet<ManagerInitializationListener> uniqueListeners =
		new HashSet<ManagerInitializationListener>();

	private static IList<ManagerInitializationListener> listeners =
		new List<ManagerInitializationListener>();

	public static LabelingManager Manager { get; private set; }

	public static bool IsInitialized => Manager != null;

	public static void AddListener(ManagerInitializationListener listener)
	{
		if (IsInitialized)
		{
			return;
		}

		if (uniqueListeners.Contains(listener))
		{
			return;
		}

		uniqueListeners.Add(listener);
		listeners.Add(listener);
	}

	public static void RemoveListener(ManagerInitializationListener listener)
	{
		if (IsInitialized)
		{
			return;
		}

		if (!uniqueListeners.Contains(listener))
		{
			return;
		}

		uniqueListeners.Remove(listener);
		listeners.Remove(listener);
	}

	public static void Init(LabelingManager manager)
	{
		if (IsInitialized)
		{
			return;
		}

		Manager = manager;
		if (IsInitialized)
		{
			for (int i = 0; i < listeners.Count; ++i)
			{
				listeners[i].OnInitialized(manager);
			}
		}

		listeners = null;
		uniqueListeners = null;
	}
}
