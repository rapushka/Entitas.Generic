using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	/// <summary>
	/// Inherit for concrete Scope and add the attribute:
	/// [CustomEditor(typeof(EntityBehaviour&lt;TScope&gt;))]
	/// </summary>
	public class EntityBehaviourEditor<TScope> : Editor
		where TScope : IScope
	{
		private EntityBehaviour<TScope> Target => (EntityBehaviour<TScope>)target;

		private SerializedProperty _componentBehavioursProperty;
		private SerializedProperty _listenersProperty;

		private SerializedProperty _ensureComponentsCountProperty;
		private SerializedProperty _collectInChildrenProperty;
		private SerializedProperty _interruptChildEntitiesProperty;

		private bool _foldout;

		private ComponentBehaviourBase<TScope>[] ComponentBehaviours
		{
			get => _componentBehavioursProperty.GetArray<ComponentBehaviourBase<TScope>>();
			set => _componentBehavioursProperty.SetArray(value);
		}

		private BaseListener<TScope>[] Listeners
		{
			get => _listenersProperty.GetArray<BaseListener<TScope>>();
			set => _listenersProperty.SetArray(value);
		}

		private void OnEnable()
		{
			_ensureComponentsCountProperty = serializedObject
				.FindProperty(EntityBehaviour<TScope>.NameOf.EnsureComponentsCountOnAwake);
			_collectInChildrenProperty = serializedObject
				.FindProperty(EntityBehaviour<TScope>.NameOf.CollectInChildren);
			_interruptChildEntitiesProperty = serializedObject
				.FindProperty(EntityBehaviour<TScope>.NameOf.InterruptChildEntities);

			_componentBehavioursProperty = serializedObject
				.FindProperty(EntityBehaviour<TScope>.NameOf.ComponentBehaviours);
			_listenersProperty = serializedObject.FindProperty(EntityBehaviour<TScope>.NameOf.Listeners);
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
			ComponentBehaviours = EntityBehaviourUtils.CollectComponents(Target);
			// TODO: Collect Listeners

			EditorUtility.SetDirty(Target);
		}
	}
}