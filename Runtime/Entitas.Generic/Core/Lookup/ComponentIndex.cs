using JetBrains.Annotations;

namespace Entitas.Generic
{
	[UsedImplicitly]
	public class ComponentIndex<TScope, TComponent>
		where TScope : IScope
		where TComponent : IComponent, IInScope<TScope>, new()
	{
		[UsedImplicitly]
		public static int Value = -1;
	}
}