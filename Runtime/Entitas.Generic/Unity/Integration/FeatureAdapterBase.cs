using JetBrains.Annotations;
using UnityEngine;

namespace Entitas.Generic
{
    public abstract class FeatureAdapterBase : MonoBehaviour
    {
        private Systems Systems { get; set; }

        protected abstract Systems CreateSystems();

        private void Start()
        {
            Systems = CreateSystems();
            Systems.Initialize();
        }

        private void Update()
        {
            Systems.Execute();
            Systems.Cleanup();
        }

        private void OnDestroy()
        {
            Systems.DeactivateReactiveSystems();
            Systems.ClearReactiveSystems();

            Dispose();

            Systems.Cleanup();
            Systems.TearDown();
        }

        /// This method is called after the reactive system deactivation and before the last Cleanup and TearDown.
        /// So you can override it if you need to mark all entities with your Destroy component or something.
        [PublicAPI]
        protected virtual void Dispose() { }
    }

    public abstract class FeatureAdapterBase<TSystems> : FeatureAdapterBase
        where TSystems : Systems, new()
    {
        protected override Systems CreateSystems() => new TSystems();
    }
}