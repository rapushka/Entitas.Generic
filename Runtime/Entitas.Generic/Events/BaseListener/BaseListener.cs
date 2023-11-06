using JetBrains.Annotations;

namespace Entitas.Generic
{
	public interface IRegistrableListener<TScope>
		where TScope : IScope
	{
		void Register(Entity<TScope> entity);
	}

	public interface IRegistrableListener<TScope, in TComponent>
		: IRegistrableListener<TScope>, IListener<TScope, TComponent>
		where TScope : IScope
		where TComponent : IComponent, IEvent, new() { }

	public abstract class BaseListener<TScope, TComponent>
		: UnityEngine.MonoBehaviour, IRegistrableListener<TScope, TComponent>
		where TScope : IScope
		where TComponent : IComponent, IEvent, new()
	{
		[PublicAPI]
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