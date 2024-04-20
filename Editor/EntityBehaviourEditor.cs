#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	[CustomEditor(typeof(EntityBehaviour<>), editorForChildClasses: true)]
	public class EntityBehaviourEditor : Editor
	{
		private SerializedProperty _ensureComponentsCountProperty;
		private SerializedProperty _forceSubEntitiesAutoCollectProperty;

		private Type ScopeType => target.GetScopeType();

		private void OnEnable()
		{
			_ensureComponentsCountProperty = serializedObject.FindProperty("_ensureComponentsCountOnAwake");
			_forceSubEntitiesAutoCollectProperty = serializedObject.FindProperty("_forceSubEntitiesAutoCollect");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();

			GUILayout.Label("Auto Collect");

			GUILayout.Button(nameof(CollectAll).Pretty()).OnClick(CollectAll);
			_forceSubEntitiesAutoCollectProperty.GuiField();
			GUILayout.Space(5f);
			_ensureComponentsCountProperty.GuiField();

			serializedObject.ApplyModifiedProperties();
		}

		private void CollectAll()
		{
			// EntityBehaviourUtils.FillAll<TScope>(target);
			typeof(EntityBehaviourUtils)
				.GetMethod(nameof(EntityBehaviourUtils.FillAll))
				!.MakeGenericMethod(ScopeType)
				.Invoke(null, new object[] { serializedObject });

			EditorUtility.SetDirty(target);
		}
	}
}
#endif