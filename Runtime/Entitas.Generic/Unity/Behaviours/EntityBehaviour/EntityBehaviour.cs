using JetBrains.Annotations;
using UnityEngine;

namespace Entitas.Generic
{
	public abstract class EntityBehaviour : MonoBehaviour
	{
		[PublicAPI]
		public void Register(Contexts contexts)
		{
			CreateEntity(contexts);
			Register();
		}

		public abstract void CreateEntity(Contexts contexts);
		public abstract void Register();
	}

	public class EntityBehaviour<TScope> : EntityBehaviour
		where TScope : IScope
	{
		[SerializeField] private ComponentBehaviourBase<TScope>[] _componentBehaviours;
		[SerializeField] private BaseListener<TScope>[] _listeners;

#if UNITY_EDITOR
		[HideInInspector] [SerializeField] private bool _ensureComponentsCountOnAwake = true;
		[HideInInspector] [SerializeField] private bool _collectInChildren;
		[HideInInspector] [SerializeField] private bool _interruptChildEntities = true;
#endif

		private Entity<TScope> _entity;

		[PublicAPI]
		public Entity<TScope> Entity => _entity;

#if UNITY_EDITOR
		private void Awake()
		{
			if (_ensureComponentsCountOnAwake)
				EntityBehaviourUtils.CheckComponentsMatch(this);
		}
#endif

		public override void CreateEntity(Contexts contexts)
		{
			_entity = contexts.Get<TScope>().CreateEntity();
		}

		public override void Register()
		{
			foreach (var component in _componentBehaviours)
				component.Add(ref _entity);

			foreach (var listener in _listeners)
				listener.Register(_entity);
		}

#if UNITY_EDITOR
		public static class NameOf
		{
			public const string ComponentBehaviours = nameof(_componentBehaviours);
			public const string Listeners = nameof(_listeners);

			public const string EnsureComponentsCountOnAwake = nameof(_ensureComponentsCountOnAwake);
			public const string CollectInChildren = nameof(_collectInChildren);
			public const string InterruptChildEntities = nameof(_interruptChildEntities);
		}
#endif
	}
}