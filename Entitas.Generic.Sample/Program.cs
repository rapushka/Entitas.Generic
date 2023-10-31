﻿using System.Threading.Tasks;
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

			_systems = new Feature()
			           .Add(new SpawnJackSystem(contexts))
			           .Add(new SayHelloSystem(contexts))
				;

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