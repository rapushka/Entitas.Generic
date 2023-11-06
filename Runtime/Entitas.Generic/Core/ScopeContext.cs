using System;

namespace Entitas.Generic
{
	public class ScopeContext<TScope> : Context<Entity<TScope>>
		where TScope : IScope
	{
		public ScopeContext(Func<IEntity, IAERC> aercFactory) : base
		(
			ComponentsLookup<TScope>.Instance.TotalComponents,
			1,
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
			Unique = new UniqueComponentsContainer<TScope>(CreateEntity);
			Instance = this;
		}

		public static ScopeContext<TScope> Instance { get; private set; }

		public UniqueComponentsContainer<TScope> Unique { get; }
	}
}