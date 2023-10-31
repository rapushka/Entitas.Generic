using System;

namespace Entitas.Generic
{
	public class ScopeContext<TScope> : Context<Entity<TScope>>
		where TScope : IScope
	{
		public static ScopeContext<TScope> Instance { get; private set; }

		public ScopeContext(Func<IEntity, IAERC> aercFactory) : base
		(
			ComponentTypeManager<TScope>.TotalComponents,
			1,
			new ContextInfo
			(
				typeof(TScope).Name,
				ComponentTypeManager<TScope>.ComponentNames,
				ComponentTypeManager<TScope>.ComponentTypes
			),
			aercFactory,
			() => new Entity<TScope>()
		)
		{
			Instance = this;
		}

		public TComponent CreateUniqueComponent<TComponent>()
			where TComponent : IComponent, new()
		{
			if (GetUniqueComponent<TComponent>() != null)
				throw new Exception($"Unique Component {typeof(TComponent).Name} already exist");

			var entity = CreateEntity();
			return entity.Add<TComponent>();
		}

		public Entity<TScope> GetEntity<TComponent>()
			where TComponent : IComponent, new()
			=> GetGroup(Matchers.Get<TScope, TComponent>()).GetSingleEntity();

		public TComponent GetUniqueComponent<TComponent>()
			where TComponent : IComponent, new()
		{
			var entity = GetEntity<TComponent>();
			return entity != null ? entity.Get<TComponent>() : default;
		}

		public void SetUniqueComponent<TComponent>(bool value)
			where TComponent : IComponent, new()
		{
			var entity = GetEntity<TComponent>();
			if (value && entity == null)
			{
				CreateEntity().Add<TComponent>();
				return;
			}

			if (value == false && entity != null)
			{
				entity.Destroy();
			}
		}

		public bool Has<TComponent>()
			where TComponent : IComponent, new()
			=> GetEntity<TComponent>() != null;
	}
}