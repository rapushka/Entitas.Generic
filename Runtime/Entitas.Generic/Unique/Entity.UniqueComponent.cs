using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		private static ScopeContext<TScope> Context => Contexts.Instance.Get<TScope>();

		[PublicAPI]
		public TSelf GetUnique<TSelf>()
			where TSelf : IUnique<TSelf, IComponent>, new()
			=> Get<UniqueComponent<TSelf>>().Value;

		[PublicAPI]
		public Entity<TScope> SetUnique<TSelf>(bool value)
			where TSelf : IUnique<TSelf, IComponent>, new()
		{
			if (value && !Context.Unique.Has<TSelf>())
				AddUnique<TSelf>();

			if (!value && Context.Unique.Has<TSelf>())
				RemoveUnique<TSelf>();

			return this;
		}

		[PublicAPI]
		public Entity<TScope> AddUnique<TSelf>()
			where TSelf : IUnique<TSelf, IComponent>, new()
		{
			Context.Unique.Add<TSelf>(this);
			return this;
		}

		[PublicAPI]
		public Entity<TScope> RemoveUnique<TSelf>()
			where TSelf : IUnique<TSelf, IComponent>, new()
		{
			Context.Unique.Remove<TSelf>(this);
			return this;
		}
	}
}