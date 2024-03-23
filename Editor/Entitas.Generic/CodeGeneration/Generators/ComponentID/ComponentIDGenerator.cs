using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Entitas.Generic
{
	[UsedImplicitly]
	internal class ComponentIDGenerator : GeneratorBase
	{
		public override string Name => nameof(ComponentIDGenerator);

		public override void Generate()
		{
			var scopes = ReflectionUtils.FindAllChildrenInAllAssemblies<IScope>();

			var code = new StringBuilder();
			using var file = File.CreateText(Settings.Instance.OutputPath + "ComponendIDs.cs");

			foreach (var scope in scopes)
			{
				var componentID = ComponentIDTemplates.ComponentID(scope);
				code.AppendLine(componentID);
				code.AppendLine();
			}

			code.Set(CommonTemplates.WrapNamespace(Settings.Instance.BaseNamespace, code.ToString()));

			file.Write(code.ToString());
		}
	}
}