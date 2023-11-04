#if UNITY_EDITOR
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

		private void OnEnable()
		{
			_collectInChildren = serializedObject.FindProperty("_collectInChildren");
			_interruptChildrenEntities = serializedObject.FindProperty("_interruptChildrenEntities");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();

			GUILayout.Label("Auto Collect (debug only)");

			EditorGUILayout.BeginHorizontal();
			{
				_collectInChildren.GuiField(nameof(_collectInChildren).Pretty());
				_interruptChildrenEntities.GuiField(nameof(_interruptChildrenEntities).Pretty());
			}
			EditorGUILayout.EndHorizontal();

			GUILayout.Button(nameof(Target.CollectComponents).Pretty()).OnClick(Target.CollectComponents);

			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif