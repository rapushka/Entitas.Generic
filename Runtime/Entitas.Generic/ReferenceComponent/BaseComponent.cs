namespace Entitas.Generic
{
	// ReSharper disable once UnusedTypeParameter - Public API
	public class ReferenceComponent<TFrom, TValue> : IndexComponent<TValue>
		where TFrom : PrimaryIndexComponent<TValue> { }

	public class ReferenceComponent<TFrom> : ReferenceComponent<TFrom, int>
		where TFrom : PrimaryIndexComponent<int> { }

	public interface IFromCreationIndex { }
}