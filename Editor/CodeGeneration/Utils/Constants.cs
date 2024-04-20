namespace Entitas.Generic
{
	internal static class Constants
	{
		internal static class Path
		{
			public const string Settings = CodeGenerationFolder + "Settings/" + SettingsFileName + ".asset";
			public const string SettingsFileName = "Entitas.Generic Settings";

			private const string CodeGenerationFolder = PackageFolder + "Editor/Entitas.Generic/CodeGeneration/";
			private const string PackageFolder = "Packages/com.rapuska.entitas-generic/";
		}

		internal static class MenuItem
		{
			public const string Root = "+375/Entitas.Generic/";
		}

		internal static class Define
		{
			internal const string EnableCodeGeneration = "ENTITAS_GENERIC_CODE_GENERATION";
		}
	}
}