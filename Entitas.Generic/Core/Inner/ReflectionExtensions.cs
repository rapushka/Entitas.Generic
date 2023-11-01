using System;
using System.Reflection;

namespace Entitas.Generic
{
	public static class ReflectionExtensions
	{
		public static bool HasAttribute<T>(this Type @this) => @this.GetCustomAttribute(typeof(T)) != null;

		public static bool IsDerivedFrom<T>(this Type @this) => @this.IsDerivedFrom(typeof(T));

		public static bool IsDerivedFrom(this Type @this, Type type) => type.IsAssignableFrom(@this);

		public static void SetStaticField<T>(this Type @this, string fieldName, T value)
			=> @this.GetField(fieldName, BindingFlags.Static | BindingFlags.Public)!.SetValue(null, value);
	}
}