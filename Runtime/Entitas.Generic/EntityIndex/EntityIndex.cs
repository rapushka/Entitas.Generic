using System.Collections.Generic;
using JetBrains.Annotations;

namespace Entitas.Generic
{
	public class EntityIndex<TScope, TComponent, TValue>
		: EntityIndexBase<TScope, TComponent, TValue, EntityIndexFactory<Entity<TScope>, TValue>,
			EntityIndex<TScope, TComponent, TValue>>
		where TScope : IScope
		where TComponent : IndexComponent<TValue>, IInScope<TScope>, new()
	{
		[PublicAPI]
		public HashSet<Entity<TScope>> GetEntities(TValue value)
			=> ((EntityIndex<Entity<TScope>, TValue>)Context.GetEntityIndex(Name)).GetEntities(value);
	}
}