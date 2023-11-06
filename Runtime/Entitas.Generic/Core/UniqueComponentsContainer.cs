using System;
using System.Collections.Generic;

namespace Entitas.Generic
{
	public class UniqueComponentsContainer<TScope>
		where TScope : IScope
	{
		private readonly Dictionary<int, Entity<TScope>> _uniqueEntities = new();
		private readonly Func<Entity<TScope>> _entityFactory;

		public UniqueComponentsContainer(Func<Entity<TScope>> entityFactory)
		{
			_entityFactory = entityFactory;
		}

		public Entity<TScope> Add<TComponent>(Entity<TScope> entity)
			where TComponent : IComponent, IUnique, new()
		{
			entity.Add<TComponent>();
			_uniqueEntities.Add(Id<TComponent>(), entity);

			return entity;
		}

		public TComponent Create<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> CreateEntity<TComponent>().Get<TComponent>();

		public Entity<TScope> CreateEntity<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> Add<TComponent>(_entityFactory.Invoke());

		public Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> Remove<TComponent>(GetEntity<TComponent>());

		public Entity<TScope> Remove<TComponent>(Entity<TScope> entity) where TComponent : IComponent, IUnique, new()
		{
			_uniqueEntities.Remove(Id<TComponent>());
			return entity.Remove<TComponent>();
		}

		public void DestroyEntity<TComponent>()
			where TComponent : IComponent, IUnique, new()
		{
			GetEntity<TComponent>().Destroy();
			_uniqueEntities.Remove(Id<TComponent>());
		}

		public Entity<TScope> GetEntity<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> _uniqueEntities[Id<TComponent>()];

		public Entity<TScope> GetEntityOrDefault<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> _uniqueEntities.GetValueOrDefault(Id<TComponent>());

		public TComponent Get<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> GetEntity<TComponent>().Get<TComponent>();

		public TComponent GetOrDefault<TComponent>()
			where TComponent : IComponent, IUnique, new()
		{
			var entity = GetEntityOrDefault<TComponent>();
			return entity is not null ? entity.Get<TComponent>() : default;
		}

		public void Set<TComponent>(bool value)
			where TComponent : IComponent, IUnique, new()
		{
			var entityExists = Has<TComponent>();

			if (value && !entityExists)
				CreateEntity<TComponent>();

			if (!value && entityExists)
				DestroyEntity<TComponent>();
		}

		public bool Has<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> GetEntityOrDefault<TComponent>() is not null;

		private static int Id<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> ComponentIndex<TScope, TComponent>.Value;
	}
}