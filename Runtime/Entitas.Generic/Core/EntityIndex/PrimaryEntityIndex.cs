using JetBrains.Annotations;

namespace Entitas.Generic
{
	public class PrimaryEntityIndex<TScope, TComponent, TValue>
		: EntityIndexBase<TScope, TComponent, TValue, PrimaryEntityIndexFactory<Entity<TScope>, TValue>,
			PrimaryEntityIndex<TScope, TComponent, TValue>>
		where TScope : IScope
		where TComponent : ValueComponent<TValue>, new()
	{
		[PublicAPI]
		public TComponent Get(TValue value) => GetEntity(value).Get<TComponent>();

		[PublicAPI]
		public bool HasEntity(TValue value) => GetEntity(value) is not null;

		[PublicAPI]
		public Entity<TScope> GetEntity(TValue value)
			=> ((PrimaryEntityIndex<Entity<TScope>, TValue>)Context.GetEntityIndex(Name)).GetEntity(value);
	}
}