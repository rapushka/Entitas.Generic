namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		public TComponent Get<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> GetEntity<TComponent>().Get<TComponent>();

		public Entity<TScope> GetEntity<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> GetEntityOrDefault<TComponent>() ?? Create<TComponent>();

		public Entity<TScope> GetEntityOrDefault<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> _context.SingleOrDefault(ScopeMatcher<TScope>.Get<TComponent>());
	}
}