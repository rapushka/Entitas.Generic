using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Entitas.Generic
{
	internal static class ReflectionExtensions
	{
		internal static bool HasAttribute<T>(this Type @this) => @this.GetCustomAttribute(typeof(T)) != null;

		internal static bool IsDerivedFrom<T>(this Type @this) => @this.IsDerivedFrom(typeof(T));

		internal static bool IsDerivedFrom(this Type @this, Type other) => other.IsAssignableFrom(@this);

		internal static IEnumerable<Type> GetAllChildrenOfType<TBase>(this Assembly @this)
			=> @this.GetTypes().Where(t => !t.IsAbstract && t.IsDerivedFrom<TBase>());
	}
}