using JetBrains.Annotations;
using UnityEngine;

namespace Entitas.Generic
{
	[UsedImplicitly]
	internal class ComponentIDGenerator : GeneratorBase
	{
		public override string Name => nameof(ComponentIDGenerator);

		public override void Generate()
		{
			Debug.Log(nameof(ComponentIDGenerator) + " Generated!");
		}
	}
}