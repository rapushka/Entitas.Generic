// Copyright (c) 2012-2022 FuryLion Group. All Rights Reserved.

using Entitas;
using Entitas.Generic;
using static Entitas.Generic.Matcher<Sample.GameScope>;

namespace Sample
{
	public class GravitySystem : IExecuteSystem
	{
		private readonly IGroup<Entity<GameScope>> _entities;

		public GravitySystem(Contexts contexts)
		{
			_entities = contexts.Get<GameScope>().GetGroup(Get<Position>());
		}

		public void Execute()
		{
			foreach (var entity in _entities)
			{
				var position = entity.Get<Position>().Value;
				if (position.Y <= 0f)
					continue;

				position.Y -= 0.5f;
				entity.Replace<Position, Vector2>(position);
			}
		}
	}
}