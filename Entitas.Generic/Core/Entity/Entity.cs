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
			AddComponent(ComponentIdx<TScope, TComponent>.Id, component);
			return this;
		}

		public TComponent Create<TComponent>()
			where TComponent : IComponent, new()
			=> CreateComponent<TComponent>(ComponentIdx<TScope, TComponent>.Id);

		public TComponent Get<TComponent>()
			where TComponent : IComponent, new()
			=> (TComponent)GetComponent(ComponentIdx<TScope, TComponent>.Id);

		public bool Has<TComponent>()
			where TComponent : IComponent, new()
			=> HasComponent(ComponentIdx<TScope, TComponent>.Id);

		public Entity<TScope> Replace<TComponent>(TComponent component)
			where TComponent : IComponent, new()
		{
			ReplaceComponent(ComponentIdx<TScope, TComponent>.Id, component);
			return this;
		}

		public Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, new()
		{
			RemoveComponent(ComponentIdx<TScope, TComponent>.Id);
			return this;
		}
	}
}