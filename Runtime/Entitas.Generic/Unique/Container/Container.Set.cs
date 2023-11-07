namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
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

		public void Set<TComponent, TValue>(TValue value)
			where TComponent : ValueComponent<TValue>, IUnique, new()
			=> GetEntity<TComponent>().Replace<TComponent, TValue>(value);
	}
}