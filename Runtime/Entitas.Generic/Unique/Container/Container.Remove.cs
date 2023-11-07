namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		private Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> GetEntity<TComponent>().Remove<TComponent>();

		private Entity<TScope> RemoveSafety<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> GetEntityOrDefault<TComponent>()?.Remove<TComponent>();
	}
}