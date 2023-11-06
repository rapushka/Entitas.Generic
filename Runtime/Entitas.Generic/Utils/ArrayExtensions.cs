using System;

namespace Entitas.Generic
{
	public static class ArrayExtensions
	{
		public static int IndexOf<T>(this T[] @this, T item, bool clamp = false)
		{
			var index = Array.IndexOf(@this, item);
			return clamp ? Math.Clamp(index, min: 0, max: @this.Length) : index;
		}
	}
}