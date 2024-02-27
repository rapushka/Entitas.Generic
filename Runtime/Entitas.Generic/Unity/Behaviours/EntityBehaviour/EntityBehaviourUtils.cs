#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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

			var actualComponents = Collect<TScope, ComponentBehaviourBase<TScope>>(target)
				.OrderBy((a) => a.GetInstanceID());
			var setComponents = target.ComponentBehaviours().OrderBy((a) => a.GetInstanceID());

			if (!actualComponents.SequenceEqual(setComponents))
				Debug.LogError($"Actual components don't match the currently set on {target.name}!", target);
		}

		public static void FillAll<TScope>(SerializedObject serializedObject)
			where TScope : IScope
		{
			FillObject<ComponentBehaviourBase<TScope>>(EntityBehaviour<TScope>.NameOf.ComponentBehaviours);
			FillObject<BaseListener<TScope>>(EntityBehaviour<TScope>.NameOf.Listeners);
			return;

			void FillObject<TTarget>(string path)
				where TTarget : Object
				=> Fill<TScope, TTarget>(serializedObject, path);
		}

		private static void Fill<TScope, TTarget>(SerializedObject serializedObject, string propertyPath)
			where TScope : IScope
			where TTarget : Object
		{
			var target = (EntityBehaviour<TScope>)serializedObject.targetObject;

			var componentBehavioursProperty = serializedObject.FindProperty(propertyPath);

			var components = Collect<TScope, TTarget>(target);
			componentBehavioursProperty.SetArray(components);
		}

		[Pure]
		public static TTarget[] Collect<TScope, TTarget>(EntityBehaviour<TScope> target)
			where TScope : IScope
		{
			var components = target.CollectInChildren()
				? target.GetComponentsInChildren<TTarget>()
				: target.GetComponents<TTarget>();

			if (target.CollectInChildren() && !target.InterruptChildren())
			{
				var childComponents = target.GetComponentsInChildren<EntityBehaviour<TScope>>()
				                            .SelectMany(CollectChildComponents);
				var ourComponents = target.GetComponents<TTarget>();

				components = components.Except(childComponents).Concat(ourComponents).ToArray();
			}

			return components;

			IEnumerable<TTarget> CollectChildComponents(EntityBehaviour<TScope> e)
				=> e.GetComponentsInChildren<TTarget>();
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
			=> @this.GetProperty(EntityBehaviour<TScope>.NameOf.ComponentBehaviours)
			        .GetArray<ComponentBehaviourBase<TScope>>();

		private static SerializedProperty GetProperty(this Object @this, string path)
			=> new SerializedObject(@this).FindProperty(path);
	}
}
#endif