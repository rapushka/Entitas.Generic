namespace Entitas.Generic
{
	public partial class Entity<TScope> : Entity
		where TScope : IScope
	{
		public Entity<TScope> Add<TComponent>()
			where TComponent : IComponent, new()
			=> Add(Create<TComponent>());

		public Entity<TScope> Add<TComponent>(TComponent component)
			where TComponent : IComponent, new()
		{
			AddComponent(Id<TComponent>(), component);
			return this;
		}

		public TComponent Create<TComponent>()
			where TComponent : IComponent, new()
			=> CreateComponent<TComponent>(Id<TComponent>());

		public TComponent Get<TComponent>()
			where TComponent : IComponent, new()
			=> (TComponent) GetComponent(Id<TComponent>());

		public bool Has<TComponent>()
			where TComponent : IComponent, new()
			=> HasComponent(Id<TComponent>());

		public Entity<TScope> Replace<TComponent>(TComponent component)
			where TComponent : IComponent, new()
		{
			ReplaceComponent(Id<TComponent>(), component);
			return this;
		}

		public Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, new()
		{
			RemoveComponent(Id<TComponent>());
			return this;
		}

		private static int Id<TComponent>() where TComponent : IComponent, new() => ComponentIdx<TScope, TComponent>.Id;
	}
}