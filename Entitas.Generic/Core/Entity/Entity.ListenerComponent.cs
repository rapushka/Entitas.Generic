using System.Collections.Generic;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
		where TScope : IScope
	{
		public Entity<TScope> AddListener<TComponent>(IListener<TScope, TComponent> value)
			where TComponent : IComponent, new()
		{
			var listeners = Has<ListenerComponent<TScope, TComponent>>()
				? Get<ListenerComponent<TScope, TComponent>>().Value
				: new List<IListener<TScope, TComponent>>();

			listeners.Add(value);
			ReplaceListener(listeners);
			return this;
		}

		public Entity<TScope> ReplaceListener<TComponent>(List<IListener<TScope, TComponent>> newValue)
			where TComponent : IComponent, new()
		{
			var index = Id<ListenerComponent<TScope, TComponent>>();
			var component = CreateComponent<ListenerComponent<TScope, TComponent>>(index);
			component.Value = newValue;
			ReplaceComponent(index, component);
			return this;
		}

		public Entity<TScope> RemoveListener<TComponent>(IListener<TScope, TComponent> value, bool removeComponentWhenEmpty = true)
			where TComponent : IComponent, new()
		{
			var listeners = Get<ListenerComponent<TScope, TComponent>>().Value;
			listeners.Remove(value);

			if (removeComponentWhenEmpty && listeners.Count == 0)
				RemoveListener<TComponent>();
			else
				ReplaceListener(listeners);

			return this;
		}

		public Entity<TScope> RemoveListener<TComponent>()
			where TComponent : IComponent, new()
		{
			RemoveComponent(Id<ListenerComponent<TScope, TComponent>>());
			return this;
		}
	}
}