using System;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace Entitas.Generic
{
	public static class ReflectionExtensions
	{
		public static void SetPrivateFieldValue<T>(this T @this, string fieldName, object value)
		{
			var fieldInfo = GetPrivateField(@this, fieldName);

			fieldInfo.SetValue(@this, value);
		}

		private static FieldInfo GetPrivateField<T>(T @this, string fieldName)
		{
			var type = @this.GetType();
			var fieldInfo = type.GetField(fieldName, Instance | NonPublic);

			return fieldInfo ?? throw new NullReferenceException(NoField(fieldName, type));
		}

		private static string NoField
			(string fieldName, Type type) => $"Type {type.Name} doesn't contain private instance {fieldName} field";
	}
}