using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxWalker
{
    public class UsingCollector : CSharpSyntaxWalker
    {
        public ICollection<MethodDeclarationSyntax> Methods { get; } = new List<MethodDeclarationSyntax>();

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            Console.WriteLine($"\tVisitMethodDeclaration called with {node.Identifier.ValueText}.");
            if(node.ParameterList.Parameters.Count == 1)
            this.Methods.Add(node);
        }

        public static void SyntaxWalkerMain(string[] args)
        {
            const string programText = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace TopLevel
{
    using Microsoft;
    using System.ComponentModel;

    public class MyClass 
    {
        int func1(int arg1){ return 0;}
    }
    int func2(){ return 0;}
}";

            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            var collector = new UsingCollector();
            collector.Visit(root);
            foreach (var directive in collector.Methods)
            {
                Console.WriteLine(directive.Identifier.ValueText);
            }


        }
    }
}