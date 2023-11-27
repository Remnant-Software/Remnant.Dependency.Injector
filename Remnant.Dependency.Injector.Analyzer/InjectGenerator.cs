using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remnant.Dependency.Injector
{
	[Generator]
	class DIGenerator : ISourceGenerator
	{
		public void Initialize(GeneratorInitializationContext context)
		{
		}

		public void Execute(GeneratorExecutionContext context)
		{
			var classes = context.Compilation.SyntaxTrees.SelectMany(st => st.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>());

			foreach (var @class in classes)
			{
				var fieldNodes = (from f in @class.DescendantNodes().OfType<FieldDeclarationSyntax>()
										from d in f.Declaration.Variables
										from attrs in f.AttributeLists
										from attr in attrs.Attributes
										where attr.Name + "Attribute" == "InjectAttribute"
										select f).ToList();

				if (fieldNodes.Any())
				{
					var constructor = @class.Members.OfType<ConstructorDeclarationSyntax>()
							.Where(x => x.ParameterList.Parameters.Count == 0).FirstOrDefault();

					var usings = new List<string>();
					var sb = new StringBuilder();

					var ns = @class.Parent as NamespaceDeclarationSyntax;

					sb.AppendLine("using Remnant.Dependency.Injector;");
					sb.AppendLine($"namespace {ns.Name}");
					sb.AppendLine("{");
					sb.AppendLine($"\tpublic partial class {@class.Identifier.Text}");
					sb.AppendLine("\t{");

					if (constructor == null)
					{
						sb.AppendLine($"\t\tpublic {@class.Identifier.Text}()");
						sb.AppendLine("\t\t{");
						usings = GenerateResolve(context, fieldNodes, sb);
						sb.AppendLine("\t\t}");
					}
					else
					{
						sb.AppendLine($"\t\tpublic static {@class.Identifier.Text} Create()");
						sb.AppendLine("\t\t{");
						sb.AppendLine($"\t\t\tvar instance = new {@class.Identifier.Text}();");
						sb.AppendLine("\t\t\tinstance.Inject();");
						sb.AppendLine("\t\t\treturn instance;");
						sb.AppendLine("\t\t}");

						sb.AppendLine($"\t\tpublic void Inject() {{");
						usings = GenerateResolve(context, fieldNodes, sb);
						sb.AppendLine("\t\t}");
					}

					sb.AppendLine("\t}"); //class
					sb.AppendLine("}"); //ns

					usings.ForEach(u => sb.Insert(0, u + "\n"));

					context.AddSource($"{@class.Identifier.Text}.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
				}
			}
		}

		private static List<string> GenerateResolve(GeneratorExecutionContext context, List<FieldDeclarationSyntax> fieldNodes, StringBuilder sb)
		{
			var usings = new List<string>();

			foreach (var fieldDeclaration in fieldNodes)
			{
				foreach (var variable in fieldDeclaration.Declaration.Variables)
				{
					var fieldSymbol = variable.Identifier.Text;
					var symbol = context.Compilation.GetSemanticModel(variable.SyntaxTree).GetDeclaredSymbol(variable) as IFieldSymbol;
					var attr = symbol.GetAttributes().FirstOrDefault(a => a.AttributeClass.ToDisplayString() == "Remnant.Dependency.Injector.InjectAttribute");
					var injectType = symbol.Type.Name;				
					var useClause = $"using {symbol.Type.ContainingNamespace};";

					if (!usings.Contains(useClause))
					{
						usings.Add(useClause);
					}

					string injectName = null;

					if (attr != null)
					{
						if (attr.ConstructorArguments.Length == 1 && attr.ConstructorArguments[0].Kind == TypedConstantKind.Primitive)
						{
							injectName = attr.ConstructorArguments[0].Value.ToString();
						}
						else if (attr.ConstructorArguments.Length == 2)
						{
							// assuming the first argument is the Type and the second argument is the string
							injectName = attr.ConstructorArguments[1].Value?.ToString();
						}
					}

					if (!string.IsNullOrEmpty(injectName))
						sb.AppendLine($"\t\t\t{fieldSymbol} = this.Resolve<{injectType}>(\"{injectName}\");");
					else
						sb.AppendLine($"\t\t\t{fieldSymbol} = this.Resolve<{injectType}>();");
				}
			}

			return usings;
		}
	}
}
