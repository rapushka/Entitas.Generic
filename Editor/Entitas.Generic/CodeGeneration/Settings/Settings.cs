using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	[CreateAssetMenu(fileName = FileName, menuName = "Entitas/Generic/Settings")]
	internal class Settings : ScriptableObject
	{
		public const string Path = "Packages/com.rapuska.entitas-generic/Editor/Entitas.Generic/CodeGeneration/Settings/" + FileName + ".asset";
		public const string PathInWindow = "+375/Entitas.Generic/Code Generation";
		public const string FileName = "Entitas.Generic Settings";

		[field: SerializeField] public bool EnableCodeGeneration { get; private set; }
		[field: SerializeField] public bool GenerateOnRecompile  { get; private set; }

		private static Settings _instance;

		internal static Settings Instance => _instance ??= ScriptableObjectUtils.LoadOrCreate<Settings>(Path);

		internal static SerializedObject SerializedSettings => new(Instance);
	}
}