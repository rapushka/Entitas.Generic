using System.Collections.Generic;
using System.Linq;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public Entity<TScope> AddListener<TComponent>(IListener<TScope, TComponent> value)
			where TComponent : IComponent, IEvent, IInScope<TScope>, new()
		{
			var listeners = GetOrDefault<ListenerComponent<TScope, TComponent>>()?.Value
			                ?? new List<IListener<TScope, TComponent>>();

			listeners.Add(value);
			ReplaceListener(listeners);
			return this;
		}

		public Entity<TScope> ReplaceListener<TComponent>(List<IListener<TScope, TComponent>> newValue)
			where TComponent : IComponent, IEvent, IInScope<TScope>, new()
		{
			var index = Id<ListenerComponent<TScope, TComponent>>();
			var component = CreateComponent<ListenerComponent<TScope, TComponent>>(index);
			component.Value = newValue;
			ReplaceComponent(index, component);

			return this;
		}

		public Entity<TScope> RemoveListener<TComponent>(IListener<TScope, TComponent> value, bool removeEmpty = true)
			where TComponent : IComponent, IEvent, IInScope<TScope>, new()
		{
			var listeners = Get<ListenerComponent<TScope, TComponent>>().Value;
			listeners.Remove(value);

			if (removeEmpty && !listeners.Any())
				RemoveListener<TComponent>();
			else
				ReplaceListener(listeners);

			return this;
		}

		public Entity<TScope> RemoveListener<TComponent>()
			where TComponent : IComponent, IEvent, IInScope<TScope>, new()
		{
			RemoveComponent(Id<ListenerComponent<TScope, TComponent>>());
			return this;
		}
	}
}