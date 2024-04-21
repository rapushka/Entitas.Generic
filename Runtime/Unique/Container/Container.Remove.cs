namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		// ReSharper disable once UnusedMethodReturnValue.Local – Public API
		private Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntity<TComponent>().Remove<TComponent>();

		// ReSharper disable once UnusedMember.Local – Public API
		private Entity<TScope> RemoveSafety<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntityOrDefault<TComponent>()?.Remove<TComponent>();
	}
}