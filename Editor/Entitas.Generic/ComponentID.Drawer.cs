using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	[CustomPropertyDrawer(typeof(ComponentID<>), useForChildren: true)]
	public class ComponentIDDrawer : PropertyDrawer
	{
		private string[] _names;
		private Type _scopeType;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			_scopeType ??= property.boxedValue.GetScopeType();
			_names ??= GetNamesArray();

			EditorGUI.BeginProperty(position, label, property);

			var nameProperty = property.EnsurePropertyRelative("_name");

			var selectedIndex = _names.IndexOf(nameProperty.stringValue, clamp: true);
			selectedIndex.GuiPopup(_names, position, label.text);
			nameProperty.stringValue = _names[selectedIndex];

			EditorGUI.EndProperty();
		}

		private dynamic GetNamesArray()
		{
			var lookupType = typeof(ComponentsLookup<>).MakeGenericType(_scopeType);
			var instance = (dynamic)lookupType.GetProperty("Instance")!.GetValue(null);

			instance.Initialize();
			return instance.ComponentNames;
		}
	}
}