#if UNITY_EDITOR
namespace Entitas.Generic
{
	public sealed class RegisterBehavioursSystem : IInitializeSystem
	{
		private readonly Contexts _contexts;
		private readonly EntityBehaviour[] _behaviours;

		public RegisterBehavioursSystem(Contexts contexts, EntityBehaviour[] behaviours)
		{
			_contexts = contexts;
			_behaviours = behaviours;
		}

		public void Initialize()
		{
			foreach (var behaviour in _behaviours)
				behaviour.Register(_contexts);
		}
	}
}
#endif