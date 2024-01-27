using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		private Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntity<TComponent>().Remove<TComponent>();

		[PublicAPI]
		private Entity<TScope> RemoveSafety<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntityOrDefault<TComponent>()?.Remove<TComponent>();
	}
}