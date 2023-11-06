using System.Collections.Generic;
using JetBrains.Annotations;

namespace Entitas.Generic
{
	public class ListenerComponent<TScope, TComponent> : IComponent
		where TScope : IScope
		where TComponent : IComponent, IEvent
	{
		public List<IListener<TScope, TComponent>> Value;
	}

	public interface IListener<TScope, in TComponent>
		where TScope : IScope
		where TComponent : IComponent, IEvent
	{
		[PublicAPI]
		void OnValueChanged(Entity<TScope> entity, TComponent component);
	}
}