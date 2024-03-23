using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	/// Implement for your scopes
	/// [CustomPropertyDrawer(typeof(ComponentID&lt;TScope&gt;))]
	public class ComponentIDDrawer<TScope> : PropertyDrawer
		where TScope : IScope
	{
		private static string[] ComponentNames => ComponentsLookup<TScope>.Instance.ComponentNames;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			var nameProperty = property.EnsurePropertyRelative("_name");

			var selectedIndex = ComponentNames.IndexOf(nameProperty.stringValue, clamp: true);
			selectedIndex.GuiPopup(ComponentNames, position, label.text);
			nameProperty.stringValue = ComponentNames[selectedIndex];

			EditorGUI.EndProperty();
		}
	}
}