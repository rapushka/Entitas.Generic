using System;
using Entitas;
using Entitas.Generic;

namespace Sample
{
	public class GameScope : Attribute, IScope { }

	public class InputScope : Attribute, IScope { }

	public static class GameCtx
	{
		public static ScopeContext<GameScope> Inst => ScopeContext<GameScope>.Instance;
	}

	public class GameMatchers
	{
		public static IMatcher<Entity<GameScope>> Get<TComponent>()
			where TComponent : IComponent, new()
			=> Matchers.Get<GameScope, TComponent>();

		public static IAllOfMatcher<Entity<GameScope>> AllOf(params IMatcher<Entity<GameScope>>[] matchers)
			=> Matchers.AllOf(matchers);

		public static IAnyOfMatcher<Entity<GameScope>> AnyOf(params IMatcher<Entity<GameScope>>[] matchers)
			=> Matchers.AnyOf(matchers);
	}
}