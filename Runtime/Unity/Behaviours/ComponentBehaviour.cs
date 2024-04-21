#if ENTITAS_GENERIC_UNITY_SUPPORT
using UnityEngine;
#elif GODOT
using Godot;
#endif

namespace Entitas.Generic
{
	public abstract partial class ComponentBehaviourBase<TScope>
#if ENTITAS_GENERIC_UNITY_SUPPORT
		: MonoBehaviour
#elif GODOT
		: Node
#endif
		where TScope : IScope
	{
		public abstract void Add(ref Entity<TScope> entity);
	}

	public abstract partial class ComponentBehaviour<TScope, TComponent> : ComponentBehaviourBase<TScope>
		where TScope : IScope
		where TComponent : FlagComponent, IInScope<TScope>, new()
	{
		public override void Add(ref Entity<TScope> entity)
		{
			entity.Is<TComponent>(true);
		}
	}

#if ENTITAS_GENERIC_UNITY_SUPPORT
	public abstract class ComponentBehaviour<TScope, TComponent, TValue> : ComponentBehaviourBase<TScope>
		where TScope : IScope
		where TComponent : ValueComponent<TValue>, IInScope<TScope>, new()
	{
		[SerializeField] private TValue _value;

		public override void Add(ref Entity<TScope> entity)
		{
			entity.Add<TComponent, TValue>(_value);
		}
	}
#endif
}