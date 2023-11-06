namespace Entitas.Generic
{
	public class Matcher<TScope, TComponent>
		where TScope : IScope
		where TComponent : IComponent, new()
	{
		private static IMatcher<Entity<TScope>> _cached;

		public static IMatcher<Entity<TScope>> Instance
		{
			get
			{
				if (_cached == null)
				{
					var matcher = (Entitas.Matcher<Entity<TScope>>)Entitas.Matcher<Entity<TScope>>
					                                                      .AllOf(ComponentIndex<TScope, TComponent>.Id);
					matcher.componentNames = ComponentsLookup<TScope>.ComponentNames;
					_cached = matcher;
				}

				return _cached;
			}
		}
	}

	public class Matchers
	{
		public static IMatcher<Entity<TScope>> Get<TScope, TComponent>()
			where TScope : IScope
			where TComponent : IComponent, new()
			=> Matcher<TScope, TComponent>.Instance;

		public static IAllOfMatcher<Entity<TScope>> AllOf<TScope>(params IMatcher<Entity<TScope>>[] matchers)
			where TScope : IScope
			=> Entitas.Matcher<Entity<TScope>>.AllOf(matchers);

		public static IAnyOfMatcher<Entity<TScope>> AnyOf<TScope>(params IMatcher<Entity<TScope>>[] matchers)
			where TScope : IScope
			=> Entitas.Matcher<Entity<TScope>>.AnyOf(matchers);
	}
}