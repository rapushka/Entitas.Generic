using JetBrains.Annotations;
using UnityEngine;

namespace Entitas.Generic
{
	public abstract class EntityBehaviour : MonoBehaviour
	{
		public abstract void CreateEntity(Contexts contexts);
		public abstract void Register();

		public abstract void CollectComponents();
	}

	public class EntityBehaviour<TScope> : EntityBehaviourDebug<TScope>
		where TScope : IScope
	{
		[SerializeField] private ComponentBehaviourBase<TScope>[] _componentBehaviours;

		private Entity<TScope> _entity;

		[PublicAPI]
		public Entity<TScope> Entity => _entity;

		protected override ComponentBehaviourBase<TScope>[] ComponentBehaviours
		{
			get => _componentBehaviours;
			set => _componentBehaviours = value;
		}

		public override void CreateEntity(Contexts contexts)
		{
			_entity = contexts.Get<TScope>().CreateEntity();
		}

		public override void Register()
		{
			foreach (var component in ComponentBehaviours)
				component.Add(ref _entity);
		}
	}
}