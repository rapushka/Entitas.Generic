#if UNITY_EDITOR
using System;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Entitas.Generic
{
	internal static class SerializedPropertyArrayExtensions
	{
		private static NotImplementedException NoBoxedValueException
			=> new("There's no `.boxedValue` in Unity before Unity 2022");

		internal static T[] GetArray<T>(this SerializedProperty property)
			where T : Object
		{
			var array = new T[property.arraySize];

			for (var i = 0; i < property.arraySize; i++)
				array[i] = property.GetArrayElementAtIndex(i).objectReferenceValue as T;

			return array;
		}

		internal static T[] GetBoxedArray<T>(this SerializedProperty property)
		{
#if UNITY_2022_1_OR_NEWER
			var array = new T[property.arraySize];

			for (var i = 0; i < property.arraySize; i++)
				array[i] = (T)property.GetArrayElementAtIndex(i).boxedValue;

			return array;
#else
			throw NoBoxedValueException;
#endif
		}

		internal static void SetArray<T>(this SerializedProperty @this, T[] value)
			where T : Object
		{
			@this.ClearArray();
			@this.arraySize = value.Length;

			for (var i = 0; i < value.Length; i++)
				@this.GetArrayElementAtIndex(i).objectReferenceValue = value[i];
		}

		internal static void SetBoxedArray<T>(this SerializedProperty @this, T[] value)
		{
#if UNITY_2022_1_OR_NEWER
			@this.ClearArray();
			@this.arraySize = value.Length;

			for (var i = 0; i < value.Length; i++)
				@this.GetArrayElementAtIndex(i).boxedValue = value[i];
#else
			throw NoBoxedValueException;
#endif
		}
	}
}
#endif