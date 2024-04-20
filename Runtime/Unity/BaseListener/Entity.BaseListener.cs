namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public Entity<TScope> Register(IRegistrableListener<TScope> value)
		{
			value.Register(this);
			return this;
		}

		public Entity<TScope> Register<TComponent>(IRegistrableListener<TScope, TComponent> value)
			where TComponent : IComponent, IEvent, IInScope<TScope>, new()
		{
			if (!Has<ListenerComponent<TScope, TComponent>>())
				value.Register(this);

			return this;
		}
	}
}