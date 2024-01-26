using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		[PublicAPI]
		public Entity<TScope> Add<TComponent, TValue>(TValue value)
			where TComponent : ValueComponent<TValue>, IInScope<TScope>, new()
		{
			var component = Create<TComponent>();
			component.Value = value;

			return Add(component);
		}

		[PublicAPI]
		public Entity<TScope> Replace<TComponent, TValue>(TValue value)
			where TComponent : ValueComponent<TValue>, IInScope<TScope>, new()
		{
			var component = Create<TComponent>();
			component.Value = value;

			return Replace(component);
		}

		[PublicAPI]
		public TValue Get<TComponent, TValue>()
			where TComponent : ValueComponent<TValue>, IInScope<TScope>, new()
			=> Get<TComponent>().Value;
	}
}