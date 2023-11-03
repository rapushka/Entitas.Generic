#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Entitas.Generic
{
	public class ButtonAttribute : Attribute { }

	[CustomEditor(typeof(Component), editorForChildClasses: true)]
	public class CustomEditorScript : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var component = (Component)target;
			var methods = component.GetType().GetMethods();

			foreach (var method in methods)
			{
				if (!method.GetCustomAttributes(typeof(ButtonAttribute), inherit: true).Any())
					continue;

				if (GUILayout.Button(method.Name))
					method.Invoke(component, null);
			}
		}
	}
}
#endif