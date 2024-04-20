namespace Entitas.Generic
{
	public interface ICleanup<TStrategy> where TStrategy : ICleanupStrategy { }

	public interface ICleanupStrategy { }

	public class RemoveComponent : ICleanupStrategy { }

	public class DestroyEntity : ICleanupStrategy { }
}