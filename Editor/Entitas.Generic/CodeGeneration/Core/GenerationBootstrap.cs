using System.Linq;
using JetBrains.Annotations;
using UnityEditor;

namespace Entitas.Generic
{
    [InitializeOnLoad]
    internal class GenerationBootstrap
    {
        // ReSharper disable once EmptyConstructor - TODO: fix CodeGeneration
        static GenerationBootstrap()
        {
            // if (Settings.Instance.GenerateOnRecompile)
            // 	Generate();
        }

#if ENTITAS_GENERIC_CODE_GENERATION
		[MenuItem("Tools/" + Constants.MenuItem.Root + "Generate")]
#endif
        [UsedImplicitly]
        private static void Generate()
        {
            foreach (var generator in Settings.Instance.Generators.Where((g) => g.Enabled))
                generator.Generate();
        }
    }
}