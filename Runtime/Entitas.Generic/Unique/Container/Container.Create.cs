using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public TSelf Create<TSelf>()
			where TSelf : IUnique<TSelf>, new()
			=> CreateEntity<TSelf>().Get<UniqueComponent<TSelf>>().Self;

		[PublicAPI]
		public TSelf Create<TSelf, TValue>()
			where TSelf : IUnique<TSelf, TValue>, new()
			=> CreateEntity<TSelf, TValue>().Get<UniqueComponent<TSelf, TValue>>().Self;

		[PublicAPI]
		public Entity<TScope> CreateEntity<TSelf>()
			where TSelf : IUnique<TSelf>, new()
			=> Add<TSelf>(_entityFactory.Invoke());

		[PublicAPI]
		public Entity<TScope> CreateEntity<TSelf, TValue>()
			where TSelf : IUnique<TSelf, TValue>, new()
			=> Add<TSelf, TValue>(_entityFactory.Invoke());
	}
}