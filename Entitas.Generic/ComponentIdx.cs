namespace Entitas.Generic
{
	// ReSharper disable UnusedTypeParameter - used by Reflection
	// ReSharper disable once UnassignedField.Global - used by Reflection
	public class ComponentIdx<TScope, TComponent>
		where TScope : IScope
		where TComponent : IComponent, new()
	{
		public static int Id;
	}
}