using System;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace Entitas.Generic
{
	internal static class ReflectionAccessExtensions
	{
		internal static T GetPrivateFieldValue<T>(this object @this, string fieldName)
			=> (T)@this.GetPrivateFieldValue(fieldName);

		internal static object GetPrivateFieldValue(this object @this, string fieldName)
			=> @this.GetPrivateField(fieldName).GetValue(@this);

		internal static void SetPrivateFieldValue<T>(this object @this, string fieldName, T value)
			=> @this.GetPrivateField(fieldName).SetValue(@this, value);

		internal static T GetStaticField<T>(this Type @this, string fieldName)
			=> (T)@this.GetStaticField(fieldName);

		internal static object GetStaticField(this Type @this, string fieldName)
			=> @this.EnsureField(fieldName, Static | Public).GetValue(null);

		internal static void SetStaticField<T>(this Type @this, string fieldName, T value)
			=> @this.EnsureField(fieldName, Static | Public).SetValue(null, value);

		private static FieldInfo GetPrivateField(this object @this, string fieldName)
			=> @this.GetType().EnsureField(fieldName, Instance | NonPublic);

		private static FieldInfo EnsureField(this Type @this, string fieldName, BindingFlags flags)
			=> @this.GetField(fieldName, flags) ?? throw new NullReferenceException(NoField(fieldName, @this, flags));

		private static string NoField(string fieldName, MemberInfo type, BindingFlags flags)
			=> $"Type {type.Name} doesn't contain {flags} \"{fieldName}\" field";
	}
}