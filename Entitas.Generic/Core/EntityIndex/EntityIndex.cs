using System.Collections.Generic;

namespace Entitas.Generic
{
	public class EntityIndex<TScope, TComponent, TValue>
		where TScope : IScope
		where TComponent : ValueComponent<TValue>, new()
	{
		private static EntityIndex<TScope, TComponent, TValue> _instance;

		public static string Name => typeof(TComponent).Name;

		public static EntityIndex<TScope, TComponent, TValue> Instance
			=> _instance ??= new EntityIndex<TScope, TComponent, TValue>();

		private static ScopeContext<TScope> Context => Contexts.Instance.Get<TScope>();

		public void Initialize()
		{
			Context.AddEntityIndex
			(
				new EntityIndex<Entity<TScope>, TValue>
				(
					Name,
					Context.GetGroup(Matcher<TScope>.Get<TComponent>()),
					(_, c) => ((TComponent)c).Value
				)
			);
		}

		public HashSet<Entity<TScope>> GetEntities(TValue value)
			=> ((EntityIndex<Entity<TScope>, TValue>)Context.GetEntityIndex(Name)).GetEntities(value);
	}
}