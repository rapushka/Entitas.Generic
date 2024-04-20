namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public Entity<TScope> Add<TComponent>(TComponent component)
			where TComponent : IComponent, IInScope<TScope>, new()
		{
			AddComponent(Id<TComponent>(), component);
			return this;
		}

		public Entity<TScope> Replace<TComponent>(TComponent component)
			where TComponent : IComponent, IInScope<TScope>, new()
		{
			ReplaceComponent(Id<TComponent>(), component);
			return this;
		}
	}
}