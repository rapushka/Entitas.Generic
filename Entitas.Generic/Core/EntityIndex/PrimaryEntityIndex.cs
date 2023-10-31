namespace Entitas.Generic
{
	public class PrimaryEntityIndex<TScope, TComponent, TValue>
		where TScope : IScope
		where TComponent : ValueComponent<TValue>, new()
	{
		private static PrimaryEntityIndex<TScope, TComponent, TValue> _instance;

		public static string Name => typeof(TComponent).Name;

		public static PrimaryEntityIndex<TScope, TComponent, TValue> Instance
			=> _instance ??= new PrimaryEntityIndex<TScope, TComponent, TValue>();

		private static ScopeContext<TScope> Context => Contexts.Instance.Get<TScope>();

		public PrimaryEntityIndex<TScope, TComponent, TValue> Initialize()
		{
			Context.AddEntityIndex
			(
				new PrimaryEntityIndex<Entity<TScope>, TValue>
				(
					Name,
					Context.GetGroup(Matcher<TScope>.Get<TComponent>()),
					(_, c) => ((TComponent)c).Value
				)
			);

			return this;
		}

		public Entity<TScope> GetEntity(TValue value)
			=> ((PrimaryEntityIndex<Entity<TScope>, TValue>)Context.GetEntityIndex(Name)).GetEntity(value);

		public bool HasEntity(TValue value)
			=> GetEntity(value) is not null;
	}
}