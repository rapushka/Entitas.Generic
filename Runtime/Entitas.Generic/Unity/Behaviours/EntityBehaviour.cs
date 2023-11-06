using UnityEngine;

namespace Entitas.Generic
{
	public abstract class EntityBehaviour : MonoBehaviour
	{
		public abstract void Register(Contexts contexts);

		public abstract void CollectComponents();
	}

	public class EntityBehaviour<TScope> : EntityBehaviourDebug<TScope>
		where TScope : IScope
	{
		[SerializeField] private ComponentBehaviourBase<TScope>[] _componentBehaviours;

		private Entity<TScope> _entity;

		public Entity<TScope> Entity => _entity;

		protected override ComponentBehaviourBase<TScope>[] ComponentBehaviours
		{
			get => _componentBehaviours;
			set => _componentBehaviours = value;
		}

		public override void Register(Contexts contexts)
		{
			_entity = contexts.Get<TScope>().CreateEntity();

			foreach (var component in ComponentBehaviours)
				component.Add(ref _entity);
		}
	}
}