using System;

namespace Entitas.Generic
{
	public class EntityIndexBase<TScope, TComponent, TValue, TEntityIndexFactory, TEntityIndex>
		where TScope : IScope
		where TComponent : ValueComponent<TValue>, IInScope<TScope>, new()
		where TEntityIndexFactory : IEntityIndexFactory<Entity<TScope>, TValue>, new()
		where TEntityIndex : EntityIndexBase<TScope, TComponent, TValue, TEntityIndexFactory, TEntityIndex>, new()
	{
		private readonly TEntityIndexFactory _entityIndexFactory = new();
		private static TEntityIndex _instance;

		public static TEntityIndex Instance
			=> _instance ??= new TEntityIndex();

		private static string Name => typeof(TComponent).Name;

		private static ScopeContext<TScope> Context => Contexts.Instance.Get<TScope>();

		private static InvalidOperationException NotInitializedException
			=> new($"Entity Index for component {Name} in context {Context} wasn't initialized!");

		protected IEntityIndex Index
		{
			get
			{
				try
				{
					return Context.GetEntityIndex(Name);
				}
				catch (ContextEntityIndexDoesNotExistException)
				{
					throw NotInitializedException;
				}
			}
		}

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