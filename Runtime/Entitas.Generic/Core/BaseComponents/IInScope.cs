using JetBrains.Annotations;

namespace Entitas.Generic
{
	public interface IInScope<[UsedImplicitly] TScope>
		where TScope : IScope { }
}