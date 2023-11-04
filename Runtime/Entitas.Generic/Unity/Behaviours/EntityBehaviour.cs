#if UNITY_EDITOR
using System.Linq;
using UnityEngine;

namespace Entitas.Generic
{
	public abstract class EntityBehaviour : MonoBehaviour
	{
		public abstract void Register(Contexts contexts);

		[Button] public abstract void CollectComponents();
	}

	public class EntityBehaviour<TScope> : EntityBehaviour
		where TScope : IScope
	{
		[SerializeField] private ComponentBehaviourBase<TScope>[] _componentBehaviours;

#if DEBUG
		[Header("Auto Collect (debug only)")]
		[SerializeField] private bool _collectInChildren;
		[SerializeField] private bool _interruptChildrenEntities = true;
#endif

		private Entity<TScope> _entity;

		public Entity<TScope> Entity => _entity;

#if DEBUG
		private void Awake()
		{
			var actualComponents = Collect().OrderBy((a) => a.GetInstanceID());
			var setComponents = _componentBehaviours.OrderBy((a) => a.GetInstanceID());

			if (!actualComponents.SequenceEqual(setComponents))
				Debug.LogWarning("Actual components don't match the currently set!", this);
		}
#endif

		public override void Register(Contexts contexts)
		{
			_entity = contexts.Get<TScope>().CreateEntity();

			foreach (var component in _componentBehaviours)
				component.Add(ref _entity);
		}

		public override void CollectComponents()
		{
#if DEBUG
			_componentBehaviours = Collect();
#endif
		}

#if DEBUG
		protected virtual ComponentBehaviourBase<TScope>[] Collect()
		{
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
		}
#endif
	}
}
#endif