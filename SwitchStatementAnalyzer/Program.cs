using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchStatementAnalyzer
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			if (args.Length != 2) 
			{
				Console.WriteLine("Invalid input, type -h for help");
				return;
			}
			// Creating workspace
			MSBuildLocator.RegisterDefaults();
			MSBuildWorkspace workspace = MSBuildWorkspace.Create();

			// Openning solution
			Solution solution = workspace.OpenSolutionAsync(args[0]).Result;
			IEnumerable<Project> projects = solution.Projects;
			Project currProject = projects.First();

			Compilation compilation = currProject.GetCompilationAsync().Result;

			foreach (var file in currProject.Documents) 
			{
				Console.WriteLine($"Analyzing file: {file.Name}");
				SyntaxTree tree = file.GetSyntaxTreeAsync().Result;
				SemanticModel semanticModel = compilation.GetSemanticModel(tree);

				IEnumerable<SwitchStatementSyntax> switchStatementSyntaxes = tree.GetRoot().DescendantNodes().OfType<SwitchStatementSyntax>();

				foreach(SwitchStatementSyntax switchStatement in switchStatementSyntaxes)
				{
					IdentifierNameSyntax identifierName = switchStatement.DescendantNodes().OfType<IdentifierNameSyntax>().First();
					ITypeSymbol identifierType = semanticModel.GetTypeInfo(identifierName).Type;

					if (identifierType.TypeKind == TypeKind.Enum) 
					{
						Console.WriteLine($"Switch по перечислению, количество членов в перечислении: {identifierType.GetMembers().Length - 1}");
					}

					IEnumerable<DefaultSwitchLabelSyntax> defaultSwitch = switchStatement.DescendantNodes().OfType<DefaultSwitchLabelSyntax>();
					if (defaultSwitch.Count() > 0) { Console.WriteLine("Default case detected"); }
				}
			}

		}
		

	}
}
