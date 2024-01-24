using System;

namespace Entitas.Generic
{
	internal static class ArrayExtensions
	{
		internal static int IndexOf<T>(this T[] @this, T item, bool clamp = false)
		{
			var index = Array.IndexOf(@this, item);
			return clamp ? Math.Clamp(index, min: 0, max: @this.Length) : index;
		}
	}
}