#if UNITY_EDITOR
using UnityEngine;

namespace Entitas.Generic
{
	public abstract class FeatureAdapter<TSystems> : MonoBehaviour
		where TSystems : Systems, new()
	{
		private TSystems _systems;

		private void Start()
		{
			_systems = new TSystems();
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
}
#endif