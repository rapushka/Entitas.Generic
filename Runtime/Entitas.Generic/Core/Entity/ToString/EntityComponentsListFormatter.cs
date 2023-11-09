using System.Collections.Generic;
using System.Linq;

namespace Entitas.Generic
{
	public abstract class EntityComponentsListFormatter<TScope> : IEntityFormatter<TScope>
		where TScope : IScope
	{
		public string ToString(Entity<TScope> entity)
			=> string.Join(" ", CreateList(entity).Where((s) => !string.IsNullOrEmpty(s)));

		protected abstract IEnumerable<string> CreateList(Entity<TScope> entity);
	}
}