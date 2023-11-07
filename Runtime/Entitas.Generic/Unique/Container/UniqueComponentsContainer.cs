namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
		where TScope : IScope
	{
		private readonly ScopeContext<TScope> _context;

		public UniqueComponentsContainer(ScopeContext<TScope> context)
		{
			_context = context;
		}

		private static int Id<TComponent>()
			where TComponent : IComponent, new()
			=> ComponentIndex<TScope, TComponent>.Value;
	}
}