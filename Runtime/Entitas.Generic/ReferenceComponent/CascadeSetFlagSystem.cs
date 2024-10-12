using System.Collections.Generic;

namespace Entitas.Generic
{
	public sealed class CascadeSetFlagSystem<TTargetScope, TChildrenScope, TFlag, TFrom, TReference> : ReactiveSystem<Entity<TTargetScope>>
		where TTargetScope : IScope
		where TChildrenScope : IScope
		where TFlag : FlagComponent, IInScope<TTargetScope>, IInScope<TChildrenScope>, new()
		where TReference : ReferenceComponent<TFrom>, IInScope<TChildrenScope>, new()
		where TFrom : PrimaryIndexComponent<int>, IInScope<TTargetScope>, new()
	{
		public CascadeSetFlagSystem(Contexts contexts) : base(contexts.Get<TTargetScope>()) { }

		protected override ICollector<Entity<TTargetScope>> GetTrigger(IContext<Entity<TTargetScope>> context)
			=> context.CreateCollector
			(
				ScopeMatcher<TTargetScope>.AllOf
				(
					ScopeMatcher<TTargetScope>.Get<TFrom>(),
					ScopeMatcher<TTargetScope>.Get<TFlag>()
				).AddedOrRemoved()
			);

		protected override bool Filter(Entity<TTargetScope> entity) => entity.Has<TFrom>();

		protected override void Execute(List<Entity<TTargetScope>> entities)
		{
			foreach (var parent in entities)
			foreach (var child in parent.GetEntities<TChildrenScope, TFrom, TReference>())
				child.Is<TFlag>(parent.Is<TFlag>());
		}
	}
}