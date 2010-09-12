using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace CircleLangEditor.Classification
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "numeric")]
    [Name("numeric")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class CircleNumericClassiciationFormat: ClassificationFormatDefinition
    {
        public CircleNumericClassiciationFormat()
        {
            DisplayName = "numeric";
            ForegroundColor = Colors.Blue;
        }
    }
}