using System.Linq;
using UnityEditor;
using UnityEngine;
using static System.Reflection.BindingFlags;

namespace Entitas.Generic
{
	[CustomPropertyDrawer(typeof(ComponentIDBase), useForChildren: true)]
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
			var type = GetPropertyInstance(property).GetType();
			var scopeType = type.GetGenericArguments().Single();
			var lookupType = typeof(ComponentTypeManager<>).MakeGenericType(scopeType);
			var autoScanMethod = lookupType.GetMethod("AutoScan", Static | Public);
			autoScanMethod!.Invoke(null, null);

			return lookupType.GetStaticField<string[]>("ComponentNames");
		}

		public object GetPropertyInstance(SerializedProperty property)
		{
			var path = property.propertyPath;

			object @object = property.serializedObject.targetObject;
			var type = @object.GetType();
			var baseType = type.BaseType;

			Debug.Log("IM COCKSUCCER");
			while (baseType != null)
			{
				foreach (var field in baseType.GetFields())
					Debug.Log($"field = {field} {field.Name}");

				baseType = baseType.BaseType;
			}

			var fieldNames = path.Split('.');

			foreach (var name in fieldNames)
			{
				var info = type.GetField(name, NonPublic | Instance);
				foreach (var field in type.GetFields())
				{
					Debug.Log($"field = {field}");
				}

				if (info == null)
					break;

				@object = info.GetValue(@object);
				type = info.FieldType;
			}

			Debug.Log($"type = {type.Name}\n@object = {@object}");

			return @object;
		}
	}
}