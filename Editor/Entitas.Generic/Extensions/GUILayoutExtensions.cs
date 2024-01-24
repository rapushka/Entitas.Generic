using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	internal static class GUILayoutExtensions
	{
		internal static void OnClick(this bool @this, Action action)
		{
			if (@this)
				action.Invoke();
		}

		internal static void GuiToggle(this ref bool @this, string text)
		{
			@this = GUILayout.Toggle(@this, text);
		}

		internal static void GuiField(this SerializedProperty @this, string text)
		{
			EditorGUILayout.PropertyField(@this, new GUIContent(text));
		}
	}
}