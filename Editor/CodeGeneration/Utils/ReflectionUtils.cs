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
			=> GetAllChildrenOfType<TBase>(typeof(TBase).Assembly);

		internal static IEnumerable<Type> FindAllChildrenInAllAssemblies<TBase>()
			=> AllAssemblies.SelectMany(GetAllChildrenOfType<TBase>);

		private static IEnumerable<Type> GetAllChildrenOfType<TBase>(Assembly @this)
			=> @this.GetTypes().Where(t => !t.IsAbstract && IsDerivedFrom<TBase>(t));

		// Duplicates because the extensions of mine are internal
		internal static bool IsDerivedFrom<T>(Type @this) => IsDerivedFrom(@this, typeof(T));

		internal static bool IsDerivedFrom(Type @this, Type other) => other.IsAssignableFrom(@this);

		internal static T GetPrivateFieldValue<T>(this object @this, string fieldName)
			=> (T)@this.GetPrivateFieldValue(fieldName);

		internal static object GetPrivateFieldValue(this object @this, string fieldName)
			=> @this.GetPrivateField(fieldName).GetValue(@this);

		internal static void SetPrivateFieldValue<T>(object @this, string fieldName, T value)
			=> @this.GetPrivateField(fieldName).SetValue(@this, value);

		internal static T GetStaticField<T>(this Type @this, string fieldName)
			=> (T)@this.EnsureField(fieldName, BindingFlags.Static | BindingFlags.Public).GetValue(null);

		internal static void SetStaticField<T>(this Type @this, string fieldName, T value)
			=> @this.EnsureField(fieldName, BindingFlags.Static | BindingFlags.Public).SetValue(null, value);

		private static FieldInfo GetPrivateField(this object @this, string fieldName)
			=> @this.GetType().EnsureField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

		private static FieldInfo EnsureField(this Type @this, string fieldName, BindingFlags flags)
			=> @this.GetField(fieldName, flags) ?? throw NoFieldException(fieldName, @this, flags);

		private static Exception NoFieldException(string fieldName, MemberInfo type, BindingFlags flags)
			=> new NullReferenceException($"Type {type.Name} doesn't contain {flags} {fieldName} field");
	}
}