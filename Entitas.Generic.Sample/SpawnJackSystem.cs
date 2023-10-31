using Entitas;
using Entitas.Generic;

namespace Sample
{
	public class SpawnJackSystem : IInitializeSystem
	{
		private readonly Contexts _contexts;

		public SpawnJackSystem(Contexts contexts)
		{
			_contexts = contexts;
		}

		private Entity<GameScope> NewEntity => _contexts.Get<GameScope>().CreateEntity();

		public void Initialize()
		{
			NewEntity
				.Is<Player>(true)
				.Add<Id, int>(Constants.JackId)
				.Add<Name, string>("Jack");

			NewEntity
				.Is<Item>(true)
				// .Add<AttachedTo, int>(Constants.JackId)
				;
		}
	}
}