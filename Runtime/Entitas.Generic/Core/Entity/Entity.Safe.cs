using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		[PublicAPI]
		public Entity<TScope> AddSafety<TComponent>()
			where TComponent : IComponent, new()
		{
			if (!Has<TComponent>())
				Add<TComponent>();

			return this;
		}

		[PublicAPI]
		public TComponent GetOrDefault<TComponent>()
			where TComponent : IComponent, new()
			=> Has<TComponent>() ? Get<TComponent>() : default;

		[PublicAPI]
		public Entity<TScope> RemoveSafety<TComponent>()
			where TComponent : IComponent, new()
		{
			if (Has<TComponent>())
				Remove<TComponent>();

			return this;
		}
	}
}