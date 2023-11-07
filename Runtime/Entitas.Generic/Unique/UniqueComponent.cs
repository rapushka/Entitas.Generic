namespace Entitas.Generic
{
	public class UniqueComponent<TSelf> : FlagComponent
		where TSelf : IUnique<TSelf>, new()
	{
		public readonly TSelf Self = new();
	}

	public class UniqueComponent<TSelf, TValue> : ValueComponent<TValue>
		where TSelf : IUnique<TSelf, TValue>, new()
	{
		public readonly TSelf Self = new();
	}

	public interface IUnique<TSelf> : IUnique // FlagComponent
		where TSelf : IUnique<TSelf> { }

	public interface IUnique<TSelf, TValue> : IUnique
		where TSelf : IUnique<TSelf, TValue> { }

	public interface IUnique { }
}