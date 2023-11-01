namespace Entitas.Generic
{
	public class DestroyEntitySystem<TComponent, TScope> : ICleanupSystem
		where TScope : IScope
		where TComponent : IComponent, new()
	{
		private readonly IGroup<Entity<TScope>> _entities;

		public DestroyEntitySystem(Contexts contexts)
		{
			_entities = contexts.Get<TScope>().GetGroup(Matcher<TScope>.Get<TComponent>());
		}

		public void Cleanup()
		{
			foreach (var e in _entities.GetEntities())
				e.Destroy();
		}
	}
}