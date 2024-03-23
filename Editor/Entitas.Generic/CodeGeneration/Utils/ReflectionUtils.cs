using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Entitas.Generic
{
	internal static class ReflectionUtils
	{
		private static IEnumerable<Assembly> AllAssemblies => AppDomain.CurrentDomain.GetAssemblies();

		internal static IEnumerable<Type> FindAllChildrenInCurrentAssembly<TBase>()
			=> typeof(TBase).Assembly.GetAllChildrenOfType<TBase>();

		internal static IEnumerable<Type> FindAllChildrenInAllAssemblies<TBase>()
			=> AllAssemblies.SelectMany((a) => a.GetAllChildrenOfType<TBase>());
	}
}