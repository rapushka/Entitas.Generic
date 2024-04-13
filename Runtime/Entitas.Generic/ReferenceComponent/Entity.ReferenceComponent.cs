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
	// 	public static class RequestExtensions
	// {
	// 	public static Entity<Requests> AddTarget(this Entity<Requests> @this, Entity<Game> target)
	// 		=> @this.Add<Component.Target, int>(target.EnsureID());

	// 	public static Entity<Game> GetTarget(this Entity<Requests> @this)
	// 		=> @this.GetEntityByID<Requests, Component.Target>();
	// }
}