#if ENTITAS_GENERIC_UNITY_SUPPORT
using UnityEngine;
#elif GODOT
using Godot;
#endif

namespace Entitas.Generic
{
	public abstract partial class EntityBehaviourBase
#if ENTITAS_GENERIC_UNITY_SUPPORT
		: MonoBehaviour
#elif GODOT
		: Node
#endif
	{
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

	public abstract partial class EntityBehaviourBase<TScope> : EntityBehaviourBase
		where TScope : IScope
	{
		public Entity<TScope> Entity { get; private set; }

		public override void CreateEntity(Contexts contexts)
		{
			Entity = contexts.Get<TScope>().CreateEntity();
		}
	}

	public partial class EntityBehaviour<TScope> : EntityBehaviourBase<TScope>
		where TScope : IScope
	{
#if ENTITAS_GENERIC_UNITY_SUPPORT
		[SerializeField] private ComponentBehaviourBase<TScope>[] _componentBehaviours;
		[SerializeField] private BaseListener<TScope>[] _listeners;
		[SerializeField] private EntityBehaviourBase[] _subEntities;
#elif GODOT
		[Export] private ComponentBehaviourBase<TScope>[] _componentBehaviours;
		[Export] private BaseListener<TScope>[] _listeners;
		[Export] private EntityBehaviourBase[] _subEntities;
#endif

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