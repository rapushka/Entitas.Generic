using JetBrains.Annotations;

namespace Entitas.Generic
{
    [PublicAPI]
    public class ReferenceComponent<TFrom, TValue> : IndexComponent<TValue>
        where TFrom : PrimaryIndexComponent<TValue> { }

    public class ReferenceComponent<TFrom> : ReferenceComponent<TFrom, int>
        where TFrom : PrimaryIndexComponent<int> { }

    public interface IFromCreationIndex { }
}