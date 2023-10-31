namespace Entitas.Generic
{
	public class PrimaryIndexComponent<TScope, TComponent, TValue> : ValueComponent<TValue>
		where TScope : IScope
		where TComponent : PrimaryIndexComponent<TScope, TComponent, TValue>, new()
	{
		public static PrimaryEntityIndex<TScope, TComponent, TValue> Index
			=> PrimaryEntityIndex<TScope, TComponent, TValue>.Instance;
	}
}