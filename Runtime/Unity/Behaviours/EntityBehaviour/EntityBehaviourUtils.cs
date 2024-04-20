#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable SuggestBaseTypeForParameter – no it can't

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
			FillSubEntities<TScope>(serializedObject);

			LogInfo<TScope>(serializedObject);
			return;

			void FillObject<TTarget>(string path)
				where TTarget : Object
				=> Fill<TScope, TTarget>(serializedObject, path);
		}

		private static void LogInfo<TScope>(SerializedObject serializedObject) where TScope : IScope
		{
			var path = EntityBehaviour<TScope>.NameOf.ComponentBehaviours;
			var componentBehavioursCount = serializedObject.FindProperty(path).arraySize;
			var listenersCount = serializedObject.FindProperty(EntityBehaviour<TScope>.NameOf.Listeners).arraySize;
			var subEntitiesCount = serializedObject.FindProperty(EntityBehaviour<TScope>.NameOf.SubEntities).arraySize;

			Debug.Log
			(
				$"{serializedObject.targetObject.name} has been filled. "
				+ $"(componentBehaviours: {componentBehavioursCount}, "
				+ $"listeners: {listenersCount}, "
				+ $"subEntities: {subEntitiesCount})"
			);
		}

		private static void FillSubEntities<TScope>(SerializedObject serializedObject)
			where TScope : IScope
		{
			var target = (EntityBehaviour<TScope>)serializedObject.targetObject;
			var property = serializedObject.FindProperty(EntityBehaviour<TScope>.NameOf.SubEntities);

			property.SetArray(GetSubEntities(target).ToArray());

			ForceFillSubEntities(target);
		}

		private static void ForceFillSubEntities<TScope>(EntityBehaviour<TScope> target)
			where TScope : IScope
		{
			if (!target.ForceSubEntitiesAutoCollect())
				return;

			foreach (var subEntity in GetSubEntities(target).OfType<EntityBehaviour<TScope>>())
			{
				var serializedSubEntity = new SerializedObject(subEntity);
				FillAll<TScope>(serializedSubEntity);
				serializedSubEntity.ApplyModifiedProperties();
			}
		}

		private static IEnumerable<EntityBehaviourBase> GetSubEntities(EntityBehaviourBase target)
			=> target.GetComponentsInChildren<EntityBehaviourBase>(true).Skip(1);

		private static void Fill<TScope, TTarget>(SerializedObject serializedObject, string propertyPath)
			where TScope : IScope
			where TTarget : Object
		{
			var target = (EntityBehaviour<TScope>)serializedObject.targetObject;
			var property = serializedObject.FindProperty(propertyPath);

			var components = Collect<TScope, TTarget>(target);
			property.SetArray(components);
		}

		[Pure]
		public static TTarget[] Collect<TScope, TTarget>(EntityBehaviour<TScope> target)
			where TScope : IScope
		{
			var components = target.GetComponentsInChildren<TTarget>(true).ToList();
			components.RemoveAll(CollidedComponents);

			return components.ToArray();

			bool CollidedComponents(TTarget component)
				=> GetSubEntities(target)
				   .SelectMany(e => e.GetComponentsInChildren<TTarget>())
				   .Contains(component);
		}

		private static bool EnsureComponentsCount<TScope>(this EntityBehaviour<TScope> @this)
			where TScope : IScope
			=> @this.GetProperty(EntityBehaviour<TScope>.NameOf.EnsureComponentsCountOnAwake).boolValue;

		private static bool ForceSubEntitiesAutoCollect<TScope>(this EntityBehaviour<TScope> @this)
			where TScope : IScope
			=> @this.GetProperty(EntityBehaviour<TScope>.NameOf.ForceSubEntitiesAutoCollect).boolValue;

		private static ComponentBehaviourBase<TScope>[] ComponentBehaviours<TScope>(this EntityBehaviour<TScope> @this)
			where TScope : IScope
			=> @this.GetProperty(EntityBehaviour<TScope>.NameOf.ComponentBehaviours)
			        .GetArray<ComponentBehaviourBase<TScope>>();

		internal static EntityBehaviour<TScope>[] SubEntities<TScope>(this EntityBehaviour<TScope> @this)
			where TScope : IScope
			=> @this.GetProperty(EntityBehaviour<TScope>.NameOf.SubEntities)
			        .GetArray<EntityBehaviour<TScope>>();

		private static SerializedProperty GetProperty(this Object @this, string path)
			=> new SerializedObject(@this).FindProperty(path);
	}
}
#endif