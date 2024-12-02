using System.Collections.Generic;
using JetBrains.Annotations;

namespace Entitas.Generic
{
    [PublicAPI]
    public sealed class SelfFlagEventSystem<TScope, TComponent> : ReactiveSystem<Entity<TScope>>
        where TScope : IScope
        where TComponent : FlagComponent, IEvent<Self>, IInScope<TScope>, new()
    {
        private readonly List<IListener<TScope, TComponent>> _listenerBuffer;

        public SelfFlagEventSystem(Contexts contexts)
            : base(contexts.Get<TScope>())
        {
            _listenerBuffer = new List<IListener<TScope, TComponent>>();
        }

        protected override ICollector<Entity<TScope>> GetTrigger(IContext<Entity<TScope>> context)
            => context.CreateCollector(ScopeMatcher<TScope>.Get<TComponent>().AddedOrRemoved());

        protected override bool Filter(Entity<TScope> entity)
            => entity.Has<ListenerComponent<TScope, TComponent>>();

        protected override void Execute(List<Entity<TScope>> entities)
        {
            foreach (var e in entities)
            {
                _listenerBuffer.Clear();
                _listenerBuffer.AddRange(e.Get<ListenerComponent<TScope, TComponent>>().Value);

                foreach (var listener in _listenerBuffer)
                    listener.OnValueChanged(e, e.GetOrDefault<TComponent>());
            }
        }
    }
}