using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entitas.Generic
{
	public static class ComponentTypeManager<TScope>
		where TScope : IScope
	{
		private static readonly List<Type> _componentIdxTypes = new();
		private static int _lastComponentIdx;
		private static bool _initialized;

		public static string[] ComponentNames { get; private set; }

		public static Type[] ComponentTypes { get; private set; }

		public static int TotalComponents => _componentIdxTypes.Count;

		public static void AutoScan()
		{
			if (_initialized)
				return;

			foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany((a) => a.GetTypes()))
			{
				if (!type.HasAttribute<TScope>())
					continue;

				if (type.IsDerivedFrom<IComponent>())
					Register(type);

				if (type.IsDerivedFrom(typeof(IEvent)))
					Register(typeof(ListenerComponent<,>).MakeGenericType(typeof(TScope), type));
			}

			ComponentTypes = _componentIdxTypes.Select(x => x.GetGenericArguments()[1]).ToArray();
			ComponentNames = ComponentTypes.Select(x => x.Name).ToArray();

			_initialized = true;
		}

		private static void Register(Type componentType)
		{
			var idxType = typeof(ComponentIndex<,>);
			var genericType = idxType.MakeGenericType(typeof(TScope), componentType);

			if (_componentIdxTypes.Contains(genericType))
				return;

			_componentIdxTypes.Add(genericType);
			genericType.SetStaticField("Id", _lastComponentIdx++);
		}
	}
}