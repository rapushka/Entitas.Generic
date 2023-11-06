using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public TSelf Get<TSelf>()
			where TSelf : IUnique<TSelf, IComponent>, new()
			=> GetEntity<TSelf>().GetUnique<TSelf>();

		[PublicAPI]
		public Entity<TScope> GetEntity<TSelf>()
			where TSelf : IUnique<TSelf, IComponent>, new()
			=> _uniqueEntities[Id<TSelf>()];
	}
}