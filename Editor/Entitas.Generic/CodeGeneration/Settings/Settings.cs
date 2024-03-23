using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	[CreateAssetMenu(fileName = FileName, menuName = "Entitas/Generic/Settings")]
	public class Settings : ScriptableObject
	{
		public const string Path = "Packages/com.rapuska.entitas-generic/Editor/Entitas.Generic/CodeGeneration/Settings/" + FileName + ".asset";
		public const string PathInWindow = "+375/Entitas.Generic/Code Generation";
		public const string FileName = "Entitas.Generic Settings";

		[field: SerializeField] public bool EnableCodeGeneration { get; private set; }
		[field: SerializeField] public bool GenerateOnRecompile  { get; private set; }

		internal static Settings GetOrCreateSettings()
		{
			var settings = AssetDatabase.LoadAssetAtPath<Settings>(Path);
			if (settings == null)
			{
				settings = CreateInstance<Settings>();

				AssetDatabase.CreateAsset(settings, Path);
				AssetDatabase.SaveAssets();
			}

			return settings;
		}

		internal static SerializedObject GetSerializedSettings() => new(GetOrCreateSettings());
	}
}