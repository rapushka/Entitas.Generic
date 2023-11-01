// Copyright (c) 2012-2022 FuryLion Group. All Rights Reserved.

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