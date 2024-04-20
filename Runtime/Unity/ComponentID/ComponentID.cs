using System;

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
#if ENTITAS_GENERIC_UNITY_SUPPORT
		[UnityEngine.SerializeField] public string Name { get; private set;}
#elif GODOT
		[Godot.Export] private string Name { get; set; }
#else
		public string Name;
#endif

		private int? _cashedIndex;

		protected ComponentID() { }

		public override int Index => _cashedIndex ??= IndexOf();

		public Type Type => Lookup.ComponentTypes[Index];

		private static ComponentsLookup<TScope> Lookup => ComponentsLookup<TScope>.Instance;

		public static ComponentID<TScope> Create<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> new() { Name = typeof(TComponent).Name, };

		private int IndexOf()
		{
			var indexOf = Lookup.ComponentNames.IndexOf(Name);
			if (indexOf == -1)
				throw new InvalidOperationException($"the component {Name} is lost");

			return indexOf;
		}

		public override string ToString() => ComponentsLookup<TScope>.Instance.ComponentNames[Index];
	}
}