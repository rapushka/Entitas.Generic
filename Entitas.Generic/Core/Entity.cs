namespace Entitas.Generic
{
	public class Entity<TScope> : Entity
		where TScope : IScope
	{
		public TComponent Add<TComponent>()
			where TComponent : IComponent, new()
		{
			Add(Create<TComponent>());
			return Get<TComponent>();
		}

		public Entity<TScope> Add<TComponent>(TComponent component)
			where TComponent : IComponent, new()
		{
			var index = ComponentIdx<TScope, TComponent>.Id;
			AddComponent(index, component);
			return this;
		}

		public Entity<TScope> Replace<TComponent>(TComponent component)
			where TComponent : IComponent, new()
		{
			var index = ComponentIdx<TScope, TComponent>.Id;
			ReplaceComponent(index, component);
			return this;
		}

		public TComponent Create<TComponent>()
			where TComponent : IComponent, new()
			=> CreateComponent<TComponent>(ComponentIdx<TScope, TComponent>.Id);

		public Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, new()
		{
			RemoveComponent(ComponentIdx<TScope, TComponent>.Id);
			return this;
		}

		public TComponent Get<TComponent>()
			where TComponent : IComponent, new()
			=> (TComponent)GetComponent(ComponentIdx<TScope, TComponent>.Id);

		public bool Has<TComponent>()
			where TComponent : IComponent, new()
			=> HasComponent(ComponentIdx<TScope, TComponent>.Id);
	}
}