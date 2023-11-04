#if UNITY_EDITOR
using System;
using UnityEngine;

namespace Entitas.Generic
{
	public static class GUILayoutExtensions
	{
		public static void OnClick(this bool @this, Action action)
		{
			if (@this)
				action.Invoke();
		}

		public static void GuiToggle(this ref bool @this, string text)
		{
			@this = GUILayout.Toggle(@this, text);
		}
	}
}
#endif