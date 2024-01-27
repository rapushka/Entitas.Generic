using JetBrains.Annotations;

namespace Entitas.Generic
{
	[PublicAPI]
	public class IndexComponent<TValue> : ValueComponent<TValue> { }

	[PublicAPI]
	public class PrimaryIndexComponent<TValue> : ValueComponent<TValue> { }
}