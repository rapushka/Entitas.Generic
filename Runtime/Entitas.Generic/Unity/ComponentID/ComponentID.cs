using System;
using UnityEngine;

namespace Entitas.Generic
{
	[Serializable]
	public abstract class ComponentIDBase
	{
		public abstract int Index { get; }
	}

	public class ComponentID<TScope> : ComponentIDBase
		where TScope : IScope
	{
		[SerializeField] private string _name;

		private int? _cashedIndex;

		public override int Index => _cashedIndex ??= GetIndex();

		private int GetIndex()
		{
			var indexOf = ComponentTypeManager<TScope>.ComponentNames.IndexOf(_name);
			Debug.Assert(indexOf != -1, $"the component {_name} is lost");

			return indexOf;
		}
	}
}