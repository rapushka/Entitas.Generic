#if UNITY_EDITOR
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Entitas.Generic
{
	[UsedImplicitly]
	internal class ComponentIDDrawerGenerator : GeneratorBase
	{
		public override string Name => nameof(ComponentIDDrawerGenerator);

		public override void Generate()
		{
			var scopes = ReflectionUtils.FindAllChildrenInAllAssemblies<IScope>();

			var code = new StringBuilder();
			using var file = File.CreateText(Settings.Instance.OutputEditorPath + "ComponendIDDrawers.cs");

			foreach (var scope in scopes)
			{
				code.AppendLine(ComponentIDTemplates.PropertyDrawer(scope));
				code.AppendLine();
			}

			code.RemoveLast(2);
			code.Set(CommonTemplates.WrapNamespace(Settings.Instance.EditorNamespace, code.ToString()));
			code.Insert(0, CommonTemplates.HeaderGenerated);

			file.Write(code.ToString());
		}
	}
}
#endif