namespace Entitas.Generic
{
	public class ComponentIdx<TScope, TComponent>
		where TScope : IScope
		where TComponent : IComponent, new()
	{
		// ReSharper disable once UnassignedField.Global - used by Reflection
		public static int Id;
	}
}