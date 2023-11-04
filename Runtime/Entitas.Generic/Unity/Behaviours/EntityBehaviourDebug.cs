#if UNITY_EDITOR
using System.Linq;
using UnityEngine;

namespace Entitas.Generic
{
	/// <summary> mid-class to keep the majority of editor stuff here </summary>
	public abstract class EntityBehaviourDebug<TScope> : EntityBehaviour where TScope : IScope
	{
#if DEBUG
		[HideInInspector] [SerializeField] private bool _collectInChildren;
		[HideInInspector] [SerializeField] private bool _interruptChildrenEntities = true;
#endif

		protected abstract ComponentBehaviourBase<TScope>[] ComponentBehaviours { get; set; }

		public override void CollectComponents()
		{
#if DEBUG
			ComponentBehaviours = Collect();
#endif
		}

#if DEBUG
		private void Awake()
		{
			var actualComponents = Collect().OrderBy((a) => a.GetInstanceID());
			var setComponents = ComponentBehaviours.OrderBy((a) => a.GetInstanceID());

			if (!actualComponents.SequenceEqual(setComponents))
				Debug.LogWarning("Actual components don't match the currently set!", this);
		}
#endif

		protected virtual ComponentBehaviourBase<TScope>[] Collect()
		{
#if DEBUG
			var componentBehaviours = _collectInChildren
				? GetComponentsInChildren<ComponentBehaviourBase<TScope>>()
				: GetComponents<ComponentBehaviourBase<TScope>>();

			if (!_interruptChildrenEntities)
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
			throw new NotImplementedException($"Call the {nameof(Collect)} in DEBUG only, or override it");
#endif
		}
	}
}
#endif