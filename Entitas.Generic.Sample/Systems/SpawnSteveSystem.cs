using Entitas;
using Entitas.Generic;

namespace Sample
{
	public class SpawnSteveSystem : IInitializeSystem
	{
		private readonly Contexts _contexts;

		public SpawnSteveSystem(Contexts contexts)
		{
			_contexts = contexts;
		}

		private Entity<GameScope> NewEntity => _contexts.Get<GameScope>().CreateEntity();

		public void Initialize()
		{
			NewEntity
				.Is<Player>(true)
				.Add<Id, int>(Constants.SteveId)
				.Add<Name, string>("Steve");

			NewEntity
				.Is<Item>(true)
				.Add<Name, string>("Pickaxe")
				.Add<OwnerId, int>(Constants.SteveId);
		}
	}
}