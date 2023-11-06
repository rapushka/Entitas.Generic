using System.Collections.Generic;
using System.Linq;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
		where TScope : IScope
	{
		public bool Has<TComponentID>(TComponentID component)
			where TComponentID : ComponentID<TScope>
			=> HasComponent(component.Index);

		public IComponent Get<TComponentID>(TComponentID component)
			where TComponentID : ComponentID<TScope>
			=> GetComponent(component.Index);

		public bool HasAll<TComponentID>(IEnumerable<TComponentID> components)
			where TComponentID : ComponentID<TScope>
			=> components.All(Has);

		public bool HasAny<TComponentID>(IEnumerable<TComponentID> components)
			where TComponentID : ComponentID<TScope>
			=> components.Any(Has);

		public IEnumerable<IComponent> GetAll<TComponentID>(IEnumerable<TComponentID> components)
			where TComponentID : ComponentID<TScope>
			=> components.Select(Get);
	}
}