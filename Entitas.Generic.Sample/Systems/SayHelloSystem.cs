using System;
using Entitas;
using Entitas.Generic;
using static Entitas.Generic.Matcher<Sample.GameScope>;

namespace Sample
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