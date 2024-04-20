namespace Entitas.Generic
{
	public interface IEvent : IComponent { }

	public interface IEvent<TEventTarget> : IEvent where TEventTarget : IEventTarget { }

	public interface IEventTarget { }

	public class Self : IEventTarget { }

	public class Any : IEventTarget { }
}