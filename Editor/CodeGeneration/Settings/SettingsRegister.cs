#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace Entitas.Generic
{
	internal static partial class SettingsRegister
	{
#if !UNITY_2022_1_OR_NEWER
		private static System.NotImplementedException NoBoxedValueException
			=> new("There's no `.boxedValue` in Unity before Unity 2022");
#endif

		private static SerializedObject _settings;

		private static SerializedProperty _enableCodeGenerationProperty;
		private static SerializedProperty _generateOnRecompileProperty;
		private static SerializedProperty _outputPathProperty;
		private static SerializedProperty _customOutputEditorPathProperty;
		private static SerializedProperty _outputEditorPathProperty;
		private static SerializedProperty _generatorsProperty;
		private static SerializedProperty _namespaceProperty;
		private static SerializedProperty _customNamespaceProperty;
		private static SerializedProperty _editorNamespaceProperty;

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
			_customNamespaceProperty = Find(nameof(Settings.CustomNamespace));
			_editorNamespaceProperty = Find(nameof(Settings.EditorNamespace));

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
			DrawNamespacesFields();
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

#if UNITY_2022_1_OR_NEWER
			for (var i = 0; i < _generatorsProperty.arraySize; i++)
			{
				var generator = (GeneratorBase)_generatorsProperty.GetArrayElementAtIndex(i).boxedValue;
				generator.Enabled = EditorGUILayout.ToggleLeft(generator.Name, generator.Enabled);
			}
#else
			throw NoBoxedValueException;
#endif
		}

		private static void DrawNamespacesFields()
		{
			EditorGUILayout.PropertyField(_namespaceProperty);
			EditorGUILayout.PropertyField(_customNamespaceProperty);
			var useCustomNamespaceForEditor = _customNamespaceProperty.boolValue;

			using (new EditorGUI.DisabledScope(disabled: !useCustomNamespaceForEditor))
			{
				if (!useCustomNamespaceForEditor)
					DuplicateNamespaces();

				EditorGUILayout.PropertyField(_editorNamespaceProperty);
			}
		}
	}
}
#endif