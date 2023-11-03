namespace Entitas.Generic.Sample.Unity
{
	public class EntitasAdapter : EntitasAdapterBase
	{
		protected override void Initialize(Contexts contexts)
		{
			contexts.InitializeScope<GameScope>();
		}
	}
}