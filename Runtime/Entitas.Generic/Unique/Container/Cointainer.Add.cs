using JetBrains.Annotations;

namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		[PublicAPI]
		public Entity<TScope> Add<TSelf>(Entity<TScope> entity)
			where TSelf : IUnique<TSelf, IComponent>, new()
		{
			var uniqueComponent = entity.Create<UniqueComponent<TSelf>>();
			uniqueComponent.Value = new TSelf();
			entity.Add(uniqueComponent);

			_uniqueEntities.Add(Id<TSelf>(), entity);

			return entity;
		}
	}
}