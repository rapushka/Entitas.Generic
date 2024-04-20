namespace Entitas.Generic
{
	public class PrimaryEntityIndex<TScope, TComponent, TValue>
		: EntityIndexBase<TScope, TComponent, TValue, PrimaryEntityIndexFactory<Entity<TScope>, TValue>,
			PrimaryEntityIndex<TScope, TComponent, TValue>>
		where TScope : IScope
		where TComponent : PrimaryIndexComponent<TValue>, IInScope<TScope>, new()
	{
		public TComponent Get(TValue value) => GetEntity(value).Get<TComponent>();

		public bool HasEntity(TValue value) => GetEntity(value) is not null;

		public Entity<TScope> GetEntity(TValue value)
			=> ((PrimaryEntityIndex<Entity<TScope>, TValue>)Index).GetEntity(value);

		public bool TryGetEntity(TValue value, out Entity<TScope> entity)
		{
			entity = GetEntity(value);
			return entity is not null;
		}
	}
}