using System.Collections.Generic;

namespace Entitas.Generic
{
	public class EntityIndex<TScope, TComponent, TValue>
		: EntityIndexBase<TScope, TComponent, TValue, EntityIndexFactory<Entity<TScope>, TValue>,
			EntityIndex<TScope, TComponent, TValue>>
		where TScope : IScope
		where TComponent : IndexComponent<TValue>, IInScope<TScope>, new()
	{
		public HashSet<Entity<TScope>> GetEntities(TValue value)
			=> ((EntityIndex<Entity<TScope>, TValue>)Index).GetEntities(value);
	}
}