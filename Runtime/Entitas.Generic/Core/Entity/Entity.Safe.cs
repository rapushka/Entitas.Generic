using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		[PublicAPI]
		[CanBeNull]
		public TComponent GetOrDefault<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> Has<TComponent>() ? Get<TComponent>() : default;

		[PublicAPI]
		public TValue GetOrDefault<TComponent, TValue>(TValue defaultValue = default)
			where TComponent : ValueComponent<TValue>, IInScope<TScope>, new()
			=> Has<TComponent>() ? Get<TComponent>().Value : defaultValue;

		[PublicAPI]
		public Entity<TScope> AddSafely<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
		{
			if (!Has<TComponent>())
				Add<TComponent>();

			return this;
		}

		[PublicAPI]
		public Entity<TScope> RemoveSafely<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
		{
			if (Has<TComponent>())
				Remove<TComponent>();

			return this;
		}
	}
}