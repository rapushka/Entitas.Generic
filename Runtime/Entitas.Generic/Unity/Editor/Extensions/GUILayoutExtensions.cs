using System;

namespace Entitas.Generic
{
	public static class GUILayoutExtensions
	{
		public static void OnClick(this bool @this, Action action)
		{
			if (@this)
				action.Invoke();
		}
	}
}