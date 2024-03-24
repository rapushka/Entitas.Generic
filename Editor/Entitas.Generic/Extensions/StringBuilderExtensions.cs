using System.Text;

namespace Entitas.Generic
{
	internal static class StringBuilderExtensions
	{
		internal static void Set(this StringBuilder @this, string value)
		{
			@this.Clear();
			@this.Append(value);
		}

		internal static void RemoveLast(this StringBuilder @this, int charsCount)
			=> @this.Remove(@this.Length - charsCount, charsCount);
	}
}