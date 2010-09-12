using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;
using Irony.Interpreter;
using LangExp;

namespace ConsoleLangRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var grammar = new ExampleExpressionGrammar();
            var interpriter = new ScriptInterpreter(grammar);
            interpriter.Evaluate("circle 4,53 123\ncircle 2,4 12");
            if(interpriter.Status != InterpreterStatus.Ready)
            {
                throw new InvalidOperationException("You syntax errored!?");
            }
            var program = interpriter.ParsedScript.Root.AstNode as StatementListNode;
            foreach (var circle in program.ChildNodes.OfType<CircleDefNode>())
            {
                Console.WriteLine("I saw a circle at ({0},{1}) with a radius {2}",
                    circle.Point.X,
                    circle.Point.Y,
                    circle.Radius);
            }
            Console.ReadKey();
        }
    }
}
