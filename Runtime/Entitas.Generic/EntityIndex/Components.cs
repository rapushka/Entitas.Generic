using JetBrains.Annotations;

namespace Entitas.Generic
{
	[PublicAPI]
	public class IndexComponent<TScope, TComponent, TValue> : ValueComponent<TValue>
		where TScope : IScope
		where TComponent : IndexComponent<TScope, TComponent, TValue>, new()
	{
		public static EntityIndex<TScope, TComponent, TValue> Index
			=> EntityIndex<TScope, TComponent, TValue>.Instance;
	}

	[PublicAPI]
	public class PrimaryIndexComponent<TScope, TComponent, TValue> : ValueComponent<TValue>
		where TScope : IScope
		where TComponent : PrimaryIndexComponent<TScope, TComponent, TValue>, new()
	{
		public static PrimaryEntityIndex<TScope, TComponent, TValue> Index
			=> PrimaryEntityIndex<TScope, TComponent, TValue>.Instance;
	}
}