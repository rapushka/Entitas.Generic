using System;
using Entitas.Generic;

namespace Sample
{
	public class PositionView : BaseListener<GameScope, Position>
	{
		public override void OnValueChanged(Entity<GameScope> entity, Position component)
		{
			Console.WriteLine($"position view: {component.Value}");
		}
	}
}