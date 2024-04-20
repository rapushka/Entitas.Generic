#if UNITY_EDITOR
using System.Linq;
using UnityEditor;

namespace Entitas.Generic
{
	[InitializeOnLoad]
	internal class GenerationBootstrap
	{
		static GenerationBootstrap()
		{
			if (Settings.Instance.GenerateOnRecompile)
				Generate();
		}

#if ENTITAS_GENERIC_CODE_GENERATION
		[MenuItem("Tools/" + Constants.MenuItem.Root + "Generate")]
#endif
		private static void Generate()
		{
			foreach (var generator in Settings.Instance.Generators.Where((g) => g.Enabled))
				generator.Generate();
		}
	}
}
#endif