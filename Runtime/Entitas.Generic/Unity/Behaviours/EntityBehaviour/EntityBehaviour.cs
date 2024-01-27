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

		public abstract void CollectAll();
	}

	public class EntityBehaviour<TScope> : EntityBehaviourDebug<TScope>
		where TScope : IScope
	{
		[SerializeField] private ComponentBehaviourBase<TScope>[] _componentBehaviours;
		[SerializeField] private BaseListener<TScope>[] _listeners;

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

			foreach (var listener in _listeners)
				listener.Register(_entity);
		}
	}
}