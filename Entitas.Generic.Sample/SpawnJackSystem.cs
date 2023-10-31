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

		public void Initialize()
		{
			_contexts
				.Get<GameScope>()
				.CreateEntity()
				.Is<Player>(true)
				.Add<Id, int>(1)
				.Add<Name, string>("Jack");
		}
	}
}