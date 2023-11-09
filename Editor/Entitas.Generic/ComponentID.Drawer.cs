using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	/// <summary> Implement for concrete scope, and add CustomPropertyDrawer attribute </summary>
	public class ComponentIDDrawer<TScope> : PropertyDrawer
		where TScope : IScope
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			ComponentsLookup<TScope>.Instance.Initialize();
			var names = ComponentsLookup<TScope>.Instance.ComponentNames;

			var nameProperty = property.EnsurePropertyRelative("_name");

			var selectedIndex = names.IndexOf(nameProperty.stringValue, clamp: true);
			selectedIndex.GuiPopup(names, position, label.text);
			nameProperty.stringValue = names[selectedIndex];

			EditorGUI.EndProperty();
		}
	}
}
