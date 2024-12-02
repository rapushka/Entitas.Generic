using JetBrains.Annotations;

namespace Entitas.Generic
{
    public abstract class BaseFlagListener<TScope, TComponent>
        : BaseListener<TScope>, IRegistrableListener<TScope, TComponent>
        where TScope : IScope
        where TComponent : FlagComponent, IEvent, IInScope<TScope>, new()
    {
        [PublicAPI]
        public Entity<TScope> Entity { get; private set; }

        public override void Register(Entity<TScope> entity)
        {
            Entity = entity;
            Entity.Retain(this);

            entity.AddListener(this);
            OnValueChanged(entity);
        }

        public void OnValueChanged(Entity<TScope> entity, TComponent component) => OnValueChanged(entity);

        protected abstract void OnValueChanged(Entity<TScope> entity);

        public override void Unregister()
        {
            Entity.RemoveListener(this);

            Entity.Release(this);
            Entity = null;
        }
    }
}