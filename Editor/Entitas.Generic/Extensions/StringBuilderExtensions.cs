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
	}
}