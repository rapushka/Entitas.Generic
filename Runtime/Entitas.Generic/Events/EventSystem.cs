using System.Collections.Generic;

namespace Entitas.Generic
{
	public sealed class EventSystem<TScope, TComponent> : ReactiveSystem<Entity<TScope>>
		where TScope : IScope
		where TComponent : class, IComponent, IEvent, new()
	{
		private readonly List<IListener<TScope, TComponent>> _listenerBuffer;

		public EventSystem(Contexts contexts)
			: base(contexts.Get<TScope>())
		{
			_listenerBuffer = new List<IListener<TScope, TComponent>>();
		}

		protected override ICollector<Entity<TScope>> GetTrigger(IContext<Entity<TScope>> context)
			=> context.CreateCollector(Matcher<TScope>.Get<TComponent>().Added());

		protected override bool Filter(Entity<TScope> entity)
			=> entity.Has<TComponent>() && entity.Has<ListenerComponent<TScope, TComponent>>();

		protected override void Execute(List<Entity<TScope>> entities)
		{
			foreach (var e in entities)
			{
				_listenerBuffer.Clear();
				_listenerBuffer.AddRange(e.Get<ListenerComponent<TScope, TComponent>>().Value);

				foreach (var listener in _listenerBuffer)
					listener.OnValueChanged(e, e.Get<TComponent>());
			}
		}
	}
}