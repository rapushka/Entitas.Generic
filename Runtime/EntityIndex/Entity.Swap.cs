namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public void SwapValues<TComponent, TValue>(Entity<TScope> target)
			where TComponent : PrimaryIndexComponent<TValue>, IInScope<TScope>, new()
		{
			var ourValue = Get<TComponent>().Value;
			var theirValue = target.Get<TComponent>().Value;

			Remove<TComponent>();
			target.Remove<TComponent>();

			Add<TComponent, TValue>(theirValue);
			target.Add<TComponent, TValue>(ourValue);
		}

		/// <summary> Can be used for the situation where one of entities has the component, and other isn't </summary>
		public void SwapValuesSafety<TComponent, TValue>(Entity<TScope> target)
			where TComponent : PrimaryIndexComponent<TValue>, IInScope<TScope>, new()
		{
			var ourComponent = GetOrDefault<TComponent>();
			var ourValue = ourComponent is null ? default : ourComponent.Value;

			var theirComponent = target.GetOrDefault<TComponent>();
			var theirValue = theirComponent is null ? default : theirComponent.Value;

			RemoveSafety<TComponent>();
			target.RemoveSafety<TComponent>();

			if (theirComponent is not null)
				Replace<TComponent, TValue>(theirValue);

			if (ourComponent is not null)
				target.Replace<TComponent, TValue>(ourValue);
		}
	}
}