using System.Linq;
using Entitas;
using Entitas.Generic;
using static Entitas.Generic.Matcher<Sample.GameScope>;

namespace Sample
{
	public class PassCurrentPlayerSystem : IInitializeSystem, IExecuteSystem
	{
		private readonly Contexts _contexts;
		private readonly IGroup<Entity<GameScope>> _players;

		private int _currentIndex;

		public PassCurrentPlayerSystem(Contexts contexts)
		{
			_contexts = contexts;
			_players = _contexts.Get<GameScope>().GetGroup(Get<Player>());
		}

		private Entity<GameScope> CurrentPlayer
			=> _contexts.Get<GameScope>().Unique.GetEntity<CurrentPlayer>();

		private int NextIndex => ++_currentIndex > PlayersArray.Length - 1 ? _currentIndex = 0 : _currentIndex;

		private Entity<GameScope>[] PlayersArray => _players.GetEntities();

		public void Initialize()
		{
			PlayersArray.First().AddUnique<CurrentPlayer>();
		}

		public void Execute()
		{
			var currentPlayer = CurrentPlayer;

			if (currentPlayer.isEnabled)
				currentPlayer.RemoveUnique<CurrentPlayer>();

			PlayersArray[NextIndex].AddUnique<CurrentPlayer>();
		}
	}
}