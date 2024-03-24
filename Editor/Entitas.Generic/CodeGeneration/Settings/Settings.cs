using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	[CreateAssetMenu(fileName = Constants.Path.SettingsFileName, menuName = Constants.MenuItem.Root + "Settings")]
	internal class Settings : ScriptableObject
	{
		public const string PathInWindow = Constants.MenuItem.Root + "Code Generation";

		[field: SerializeField] public bool   EnableCodeGeneration   { get; private set; }
		[field: SerializeField] public bool   GenerateOnRecompile    { get; private set; }
		[field: SerializeField] public string OutputPath             { get; private set; } = "Assets/Code/Generated/";
		[field: SerializeField] public bool   CustomOutputEditorPath { get; private set; }
		[field: SerializeField] public string OutputEditorPath       { get; private set; }

		[field: SerializeReference]
		[field: SerializeField] public GeneratorBase[] Generators { get; private set; }

		[field: SerializeField] public string BaseNamespace   { get; private set; }
		[field: SerializeField] public bool   CustomNamespace { get; private set; }
		[field: SerializeField] public string EditorNamespace { get; private set; }

		private static Settings _instance;

		internal static Settings Instance => _instance ??= ScriptableObjectUtils.LoadOrCreate<Settings>(Constants.Path.Settings);

		internal static SerializedObject SerializedSettings => new(Instance);
	}
}