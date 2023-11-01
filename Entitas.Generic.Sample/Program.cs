using System;
using System.Threading.Tasks;
using Entitas;
using Entitas.Generic;

namespace Sample
{
	public class Program
	{
		private static Systems _systems;

		public static void Main() => GameLoop().GetAwaiter().GetResult();

		private static async Task GameLoop()
		{
			var contexts = Contexts.Instance;

			contexts.InitializeScope<GameScope>();
			contexts.InitializeScope<InputScope>();

			Id.Index.Initialize();
			OwnerId.Index.Initialize();

			_systems = new Feature()
					// Initialize
					.Add(new SpawnJackSystem(contexts))
					.Add(new SpawnSteveSystem(contexts))
					// .Add(new FindJackSystem(contexts))
					// .Add(new ShowItemsSystem(contexts))

					// Execute
					.Add(new PassCurrentPlayerSystem(contexts))
					.Add(new SayHelloSystem(contexts))
					.Add(new FightSystem(contexts))
					.Add(new LogDamageSystem(contexts))
					.Add(new LogDeathSystem(contexts))
					.Add(new LogCurrentPlayerSystem(contexts))

					// Cleanup
					.Add(new RemoveComponentsSystem<Damaged, GameScope>(contexts))
					.Add(new DestroyEntitySystem<Dead, GameScope>(contexts))
				;

			_systems.Initialize();

			for (var i = 0; i < 5; i++)
			{
				Console.WriteLine("---[frame]---");
				_systems.Execute();
				_systems.Cleanup();
				Console.WriteLine();

				await Task.Delay(50);
			}

			_systems.TearDown();
		}
	}
}