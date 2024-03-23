using System;
using UnityEngine;

namespace Entitas.Generic
{
	[Serializable]
	public abstract class ComponentIDBase
	{
		public abstract int Index { get; }
	}

	[Serializable]
	public class ComponentID<TScope> : ComponentIDBase
		where TScope : IScope
	{
		[SerializeField] private string _name;

		private int? _cashedIndex;

		public override int Index => _cashedIndex ??= IndexOf();

		public Type Type => Lookup.ComponentTypes[Index];

		private static ComponentsLookup<TScope> Lookup => ComponentsLookup<TScope>.Instance;

		public static ComponentID<TScope> Create<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> new() { _name = typeof(TComponent).Name, };

		private int IndexOf()
		{
			var indexOf = Lookup.ComponentNames.IndexOf(_name);
			Debug.Assert(indexOf != -1, $"the component {_name} is lost");

			return indexOf;
		}

		public override string ToString() => ComponentsLookup<TScope>.Instance.ComponentNames[Index];
	}
}