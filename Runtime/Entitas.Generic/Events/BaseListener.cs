namespace Entitas.Generic
{
#if UNITY_EDITOR
	public abstract class BaseListener<TScope, TComponent> : UnityEngine.MonoBehaviour, IListener<TScope, TComponent>
#else
	public abstract class BaseListener<TScope, TComponent> : IListener<TScope, TComponent>
#endif
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