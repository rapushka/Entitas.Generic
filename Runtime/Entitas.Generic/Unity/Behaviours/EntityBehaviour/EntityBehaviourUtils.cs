#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	public static class EntityBehaviourUtils
	{
		public static void CheckComponentsMatch<TScope>(EntityBehaviour<TScope> target)
			where TScope : IScope
		{
			if (!target.EnsureComponentsCount())
				return;

			var actualComponents = CollectComponents(target).OrderBy((a) => a.GetInstanceID());
			var setComponents = target.ComponentBehaviours().OrderBy((a) => a.GetInstanceID());

			if (!actualComponents.SequenceEqual(setComponents))
				Debug.LogError($"Actual components don't match the currently set on {target.name}!", target);
		}

		public static ComponentBehaviourBase<TScope>[] CollectComponents<TScope>(EntityBehaviour<TScope> target)
			where TScope : IScope
		{
			var componentBehaviours = target.CollectInChildren()
				? target.GetComponentsInChildren<ComponentBehaviourBase<TScope>>()
				: target.GetComponents<ComponentBehaviourBase<TScope>>();

			if (target.CollectInChildren() && !target.InterruptChildren())
			{
				var childComponents = target.GetComponentsInChildren<EntityBehaviour<TScope>>()
				                            .SelectMany(CollectChildComponents);
				var ourComponents = target.GetComponents<ComponentBehaviourBase<TScope>>();

				componentBehaviours = componentBehaviours.Except(childComponents).Concat(ourComponents).ToArray();
			}

			return componentBehaviours;

			IEnumerable<ComponentBehaviourBase<TScope>> CollectChildComponents(EntityBehaviour<TScope> e)
				=> e.GetComponentsInChildren<ComponentBehaviourBase<TScope>>();
		}

		private static bool EnsureComponentsCount<TScope>(this EntityBehaviour<TScope> @this)
			where TScope : IScope
			=> @this.GetProperty(EntityBehaviour<TScope>.NameOf.EnsureComponentsCountOnAwake).boolValue;

		private static bool CollectInChildren<TScope>(this EntityBehaviour<TScope> @this)
			where TScope : IScope
			=> @this.GetProperty(EntityBehaviour<TScope>.NameOf.CollectInChildren).boolValue;

		private static bool InterruptChildren<TScope>(this EntityBehaviour<TScope> @this)
			where TScope : IScope
			=> @this.GetProperty(EntityBehaviour<TScope>.NameOf.InterruptChildEntities).boolValue;

		private static ComponentBehaviourBase<TScope>[] ComponentBehaviours<TScope>(this EntityBehaviour<TScope> @this)
			where TScope : IScope
		{
			var fieldName = EntityBehaviour<TScope>.NameOf.ComponentBehaviours;
			return @this.GetPrivateFieldValue<ComponentBehaviourBase<TScope>[]>(fieldName);
		}

		private static SerializedProperty GetProperty(this Object @this, string path)
			=> new SerializedObject(@this).FindProperty(path);
	}
}
#endif