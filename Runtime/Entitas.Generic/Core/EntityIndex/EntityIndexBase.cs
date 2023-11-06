namespace Entitas.Generic
{
	public class EntityIndexBase<TScope, TComponent, TValue, TEntityIndexFactory, TEntityIndex>
		where TScope : IScope
		where TComponent : ValueComponent<TValue>, new()
		where TEntityIndexFactory : IEntityIndexFactory<Entity<TScope>, TValue>, new()
		where TEntityIndex : EntityIndexBase<TScope, TComponent, TValue, TEntityIndexFactory, TEntityIndex>, new()
	{
		private readonly TEntityIndexFactory _entityIndexFactory = new();
		private static TEntityIndex _instance;

		public static TEntityIndex Instance
			=> _instance ??= new TEntityIndex();

		protected static string Name => typeof(TComponent).Name;

		protected static ScopeContext<TScope> Context => Contexts.Instance.Get<TScope>();

		public void Initialize()
		{
			Context.AddEntityIndex
			(
				_entityIndexFactory.Create
				(
					Name,
					Contexts.Instance.GetGroup(ScopeMatcher<TScope>.Get<TComponent>()),
					(_, c) => ((TComponent)c).Value
				)
			);
		}
	}
}