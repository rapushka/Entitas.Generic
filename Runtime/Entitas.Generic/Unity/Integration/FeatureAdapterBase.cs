using UnityEngine;

namespace Entitas.Generic
{
	public abstract class FeatureAdapterBase : MonoBehaviour
	{
		private Systems _systems;

		protected abstract Systems CreateSystems();

		private void Start()
		{
			_systems = CreateSystems();
			_systems.Initialize();
		}

		private void Update()
		{
			_systems.Execute();
			_systems.Cleanup();
		}

		private void OnDestroy()
		{
			_systems.TearDown();
		}
	}

	public abstract class FeatureAdapterBase<TSystems> : FeatureAdapterBase
		where TSystems : Systems, new()
	{
		protected override Systems CreateSystems() => new TSystems();
	}
}