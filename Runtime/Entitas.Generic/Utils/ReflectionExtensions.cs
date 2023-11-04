using System;
using System.Collections.Generic;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace Entitas.Generic
{
	public static class ReflectionExtensions
	{
		public static T GetPrivateFieldValue<T>(this object @this, string fieldName)
			=> (T)@this.GetPrivateField(fieldName).GetValue(@this);

		public static void SetPrivateFieldValue<T>(this object @this, string fieldName, T value)
			=> @this.GetPrivateField(fieldName).SetValue(@this, value);

		public static void SetStaticField<T>(this Type @this, string fieldName, T value)
			=> @this.EnsureField(fieldName, Static | Public).SetValue(null, value);

		private static FieldInfo GetPrivateField(this object @this, string fieldName)
			=> @this.GetType().EnsureField(fieldName, Instance | NonPublic);

		private static FieldInfo EnsureField(this Type @this, string fieldName, BindingFlags flags)
			=> @this.GetField(fieldName, flags) ?? throw new NullReferenceException(NoField(fieldName, @this, flags));

		public static bool HasAttribute<T>(this Type @this) => @this.GetCustomAttribute(typeof(T)) != null;

		public static bool IsDerivedFrom<T>(this Type @this) => @this.IsDerivedFrom(typeof(T));

		public static bool IsDerivedFrom(this Type @this, Type other) => other.IsAssignableFrom(@this);

		private static string NoField(string fieldName, MemberInfo type, BindingFlags flags)
			=> $"Type {type.Name} doesn't contain {flags} {fieldName} field";
	}
}