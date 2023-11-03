using System;
using static Entitas.Generic.Matcher<Entitas.Generic.Sample.PureCSharp.GameScope>;

namespace Entitas.Generic.Sample.PureCSharp
{
	public class SayHelloSystem : IExecuteSystem
	{
		private readonly IGroup<Entity<GameScope>> _players;

		public SayHelloSystem(Contexts contexts)
		{
			_players = contexts.Get<GameScope>().GetGroup(Get<Player>());
		}

		public void Execute()
		{
			foreach (var entity in _players.GetEntities())
				Console.WriteLine($"Hello {entity.Get<Name, string>()}!");
		}
	}
}