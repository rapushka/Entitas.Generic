using JetBrains.Annotations;

namespace Entitas.Generic
{
	public class ScopeMatcher<TScope>
		where TScope : IScope
	{
		[PublicAPI]
		public static IMatcher<Entity<TScope>> Get<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> MatcherSingle<TScope, TComponent>.Instance;

		[PublicAPI]
		public static IAllOfMatcher<Entity<TScope>> AllOf(params IMatcher<Entity<TScope>>[] matchers)
			=> Matcher<Entity<TScope>>.AllOf(matchers);

		[PublicAPI]
		public static IAnyOfMatcher<Entity<TScope>> AnyOf(params IMatcher<Entity<TScope>>[] matchers)
			=> Matcher<Entity<TScope>>.AnyOf(matchers);
	}
}