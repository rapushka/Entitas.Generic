namespace Entitas.Generic
{
	// ReSharper disable once UnusedTypeParameter – Used implicitly
	public interface ICleanup<TStrategy> where TStrategy : ICleanupStrategy;

	public interface ICleanupStrategy;

	public class RemoveComponent : ICleanupStrategy;

	public class DestroyEntity : ICleanupStrategy;
}