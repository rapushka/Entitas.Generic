using System;
using System.Linq;

namespace Entitas.Generic
{
	public static class GenericReflectionExtensions
	{
		internal static Type GetScopeType(this object @this)
		{
			var type = @this.GetType();

			while (type is not null && !IsScope(type.GenericTypeArguments.FirstOrDefault()))
				type = type.BaseType;

			if (type is null)
				throw new InvalidOperationException();

			return type.GenericTypeArguments[0];
		}

		internal static bool TryGetScopeType(this object @this, out Type scopeType)
		{
			var type = @this.GetType();

			while (type is not null && !IsScope(type.GenericTypeArguments.FirstOrDefault()))
				type = type.BaseType;

			scopeType = type?.GenericTypeArguments[0];
			return type is not null;
		}

		private static bool IsScope(Type target)
			=> ReflectionUtils.IsDerivedFrom<IScope>(target);
	}
}