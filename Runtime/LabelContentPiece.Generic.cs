using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grabli.Labeling
{
	[Serializable]
	public abstract class LabelContentPiece<T> : LabelContentPiece where T : Component
	{
		internal const string LabelHashFieldName = nameof(labelHash);
		internal const string ComponentFieldName = nameof(components);

		[SerializeField] private int labelHash;
		public int LabelHash => labelHash;

		[SerializeField] private List<T> components;

		public Type ComponentType => typeof(T);

		public int Count => components.Count;

		public object this[int index] => components[index];
	}
}
