namespace Entitas.Generic.Sample.PureCSharp
{
	public static class NameExtensions
	{
		public static string GetName(this Entity<GameScope> @this)
			=> @this.Has<Name>() ? @this.Get<Name>().Value : "entity";
	}
}