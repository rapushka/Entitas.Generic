using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public TSelf Create<TSelf>()
			where TSelf : IUnique<TSelf, IComponent>, new()
			=> CreateEntity<TSelf>().Get<UniqueComponent<TSelf>>().Value;

		[PublicAPI]
		public Entity<TScope> CreateEntity<TSelf>()
			where TSelf : IUnique<TSelf, IComponent>, new()
			=> Add<TSelf>(_entityFactory.Invoke());
	}
}