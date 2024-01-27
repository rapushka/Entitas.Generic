using System;
using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public TComponent Get<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> EnsureEntity<TComponent>().Get<TComponent>();

		[PublicAPI]
		public Entity<TScope> EnsureEntity<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntityOrDefault<TComponent>() ?? Create<TComponent>();

		[PublicAPI]
		public Entity<TScope> GetEntity<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> GetEntityOrDefault<TComponent>() ?? throw new NullReferenceException();

		[PublicAPI]
		public Entity<TScope> GetEntityOrDefault<TComponent>()
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
			=> _context.SingleOrDefault(ScopeMatcher<TScope>.Get<TComponent>());
	}
}