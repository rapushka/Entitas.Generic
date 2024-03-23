using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Entitas.Generic
{
	// internal class SettingsProvider : UnityEditor.SettingsProvider
	// {
	// 	private SerializedObject _settings;
	//
	// 	public SettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
	// 		: base(path, scope) { }
	//
	// 	public static bool IsSettingsAvailable => File.Exists(Settings.Path);
	//
	// 	public override void OnActivate(string searchContext, VisualElement rootElement)
	// 	{
	// 		_settings = Settings.GetSerializedSettings();
	// 	}
	//
	// 	public override void OnGUI(string searchContext)
	// 	{
	// 		var enableCodeGenerationProperty = _settings.FindProperty(nameof(Settings.EnableCodeGeneration));
	// 		var generateOnRecompileProperty = _settings.FindProperty(nameof(Settings.GenerateOnRecompile));
	//
	// 		EditorGUILayout.PropertyField(enableCodeGenerationProperty);
	// 		EditorGUILayout.PropertyField(generateOnRecompileProperty);
	// 	}
	//
	// 	[SettingsProvider]
	// 	public static UnityEditor.SettingsProvider CreateMyCustomSettingsProvider()
	// 	{
	// 		if (IsSettingsAvailable)
	// 		{
	// 			var provider = new SettingsProvider(Settings.PathInWindow)
	// 			{
	// 				// Automatically extract all keywords from the Styles.
	// 				// keywords = GetSearchKeywordsFromGUIContentProperties<Styles>(),
	// 			};
	//
	// 			return provider;
	// 		}
	//
	// 		// Settings Asset doesn't exist yet; no need to display anything in the Settings window.
	// 		return null;
	// 	}
	// }
}