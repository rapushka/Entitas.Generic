namespace Entitas.Generic
{
	public class RemoveComponentsSystem<TComponent, TScope> : ICleanupSystem
		where TScope : IScope
		where TComponent : IComponent, ICleanup<RemoveComponent>, new()
	{
		private readonly IGroup<Entity<TScope>> _entities;

		public RemoveComponentsSystem(Contexts contexts)
		{
			_entities = contexts.GetGroup(Matcher<TScope>.Get<TComponent>());
		}

		public void Cleanup()
		{
			foreach (var e in _entities.GetEntities())
			{
				if (e.Has<TComponent>())
					e.Remove<TComponent>();
			}
		}
	}
}