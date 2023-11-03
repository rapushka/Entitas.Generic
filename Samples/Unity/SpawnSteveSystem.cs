namespace Entitas.Generic.Sample.Unity
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
			_contexts.Get<GameScope>().CreateEntity()
				.Add<Name, string>("Steve")
				.Is<Player>(true);
		}
	}
}