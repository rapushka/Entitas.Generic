﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Entitas.Generic
{
	public class ComponentsLookup<TScope>
		where TScope : IScope
	{
		private readonly List<Type> _componentTypes = new();

		private int _lastComponentIndex;
		private bool _initialized;

		private static ComponentsLookup<TScope> _instance;
		public static ComponentsLookup<TScope> Instance => _instance ??= new ComponentsLookup<TScope>();

		private ComponentsLookup() { }

		public string[] ComponentNames { get; private set; }

		public Type[] ComponentTypes { get; private set; }

		public int TotalComponents => _componentTypes.Count;

		private static IEnumerable<Type> AllTypes
			=> AppDomain.CurrentDomain.GetAssemblies().SelectMany((a) => a.GetTypes());

		public void Initialize()
		{
			if (_initialized)
				return;

			RegisterAllTypes();

			ComponentTypes = _componentTypes.ToArray();
			ComponentNames = ComponentTypes.Select((x) => x.Name).ToArray();

			_initialized = true;
		}

		private void RegisterAllTypes()
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

		private void RegisterListener(Type componentType)
			=> Register(typeof(ListenerComponent<,>).MakeGenericType(typeof(TScope), componentType));

		private void Register(Type componentType)
		{
			var indexType = typeof(ComponentIndex<,>).MakeGenericType(typeof(TScope), componentType);

			if (_componentTypes.Contains(componentType))
				return;

			_componentTypes.Add(componentType);
			indexType.SetStaticField("Id", _lastComponentIndex++);
		}
	}
}