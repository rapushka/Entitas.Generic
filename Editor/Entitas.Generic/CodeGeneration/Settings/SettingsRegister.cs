using UnityEditor;
using UnityEngine.UIElements;

namespace Entitas.Generic
{
	internal static partial class SettingsRegister
	{
		private static SerializedObject _settings;

		private static SerializedProperty _enableCodeGenerationProperty;
		private static SerializedProperty _generateOnRecompileProperty;
		private static SerializedProperty _outputPathProperty;
		private static SerializedProperty _customOutputEditorPathProperty;
		private static SerializedProperty _outputEditorPathProperty;

		private static bool EnableCodeGeneration => _enableCodeGenerationProperty.boolValue;

		private static void OnActivate(string searchContext, VisualElement rootElement)
		{
			_settings = Settings.SerializedSettings;

			_enableCodeGenerationProperty = _settings.FindProperty(nameof(Settings.EnableCodeGeneration).WrapSerializedProperty());
			_generateOnRecompileProperty = _settings.FindProperty(nameof(Settings.GenerateOnRecompile).WrapSerializedProperty());
			_outputPathProperty = _settings.FindProperty(nameof(Settings.OutputPath).WrapSerializedProperty());
			_customOutputEditorPathProperty = _settings.FindProperty(nameof(Settings.CustomOutputEditorPath).WrapSerializedProperty());
			_outputEditorPathProperty = _settings.FindProperty(nameof(Settings.OutputEditorPath).WrapSerializedProperty());
		}

		private static void OnGUI(string searchContext)
		{
			using var changeScope = new EditorGUI.ChangeCheckScope();

			DrawEnableToggle();

			using (new EditorGUI.DisabledScope(disabled: !EnableCodeGeneration))
				DrawEnabledSettings();

			if (changeScope.changed)
				_settings.ApplyModifiedProperties();
		}

		private static void DrawEnableToggle()
		{
			using var enableCodeGenerationCheckScope = new EditorGUI.ChangeCheckScope();
			EditorGUILayout.PropertyField(_enableCodeGenerationProperty);

			if (enableCodeGenerationCheckScope.changed)
				UpdateDefines();
		}

		private static void DrawEnabledSettings()
		{
			EditorGUILayout.PropertyField(_generateOnRecompileProperty);

			DrawPathsFields();
		}

		private static void DrawPathsFields()
		{
			EditorGUILayout.PropertyField(_outputPathProperty);
			EditorGUILayout.PropertyField(_customOutputEditorPathProperty);
			var useCustomPathForEditorOutput = _customOutputEditorPathProperty.boolValue;

			using (new EditorGUI.DisabledScope(disabled: !useCustomPathForEditorOutput))
			{
				if (!useCustomPathForEditorOutput)
					_outputEditorPathProperty.stringValue = $"{_outputPathProperty.stringValue}/Editor/";

				EditorGUILayout.PropertyField(_outputEditorPathProperty);
			}
		}
	}
}