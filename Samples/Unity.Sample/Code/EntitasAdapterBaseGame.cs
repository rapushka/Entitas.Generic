using Entitas.Generic;

namespace Entitas.Generic.Example
{
	public class EntitasAdapter : EntitasAdapterBase
	{
		protected override void Initialize(Contexts contexts)
		{
			contexts.InitializeScope<GameScope>();
		}
	}
}