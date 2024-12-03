using System;
using JetBrains.Annotations;

namespace Entitas.Generic
{
    public class PrimaryEntityIndex<TScope, TComponent, TValue>
        : EntityIndexBase<TScope, TComponent, TValue, PrimaryEntityIndexFactory<Entity<TScope>, TValue>,
            PrimaryEntityIndex<TScope, TComponent, TValue>>
        where TScope : IScope
        where TComponent : PrimaryIndexComponent<TValue>, IInScope<TScope>, new()
    {
        [PublicAPI]
        public TComponent Get(TValue value) => GetEntity(value).Get<TComponent>();

        [PublicAPI]
        public bool HasEntity(TValue value) => GetEntityOrDefault(value) is not null;

        [PublicAPI]
        public Entity<TScope> GetEntity(TValue value)
            => GetEntityOrDefault(value)
                ?? throw new ArgumentException($"Has no Entity in index {typeof(TComponent).Name} for value {value}");

        [PublicAPI]
        public Entity<TScope> GetEntityOrDefault(TValue value)
            => ((PrimaryEntityIndex<Entity<TScope>, TValue>)Index).GetEntity(value);

        [PublicAPI]
        public bool TryGetEntity(TValue value, out Entity<TScope> entity)
        {
            entity = GetEntityOrDefault(value);
            return entity is not null;
        }
    }
}