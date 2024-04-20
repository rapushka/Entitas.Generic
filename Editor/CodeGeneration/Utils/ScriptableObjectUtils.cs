#if UNITY_EDITOR
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
			var settings = ScriptableObject.CreateInstance<T>();

			AssetDatabase.CreateAsset(settings, path);
			AssetDatabase.SaveAssets();
			return settings;
		}
	}
}
#endif