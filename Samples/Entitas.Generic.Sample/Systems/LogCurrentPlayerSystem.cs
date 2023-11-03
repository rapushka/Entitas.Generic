using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using static Entitas.Generic.Matcher<Sample.GameScope>;

namespace Sample
{
	public class LogCurrentPlayerSystem : ReactiveSystem<Entity<GameScope>>
	{
		private readonly Contexts _contexts;

		public LogCurrentPlayerSystem(Contexts contexts)
			: base(contexts.Get<GameScope>())
		{
			_contexts = contexts;
		}

		private Entity<GameScope> CurrentPlayer
			=> _contexts.Get<GameScope>().Unique.GetEntity<CurrentPlayer>();

		protected override ICollector<Entity<GameScope>> GetTrigger(IContext<Entity<GameScope>> context)
			=> context.CreateCollector(Get<CurrentPlayer>().Added());

		protected override bool Filter(Entity<GameScope> entity) => true;

		protected override void Execute(List<Entity<GameScope>> entities)
		{
			Console.WriteLine($"Current player changed to {CurrentPlayer.GetName()}");
		}
	}
}