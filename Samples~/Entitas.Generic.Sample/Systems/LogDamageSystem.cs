using System;
using Entitas;
using Entitas.Generic;
using static Entitas.Generic.Matcher<Sample.GameScope>;

namespace Sample
{
	public class LogDamageSystem : IExecuteSystem
	{
		private readonly IGroup<Entity<GameScope>> _players;

		public LogDamageSystem(Contexts contexts)
		{
			_players = contexts.Get<GameScope>().GetGroup(AllOf(Get<Player>(), Get<Damaged>()));
		}

		public void Execute()
		{
			foreach (var entity in _players.GetEntities())
				Console.WriteLine($"{entity.GetName()} took {entity.Get<Damaged>().Value} damage");
		}
	}
}