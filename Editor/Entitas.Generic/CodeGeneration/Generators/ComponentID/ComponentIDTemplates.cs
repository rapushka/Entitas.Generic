using System;

namespace Entitas.Generic
{
	internal static class ComponentIDTemplates
	{
		internal static string ComponentID(Type scope)
			=> $"\tpublic class {scope.Name}ComponentID : {BaseComponentID(scope)} {{ }}";

		internal static string PropertyDrawer(Type scope)
			=> $"\t[UnityEditor.CustomPropertyDrawer(typeof({BaseComponentID(scope)}), true)]\n"
			   + $"\tpublic class {scope.Name}ComponentIDDrawer "
			   + $": Entitas.Generic.ComponentIDDrawer<{scope.FullName}> {{ }}";

		private static string BaseComponentID(Type scope) => $"Entitas.Generic.ComponentID<{scope.FullName}>";
	}
}