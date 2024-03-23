using UnityEditor;
using UnityEngine;

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
		[MenuItem("Tools/+375/Entitas.Generic/Generate")]
#endif
		private static void Generate()
		{
			Debug.Log("Generated");
		}
	}
}