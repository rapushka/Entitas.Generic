using System.Collections.Generic;

namespace Entitas.Generic
{
	public class ListenerComponent<TScope, TComponent> : IComponent
		where TScope : IScope
		where TComponent : IComponent
	{
		public List<IListener<TScope, TComponent>> Value;
	}

	public interface IListener<TScope, in TComponent>
		where TScope : IScope
		where TComponent : IComponent
	{
		void OnValueChanged(Entity<TScope> entity, TComponent component);
	}
}