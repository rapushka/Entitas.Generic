using System;
using Entitas;
using Entitas.Generic;

namespace Sample
{
	public class FindJackSystem : IInitializeSystem
	{
		// ReSharper disable once UnusedParameter.Local - for consistent .Add
		public FindJackSystem(Contexts contexts) { }

		public void Initialize()
		{
			var entity = Id.Index.GetEntity(Constants.JackId);

			Console.WriteLine($"There's Jack! {entity}");
		}
	}
}