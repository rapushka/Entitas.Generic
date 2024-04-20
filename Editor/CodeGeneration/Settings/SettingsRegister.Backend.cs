#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Entitas.Generic
{
	internal static partial class SettingsRegister
	{
		[SettingsProvider]
		public static SettingsProvider CreateMyCustomSettingsProvider()
			=> new(Settings.PathInWindow, SettingsScope.Project)
			{
				guiHandler = OnGUI,
				activateHandler = OnActivate,
				keywords = new[] { "Entitas.Generic", "Code Generation" },
			};

		private static void UpdateDefines()
		{
			if (EnableCodeGeneration)
				PlayerDefinesUtil.AddDefineSymbol(Constants.Define.EnableCodeGeneration);
			else
				PlayerDefinesUtil.RemoveDefineSymbol(Constants.Define.EnableCodeGeneration);
		}

		private static void DuplicatePaths()
			=> _outputEditorPathProperty.stringValue = $"{_outputPathProperty.stringValue}/Editor/";

		private static void CollectGenerators(ref List<GeneratorBase> previous)
		{
			var types = ReflectionUtils
				.FindAllChildrenInCurrentAssembly<GeneratorBase>();

			foreach (var type in types)
			{
				var generator = (GeneratorBase)Activator.CreateInstance(type);

				if (!previous.Contains(generator))
					previous.Add(generator);
			}

			previous = previous.OrderBy((g) => g.Name).ToList();
		}

		private static void DuplicateNamespaces()
			=> _editorNamespaceProperty.stringValue = $"{_namespaceProperty.stringValue}.Editor";
	}
}
#endif