using Entitas;
using Entitas.Generic;

namespace Sample
{
	public class JumpSystem : IExecuteSystem
	{
		private readonly Contexts _contexts;

		public JumpSystem(Contexts contexts)
		{
			_contexts = contexts;
		}

		public void Execute()
		{
			if (Program.CurrentFrame == 3)
				return;

			var player = _contexts.Get<GameScope>().Unique.GetEntity<CurrentPlayer>();
			var position = player.Get<Position>().Value;
			position.Y += 2f;
			player.Replace<Position, Vector2>(position);
		}
	}
}