using System;

namespace Entitas.Generic
{
	public class ContextsBase
	{
		private static ContextsBase _instance;
		public static ContextsBase Instance => _instance ??= new ContextsBase();

		protected ContextsBase() { }

		public virtual void Initialize()
		{
			throw new NotImplementedException("Initialize here Scopes, EntityIndexes, etc.");
		}

		public void InitializeScope<TScope>()
			where TScope : IScope
		{
			ComponentTypeManager<TScope>.AutoScan();
			var context = new ScopeContext<TScope>(AERCFactories.SafeAERCFactory);

			InitScopeObserver(context);
		}

		public ScopeContext<TScope> Get<TScope>()
			where TScope : IScope
			=> ScopeContext<TScope>.Instance;

		// ReSharper disable once UnusedParameter.Local - used in #if
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

	public class ContextsBase<TContexts> : ContextsBase
		where TContexts : ContextsBase<TContexts>, new()
	{
		private static TContexts _instance;
		public new static TContexts Instance => _instance ??= new TContexts();
	}
}