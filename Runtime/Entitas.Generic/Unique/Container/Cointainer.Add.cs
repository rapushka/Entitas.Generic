using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public Entity<TScope> Add<TSelf>(Entity<TScope> entity)
			where TSelf : IUnique<TSelf>, new()
		{
			var component = entity.Create<UniqueComponent<TSelf>>();
			entity.Add(component);
			_uniqueEntities.Add(Id<TSelf>(), entity);

			return entity;
		}

		[PublicAPI]
		public Entity<TScope> Add<TSelf, TValue>(Entity<TScope> entity, TValue value = default)
			where TSelf : IUnique<TSelf, TValue>, new()
		{
			var component = entity.Create<UniqueComponent<TSelf, TValue>>();
			component.Value = value;
			entity.Add(component);

			_uniqueEntities.Add(Id<TSelf, TValue>(), entity);

			return entity;
		}
	}
}