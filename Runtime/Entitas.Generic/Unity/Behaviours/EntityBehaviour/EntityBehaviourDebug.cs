using System.Linq;
using UnityEngine;

namespace Entitas.Generic
{
	/// <summary> mid-class to keep the majority of editor stuff here </summary>
	public abstract class EntityBehaviourDebug<TScope> : EntityBehaviour where TScope : IScope
	{
#if DEBUG
		[HideInInspector] [SerializeField] private bool _ensureComponentsCountOnAwake = true;
		[HideInInspector] [SerializeField] private bool _collectInChildren;
		[HideInInspector] [SerializeField] private bool _interruptChildEntities = true;
#endif

		protected abstract ComponentBehaviourBase<TScope>[] ComponentBehaviours { get; set; }

		public override void CollectAll()
		{
			ComponentBehaviours = CollectComponents();
		}

#if DEBUG
		private void Awake()
		{
			var actualComponents = CollectComponents().OrderBy((a) => a.GetInstanceID());
			var setComponents = ComponentBehaviours.OrderBy((a) => a.GetInstanceID());

			if (_ensureComponentsCountOnAwake && !actualComponents.SequenceEqual(setComponents))
				Debug.LogWarning("Actual components don't match the currently set!", this);
		}
#endif

		protected virtual ComponentBehaviourBase<TScope>[] CollectComponents()
		{
#if DEBUG
			var componentBehaviours = _collectInChildren
				? GetComponentsInChildren<ComponentBehaviourBase<TScope>>()
				: GetComponents<ComponentBehaviourBase<TScope>>();

			if (_collectInChildren && !_interruptChildEntities)
			{
				var childrenComponents = GetComponentsInChildren<EntityBehaviour<TScope>>()
					.SelectMany((e) => e.GetComponentsInChildren<ComponentBehaviourBase<TScope>>());
				var ourComponents = GetComponents<ComponentBehaviourBase<TScope>>();

				componentBehaviours = componentBehaviours
				                      .Except(childrenComponents)
				                      .Concat(ourComponents)
				                      .ToArray();
			}

			return componentBehaviours;
#else
			throw new System.NotImplementedException($"Call this method in DEBUG only, or override it");
#endif
		}
	}
}