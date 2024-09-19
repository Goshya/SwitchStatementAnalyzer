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
	public class Program
	{
		public static async Task Main(string[] args)
		{
			if (args.Length != 2) 
			{
				Console.WriteLine("Invalid input, type -h for help");
				return;
			}
			// Creating workspace
			MSBuildLocator.RegisterDefaults();
			MSBuildWorkspace workspace = MSBuildWorkspace.Create();

			// Openning solution or project
			//Project currProject;
			IEnumerable<Project> projects = new List<Project>();

			if (args[0].EndsWith("sln"))
			{
				Console.WriteLine("SLN");
				Solution solution = workspace.OpenSolutionAsync(args[0]).Result;
				projects = solution.Projects;
				//currProject = projects.ElementAt(0);
			}
			else
			{
				Console.WriteLine("CSPROJ");
				Project project = workspace.OpenProjectAsync(args[0]).Result;
				projects = projects.Append(project);
				//projects = workspace.OpenProjectAsync(args[0]).Result;
				//currProject = workspace.OpenProjectAsync(args[0]).Result;
			}

			foreach (Project currProject in projects)
			{
				Console.WriteLine($"-------Analyzing {currProject.Name}'s files --------");
				Compilation compilation = currProject.GetCompilationAsync().Result;
				StreamWriter stream = new StreamWriter(args[1], false, Encoding.UTF8);

				Console.WriteLine(currProject.Documents.Count());
				foreach (var file in currProject.Documents)
				{
					Console.WriteLine($"Analyzing file: {file.Name}");
					SyntaxTree tree = file.GetSyntaxTreeAsync().Result;
					SemanticModel semanticModel = compilation.GetSemanticModel(tree);

					IEnumerable<SwitchStatementSyntax> switchStatementSyntaxes = tree.GetRoot().DescendantNodes().OfType<SwitchStatementSyntax>();

					foreach (SwitchStatementSyntax switchStatement in switchStatementSyntaxes)
					{
						IdentifierNameSyntax identifierName = switchStatement.DescendantNodes().OfType<IdentifierNameSyntax>().First();
						ITypeSymbol identifierType = semanticModel.GetTypeInfo(identifierName).Type;

						// Перестаем рассматривать switch если выбор варианта осуществляется не по перечислению
						if (identifierType.TypeKind != TypeKind.Enum)
						{
							continue;
						}

						Console.WriteLine($"Switch по перечислению, количество членов в перечислении: {identifierType.GetMembers().Length - 1}");
						IEnumerable<DefaultSwitchLabelSyntax> defaultSwitch = switchStatement.DescendantNodes().OfType<DefaultSwitchLabelSyntax>();

						// Перестаем рассматривать switch если у него есть default или в перечислении меньше 3 элементов
						if (defaultSwitch.Count() > 0 || (identifierType.GetMembers().Length - 1) < 3)
						{
							continue;
						}

						// Получаем количество используемых case
						IEnumerable<SwitchSectionSyntax> switchCaseLabels = switchStatement.DescendantNodes().OfType<SwitchSectionSyntax>();
						// Перестаем рассматривать switch если он использует не больше 4 констант из перечисления или использует все элементы
						if (switchCaseLabels.Count() <= 4 || switchCaseLabels.Count() == (identifierType.GetMembers().Length - 1))
						{
							continue;
						}

						// Получаем список всех членов перечисления
						List<string> enumMembers = new List<string>();
						foreach (var enumMember in identifierType.GetMembers())
						{
							enumMembers.Add(enumMember.Name);
						}


						// Удаляем из списка используемые члены перечисления
						foreach (SwitchSectionSyntax switchCaseLabel in switchCaseLabels)
						{
							MemberAccessExpressionSyntax enumAccessExpression = switchCaseLabel.DescendantNodes().OfType<MemberAccessExpressionSyntax>().First();
							//Console.WriteLine(enumAccessExpression.Name);
							enumMembers.Remove(enumAccessExpression.Name.ToString());
						}

						// Запись в файл
						stream.Write($"{switchStatement.GetLocation().GetMappedLineSpan().Path}  " +
							$"строка: {switchStatement.GetLocation().GetLineSpan().StartLinePosition.Line + 1}  " +
							$"Использованы не все элементы перечисления в операторе switch: ");
						for (int i = 0; i < (enumMembers.Count() - 1); i++)
						{
							stream.Write($" {enumMembers[i]} ");
						}
						stream.WriteLine();


					}
				}
				stream.Close();
				Console.WriteLine("------------------------------------");
				
			}
		}
		

	}
}
