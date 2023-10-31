using Entitas.Generic;

namespace Sample
{
	[GameScope] public sealed class Player : FlagComponent { }

	[GameScope] public sealed class Item : FlagComponent { }

	[GameScope] public sealed class Id : PrimaryIndexComponent<GameScope, Id, int> { }

	[GameScope] public sealed class Name : ValueComponent<string> { }
}