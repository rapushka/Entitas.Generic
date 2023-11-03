namespace Entitas.Generic
{
	public abstract class SelfRegisterEntityBehaviour<TScope> : EntityBehaviour<TScope>
		where TScope : IScope
	{
		private void Start()
		{
			Register(Contexts.Instance);
		}
	}
}