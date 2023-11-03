#if UNITY_EDITOR
using UnityEngine;

namespace Entitas.Generic
{
	public abstract class ComponentBehaviourBase<TScope> : MonoBehaviour
		where TScope : IScope
	{
		public abstract void Add(ref Entity<TScope> entity);
	}

	public abstract class ComponentBehaviour<TScope, TComponent> : ComponentBehaviourBase<TScope>
		where TScope : IScope
		where TComponent : FlagComponent, new()
	{
		public override void Add(ref Entity<TScope> entity)
		{
			entity.Is<TComponent>(true);
		}
	}

	public abstract class ComponentBehaviour<TScope, TComponent, TValue> : ComponentBehaviourBase<TScope>
		where TScope : IScope
		where TComponent : ValueComponent<TValue>, new()
	{
		[SerializeField] private TValue _value;

		public override void Add(ref Entity<TScope> entity)
		{
			entity.Add<TComponent, TValue>(_value);
		}
	}
}
  #endif