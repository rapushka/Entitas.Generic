using System;

namespace Entitas.Generic
{
	// The whole IIndexFactory thing is needed only to don't copy-paste code of EntityIndex classes  
	public interface IEntityIndexFactory<TEntity, TKey>
		where TEntity : class, IEntity
	{
		AbstractEntityIndex<TEntity, TKey> Create
		(
			string name,
			IGroup<TEntity> group,
			Func<TEntity, IComponent, TKey> getKey
		);
	}

	public class EntityIndexFactory<TEntity, TKey> : IEntityIndexFactory<TEntity, TKey>
		where TEntity : class, IEntity
	{
		public AbstractEntityIndex<TEntity, TKey> Create
		(
			string name,
			IGroup<TEntity> group,
			Func<TEntity, IComponent, TKey> getKey
		)
			=> new EntityIndex<TEntity, TKey>(name, group, getKey);
	}

	public class PrimaryEntityIndexFactory<TEntity, TKey> : IEntityIndexFactory<TEntity, TKey>
		where TEntity : class, IEntity
	{
		public AbstractEntityIndex<TEntity, TKey> Create
		(
			string name,
			IGroup<TEntity> group,
			Func<TEntity, IComponent, TKey> getKey
		)
			=> new PrimaryEntityIndex<TEntity, TKey>(name, group, getKey);
	}
}