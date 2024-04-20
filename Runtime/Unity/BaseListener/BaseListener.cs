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
		where TComponent : IComponent, IEvent, IInScope<TScope>, new() { }

#if ENTITAS_GENERIC_UNITY_SUPPORT
	public abstract class BaseListener<TScope> : UnityEngine.MonoBehaviour, IRegistrableListener<TScope>
		where TScope : IScope
	{
		public abstract void Register(Entity<TScope> entity);
	}
#elif GODOT
	public abstract partial class BaseListener<TScope> : Godot.Node, IRegistrableListener<TScope>
		where TScope : IScope
	{
		public abstract void Register(Entity<TScope> entity);
	}
#else
	public abstract class BaseListener<TScope> : IRegistrableListener<TScope>
		where TScope : IScope
	{
		public abstract void Register(Entity<TScope> entity);
	}
#endif

	public abstract partial class BaseListener<TScope, TComponent>
		: BaseListener<TScope>, IRegistrableListener<TScope, TComponent>
		where TScope : IScope
		where TComponent : IComponent, IEvent, IInScope<TScope>, new()
	{
		public Entity<TScope> Entity { get; private set; }

		public override void Register(Entity<TScope> entity)
		{
			Entity = entity;
			entity.AddListener(this);

			if (entity.Has<TComponent>())
				OnValueChanged(entity, entity.Get<TComponent>());
		}

		public abstract void OnValueChanged(Entity<TScope> entity, TComponent component);
	}
}