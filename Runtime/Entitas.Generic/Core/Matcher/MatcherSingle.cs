namespace Entitas.Generic
{
	public class MatcherSingle<TScope, TComponent>
		where TScope : IScope
		where TComponent : IComponent, new()
	{
		private static IMatcher<Entity<TScope>> _instance;

		public static IMatcher<Entity<TScope>> Instance
		{
			get
			{
				if (_instance == null)
				{
					var index = ComponentIndex<TScope, TComponent>.Value;
					var matcher = (Matcher<Entity<TScope>>)Matcher<Entity<TScope>>.AllOf(index);
					matcher.componentNames = ComponentsLookup<TScope>.Instance.ComponentNames;
					_instance = matcher;
				}

				return _instance;
			}
		}
	}
}