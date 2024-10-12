#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;

namespace Entitas.Generic
{
	public static class PlayerDefinesUtil
	{
		private static string Defines
		{
			get => PlayerSettings.GetScriptingDefineSymbols(BuildTarget);
			set => PlayerSettings.SetScriptingDefineSymbols(BuildTarget, value);
		}

		private static NamedBuildTarget BuildTarget
			=> NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

		public static void AddDefineSymbol(string symbol)
		{
			var defines = Defines;

			if (defines.Contains(symbol))
				return;

			defines += ";" + symbol;
			Defines = defines;
		}

		public static void RemoveDefineSymbol(string symbol)
		{
			var defines = Defines;

			if (!defines.Contains(symbol))
				return;

			defines = defines.Replace(symbol, string.Empty);
			defines = defines.Replace(";;", ";");
			Defines = defines;
		}
	}
}
#endif