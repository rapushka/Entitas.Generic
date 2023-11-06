#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	/// <summary> Implement for concrete scope, and add CustomPropertyDrawer attribute </summary>
	public class ComponentIDDrawer<TScope> : PropertyDrawer
		where TScope : IScope
	{
		private SerializedProperty _nameProperty;

		private string NameValue
		{
			get => _nameProperty!.stringValue;
			set => _nameProperty!.stringValue = value;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			ComponentTypeManager<TScope>.AutoScan();
			var names = ComponentTypeManager<TScope>.ComponentNames;

			_nameProperty ??= property.EnsurePropertyRelative("_name");

			var selectedIndex = names.IndexOf(NameValue, clamp: true);
			selectedIndex.GuiPopup(names, position, label.text);
			NameValue = names[selectedIndex];

			EditorGUI.EndProperty();
		}
	}
}
#endif