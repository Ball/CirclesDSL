using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Irony.Ast;
using Irony.Parsing;

namespace LangExp
{
    [Language("ExampleExpression", "1.0", "an example expression")]
    public class ExampleExpressionGrammar : Grammar
    {
        public ExampleExpressionGrammar()
        {
            // 1. Terminals
            var number = new NumberLiteral("number");
            number.DefaultIntTypes = new[] {TypeCode.Int32, TypeCode.Int32, NumberLiteral.TypeCodeBigInt};
            var circle = new KeyTerm("circle", "circleWord");

            // 2. Non-terminals
            var circleDef = new NonTerminal("circleDef", typeof(CircleDefNode));
            var programLine = new NonTerminal("ProgramLine");
            var program = new NonTerminal("Program", typeof (StatementListNode));

            // 3. BNF Rules
            circleDef.Rule = circle + number + "," + number + number;
            programLine.Rule = circleDef + NewLine;
            program.Rule = MakeStarRule(program, programLine);
            Root = program;

            // 4. Operators precedence
            // No operators in this language

            // 5. Punctuation and transient terms
            MarkTransient(programLine);

            LanguageFlags = LanguageFlags.CreateAst
                | LanguageFlags.NewLineBeforeEOF
                | LanguageFlags.CanRunSample;

        }
    }

    public class CircleDefNode : AstNode
    {
        public CircleDefNode()
        {   
        }

        public override void Init(ParsingContext context, ParseTreeNode treeNode)
        {

            var x = (int) (treeNode.ChildNodes[1].AstNode as LiteralValueNode).Value;
            var y = (int) (treeNode.ChildNodes[3].AstNode as LiteralValueNode).Value;
            Point = new Point(x, y);
            Radius = (int) (treeNode.ChildNodes[4].AstNode as LiteralValueNode).Value;

            base.Init(context, treeNode);
        }

        public Point Point { get; private set; }

        public int Radius { get; private set; }
    }
}
