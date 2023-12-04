using JetBrains.Annotations;

namespace Entitas.Generic
{
	[PublicAPI]
	public class IndexComponent<TComponent, TValue> : ValueComponent<TValue>
		where TComponent : IndexComponent<TComponent, TValue>, new()
	{
		public static EntityIndex<TScope, TComponent, TValue> GetIndex<TScope>()
			where TScope : IScope
			=> EntityIndex<TScope, TComponent, TValue>.Instance;
	}

	[PublicAPI]
	public class IndexComponent<TScope, TComponent, TValue> : IndexComponent<TComponent, TValue>
		where TScope : IScope
		where TComponent : IndexComponent<TScope, TComponent, TValue>, new()
	{
		public static EntityIndex<TScope, TComponent, TValue> Index => GetIndex<TScope>();
	}

	[PublicAPI]
	public class PrimaryIndexComponent<TComponent, TValue> : ValueComponent<TValue>
		where TComponent : PrimaryIndexComponent<TComponent, TValue>, new()
	{
		public static PrimaryEntityIndex<TScope, TComponent, TValue> GetIndex<TScope>()
			where TScope : IScope
			=> PrimaryEntityIndex<TScope, TComponent, TValue>.Instance;
	}

	[PublicAPI]
	public class PrimaryIndexComponent<TScope, TComponent, TValue> : PrimaryIndexComponent<TComponent, TValue>
		where TScope : IScope
		where TComponent : PrimaryIndexComponent<TScope, TComponent, TValue>, new()
	{
		public static PrimaryEntityIndex<TScope, TComponent, TValue> Index => GetIndex<TScope>();
	}
}