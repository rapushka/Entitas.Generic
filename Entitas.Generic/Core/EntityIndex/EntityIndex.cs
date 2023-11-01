using System.Collections.Generic;

namespace Entitas.Generic
{
	public class EntityIndex<TScope, TComponent, TValue>
		: EntityIndexBase<TScope, TComponent, TValue, EntityIndexFactory<Entity<TScope>, TValue>>
		where TScope : IScope
		where TComponent : ValueComponent<TValue>, new()
	{
		public HashSet<Entity<TScope>> GetEntities(TValue value)
			=> ((EntityIndex<Entity<TScope>, TValue>)Context.GetEntityIndex(Name)).GetEntities(value);
	}
}