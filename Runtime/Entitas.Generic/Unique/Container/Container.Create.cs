namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		private Entity<TScope> Create<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> _context.CreateEntity().Add<TComponent>();

		private Entity<TScope> Create<TComponent, TValue>(TValue value)
			where TComponent : ValueComponent<TValue>, IUnique, IInScope<TScope>, new()
			=> _context.CreateEntity().Add<TComponent, TValue>(value);
	}
}