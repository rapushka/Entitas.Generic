namespace Entitas.Generic
{
	public partial class Entity<TScope>
		where TScope : IScope
	{
		private static ScopeContext<TScope> Context => ContextsBase.Instance.Get<TScope>();

		public Entity<TScope> SetUnique<TComponent>(bool value)
			where TComponent : IComponent, IUnique, new()
		{
			if (value && !Has<TComponent>())
				AddUnique<TComponent>();

			if (!value && Has<TComponent>())
				RemoveUnique<TComponent>();

			return this;
		}

		public Entity<TScope> AddUnique<TComponent>()
			where TComponent : IComponent, IUnique, new()
		{
			Context.Unique.Add<TComponent>(this);
			return this;
		}

		public Entity<TScope> RemoveUnique<TComponent>()
			where TComponent : IComponent, IUnique, new()
		{
			Context.Unique.Remove<TComponent>(this);
			return this;
		}
	}
}