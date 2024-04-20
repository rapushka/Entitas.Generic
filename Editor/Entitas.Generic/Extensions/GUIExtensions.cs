#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	internal static class GUIExtensions
	{
		internal static void GuiPopup(this string[] @this, ref int selectedIndex, Rect position, string text)
			=> selectedIndex = EditorGUI.Popup(position, text, selectedIndex, @this);
	}
}
#endif