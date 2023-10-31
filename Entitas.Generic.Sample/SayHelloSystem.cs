using System;
using Entitas;
using Entitas.Generic;

namespace Sample
{
	public class SayHelloSystem : IExecuteSystem
	{
		private readonly IGroup<Entity<GameScope>> _players;

		public SayHelloSystem(Contexts contexts)
		{
			_players = contexts.Get<GameScope>()
			                   .GetGroup(GameMatchers.AllOf(GameMatchers.Get<Id>(), GameMatchers.Get<Name>()));
		}

		public void Execute()
		{
			foreach (var entity in _players.GetEntities())
				Console.WriteLine($"Hello {entity.Get<Name>().Value}!");
		}
	}
}