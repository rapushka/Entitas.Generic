using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	// [CustomPropertyDrawer(typeof(ComponentIDBase), useForChildren: true)]
	public class ComponentIDDrawer<TScope> : PropertyDrawer
		where TScope : IScope
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			ComponentTypeManager<TScope>.AutoScan();
			var names = ComponentTypeManager<TScope>.ComponentNames;

			var nameProperty = property.FindPropertyRelative("_name");
			var selectedName = nameProperty.stringValue;

			var selectedIndex = names.IndexOf(selectedName, clamp: true);
			selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, names);

			nameProperty.stringValue = names[selectedIndex];

			EditorGUI.EndProperty();
		}
	}
}