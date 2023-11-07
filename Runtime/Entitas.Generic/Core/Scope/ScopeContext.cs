using System;

namespace Entitas.Generic
{
	public class ScopeContext<TScope> : Context<Entity<TScope>>
		where TScope : IScope
	{
		public ScopeContext(Func<IEntity, IAERC> aercFactory)
			: base
			(
				ComponentsLookup<TScope>.Instance.TotalComponents,
				startCreationIndex: 1,
				new ContextInfo
				(
					typeof(TScope).Name,
					ComponentsLookup<TScope>.Instance.ComponentNames,
					ComponentsLookup<TScope>.Instance.ComponentTypes
				),
				aercFactory,
				() => new Entity<TScope>()
			)
		{
			Unique = new UniqueComponentsContainer<TScope>(this);
			Instance = this;
		}

		public static ScopeContext<TScope> Instance { get; private set; }

		public UniqueComponentsContainer<TScope> Unique { get; }

		public Entity<TScope> SingleOrDefault(IMatcher<Entity<TScope>> matcher) => GetGroup(matcher).GetSingleEntity();

		public Entity<TScope> Single(IMatcher<Entity<TScope>> matcher)
			=> SingleOrDefault(matcher) ?? throw new NullReferenceException();
	}
}