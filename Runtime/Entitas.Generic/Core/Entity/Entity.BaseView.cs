namespace Entitas.Generic
{
	public partial class Entity<TScope>
		where TScope : IScope
	{
		public Entity<TScope> Register<TComponent>(BaseListener<TScope, TComponent> value)
			where TComponent : IComponent, new()
		{
			value.Register(this);
			return this;
		}
	}
}