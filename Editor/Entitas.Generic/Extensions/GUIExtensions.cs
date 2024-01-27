using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	internal static class GUIExtensions
	{
		internal static void GuiPopup(this ref int @this, string[] options, Rect position, string text)
		{
			@this = EditorGUI.Popup(position, text, @this, options);
		}
	}
}