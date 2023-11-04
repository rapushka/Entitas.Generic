using System.Linq;
using System.Text.RegularExpressions;

namespace Entitas.Generic
{
	public static class StringExtensions
	{
		/// <summary> _fewWordsField -&gt; Few Words Field </summary>
		public static string Pretty(this string @this)
			=> @this.RemovePrefix("_").FirstToUpper().PascalCaseToSpaces();

		private static string PascalCaseToSpaces(this string @this) => Regex.Replace(@this, "(\\B[A-Z])", " $1");

		private static string RemovePrefix(this string @this, string prefix)
			=> @this.StartsWith(prefix) ? @this[prefix.Length..] : @this;

		private static string FirstToUpper(this string @this)
			=> char.ToUpper(@this.First()) + @this[1..];
	}
}