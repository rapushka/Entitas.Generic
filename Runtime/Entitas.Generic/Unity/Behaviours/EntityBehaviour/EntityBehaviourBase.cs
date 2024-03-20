using JetBrains.Annotations;
using UnityEngine;

namespace Entitas.Generic
{
	public abstract class EntityBehaviourBase : MonoBehaviour
	{
		[PublicAPI]
		public void Register(Contexts contexts)
		{
			CreateEntity(contexts);
			Initialize();
		}

		// Split into two methods to allow referencing to other Behaviour's Entity without running into null
		// If you wanna register it on your own – better call `void Register(Contexts contexts)`
		public abstract void CreateEntity(Contexts contexts);
		public abstract void Initialize();
	}

	public abstract class EntityBehaviourBase<TScope> : EntityBehaviourBase
		where TScope : IScope
	{
		[PublicAPI]
		public Entity<TScope> Entity { get; private set; }

		public override void CreateEntity(Contexts contexts)
		{
			Entity = contexts.Get<TScope>().CreateEntity();
		}
	}

	public class EntityBehaviour<TScope> : EntityBehaviourBase<TScope>
		where TScope : IScope
	{
		[SerializeField] private ComponentBehaviourBase<TScope>[] _componentBehaviours;
		[SerializeField] private BaseListener<TScope>[] _listeners;
		[SerializeField] private EntityBehaviourBase[] _subEntities;

#if UNITY_EDITOR
		[HideInInspector] [SerializeField] private bool _ensureComponentsCountOnAwake = true;
		[HideInInspector] [SerializeField] private bool _forceSubEntitiesAutoCollect = true;
#endif

#if UNITY_EDITOR
		private void Awake()
		{
			if (_ensureComponentsCountOnAwake)
				EntityBehaviourUtils.CheckComponentsMatch(this);
		}
#endif

		public override void CreateEntity(Contexts contexts)
		{
			base.CreateEntity(contexts);

			foreach (var subEntity in _subEntities)
				subEntity.CreateEntity(contexts);
		}

		public override void Initialize()
		{
			var entity = Entity;

			foreach (var component in _componentBehaviours)
				component.Add(ref entity);

			foreach (var listener in _listeners)
				listener.Register(entity);

			foreach (var subEntity in _subEntities)
				subEntity.Initialize();
		}

#if UNITY_EDITOR
		public static class NameOf
		{
			public const string ComponentBehaviours = nameof(_componentBehaviours);
			public const string Listeners = nameof(_listeners);
			public const string SubEntities = nameof(_subEntities);

			public const string EnsureComponentsCountOnAwake = nameof(_ensureComponentsCountOnAwake);
			public const string ForceSubEntitiesAutoCollect = nameof(_forceSubEntitiesAutoCollect);
		}
#endif
	}
}