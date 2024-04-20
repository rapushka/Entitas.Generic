namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		public bool Has<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntityOrDefault<TComponent>() is not null;
	}
}