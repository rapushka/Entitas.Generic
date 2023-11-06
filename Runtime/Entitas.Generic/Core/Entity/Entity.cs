using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope> : Entity
		where TScope : IScope
	{
		[PublicAPI]
		public Entity<TScope> Add<TComponent>()
			where TComponent : IComponent, new()
			=> Add(Create<TComponent>());

		[PublicAPI]
		public TComponent Create<TComponent>()
			where TComponent : IComponent, new()
			=> CreateComponent<TComponent>(Id<TComponent>());

		[PublicAPI]
		public TComponent Get<TComponent>()
			where TComponent : IComponent, new()
			=> (TComponent)GetComponent(Id<TComponent>());

		[PublicAPI]
		public bool Has<TComponent>()
			where TComponent : IComponent, new()
			=> HasComponent(Id<TComponent>());

		[PublicAPI]
		public Entity<TScope> Remove<TComponent>()
			where TComponent : IComponent, new()
		{
			RemoveComponent(Id<TComponent>());
			return this;
		}

		/// <summary> Id cuz Identify;) </summary>
		private static int Id<TComponent>()
			where TComponent : IComponent, new()
			=> ComponentIndex<TScope, TComponent>.Value;
	}
}