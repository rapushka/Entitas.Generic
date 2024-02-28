using JetBrains.Annotations;

namespace Entitas.Generic
{
	public interface IEvent : IComponent { }

	[PublicAPI]
	public interface IEvent<TEventTarget> : IEvent where TEventTarget : IEventTarget { }

	public interface IEventTarget { }

	[PublicAPI]
	public class Self : IEventTarget { }

	[PublicAPI]
	public class Any : IEventTarget { }
}