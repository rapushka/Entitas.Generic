using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public bool Has<TSelf>()
			where TSelf : IUnique<TSelf>, new()
			=> _uniqueEntities.ContainsKey(Id<TSelf>());
	}
}