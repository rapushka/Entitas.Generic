namespace Entitas.Generic
{
	public interface IEvent<TEventTarget> : IComponent where TEventTarget : IEventTarget { }

	public interface IEvent : IEvent<SelfTarget> { }

	public interface IEventTarget { }

	public class SelfTarget : IEventTarget { }

	public class AnyTarget : IEventTarget { }
}