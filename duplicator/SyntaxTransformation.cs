// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static System.Console;

namespace SyntaxTransformation
{
    public class SyntaxTransformation
    {
        private const string sampleCode =
@"using System;
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
        public static void SyntaxTransformationMain(string[] args)
        {
            NameSyntax name = IdentifierName("System");
            //WriteLine($"\tCreated the identifier {name}");

            name = QualifiedName(name, IdentifierName("Collections"));
            //WriteLine(name.ToString());

            name = QualifiedName(name, IdentifierName("Generic"));
            //WriteLine(name.ToString());

            SyntaxTree tree = CSharpSyntaxTree.ParseText(sampleCode);
            var root = (CompilationUnitSyntax)tree.GetRoot();

            var oldUsing = root.Usings[1];
            var newUsing = oldUsing.WithName(name);
            WriteLine(root.ToString());

            root = root.ReplaceNode(oldUsing, newUsing); // this is not the original root, but a modified copy. Which means it doesn't have all the nodes from the original tree. It has only the replaced node and copys of the originals.
            Write("\n=============================");
            WriteLine(root.ToString());


        }
    }
}