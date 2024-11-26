using JetBrains.Annotations;
using UnityEngine;

namespace Entitas.Generic
{
    public abstract class ComponentBehaviourBase<TScope> : MonoBehaviour
        where TScope : IScope
    {
        public abstract void Add(ref Entity<TScope> entity);

        public abstract void Remove(ref Entity<TScope> entity);
    }

    [PublicAPI]
    public abstract class ComponentBehaviour<TScope, TComponent> : ComponentBehaviourBase<TScope>
        where TScope : IScope
        where TComponent : FlagComponent, IInScope<TScope>, new()
    {
        public override void Add(ref Entity<TScope> entity)
        {
            entity.Is<TComponent>(true);
        }

        public override void Remove(ref Entity<TScope> entity)
        {
            entity.Is<TComponent>(false);
        }
    }

    [PublicAPI]
    public abstract class ComponentBehaviour<TScope, TComponent, TValue> : ComponentBehaviourBase<TScope>
        where TScope : IScope
        where TComponent : ValueComponent<TValue>, IInScope<TScope>, new()
    {
        [SerializeField] private TValue _value;

        public override void Add(ref Entity<TScope> entity)
        {
            entity.Add<TComponent, TValue>(_value);
        }

        public override void Remove(ref Entity<TScope> entity)
        {
            entity.Remove<TComponent>();
        }
    }
}