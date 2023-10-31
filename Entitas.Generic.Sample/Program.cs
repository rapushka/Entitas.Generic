using System;
using System.Threading.Tasks;
using Entitas;
using Entitas.Generic;

namespace Sample
{
	[GameScope]
	public class Player : IComponent
	{
		public int Id;
		public string Name;
	}

	public class SayHelloSystem : IExecuteSystem
	{
		public void Execute()
		{
			var group = GameCtx.Inst.GetGroup(GameMatchers.Get<Player>());
			foreach (var item in group.GetEntities())
			{
				Console.WriteLine($"Hello {item.Get<Player>().Name}");
			}
		}
	}

	public class Program
	{
		private static Systems _systems;

		public static void Main()
		{
			GameLoop().GetAwaiter().GetResult();
		}

		private static async Task GameLoop()
		{
			Contexts.Instance.InitializeScope<GameScope>();
			Contexts.Instance.InitializeScope<InputScope>();

			var jack = GameCtx.Inst.CreateEntity();
			var player = jack.Add<Player>();
			player.Id = 1;
			player.Name = "Jack";

			_systems = new Feature().Add(new SayHelloSystem());

			_systems.Initialize();
			while (true)
			{
				_systems.Execute();
				_systems.Cleanup();

				await Task.Delay(500);
			}
			// ReSharper disable once FunctionNeverReturns
		}
	}
}