using UnityEngine;

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
			var positionViewPrefab = Resources.Load<PositionView>("Player");
			var positionView = Object.Instantiate(positionViewPrefab);

			_contexts.Get<GameScope>().CreateEntity()
				.Add<Name, string>("Steve")
				.Is<Player>(true)
				.Register(positionView)
				.Add<Position, Vector3>(Vector3.zero)
				;
		}
	}
}