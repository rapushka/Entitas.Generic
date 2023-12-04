using System.Collections.Generic;
using JetBrains.Annotations;

namespace Entitas.Generic
{
	[PublicAPI]
	public sealed class AnyEventSystem<TScope, TComponent> : ReactiveSystem<Entity<TScope>>
		where TScope : IScope
		where TComponent : class, IComponent, IEvent<Any>, new()
	{
		private readonly IGroup<Entity<TScope>> _listeners;
		private readonly List<Entity<TScope>> _entityBuffer;
		private readonly List<IListener<TScope, TComponent>> _listenerBuffer;

		public AnyEventSystem(Contexts contexts) : base(contexts.Get<TScope>())
		{
			_listeners = contexts.Get<TScope>()
			                     .GetGroup(ScopeMatcher<TScope>.Get<ListenerComponent<TScope, TComponent>>());
			_entityBuffer = new List<Entity<TScope>>();
			_listenerBuffer = new List<IListener<TScope, TComponent>>();
		}

		protected override ICollector<Entity<TScope>> GetTrigger(IContext<Entity<TScope>> context)
			=> context.CreateCollector(ScopeMatcher<TScope>.Get<TComponent>().Added());

		protected override bool Filter(Entity<TScope> entity) => entity.Has<TComponent>();

		protected override void Execute(List<Entity<TScope>> entities)
		{
			foreach (var e in entities)
			{
				var component = e.Get<TComponent>();
				foreach (var listenerEntity in _listeners.GetEntities(_entityBuffer))
				{
					_listenerBuffer.Clear();
					_listenerBuffer.AddRange(listenerEntity.Get<ListenerComponent<TScope, TComponent>>().Value);

					foreach (var listener in _listenerBuffer)
						listener.OnValueChanged(e, component);
				}
			}
		}
	}
}