using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public TSelf Get<TSelf>()
			where TSelf : IUnique<TSelf>, new()
			=> GetEntity<TSelf>().GetUnique<TSelf>();

		[PublicAPI]
		public TValue Get<TSelf, TValue>()
			where TSelf : IUnique<TSelf, TValue>, new()
			=> GetEntity<TSelf, TValue>().GetUnique<TSelf, TValue>();

		[PublicAPI]
		public Entity<TScope> GetEntity<TSelf>()
			where TSelf : IUnique<TSelf>, new()
			=> _uniqueEntities[Id<TSelf>()];

		[PublicAPI]
		public Entity<TScope> GetEntity<TSelf, TValue>()
			where TSelf : IUnique<TSelf, TValue>, new()
			=> _uniqueEntities[Id<TSelf, TValue>()];
	}
}