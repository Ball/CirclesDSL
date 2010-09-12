using System;
using System.Collections.Generic;
using Irony.Interpreter;
using Irony.Parsing;
using LangExp;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace CircleLangEditor
{
    internal class CircleTokenTagger : ITagger<CircleTokenTag>
    {
// ReSharper disable UnaccessedField.Local
        private ITextBuffer _buffer;
// ReSharper restore UnaccessedField.Local
        private readonly ExampleExpressionGrammar _grammar;
        private readonly ScriptInterpreter _scriptInterpreter;

        public CircleTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            _grammar = new ExampleExpressionGrammar();
            _scriptInterpreter = new ScriptInterpreter(_grammar);
        }

        public IEnumerable<ITagSpan<CircleTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var span in spans)
            {
                var containingLine = span.Start.GetContainingLine();
                _scriptInterpreter.Evaluate(containingLine.GetText());
                if (_scriptInterpreter.ParsedScript.Root != null && _scriptInterpreter.ParsedScript.Root.ChildNodes.Count > 0)
                {
                    var circle = _scriptInterpreter.ParsedScript.Root.FirstChild;
                    if (circle.AstNode as CircleDefNode == null)
                    {
                        Console.WriteLine("It's not what you think it is... ");
                    }
                    else
                    {
                        yield return GetTagSpan(span, circle, 0, CircleTagType.CircleKeyword);
                        yield return GetTagSpan(span, circle, 1, CircleTagType.Numeric);
                        yield return GetTagSpan(span, circle, 3, CircleTagType.Numeric);
                        yield return GetTagSpan(span, circle, 4, CircleTagType.Numeric);
                    }
                }
            }
        }

        private static TagSpan<CircleTokenTag> GetTagSpan(SnapshotSpan span, ParseTreeNode circle, int index, CircleTagType circleTagType)
        {
            var sourceSpan = GetSourceSpan(circle, index);
            return new TagSpan<CircleTokenTag>(GetTokenSpan(sourceSpan, span),
                                               new CircleTokenTag(circleTagType));
        }

        private static SnapshotSpan GetTokenSpan(SourceSpan sourceSpan, SnapshotSpan span)
        {
            var column = sourceSpan.Location.Column;
            var length = sourceSpan.Length;

            return new SnapshotSpan(span.Snapshot, new Span(column, length));
        }

        private static SourceSpan GetSourceSpan(ParseTreeNode circle, int index)
        {
            return circle.ChildNodes[index].Span;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
    }
}