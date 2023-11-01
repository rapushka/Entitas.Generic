namespace Entitas.Generic
{
	public abstract class BaseListener<TScope, TComponent> : IListener<TScope, TComponent>
		where TScope : IScope
		where TComponent : IComponent, new()
	{
		public Entity<TScope> Entity { get; private set; }

		public void Register(Entity<TScope> entity)
		{
			Entity = entity;
			entity.AddListener(this);

			if (entity.Has<TComponent>())
				OnValueChanged(entity, entity.Get<TComponent>());
		}

		public abstract void OnValueChanged(Entity<TScope> entity, TComponent component);
	}
}