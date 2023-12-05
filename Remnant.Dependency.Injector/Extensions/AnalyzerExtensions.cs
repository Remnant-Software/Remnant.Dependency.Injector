using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Remnant.Dependency.Injector.Extensions
{
	public static class SyntaxNodeExtensions
	{
		public static string GetNamespace(this SyntaxNode syntaxNode)
		{
			return string.Join(".", syntaxNode
					  .Ancestors()
					  .OfType<BaseNamespaceDeclarationSyntax>()
					  .Reverse()
					  .Select(_ => _.Name)
				 );
		}
	}
}
