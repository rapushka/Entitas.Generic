#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	[CustomEditor(typeof(EntityBehaviour), editorForChildClasses: true)]
	public class EntityBehaviourEditor : Editor
	{
		private EntityBehaviour Target => (EntityBehaviour)target;

		private bool _collectInChildrenValue;
		private bool _interruptChildrenEntitiesValue = true;
		private SerializedProperty _collectInChildren;
		private SerializedProperty _interruptChildrenEntities;

		private void OnEnable()
		{
			_collectInChildren = serializedObject.FindProperty(nameof(_collectInChildren));
			_interruptChildrenEntities = serializedObject.FindProperty(nameof(_interruptChildrenEntities));

			// _interruptChildrenEntities = Target.GetPrivateFieldValue<bool>(nameof(_interruptChildrenEntities));
			// _collectInChildren = Target.GetPrivateFieldValue<bool>(nameof(_collectInChildren));
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.Label("Auto Collect (debug only)");

			_collectInChildrenValue = _collectInChildren.boolValue;
			_interruptChildrenEntitiesValue = _interruptChildrenEntities.boolValue;

			_collectInChildrenValue.GuiToggle(nameof(_collectInChildrenValue).Pretty());
			_interruptChildrenEntitiesValue.GuiToggle(nameof(_interruptChildrenEntitiesValue).Pretty());

			GUILayout.BeginHorizontal();
			{
				_collectInChildren.boolValue = _collectInChildrenValue;
				_interruptChildrenEntities.boolValue = _interruptChildrenEntitiesValue;
				// Target.SetPrivateFieldValue(nameof(_interruptChildrenEntities), _interruptChildrenEntities);
				// Target.SetPrivateFieldValue(nameof(_collectInChildren), _collectInChildren);
			}
			GUILayout.EndHorizontal();

			GUILayout.Button(nameof(Target.CollectComponents).Pretty()).OnClick(Target.CollectComponents);
		}
	}
}
#endif