#if UNITY_EDITOR

using UnityEngine;

namespace Entitas.Generic
{
	public abstract class EntitasAdapter<TSystems, TContexts> : MonoBehaviour
		where TSystems : Systems, new()
		where TContexts : ContextsBase<TContexts>, new()
	{
		private TSystems _systems;

		private void Start()
		{
			ContextsBase<TContexts>.Instance.Initialize();
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