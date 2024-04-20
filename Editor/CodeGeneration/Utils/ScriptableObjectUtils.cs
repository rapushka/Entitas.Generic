#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	internal static class ScriptableObjectUtils
	{
		internal static T LoadOrCreate<T>(string path)
			where T : ScriptableObject
		{
			var settings = AssetDatabase.LoadAssetAtPath<T>(path);

			if (settings == null)
				settings = CreateAndSave<T>(path);

			return settings;
		}

		internal static T CreateAndSave<T>(string path)
			where T : ScriptableObject
		{
			var directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
				throw new InvalidDataException($"At first create the folder in {directoryName}");

			var settings = ScriptableObject.CreateInstance<T>();

			AssetDatabase.CreateAsset(settings, path);
			AssetDatabase.SaveAssets();
			return settings;
		}
	}
}
#endif