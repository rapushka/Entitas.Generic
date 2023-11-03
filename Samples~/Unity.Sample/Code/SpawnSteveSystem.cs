using Entitas;
using Entitas.Generic;

namespace Entitas.Generic.Example
{
	public class SpawnSteveSystem : IInitializeSystem
	{
		private readonly Contexts _contexts;

		public SpawnSteveSystem(Contexts contexts)
		{
			_contexts = contexts;
		}

		public void Initialize()
		{
			var steve = _contexts.Get<GameScope>().CreateEntity();
			steve.Add<Name, string>("Steve");
		}
	}
}