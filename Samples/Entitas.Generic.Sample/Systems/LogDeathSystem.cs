using System;
using Entitas;
using Entitas.Generic;
using static Entitas.Generic.Matcher<Sample.GameScope>;

namespace Sample
{
	public class LogDeathSystem : IExecuteSystem
	{
		private readonly IGroup<Entity<GameScope>> _players;

		public LogDeathSystem(Contexts contexts)
		{
			_players = contexts.Get<GameScope>().GetGroup(AllOf(Get<Player>(), Get<Dead>()));
		}

		public void Execute()
		{
			foreach (var entity in _players.GetEntities())
				Console.WriteLine($"{entity.GetName()} died:(");
		}
	}
}