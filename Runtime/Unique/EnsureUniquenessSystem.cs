using System;
using System.Collections.Generic;

namespace Entitas.Generic
{
	public class EnsureUniquenessSystem<TScope, TComponent> : ReactiveSystem<Entity<TScope>>
		where TScope : IScope
		where TComponent : IComponent, IUnique, IInScope<TScope>, new()
	{
		private readonly IGroup<Entity<TScope>> _entities;

		public EnsureUniquenessSystem(Contexts contexts)
			: base(contexts.Get<TScope>())
		{
			_entities = contexts.GetGroup(ScopeMatcher<TScope>.Get<TComponent>());
		}

		protected override ICollector<Entity<TScope>> GetTrigger(IContext<Entity<TScope>> context)
			=> context.CreateCollector(ScopeMatcher<TScope>.Get<TComponent>().Added());

		protected override bool Filter(Entity<TScope> entity) => entity.Has<TComponent>();

		protected override void Execute(List<Entity<TScope>> entities)
		{
			if (_entities.count > 1)
				throw new InvalidOperationException("There is more, than one Unique entity!");
		}
	}
}