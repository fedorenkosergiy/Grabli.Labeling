using UnityEngine;
using System;

namespace Grabli.Labeling
{
	public class InterfaceRestrictionAttribute : PropertyAttribute
	{
		public readonly Type Type;

		public InterfaceRestrictionAttribute(Type type)
		{
			if (!type.IsInterface)
			{
				throw new ArgumentException($"Interface type expected", nameof(type));
			}
			Type = type;
		}
	}
}
