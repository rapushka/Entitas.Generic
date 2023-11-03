namespace Entitas.Generic.Sample.Unity
{
	public sealed class GameFeature : Feature
	{
		public GameFeature() : base(nameof(GameFeature))
		{
			var contexts = Contexts.Instance;

			Add(new SpawnSteveSystem(contexts));
		}
	}
}