#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Entitas.Generic
{
	internal static class SerializedPropertyArrayExtensions
	{
		internal static T[] GetArray<T>(this SerializedProperty property)
			where T : Object
		{
			var array = new T[property.arraySize];

			for (var i = 0; i < property.arraySize; i++)
				array[i] = property.GetArrayElementAtIndex(i).objectReferenceValue as T;

			return array;
		}

		internal static void SetArray<T>(this SerializedProperty @this, T[] value)
			where T : Object
		{
			@this.ClearArray();
			@this.arraySize = value.Length;

			for (var i = 0; i < value.Length; i++)
				@this.GetArrayElementAtIndex(i).objectReferenceValue = value[i];
		}
	}
}
#endif