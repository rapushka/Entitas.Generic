using Entitas.Generic;

namespace Sample
{
	[GameScope] public sealed class Player : FlagComponent { }

	[GameScope] public sealed class Id : ValueComponent<int> { }

	[GameScope] public sealed class Name : ValueComponent<string> { }
}