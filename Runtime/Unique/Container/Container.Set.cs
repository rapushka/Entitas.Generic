namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		public void Set<TComponent>(bool value)
			where TComponent : FlagComponent, IUnique, IInScope<TScope>, new()
		{
			if (Has<TComponent>() == value)
				return;

			if (value)
				Create<TComponent>();
			else
				Remove<TComponent>();
		}

		public void Set<TComponent, TValue>(TValue value)
			where TComponent : ValueComponent<TValue>, IUnique, IInScope<TScope>, new()
			=> EnsureEntity<TComponent>().Replace<TComponent, TValue>(value);
	}
}