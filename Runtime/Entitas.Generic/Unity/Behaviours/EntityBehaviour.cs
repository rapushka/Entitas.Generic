#if UNITY_EDITOR
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
		[Header("Auto Collect")]
		[SerializeField] private bool _collectInChildren;
#endif

		private Entity<TScope> _entity;

		public Entity<TScope> Entity => _entity;

#if DEBUG
		private void Awake()
		{
			var actualCount = GetComponents<ComponentBehaviourBase<TScope>>().Length;

			if (actualCount != _componentBehaviours.Length)
				Debug.LogWarning("Not all component behaviours are added to the entity!", this);
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
			_componentBehaviours = _collectInChildren
				? GetComponentsInChildren<ComponentBehaviourBase<TScope>>()
				: GetComponents<ComponentBehaviourBase<TScope>>();
#endif
		}
	}
}
#endif