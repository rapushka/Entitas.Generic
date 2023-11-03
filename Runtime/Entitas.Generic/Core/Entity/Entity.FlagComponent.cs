namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public bool Is<TComponent>()
			where TComponent : FlagComponent, new()
			=> Has<TComponent>();

		public Entity<TScope> Is<TComponent>(bool value)
			where TComponent : FlagComponent, new()
		{
			if (value && !Has<TComponent>())
				Add<TComponent>();

			if (!value && Has<TComponent>())
				Remove<TComponent>();

			return this;
		}
	}
}