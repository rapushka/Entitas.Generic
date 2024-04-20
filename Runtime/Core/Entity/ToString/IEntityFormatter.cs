namespace Entitas.Generic
{
	public interface IEntityFormatter<TScope>
		where TScope : IScope
	{
		string ToString(Entity<TScope> entity);
	}
}