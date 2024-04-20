namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public bool Is<TComponent>()
			where TComponent : FlagComponent, IInScope<TScope>, new()
			=> Has<TComponent>();

		public Entity<TScope> Is<TComponent>(bool value)
			where TComponent : FlagComponent, IInScope<TScope>, new()
		{
			if (value)
				AddSafety<TComponent>();
			else
				RemoveSafety<TComponent>();

			return this;
		}
	}
}