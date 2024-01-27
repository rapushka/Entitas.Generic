using System;
using UnityEditor;

namespace Entitas.Generic
{
	internal static class SerializedPropertyExtensions
	{
		internal static SerializedProperty EnsurePropertyRelative(this SerializedProperty @this, string name)
			=> @this.FindPropertyRelative(name)
			   ?? throw new NullReferenceException($"{@this} doesn't contain {name}");
	}
}