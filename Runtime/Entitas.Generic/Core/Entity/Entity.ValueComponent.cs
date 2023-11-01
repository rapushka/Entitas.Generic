namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public Entity<TScope> Add<TComponent, TValue>(TValue value)
			where TComponent : ValueComponent<TValue>, new()
		{
			var component = Create<TComponent>();
			component.Value = value;

			return Add(component);
		}

		public Entity<TScope> Replace<TComponent, TValue>(TValue value)
			where TComponent : ValueComponent<TValue>, new()
		{
			var component = Create<TComponent>();
			component.Value = value;

			return Replace(component);
		}

		public TValue Get<TComponent, TValue>()
			where TComponent : ValueComponent<TValue>, new()
			=> Get<TComponent>().Value;
	}
}