using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ParameterDuplicator
{
    public class ParameterDuplicator : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            Console.WriteLine($"\tVisitMethodDeclaration called with {node.Identifier.ValueText}.");
            if (node.ParameterList.Parameters.Count == 1)
            {
                Console.WriteLine(node.Identifier.ValueText);
                string arg1 = node.ParameterList.Parameters[0].Identifier.ValueText;

                var leadingSpace = SyntaxFactory.TriviaList(SyntaxFactory.Space);
                var newParameter = SyntaxFactory.Parameter(SyntaxFactory.Identifier($"{arg1}_new"))
                    .WithType(node.ParameterList.Parameters[0].Type)
                    .WithLeadingTrivia(leadingSpace);
                var NewMethod = node.AddParameterListParameters(newParameter);
                return NewMethod;
            }

            return node;    
        }    
        
        public static void TaskMain(string[] args)
        {
            string sourceText = File.ReadAllText("../../../Main.cs");

            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceText);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            SyntaxNode newRoot = new ParameterDuplicator().Visit(root);

            File.WriteAllText("../../../NewCode.cs",newRoot.ToString());

        }
    }
}