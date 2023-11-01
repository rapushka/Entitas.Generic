namespace Entitas.Generic
{
	public class EntityIndexBase<TScope, TComponent, TValue, TEntityIndexFactory>
		where TScope : IScope
		where TComponent : ValueComponent<TValue>, new()
		where TEntityIndexFactory : IEntityIndexFactory<Entity<TScope>, TValue>, new()
	{
		private readonly TEntityIndexFactory _entityIndexFactory = new();
		private static EntityIndex<TScope, TComponent, TValue> _instance;

		public static EntityIndex<TScope, TComponent, TValue> Instance
			=> _instance ??= new EntityIndex<TScope, TComponent, TValue>();

		protected static string Name => typeof(TComponent).Name;

		protected static ScopeContext<TScope> Context => Contexts.Instance.Get<TScope>();

		public void Initialize()
		{
			Context.AddEntityIndex
			(
				_entityIndexFactory.Create
				(
					Name,
					Context.GetGroup(Matcher<TScope>.Get<TComponent>()),
					(_, c) => ((TComponent)c).Value
				)
			);
		}
	}
}