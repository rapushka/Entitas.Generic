using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		[PublicAPI]
		public Entity<TScope> Register(IRegistrableListener<TScope> value)
		{
			value.Register(this);
			return this;
		}

		[PublicAPI]
		public Entity<TScope> Register<TComponent>(IRegistrableListener<TScope, TComponent> value)
			where TComponent : IComponent, new()
		{
			if (!Has<ListenerComponent<TScope, TComponent>>())
				value.Register(this);

			return this;
		}
	}
}