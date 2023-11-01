namespace Entitas.Generic
{
	public class PrimaryEntityIndex<TScope, TComponent, TValue>
		: EntityIndexBase<TScope, TComponent, TValue, PrimaryEntityIndexFactory<Entity<TScope>, TValue>>
		where TScope : IScope
		where TComponent : ValueComponent<TValue>, new()
	{
		public TComponent Get(TValue value) => GetEntity(value).Get<TComponent>();

		public bool HasEntity(TValue value) => GetEntity(value) is not null;

		public Entity<TScope> GetEntity(TValue value)
			=> ((PrimaryEntityIndex<Entity<TScope>, TValue>)Context.GetEntityIndex(Name)).GetEntity(value);
	}
}