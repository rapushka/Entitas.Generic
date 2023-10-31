namespace Entitas.Generic
{
	public class Contexts
	{
		private static Contexts _instance;
		public static Contexts Instance => _instance ??= new Contexts();

		public void InitializeScope<TScope>()
			where TScope : IScope
		{
			ComponentTypeManager<TScope>.AutoScan();
			var context = new ScopeContext<TScope>(AERCFactories.SafeAERCFactory);

			InitScopeObserver(context);
		}

		private void InitScopeObserver(IContext context)
		{
#if UNITY_EDITOR
            if (UnityEngine.Application.isPlaying)
            {
                var observer = new Entitas.VisualDebugging.Unity.ContextObserver(context);
                UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);
            }
#endif
		}
	}
}