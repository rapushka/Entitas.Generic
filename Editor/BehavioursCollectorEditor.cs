#if ENTITAS_GENERIC_UNITY_SUPPORT && UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Entitas.Generic
{
	[CustomEditor(typeof(BehavioursCollector), editorForChildClasses: true)]
	public class BehavioursCollectorEditor : Editor
	{
		private BehavioursCollector Target => (BehavioursCollector)target;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.Button(nameof(CollectAll).Pretty()).OnClick(CollectAll);
		}

		public void CollectAll()
		{
			var behaviours = FindObjectsByType<EntityBehaviourBase>(FindObjectsSortMode.InstanceID)
			                 .Where((e) => e is not ISelfRegistry)
			                 .ToList();

			RemoveCollisions(ref behaviours);

			ReflectionUtils.SetPrivateFieldValue(Target, "_behaviours", behaviours.ToArray());
			EditorUtility.SetDirty(Target);

			Debug.Log($"Collected {behaviours.Count} entities.");
		}

		private static void RemoveCollisions(ref List<EntityBehaviourBase> behaviours)
		{
			var allBehaviours = behaviours.ToList();
			foreach (var entityBehaviour in allBehaviours)
			{
				var type = entityBehaviour.GetType();

				if (!entityBehaviour.TryGetScopeType(out var scopeType))
					continue;

				var closedType = typeof(EntityBehaviour<>).MakeGenericType(scopeType);
				if (!ReflectionUtils.IsDerivedFrom(type, closedType))
					continue;

				var subEntities = closedType
				                  .GetField("_subEntities", BindingFlags.NonPublic | BindingFlags.Instance)
				                  !.GetValue(entityBehaviour);

				foreach (var subEntity in (EntityBehaviourBase[])subEntities)
					behaviours.Remove(subEntity);
			}
		}
	}
}
#endif