namespace Entitas.Generic
{
	public partial class UniqueComponentsContainer<TScope>
	{
		public bool TryGet<TComponent>(out TComponent component)
			where TComponent : IComponent, IUnique, IInScope<TScope>, new()
		{
			var has = Has<TComponent>();
			component = has ? Get<TComponent>() : default;

			return has;
		}

		public bool TryGet<TComponent, TValue>(out TValue value)
			where TComponent : ValueComponent<TValue>, IUnique, IInScope<TScope>, new()
		{
			var has = Has<TComponent>();
			value = has ? Get<TComponent, TValue>() : default;

			return has;
		}
	}
}