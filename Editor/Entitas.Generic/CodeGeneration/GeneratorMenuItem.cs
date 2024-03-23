using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	[InitializeOnLoad]
	public class GeneratorMenuItem
	{
		static GeneratorMenuItem()
		{
			Debug.Log("Recompiled");
		}
	}
}