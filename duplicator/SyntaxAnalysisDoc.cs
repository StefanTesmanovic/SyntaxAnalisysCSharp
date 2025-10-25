// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxAnalysisDoc
{
    public class Program
    {
        public static void SyntaxAnalysisDocMain(string[] args)
        {
            const string programText = @"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}";

            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            Console.WriteLine($"The tree is a {root.Kind()} node.");
            Console.WriteLine($"The tree has {root.Members.Count} elements in it.");
            Console.WriteLine($"The tree has {root.Usings.Count} using directives. They are:");
            foreach (UsingDirectiveSyntax element in root.Usings)
            Console.WriteLine($"\t{element.Name}");

            MemberDeclarationSyntax firstMember = root.Members[0];
            Console.WriteLine($"The first member is a {firstMember.Kind()}.");
            var helloWorldDeclaration = (NamespaceDeclarationSyntax)firstMember;
            Console.WriteLine($"There are {helloWorldDeclaration.Members.Count} members declared in this namespace.");
            Console.WriteLine($"The first member is a {helloWorldDeclaration.Members[0].Kind()}.");

            var programDeclaration = (ClassDeclarationSyntax)helloWorldDeclaration.Members[0];
            Console.WriteLine($"\n\nThere are {programDeclaration.Members.Count} members declared in the {programDeclaration.Identifier} class.");
            Console.WriteLine($"The first member is a {programDeclaration.Members[0].Kind()}.");
            var mainDeclaration = (MethodDeclarationSyntax)programDeclaration.Members[0];

            Console.WriteLine($"\n\nThe return type of the {mainDeclaration.Identifier} method is {mainDeclaration.ReturnType}.");
            Console.WriteLine($"The method has {mainDeclaration.ParameterList.Parameters.Count} parameters.");
            foreach (ParameterSyntax item in mainDeclaration.ParameterList.Parameters)
                Console.WriteLine($"The type of the {item.Identifier} parameter is {item.Type}.");
            Console.WriteLine($"The body text of the {mainDeclaration.Identifier} method follows:");
            Console.WriteLine(mainDeclaration.Body?.ToFullString()); // mainDeclaration.Body != null ? mainDeclaration.Body.ToFullString() : null;

            var argsParameter = mainDeclaration.ParameterList.Parameters[0];
            
            var firstParameters = from methodDeclaration in root.DescendantNodes()
                                    .OfType<MethodDeclarationSyntax>()
                                    where methodDeclaration.Identifier.ValueText == "Main"
                                    select methodDeclaration.ParameterList.Parameters.First();

            var argsParameter2 = firstParameters.Single();
            Console.WriteLine(argsParameter == argsParameter2);
        }
    }
}