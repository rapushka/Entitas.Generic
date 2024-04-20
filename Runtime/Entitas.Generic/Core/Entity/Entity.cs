namespace Entitas.Generic
{
	public partial class Entity<TScope> : Entity
		where TScope : IScope
	{
		public Entity<TScope> Add<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> Add(Create<TComponent>());

		public TComponent Create<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> CreateComponent<TComponent>(Id<TComponent>());

		public TComponent Get<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> (TComponent)GetComponent(Id<TComponent>());

		public bool Has<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> HasComponent(Id<TComponent>());

		public Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
		{
			RemoveComponent(Id<TComponent>());
			return this;
		}

		/// <summary> Id because Identify;) </summary>
		private static int Id<TComponent>()
			where TComponent : IComponent, IInScope<TScope>, new()
			=> ComponentIndex<TScope, TComponent>.Value;
	}
}