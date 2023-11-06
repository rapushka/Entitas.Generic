using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		[PublicAPI]
		public bool Is<TComponent>()
			where TComponent : FlagComponent, new()
			=> Has<TComponent>();

		[PublicAPI]
		public Entity<TScope> Is<TComponent>(bool value)
			where TComponent : FlagComponent, new()
		{
			if (value)
				AddSafety<TComponent>();
			else
				RemoveSafety<TComponent>();

			return this;
		}
	}
}