namespace Entitas.Generic
{
	public partial class Entity<TScope>
	{
		public static IEntityFormatter<TScope> Formatter { get; set; }

		public string ToString<TComponent>(string prefix = "", string postfix = "", string defaultValue = "")
			where TComponent : FlagComponent, IInScope<TScope>, new()
			=> Is<TComponent>() ? $"{prefix}{typeof(TComponent).Name}{postfix}" : defaultValue;

		public string ToString<TComponent, TValue>(string prefix = "", string postfix = "", string defaultValue = "")
			where TComponent : ValueComponent<TValue>, IInScope<TScope>, new()
			=> Has<TComponent>() ? $"{prefix}{Get<TComponent, TValue>()}{postfix}" : defaultValue;

		public override string ToString() => Formatter?.ToString(this) ?? base.ToString();
	}
}