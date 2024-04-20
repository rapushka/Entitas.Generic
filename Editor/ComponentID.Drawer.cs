#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	/// Implement for your scopes
	/// [CustomPropertyDrawer(typeof(ComponentID&lt;TScope&gt;), true)]
	public class ComponentIDDrawer<TScope> : PropertyDrawer
		where TScope : IScope
	{
		private static string[] ComponentNames => ComponentsLookup<TScope>.Instance.ComponentNames;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			var nameProperty = property.EnsurePropertyRelative("_name");

			var index = Array.IndexOf(ComponentNames, nameProperty.stringValue);
			var selectedIndex = Math.Clamp(index, min: 0, max: ComponentNames.Length);

			ComponentNames.GuiPopup(ref selectedIndex, position, label.text);
			nameProperty.stringValue = ComponentNames[selectedIndex];

			EditorGUI.EndProperty();
		}
	}
}
#endif