namespace Entitas.Generic
{
	// ReSharper disable once UnusedTypeParameter – Used implicitly
	public class ComponentIndex<TScope, TComponent>
		where TScope : IScope
		where TComponent : IComponent, IInScope<TScope>, new()
	{
		public static int Value = -1;
	}
}