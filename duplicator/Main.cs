using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxAnalysisDoc;
using SyntaxWalker;
using SyntaxTransformation;
using ParameterDuplicator;

namespace Main
{
    class Program
    {
        private int dumyFunc1(int a) { return a; }
        private string dumyFunc2(string b) { return b; }
        public int dumyFunc3(int a, string b) { return a; }
        private static void Main(string[] args)
        {
            //SyntaxAnalysisDoc.Program.SyntaxAnalysisDocMain(args);
            //SyntaxWalker.UsingCollector.SyntaxWalkerMain(args);
            //SyntaxTransformation.SyntaxTransformation.SyntaxTransformationMain(args);
            ParameterDuplicator.ParameterDuplicator.TaskMain(args);
        }
    }
}