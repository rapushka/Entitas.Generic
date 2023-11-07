using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public void Set<TComponent>(bool value)
			where TComponent : FlagComponent, IUnique, new()
		{
			if (Has<TComponent>() == value)
				return;

			if (value)
				Create<TComponent>();
			else
				Remove<TComponent>();
		}

		[PublicAPI]
		public void Set<TComponent, TValue>(TValue value)
			where TComponent : ValueComponent<TValue>, IUnique, new()
			=> EnsureEntity<TComponent>().Replace<TComponent, TValue>(value);
	}
}