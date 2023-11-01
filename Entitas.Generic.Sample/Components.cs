using Entitas.Generic;

namespace Sample
{
	[GameScope] public sealed class Player : FlagComponent { }

	[GameScope] public sealed class Item : FlagComponent { }

	[GameScope] public sealed class Id : PrimaryIndexComponent<GameScope, Id, int> { }

	[GameScope] public sealed class OwnerId : IndexComponent<GameScope, OwnerId, int> { }

	[GameScope] public sealed class Name : ValueComponent<string> { }

	[GameScope] public sealed class Damaged : ValueComponent<int>, ICleanup<RemoveComponent> { }

	[GameScope] public sealed class Dead : FlagComponent, ICleanup<DestroyEntity> { }

	[GameScope] public sealed class CurrentPlayer : FlagComponent, IUnique { }

	[GameScope] public sealed class Position : ValueComponent<Vector2>, IEvent { }
	// [GameScope] public sealed class PositionListener : ListenerComponent<GameScope, Position> { }
}