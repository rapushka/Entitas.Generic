using System;
using static Entitas.Generic.Matcher<Entitas.Generic.Sample.PureCSharp.GameScope>;

namespace Entitas.Generic.Sample.PureCSharp
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