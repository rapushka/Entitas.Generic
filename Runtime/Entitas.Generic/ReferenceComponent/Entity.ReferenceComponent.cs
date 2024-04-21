namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public Entity<TScope> Bind<TComponent, TFrom>(Entity<TScope> other)
			where TComponent : ReferenceComponent<TFrom>, IInScope<TScope>, new()
			where TFrom : PrimaryIndexComponent<int>, IInScope<TScope>, new()
		{
			Ensure<TScope, TFrom>(other);
			Add<TComponent, int>(other.Get<TFrom>().Value);

			return this;
		}

		public Entity<TScope> Bind<TComponent, TTargetScope, TFrom>(Entity<TTargetScope> other)
			where TTargetScope : IScope
			where TComponent : ReferenceComponent<TFrom>, IInScope<TScope>, new()
			where TFrom : PrimaryIndexComponent<int>, IInScope<TTargetScope>, new()
		{
			Ensure<TTargetScope, TFrom>(other);
			Add<TComponent, int>(other.Get<TFrom>().Value);

			return this;
		}

		public Entity<TTargetScope> GetBindEntity<TComponent, TTargetScope, TFrom>()
			where TTargetScope : IScope
			where TComponent : ReferenceComponent<TFrom>, IInScope<TScope>, new()
			where TFrom : PrimaryIndexComponent<int>, IInScope<TTargetScope>, new()
		{
			var index = Contexts.Instance.Get<TTargetScope>().GetPrimaryIndex<TFrom, int>();
			return index.GetEntity(Get<TComponent>().Value);
		}
	}
}