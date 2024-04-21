using System.Collections.Generic;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public HashSet<Entity<TScope>> GetEntities<TPrimaryIndexComponent, TIndexComponent>()
			where TPrimaryIndexComponent : PrimaryIndexComponent<int>, IInScope<TScope>, new()
			where TIndexComponent : IndexComponent<int>, IInScope<TScope>, new()
			=> GetEntities<TScope, TPrimaryIndexComponent, TIndexComponent, int>();

		public HashSet<Entity<TTargetScope>> GetEntities<TTargetScope, TPrimaryIndexComponent, TIndexComponent>()
			where TTargetScope : IScope
			where TPrimaryIndexComponent : PrimaryIndexComponent<int>, IInScope<TScope>, new()
			where TIndexComponent : IndexComponent<int>, IInScope<TTargetScope>, new()
			=> GetEntities<TTargetScope, TPrimaryIndexComponent, TIndexComponent, int>();

		public HashSet<Entity<TResultScope>> GetEntities<TResultScope, TPrimaryIndexComponent, TIndexComponent, TValue>()
			where TResultScope : IScope
			where TPrimaryIndexComponent : PrimaryIndexComponent<TValue>, IInScope<TScope>, new()
			where TIndexComponent : IndexComponent<TValue>, IInScope<TResultScope>, new()
		{
			var index = Contexts.Instance.Get<TResultScope>().GetIndex<TIndexComponent, TValue>();
			return index.GetEntities(Get<TPrimaryIndexComponent>().Value);
		}

		private static void Ensure<TTargetScope, TFrom>(Entity<TTargetScope> other)
			where TTargetScope : IScope
			where TFrom : PrimaryIndexComponent<int>, IInScope<TTargetScope>, new()
		{
			if (!other.Has<TFrom>() && typeof(TFrom).IsDerivedFrom<IFromCreationIndex>())
				other.Add<TFrom, int>(other.creationIndex);
		}
	}
}