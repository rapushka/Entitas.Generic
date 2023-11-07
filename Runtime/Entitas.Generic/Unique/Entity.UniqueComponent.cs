using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		private static ScopeContext<TScope> Context => Contexts.Instance.Get<TScope>();

		[PublicAPI]
		public TSelf GetUnique<TSelf>()
			where TSelf : IUnique<TSelf>, new()
			=> Get<UniqueComponent<TSelf>>().Self;

		[PublicAPI]
		public TValue GetUnique<TSelf, TValue>()
			where TSelf : IUnique<TSelf, TValue>, new()
			=> Get<UniqueComponent<TSelf, TValue>>().Value;

		[PublicAPI]
		public Entity<TScope> SetUnique<TSelf>(bool value)
			where TSelf : IUnique<TSelf>, new()
		{
			if (value && !Context.Unique.Has<TSelf>())
				AddUnique<TSelf>();

			if (!value && Context.Unique.Has<TSelf>())
				RemoveUnique<TSelf>();

			return this;
		}

		[PublicAPI]
		public Entity<TScope> AddUnique<TSelf>()
			where TSelf : IUnique<TSelf>, new()
		{
			Context.Unique.Add<TSelf>(this);
			return this;
		}

		[PublicAPI]
		public Entity<TScope> AddUnique<TSelf, TValue>(TValue value = default)
			where TSelf : IUnique<TSelf, TValue>, new()
		{
			Context.Unique.Add<TSelf, TValue>(this, value);
			return this;
		}

		[PublicAPI]
		public Entity<TScope> RemoveUnique<TSelf>()
			where TSelf : IUnique<TSelf>, new()
		{
			Context.Unique.Remove<TSelf>(this);
			return this;
		}

		[PublicAPI]
		public Entity<TScope> RemoveUnique<TSelf, TValue>()
			where TSelf : IUnique<TSelf, TValue>, new()
		{
			Context.Unique.Remove<TSelf, TValue>(this);
			return this;
		}
	}
}