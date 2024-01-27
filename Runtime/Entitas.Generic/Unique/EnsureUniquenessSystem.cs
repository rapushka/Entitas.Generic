using System.Collections.Generic;
using UnityEngine;

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
			Debug.Assert(_entities.count <= 1, "There is more, than one Unique entity!");
		}
	}
}