using System;

namespace Entitas.Generic.Sample.PureCSharp
{
	public class PositionView : BaseListener<GameScope, Position>
	{
		public override void OnValueChanged(Entity<GameScope> entity, Position component)
		{
			Console.WriteLine($"position view: {component.Value}");
		}
	}
}