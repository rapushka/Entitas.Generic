using System;
using static Entitas.Generic.Matcher<Entitas.Generic.Sample.PureCSharp.GameScope>;

namespace Entitas.Generic.Sample.PureCSharp
{
	public class ShowItemsSystem : IInitializeSystem
	{
		private readonly IGroup<Entity<GameScope>> _players;
		private readonly IGroup<Entity<GameScope>> _items;

		public ShowItemsSystem(Contexts contexts)
		{
			_players = contexts.Get<GameScope>().GetGroup(Get<Player>());
			_items = contexts.Get<GameScope>().GetGroup(Get<Item>());
		}

		public void Initialize()
		{
			Console.Write("\n---\n");

			foreach (var item in _items)
			{
				var ownerId = item.Get<OwnerId>();
				var owner = Id.Index.GetEntity(ownerId.Value);

				Console.WriteLine($"item {item.GetName()} belongs to {owner.GetName()}");
			}

			Console.Write("\n---\n");

			foreach (var player in _players)
			{
				var id = player.Get<Id>();
				var items = OwnerId.Index.GetEntities(id.Value);

				Console.WriteLine($"{player.GetName()} has:");

				foreach (var item in items)
					Console.WriteLine($"\t{item.GetName()}");

				Console.WriteLine();
			}

			Console.Write("\n---\n");
		}
	}
}