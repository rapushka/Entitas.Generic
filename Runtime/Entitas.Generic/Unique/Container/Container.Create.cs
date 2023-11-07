using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		private Entity<TScope> Create<TComponent>()
			where TComponent : IComponent, IUnique, new()
			=> _context.CreateEntity().Add<TComponent>();

		[PublicAPI]
		private Entity<TScope> Create<TComponent, TValue>(TValue value)
			where TComponent : ValueComponent<TValue>, IUnique, new()
			=> _context.CreateEntity().Add<TComponent, TValue>(value);
	}
}