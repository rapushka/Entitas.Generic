using JetBrains.Annotations;
using UnityEngine;

namespace Entitas.Generic
{
    public abstract class FeatureAdapterBase : MonoBehaviour
    {
        public Systems Systems { get; private set; }

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

            MarkAllEntitiesAsDestroyed();

            Systems.Cleanup();
            Systems.TearDown();
        }

        [PublicAPI]
        protected abstract void MarkAllEntitiesAsDestroyed();
    }

    public abstract class FeatureAdapterBase<TSystems> : FeatureAdapterBase
        where TSystems : Systems, new()
    {
        protected override Systems CreateSystems() => new TSystems();
    }
}