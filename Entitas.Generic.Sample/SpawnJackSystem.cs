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
			var jack = _contexts.Get<GameScope>().CreateEntity();
			jack.Add<Id>().Value = 1;
			jack.Add<Name>().Value = "Jack";
		}
	}
}