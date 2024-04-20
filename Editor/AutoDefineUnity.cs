#if UNITY_EDITOR
using UnityEditor;

namespace Entitas.Generic
{
	/// If you, for some reason, don't want this Define Symbol – just remove this class
	[InitializeOnLoad]
	public class AutoDefineUnity
	{
		static AutoDefineUnity()
			=> PlayerDefinesUtil.AddDefineSymbol("ENTITAS_GENERIC_UNITY_SUPPORT");
	}
}
#endif