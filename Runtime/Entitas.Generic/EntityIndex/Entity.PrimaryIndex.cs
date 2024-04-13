namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public Entity<TScope> GetEntity<TIndexComponent, TPrimaryIndexComponent>()
			where TIndexComponent : ReferenceComponent<TPrimaryIndexComponent, int>, IInScope<TScope>, new()
			where TPrimaryIndexComponent : PrimaryIndexComponent<int>, IInScope<TScope>, new()
			=> GetEntity<TScope, TIndexComponent, TPrimaryIndexComponent>();

		public Entity<TResultScope> GetEntity<TResultScope, TIndexComponent, TPrimaryIndexComponent>()
			where TResultScope : IScope
			where TIndexComponent : ReferenceComponent<TPrimaryIndexComponent, int>, IInScope<TScope>, new()
			where TPrimaryIndexComponent : PrimaryIndexComponent<int>, IInScope<TResultScope>, new()
			=> GetEntity<TResultScope, TIndexComponent, TPrimaryIndexComponent, int>();

		public Entity<TResultScope> GetEntity<TResultScope, TIndexComponent, TPrimaryIndexComponent, TValue>()
			where TResultScope : IScope
			where TIndexComponent : ReferenceComponent<TPrimaryIndexComponent, TValue>, IInScope<TScope>, new()
			where TPrimaryIndexComponent : PrimaryIndexComponent<TValue>, IInScope<TResultScope>, new()
		{
			var index = Contexts.Instance.Get<TResultScope>().GetPrimaryIndex<TPrimaryIndexComponent, TValue>();
			return index.GetEntity(Get<TIndexComponent>().Value);
		}
	}
}