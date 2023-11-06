using System;
using System.Collections.Generic;
using JetBrains.Annotations;

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

		[PublicAPI]
		public Entity<TScope> Add<TComponent>(Entity<TScope> entity)
			where TComponent : IComponent, IUnique, new()
		{
			entity.Add<TComponent>();
			_uniqueEntities.Add(Id<TComponent>(), entity);

			return entity;
		}

		[PublicAPI]
		public TComponent Create<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> CreateEntity<TComponent>().Get<TComponent>();

		[PublicAPI]
		public Entity<TScope> CreateEntity<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> Add<TComponent>(_entityFactory.Invoke());

		[PublicAPI]
		public Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> Remove<TComponent>(GetEntity<TComponent>());

		[PublicAPI]
		public Entity<TScope> Remove<TComponent>(Entity<TScope> entity)
			where TComponent : IComponent, IUnique, new()
		{
			_uniqueEntities.Remove(Id<TComponent>());
			return entity.Remove<TComponent>();
		}

		[PublicAPI]
		public void DestroyEntity<TComponent>()
			where TComponent : IComponent, IUnique, new()
		{
			GetEntity<TComponent>().Destroy();
			_uniqueEntities.Remove(Id<TComponent>());
		}

		[PublicAPI]
		public Entity<TScope> GetEntity<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> _uniqueEntities[Id<TComponent>()];

		[PublicAPI]
		public Entity<TScope> GetEntityOrDefault<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> _uniqueEntities.GetValueOrDefault(Id<TComponent>());

		[PublicAPI]
		public TComponent Get<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> GetEntity<TComponent>().Get<TComponent>();

		[PublicAPI]
		public TComponent GetOrDefault<TComponent>()
			where TComponent : IComponent, IUnique, new()
		{
			var entity = GetEntityOrDefault<TComponent>();
			return entity is not null ? entity.Get<TComponent>() : default;
		}

		[PublicAPI]
		public void Set<TComponent>(bool value)
			where TComponent : IComponent, IUnique, new()
		{
			var entityExists = Has<TComponent>();

			if (value && !entityExists)
				CreateEntity<TComponent>();

			if (!value && entityExists)
				DestroyEntity<TComponent>();
		}

		[PublicAPI]
		public bool Has<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> GetEntityOrDefault<TComponent>() is not null;

		private static int Id<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> ComponentIndex<TScope, TComponent>.Value;
	}
}