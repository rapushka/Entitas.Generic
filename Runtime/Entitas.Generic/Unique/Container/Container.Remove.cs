using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public Entity<TScope> Remove<TSelf>()
			where TSelf : IUnique<TSelf, IComponent>, new()
			=> Remove<TSelf>(GetEntity<TSelf>());

		[PublicAPI]
		public Entity<TScope> Remove<TSelf>(Entity<TScope> entity)
			where TSelf : IUnique<TSelf, IComponent>, new()
		{
			_uniqueEntities.Remove(Id<TSelf>());
			return entity.Remove<UniqueComponent<TSelf>>();
		}
	}
}