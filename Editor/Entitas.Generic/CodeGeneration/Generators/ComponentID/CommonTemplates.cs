namespace Entitas.Generic
{
	internal static class CommonTemplates
	{
		public static string HeaderGenerated => "// <Auto-Generated>";

		public static string WrapNamespace(string @namespace, string content)
			=> $@"
namespace {@namespace}
{{
{content}
}}";
	}
}