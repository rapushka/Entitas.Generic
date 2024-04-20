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
			=> (T)GetPrivateFieldValue(@this, fieldName);

		internal static object GetPrivateFieldValue(object @this, string fieldName)
			=> GetPrivateField(@this, fieldName).GetValue(@this);

		internal static void SetPrivateFieldValue<T>(object @this, string fieldName, T value)
			=> GetPrivateField(@this, fieldName).SetValue(@this, value);

		internal static T GetStaticField<T>(Type @this, string fieldName)
			=> (T)EnsureField(@this, fieldName, BindingFlags.Static | BindingFlags.Public).GetValue(null);

		internal static void SetStaticField<T>(Type @this, string fieldName, T value)
			=> EnsureField(@this, fieldName, BindingFlags.Static | BindingFlags.Public).SetValue(null, value);

		private static FieldInfo GetPrivateField(object @this, string fieldName)
			=> EnsureField(@this.GetType(), fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

		private static FieldInfo EnsureField(Type @this, string fieldName, BindingFlags flags)
			=> @this.GetField(fieldName, flags) ?? throw NoFieldException(fieldName, @this, flags);

		private static Exception NoFieldException(string fieldName, MemberInfo type, BindingFlags flags)
			=> new NullReferenceException($"Type {type.Name} doesn't contain {flags} {fieldName} field");
	}
}