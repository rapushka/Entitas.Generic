using System.Linq;
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

			GUILayout.Button(nameof(ForceAllEntitiesToCollectAllComponents).Pretty())
			         .OnClick(ForceAllEntitiesToCollectAllComponents);
		}

		public void CollectAll()
		{
			var behaviours = FindObjectsOfType<EntityBehaviour>()
			                 .Where((e) => e is not ISelfRegistry)
			                 .ToArray();

			Target.SetPrivateFieldValue("_behaviours", behaviours);
			EditorUtility.SetDirty(Target);
		}

		public void ForceAllEntitiesToCollectAllComponents()
		{
			foreach (var behaviour in FindObjectsOfType<EntityBehaviour>())
			{
				behaviour.CollectAll();
				EditorUtility.SetDirty(behaviour);
			}
		}
	}
}