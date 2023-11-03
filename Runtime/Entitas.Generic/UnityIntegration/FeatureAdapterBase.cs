#if UNITY_EDITOR
using UnityEngine;

namespace Entitas.Generic
{
	public abstract class FeatureAdapterBase : MonoBehaviour
	{
		protected abstract Systems Systems { get; }

		private void Start()
		{
			Systems.Initialize();
		}

		private void Update()
		{
			Systems.Execute();
			Systems.Cleanup();
		}

		private void OnDestroy()
		{
			Systems.TearDown();
		}
	}

	public abstract class FeatureAdapterBase<TSystems> : FeatureAdapterBase
		where TSystems : Systems, new()
	{
		protected override Systems Systems => new TSystems();
	}
}
#endif