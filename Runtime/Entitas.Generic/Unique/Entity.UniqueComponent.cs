using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		private static ScopeContext<TScope> Context => Contexts.Instance.Get<TScope>();

		[PublicAPI]
		public Entity<TScope> SetUnique<TComponent>(bool value)
			where TComponent : IComponent, IUnique, new()
		{
			if (value && !Has<TComponent>())
				AddUnique<TComponent>();

			if (!value && Has<TComponent>())
				RemoveUnique<TComponent>();

			return this;
		}

		[PublicAPI]
		public Entity<TScope> AddUnique<TComponent>()
			where TComponent : IComponent, IUnique, new()
		{
			Context.Unique.Add<TComponent>(this);
			return this;
		}

		[PublicAPI]
		public Entity<TScope> RemoveUnique<TComponent>()
			where TComponent : IComponent, IUnique, new()
		{
			Context.Unique.Remove<TComponent>(this);
			return this;
		}
	}
}