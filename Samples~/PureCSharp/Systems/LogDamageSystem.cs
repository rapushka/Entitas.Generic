using System;
using static Entitas.Generic.Matcher<Entitas.Generic.Sample.PureCSharp.GameScope>;

namespace Entitas.Generic.Sample.PureCSharp
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