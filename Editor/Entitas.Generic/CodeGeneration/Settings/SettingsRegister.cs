using System.Collections.Generic;
using UnityEditor;

namespace Entitas.Generic
{
	internal static class SettingsRegister
	{
		[SettingsProvider]
		public static SettingsProvider CreateMyCustomSettingsProvider()
		{
			var provider = new SettingsProvider(Settings.PathInWindow, SettingsScope.Project)
			{
				guiHandler = OnGUI,
				keywords = new HashSet<string>(new[] { "Entitas.Generic", "Code Generation" }),
			};

			return provider;
		}

		private static void OnGUI(string searchContext)
		{
			var settings = Settings.GetSerializedSettings();

			var enableCodeGenerationProperty = settings.FindProperty(nameof(Settings.EnableCodeGeneration).WrapSerializedProperty());
			var generateOnRecompileProperty = settings.FindProperty(nameof(Settings.GenerateOnRecompile).WrapSerializedProperty());

			using var changeScope = new EditorGUI.ChangeCheckScope();

			EditorGUILayout.PropertyField(enableCodeGenerationProperty);
			EditorGUILayout.PropertyField(generateOnRecompileProperty);

			if (changeScope.changed)
				settings.ApplyModifiedProperties();
		}
	}
}