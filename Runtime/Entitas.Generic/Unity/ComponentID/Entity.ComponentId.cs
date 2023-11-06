using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		[PublicAPI]
		public bool Has<TComponentID>(TComponentID component)
			where TComponentID : ComponentID<TScope>
			=> HasComponent(component.Index);

		[PublicAPI]
		public IComponent Get<TComponentID>(TComponentID component)
			where TComponentID : ComponentID<TScope>
			=> GetComponent(component.Index);

		[PublicAPI]
		public bool HasAll<TComponentID>(IEnumerable<TComponentID> components)
			where TComponentID : ComponentID<TScope>
			=> components.All(Has);

		[PublicAPI]
		public bool HasAny<TComponentID>(IEnumerable<TComponentID> components)
			where TComponentID : ComponentID<TScope>
			=> components.Any(Has);

		[PublicAPI]
		public IEnumerable<IComponent> GetAll<TComponentID>(IEnumerable<TComponentID> components)
			where TComponentID : ComponentID<TScope>
			=> components.Select(Get);
	}
}