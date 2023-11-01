using Entitas.Generic;

namespace Sample
{
	public static class NameExtensions
	{
		public static string GetName(this Entity<GameScope> @this)
			=> @this.Has<Name>() ? @this.Get<Name>().Value : "entity";
	}
}