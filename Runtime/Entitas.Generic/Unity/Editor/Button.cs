#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Entitas.Generic
{
	[MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
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
				if (HasAttribute(method) && GUILayout.Button(method.Name.Pretty()))
					method.Invoke(component, null);
			}
		}

		private static bool HasAttribute(MethodInfo method)
			=> method.GetCustomAttributes(typeof(ButtonAttribute), inherit: true).Any();
	}

	public static class StringExtensions
	{
		public static string Pretty(this string @this) => Regex.Replace(@this, "(\\B[A-Z])", " $1");
	}
}
#endif