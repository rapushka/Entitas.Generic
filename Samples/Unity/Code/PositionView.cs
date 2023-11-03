// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

namespace Entitas.Generic.Sample.Unity
{
	public class PositionView : BaseListener<GameScope, Position>
	{
		[SerializeField] private Transform _transform;

		public override void OnValueChanged(Entity<GameScope> entity, Position component)
		{
			_transform.position = component.Value;
		}
	}
}