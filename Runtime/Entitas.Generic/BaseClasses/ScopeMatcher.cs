﻿namespace Entitas.Generic
{
	public class ScopeMatcher<TScope>
		where TScope : IScope
	{
		public static IMatcher<Entity<TScope>> Get<TComponent>()
			where TComponent : IComponent, new()
			=> Matchers.Get<TScope, TComponent>();

		public static IAllOfMatcher<Entity<TScope>> AllOf(params IMatcher<Entity<TScope>>[] matchers)
			=> Matchers.AllOf(matchers);

		public static IAnyOfMatcher<Entity<TScope>> AnyOf(params IMatcher<Entity<TScope>>[] matchers)
			=> Matchers.AnyOf(matchers);
	}
}