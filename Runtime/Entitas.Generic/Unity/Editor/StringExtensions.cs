#if UNITY_EDITOR
using System.Text.RegularExpressions;

namespace Entitas.Generic
{
	public static class StringExtensions
	{
		public static string Pretty(this string @this) => Regex.Replace(@this, "(\\B[A-Z])", " $1");
	}
}
#endif