namespace Entitas.Generic
{
	public class UniqueComponent<TSelf> : IComponent
		where TSelf : IUnique<TSelf, IComponent>
	{
		public TSelf Value;
	}

	public interface IUnique<TSelf, out TComponent> : IUnique
		where TSelf : IUnique<TSelf, TComponent> { }

	public interface IUnique { }
}