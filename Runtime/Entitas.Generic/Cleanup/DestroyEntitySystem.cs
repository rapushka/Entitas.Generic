namespace Entitas.Generic
{
	public class DestroyEntitySystem<TScope, TComponent> : ICleanupSystem
		where TScope : IScope
		where TComponent : IComponent, ICleanup<DestroyEntity>, IInScope<TScope>, new()
	{
		private readonly IGroup<Entity<TScope>> _entities;

		public DestroyEntitySystem(Contexts contexts)
		{
			_entities = contexts.GetGroup(ScopeMatcher<TScope>.Get<TComponent>());
		}

		public void Cleanup()
		{
			foreach (var e in _entities.GetEntities())
				e.Destroy();
		}
	}
}