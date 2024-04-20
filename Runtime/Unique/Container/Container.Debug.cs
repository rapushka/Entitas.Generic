using System;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		private static InvalidOperationException AlreadyHasComponentException<TComponent>()
			=> new($"The {NameOf<TScope>()} already has {NameOf<TComponent>()} component!");

		private static string NameOf<T>() => typeof(T).Name;
	}
}