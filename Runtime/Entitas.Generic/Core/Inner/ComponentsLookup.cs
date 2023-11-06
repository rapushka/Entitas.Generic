using System;
using System.Collections.Generic;
using System.Linq;

namespace Entitas.Generic
{
	public static class ComponentsLookup<TScope>
		where TScope : IScope
	{
		private static readonly List<Type> _componentTypes = new();

		private static int _lastComponentIndex;
		private static bool _initialized;

		public static string[] ComponentNames { get; private set; }

		public static Type[] ComponentTypes { get; private set; }

		public static int TotalComponents => _componentTypes.Count;

		private static IEnumerable<Type> AllTypes
			=> AppDomain.CurrentDomain.GetAssemblies().SelectMany((a) => a.GetTypes());

		public static void Initialize()
		{
			if (_initialized)
				return;

			RegisterAllTypes();

			ComponentTypes = _componentTypes.ToArray();
			ComponentNames = ComponentTypes.Select((x) => x.Name).ToArray();

			_initialized = true;
		}

		private static void RegisterAllTypes()
		{
			foreach (var type in AllTypes)
			{
				if (!type.HasAttribute<TScope>())
					continue;

				if (type.IsDerivedFrom<IComponent>())
					Register(type);

				if (type.IsDerivedFrom<IEvent>())
					RegisterListener(type);
			}
		}

		private static void RegisterListener(Type componentType)
			=> Register(typeof(ListenerComponent<,>).MakeGenericType(typeof(TScope), componentType));

		private static void Register(Type componentType)
		{
			var indexType = typeof(ComponentIndex<,>).MakeGenericType(typeof(TScope), componentType);

			if (_componentTypes.Contains(componentType))
				return;

			_componentTypes.Add(componentType);
			indexType.SetStaticField("Id", _lastComponentIndex++);
		}
	}
}