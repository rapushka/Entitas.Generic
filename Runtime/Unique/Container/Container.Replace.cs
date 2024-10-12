namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		public void Replace<TComponent>(Entity<TScope> newUnique)
			where TComponent : FlagComponent, IInScope<TScope>, IUnique, new()
		{
			var oldUnique = GetEntityOrDefault<TComponent>();
			oldUnique?.Is<TComponent>(false);

			newUnique.Is<TComponent>(true);
		}
	}
}