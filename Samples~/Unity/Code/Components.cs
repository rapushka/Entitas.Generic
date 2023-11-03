using UnityEngine;

namespace Entitas.Generic.Sample.Unity
{
	[GameScope] public sealed class Name : ValueComponent<string> { }

	[GameScope] public sealed class Player : FlagComponent { }

	[GameScope] public sealed class Position : ValueComponent<Vector3>, IEvent { }
}