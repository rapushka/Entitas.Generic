using System.Collections.Generic;
using System.Linq;

namespace Entitas.Generic
{
	public abstract class EntityComponentsListFormatter<TScope> : IEntityFormatter<TScope>
		where TScope : IScope
	{
		public string ToString(Entity<TScope> entity)
			=> string.Join(Separator, CreateList(entity).Where((s) => !string.IsNullOrEmpty(s)));

		protected virtual string Separator => " ";

		protected abstract IEnumerable<string> CreateList(Entity<TScope> entity);
	}
}