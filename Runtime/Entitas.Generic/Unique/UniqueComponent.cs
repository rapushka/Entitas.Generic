namespace Entitas.Generic
{
	public class UniqueComponent<TSelf> : IComponent
		where TSelf : IUnique<TSelf, IComponent>
	{
		public TSelf Value;
	}

	public interface IUnique<TSelf, out TComponent> where TSelf : IUnique<TSelf, TComponent> { }
}