using System.Collections.Generic;

namespace Entitas.Generic
{
	public class ListenerComponent<TScope, TComponent> : IComponent
		where TScope : IScope
		where TComponent : IComponent, IEvent
	{
		public List<IListener<TScope, TComponent>> Value;
	}
}