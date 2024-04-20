using System;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		public TComponent Get<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntity<TComponent>().Get<TComponent>();

		public TValue Get<TComponent, TValue>()
			where TComponent : ValueComponent<TValue>, IUnique, IInScope<TScope>, new()
			=> GetEntity<TComponent>().Get<TComponent>().Value;

		public Entity<TScope> EnsureEntity<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntityOrDefault<TComponent>() ?? Create<TComponent>();

		public Entity<TScope> GetEntity<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntityOrDefault<TComponent>() ?? throw new NullReferenceException();

		public Entity<TScope> GetEntityOrDefault<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> _context.SingleOrDefault(ScopeMatcher<TScope>.Get<TComponent>());
	}
}