namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public TComponent GetOrDefault<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> Has<TComponent>() ? Get<TComponent>() : default;

		public TValue GetOrDefault<TComponent, TValue>(TValue defaultValue = default)
			where TComponent : ValueComponent<TValue>, IInScope<TScope>, new()
			=> Has<TComponent>() ? Get<TComponent>().Value : defaultValue;

		public Entity<TScope> AddSafety<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
		{
			if (!Has<TComponent>())
				Add<TComponent>();

			return this;
		}

		public Entity<TScope> RemoveSafety<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
		{
			if (Has<TComponent>())
				Remove<TComponent>();

			return this;
		}
	}
}