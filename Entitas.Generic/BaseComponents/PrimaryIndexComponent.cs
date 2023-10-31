namespace Entitas.Generic
{
	public class PrimaryIndexComponent<TScope, TComponent, TValue> : ValueComponent<TValue>
		where TScope : IScope
		where TComponent : PrimaryIndexComponent<TScope, TComponent, TValue>, new()
	{
		public static PrimaryEntityIndex Index => (PrimaryEntityIndex)PrimaryEntityIndex.Instance;

		public class PrimaryEntityIndex : PrimaryEntityIndex<TScope, TComponent, TValue> { }
	}
}