using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		[PublicAPI]
		public Entity<TScope> AddID<TIDComponent, TComponent>()
			where TIDComponent : ValueComponent<ComponentID<TScope>>, IInScope<TScope>, new()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> AddID<TScope, TIDComponent, TComponent>();

		[PublicAPI]
		public Entity<TScope> AddID<TTargetScope, TIDComponent, TComponent>()
			where TTargetScope : IScope
			where TIDComponent : ValueComponent<ComponentID<TTargetScope>>, IInScope<TScope>, new()
			where TComponent : IComponent, IInScope<TTargetScope>, new()
		{
			var componentID = ComponentID<TTargetScope>.Create<TComponent>();
			Add<TIDComponent, ComponentID<TTargetScope>>(componentID);

			return this;
		}

		[PublicAPI]
		public void AddFromID<TComponentID>(TComponentID component)
			where TComponentID : ComponentID<TScope>
			=> AddComponent(component.Index, CreateComponent(component.Index, component.Type));

		[PublicAPI]
		public void RemoveFromID<TComponentID>(TComponentID component)
			where TComponentID : ComponentID<TScope>
			=> RemoveComponent(component.Index);

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