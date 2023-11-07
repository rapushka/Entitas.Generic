using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public Entity<TScope> Remove<TSelf>()
			where TSelf : IUnique<TSelf>, new()
			=> Remove<TSelf>(GetEntity<TSelf>());

		[PublicAPI]
		public Entity<TScope> Remove<TSelf, TValue>()
			where TSelf : IUnique<TSelf, TValue>, new()
			=> Remove<TSelf, TValue>(GetEntity<TSelf, TValue>());

		[PublicAPI]
		public Entity<TScope> Remove<TSelf>(Entity<TScope> entity)
			where TSelf : IUnique<TSelf>, new()
		{
			_uniqueEntities.Remove(Id<TSelf>());
			return entity.Remove<UniqueComponent<TSelf>>();
		}

		[PublicAPI]
		public Entity<TScope> Remove<TSelf, TValue>(Entity<TScope> entity)
			where TSelf : IUnique<TSelf, TValue>, new()
		{
			_uniqueEntities.Remove(Id<TSelf, TValue>());
			return entity.Remove<UniqueComponent<TSelf, TValue>>();
		}
	}
}