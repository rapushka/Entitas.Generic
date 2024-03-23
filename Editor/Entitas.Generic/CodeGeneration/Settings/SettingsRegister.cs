using System.Linq;
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
		private static SerializedProperty _generatorsProperty;
		private static SerializedProperty _namespaceProperty;

		private static bool EnableCodeGeneration => _enableCodeGenerationProperty.boolValue;

		private static void OnActivate(string searchContext, VisualElement rootElement)
		{
			_settings = Settings.SerializedSettings;

			_enableCodeGenerationProperty = Find(nameof(Settings.EnableCodeGeneration));
			_generateOnRecompileProperty = Find(nameof(Settings.GenerateOnRecompile));
			_outputPathProperty = Find(nameof(Settings.OutputPath));
			_customOutputEditorPathProperty = Find(nameof(Settings.CustomOutputEditorPath));
			_outputEditorPathProperty = Find(nameof(Settings.OutputEditorPath));
			_generatorsProperty = Find(nameof(Settings.Generators));
			_namespaceProperty = Find(nameof(Settings.BaseNamespace));

			var generators = _generatorsProperty.GetBoxedArray<GeneratorBase>().ToList();
			CollectGenerators(ref generators);
			_generatorsProperty.SetBoxedArray(generators.ToArray());
		}

		private static SerializedProperty Find(string propertyName)
			=> _settings.FindProperty(propertyName.WrapSerializedProperty());

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
			DrawEnabledGeneratorsList();
			EditorGUILayout.PropertyField(_namespaceProperty);
		}

		private static void DrawPathsFields()
		{
			EditorGUILayout.PropertyField(_outputPathProperty);
			EditorGUILayout.PropertyField(_customOutputEditorPathProperty);
			var useCustomPathForEditorOutput = _customOutputEditorPathProperty.boolValue;

			using (new EditorGUI.DisabledScope(disabled: !useCustomPathForEditorOutput))
			{
				if (!useCustomPathForEditorOutput)
					DuplicatePaths();

				EditorGUILayout.PropertyField(_outputEditorPathProperty);
			}
		}

		private static void DrawEnabledGeneratorsList()
		{
			EditorGUILayout.LabelField("Generators:");
			using var scope = new EditorGUI.IndentLevelScope(increment: 1);

			for (var i = 0; i < _generatorsProperty.arraySize; i++)
			{
				var generator = (GeneratorBase)_generatorsProperty.GetArrayElementAtIndex(i).boxedValue;
				generator.Enabled = EditorGUILayout.ToggleLeft(generator.Name, generator.Enabled);
			}
		}
	}
}