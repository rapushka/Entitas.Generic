#if UNITY_EDITOR
using UnityEngine;

namespace Entitas.Generic
{
	public abstract class EntityBehaviour : MonoBehaviour
	{
		public abstract void Register();

		[ContextMenu(nameof(CollectComponentsFromGameObject))]
		public abstract void CollectComponentsFromGameObject();
	}

	public class EntityBehaviour<TScope> : EntityBehaviour
		where TScope : IScope
	{
		[SerializeField] private ComponentBehaviourBase<TScope>[] _componentBehaviours;

		private Entity<TScope> _entity;

		public Entity<TScope> Entity => _entity;

#if DEBUG
		private void Awake()
		{
			var actualCount = GetComponents<ComponentBehaviourBase<TScope>>().Length;

			if (actualCount != _componentBehaviours.Length)
				Debug.LogWarning($"Not all component behaviours are added to the entity on {gameObject.name}!");
		}
#endif

		public override void Register()
		{
			_entity = Contexts.Instance.Get<TScope>().CreateEntity();

			foreach (var component in _componentBehaviours)
				component.Add(ref _entity);
		}

		public override void CollectComponentsFromGameObject()
		{
			_componentBehaviours = GetComponents<ComponentBehaviourBase<TScope>>();
		}
	}
}
#endif