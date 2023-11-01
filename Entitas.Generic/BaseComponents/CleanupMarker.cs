namespace Entitas.Generic
{
	// ReSharper disable once UnusedTypeParameter - used as marker
	public interface ICleanup<TStrategy> where TStrategy : ICleanupStrategy { }

	public interface ICleanupStrategy { }

	public class RemoveComponent : ICleanupStrategy { }

	public class DestroyEntity : ICleanupStrategy { }
}