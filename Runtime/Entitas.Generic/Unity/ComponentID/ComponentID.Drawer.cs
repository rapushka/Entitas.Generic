using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static System.Reflection.BindingFlags;

namespace Entitas.Generic
{
	[CustomPropertyDrawer(typeof(ComponentIDBase))]
	public class ComponentIDDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			var names = GetComponentNames(property);

			var nameProperty = property.FindPropertyRelative("_name");
			var selectedName = nameProperty.stringValue;

			var selectedIndex = names.IndexOf(selectedName, clamp: true);
			selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, names);

			nameProperty.stringValue = names[selectedIndex];

			EditorGUI.EndProperty();
		}

		private string[] GetComponentNames(SerializedProperty property)
		{
			var scopeType = property.objectReferenceValue.GetType().GetGenericArguments().Single();
			var lookupType = typeof(ComponentTypeManager<>).MakeGenericType(scopeType);
			var autoScanMethod = lookupType.GetMethod("AutoScan", Static | Public);
			autoScanMethod!.Invoke(null, null);

			return lookupType.GetStaticField<string[]>("ComponentNames");
		}
	}
}