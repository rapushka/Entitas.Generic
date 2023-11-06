using System;
using UnityEditor;

namespace Entitas.Generic
{
	public static class SerializedPropertyExtensions
	{
		public static SerializedProperty EnsurePropertyRelative(this SerializedProperty @this, string name)
			=> @this.FindPropertyRelative(name)
			   ?? throw new NullReferenceException($"{@this} doesn't contain {name}");
	}
}