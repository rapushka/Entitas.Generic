using System;

namespace Entitas.Generic
{
	internal static class ComponentIDTemplates
	{
		internal static string ComponentID(Type scopeType)
			=> $"\tpublic class {scopeType.Name}ComponentID : Entitas.Generic.ComponentID<{scopeType.FullName}> {{ }}";
	}
}