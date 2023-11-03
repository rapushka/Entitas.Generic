using System;

namespace Entitas.Generic.Sample.PureCSharp
{
	public class FightSystem : IExecuteSystem
	{
		private readonly Random _random;

		// ReSharper disable once UnusedParameter.Local - for .Add consistency
		public FightSystem(Contexts contexts)
		{
			_random = new Random();
		}

		private int RandomDamage => _random.Next(20);

		public void Execute()
		{
			if (!Id.Index.HasEntity(Constants.JackId) || !Id.Index.HasEntity(Constants.SteveId))
				return;

			var jackEntity = Id.Index.GetEntity(Constants.JackId);
			var steveEntity = Id.Index.GetEntity(Constants.SteveId);

			Attack(jackEntity.Is<CurrentPlayer>() ? steveEntity : jackEntity);
		}

		private void Attack(Entity<GameScope> target)
		{
			var damage = RandomDamage;
			target.Add<Damaged, int>(damage);

			if (damage > 15)
				target.Is<Dead>(true);
		}
	}
}