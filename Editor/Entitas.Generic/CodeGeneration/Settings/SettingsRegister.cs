using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Entitas.Generic
{
	internal static partial class SettingsRegister
	{
		private static SerializedObject _settings;

		private static SerializedProperty _enableCodeGenerationProperty;
		private static SerializedProperty _generateOnRecompileProperty;

		private static bool EnableCodeGeneration => _enableCodeGenerationProperty.boolValue;

		private static void OnActivate(string searchContext, VisualElement rootElement)
		{
			_settings = Settings.SerializedSettings;

			_enableCodeGenerationProperty = _settings.FindProperty(nameof(Settings.EnableCodeGeneration).WrapSerializedProperty());
			_generateOnRecompileProperty = _settings.FindProperty(nameof(Settings.GenerateOnRecompile).WrapSerializedProperty());
		}

		private static void OnGUI(string searchContext)
		{
			using var changeScope = new EditorGUI.ChangeCheckScope();

			using (var enableCodeGenerationCheckScope = new EditorGUI.ChangeCheckScope())
			{
				EditorGUILayout.PropertyField(_enableCodeGenerationProperty);

				if (enableCodeGenerationCheckScope.changed)
					UpdateDefines();
			}

			using (new EditorGUI.DisabledScope(disabled: !EnableCodeGeneration))
				EditorGUILayout.PropertyField(_generateOnRecompileProperty);

			if (changeScope.changed)
				_settings.ApplyModifiedProperties();
		}
	}
}