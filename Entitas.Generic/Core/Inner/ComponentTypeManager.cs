using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Entitas.Generic
{
	public static class ComponentTypeManager<TScope>
		where TScope : IScope
	{
		private static readonly List<Type> _componentIdxTypes = new();
		private static int _lastComponentIdx;

		public static string[] ComponentNames { get; private set; }

		public static Type[] ComponentTypes { get; private set; }

		public static int TotalComponents => _componentIdxTypes.Count;

		public static void AutoScan()
		{
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (var type in assembly.GetTypes())
				{
					if (type.GetInterface(nameof(IComponent)) != null
						&& type.GetCustomAttribute(typeof(TScope)) != null)
						Register(type);

					if (typeof(IEvent).IsAssignableFrom(type)
						&& type.GetCustomAttribute(typeof(TScope)) != null)
						Register(typeof(ListenerComponent<,>).MakeGenericType(typeof(TScope), type));
				}
			}

			ComponentTypes = _componentIdxTypes.Select(x => x.GetGenericArguments()[1]).ToArray();
			ComponentNames = ComponentTypes.Select(x => x.Name).ToArray();
		}

		private static void Register(Type componentType)
		{
			var idxType = typeof(ComponentIdx<,>);
			var genericType = idxType.MakeGenericType(typeof(TScope), componentType);

			if (_componentIdxTypes.Contains(genericType))
				return;

			_componentIdxTypes.Add(genericType);

			var fieldInfo = genericType.GetField("Id", BindingFlags.Static | BindingFlags.Public);

			if (fieldInfo == null)
				throw new Exception($"Type `{genericType.Name}' does not contains `Id' field");

			fieldInfo.SetValue(null, _lastComponentIdx);
			_lastComponentIdx++;
		}
	}
}