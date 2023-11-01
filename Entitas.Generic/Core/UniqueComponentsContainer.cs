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

		public TComponent Create<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> CreateEntity<TComponent>().Get<TComponent>();

		public Entity<TScope> CreateEntity<TComponent>()
			where TComponent : IComponent, IUnique, new()
		{
			var entity = _entityFactory.Invoke().Add<TComponent>();
			_uniqueEntities.Add(Id<TComponent>(), entity);

			return entity;
		}

		public void Remove<TComponent>()
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

			if (value && entityExists)
				CreateEntity<TComponent>();

			if (!value && entityExists)
				Remove<TComponent>();
		}

		public bool Has<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> GetEntityOrDefault<TComponent>() is not null;

		private static int Id<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> ComponentIdx<TScope, TComponent>.Id;
	}
}