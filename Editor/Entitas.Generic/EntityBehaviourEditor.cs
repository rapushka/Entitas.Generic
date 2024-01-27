using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	[CustomEditor(typeof(EntityBehaviour), editorForChildClasses: true)]
	public class EntityBehaviourEditor : Editor
	{
		private EntityBehaviour Target => (EntityBehaviour)target;

		private SerializedProperty _collectInChildren;
		private SerializedProperty _interruptChildrenEntities;
		private bool _foldout;

		private void OnEnable()
		{
			_collectInChildren = serializedObject.FindProperty("_collectInChildren");
			_interruptChildrenEntities = serializedObject.FindProperty("_interruptChildEntities");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();

			GUILayout.Label("Auto Collect (debug only)");

			_foldout = EditorGUILayout.BeginFoldoutHeaderGroup(_foldout, "Options");
			if (_foldout)
			{
				_collectInChildren.GuiField(nameof(_collectInChildren).Pretty());
				_interruptChildrenEntities.GuiField(nameof(_interruptChildrenEntities).Pretty());
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			GUILayout.Button(nameof(Target.CollectComponents).Pretty()).OnClick(Target.CollectComponents);

			serializedObject.ApplyModifiedProperties();
		}
	}
}