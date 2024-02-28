using JetBrains.Annotations;

namespace Entitas.Generic
{
	[PublicAPI]
	public sealed class RegisterBehavioursSystem : IInitializeSystem
	{
		private readonly Contexts _contexts;
		private readonly EntityBehaviourBase[] _behaviours;

		public RegisterBehavioursSystem(Contexts contexts, EntityBehaviourBase[] behaviours)
		{
			_contexts = contexts;
			_behaviours = behaviours;
		}

		public void Initialize()
		{
			foreach (var behaviour in _behaviours)
				behaviour.CreateEntity(_contexts);

			foreach (var behaviour in _behaviours)
				behaviour.Initialize();
		}
	}
}