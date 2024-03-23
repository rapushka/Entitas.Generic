using UnityEditor;

namespace Entitas.Generic
{
	internal static partial class SettingsRegister
	{
		[SettingsProvider]
		public static SettingsProvider CreateMyCustomSettingsProvider()
			=> new(Settings.PathInWindow, SettingsScope.Project)
			{
				guiHandler = OnGUI,
				activateHandler = OnActivate,
				keywords = new[] { "Entitas.Generic", "Code Generation" },
			};

		private static void UpdateDefines()
		{
			if (EnableCodeGeneration)
				PlayerDefinesUtil.AddDefineSymbol(Constants.Define.EnableCodeGeneration);
			else
				PlayerDefinesUtil.RemoveDefineSymbol(Constants.Define.EnableCodeGeneration);
		}
	}
}