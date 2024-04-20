namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		private Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntity<TComponent>().Remove<TComponent>();

		private Entity<TScope> RemoveSafety<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntityOrDefault<TComponent>()?.Remove<TComponent>();
	}
}