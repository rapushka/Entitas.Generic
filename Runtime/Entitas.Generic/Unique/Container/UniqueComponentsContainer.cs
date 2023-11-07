using System;
using System.Collections.Generic;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
		where TScope : IScope
	{
		private readonly Dictionary<int, Entity<TScope>> _uniqueEntities = new();
		private readonly Func<Entity<TScope>> _entityFactory;

		public UniqueComponentsContainer(Func<Entity<TScope>> entityFactory)
		{
			_entityFactory = entityFactory;
		}

		private static int Id<TSelf>()
			where TSelf : IUnique<TSelf>, new()
			=> ComponentIndex<TScope, UniqueComponent<TSelf>>.Value;

		private static int Id<TSelf, TValue>()
			where TSelf : IUnique<TSelf, TValue>, new()
			=> ComponentIndex<TScope, UniqueComponent<TSelf, TValue>>.Value;
	}
}