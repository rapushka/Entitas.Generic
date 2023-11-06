using Entitas.VisualDebugging.Unity;
using JetBrains.Annotations;
using UnityEngine;

namespace Entitas.Generic
{
	public class Contexts
	{
		private static Contexts _instance;
		public static Contexts Instance => _instance ??= new Contexts();

		private Contexts() { }

		[PublicAPI]
		public void InitializeScope<TScope>()
			where TScope : IScope
		{
			ComponentsLookup<TScope>.Instance.Initialize();
			var context = new ScopeContext<TScope>(AERCFactories.SafeAERCFactory);

			InitScopeObserver(context);
		}

		[PublicAPI]
		public ScopeContext<TScope> Get<TScope>()
			where TScope : IScope
			=> ScopeContext<TScope>.Instance;

		[PublicAPI]
		public IGroup<Entity<TScope>> GetGroup<TScope>(IMatcher<Entity<TScope>> matcher)
			where TScope : IScope
			=> Get<TScope>().GetGroup(matcher);

		[UsedImplicitly]
		private void InitScopeObserver(IContext context)
		{
#if UNITY_EDITOR
			if (Application.isPlaying)
			{
				var observer = new ContextObserver(context);
				Object.DontDestroyOnLoad(observer.gameObject);
			}
#endif
		}
	}
}