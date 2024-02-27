using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	[CustomEditor(typeof(EntityBehaviour<>), editorForChildClasses: true)]
	public class EntityBehaviourEditor : Editor
	{
		private SerializedProperty _ensureComponentsCountProperty;
		private SerializedProperty _collectInChildrenProperty;
		private SerializedProperty _interruptChildEntitiesProperty;

		private bool _foldout;

		private Type ScopeType => target.GetScopeType();

		private void OnEnable()
		{
			_ensureComponentsCountProperty = serializedObject.FindProperty("_ensureComponentsCountOnAwake");
			_collectInChildrenProperty = serializedObject.FindProperty("_collectInChildren");
			_interruptChildEntitiesProperty = serializedObject.FindProperty("_interruptChildEntities");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();

			GUILayout.Label("Auto Collect");

			_foldout = EditorGUILayout.BeginFoldoutHeaderGroup(_foldout, "Options");
			if (_foldout)
			{
				_collectInChildrenProperty.GuiField();
				_interruptChildEntitiesProperty.GuiField();
			}

			EditorGUILayout.EndFoldoutHeaderGroup();

			GUILayout.Button(nameof(CollectAll).Pretty()).OnClick(CollectAll);
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