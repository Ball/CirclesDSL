using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace CircleLangEditor.Classification
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "circleKeyword")]
    [Name("circleKeyword")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class CircleKeywordClassificationFormat : ClassificationFormatDefinition
    {
        public CircleKeywordClassificationFormat()
        {
            DisplayName = "circleKeyword";
            ForegroundColor = Colors.DarkOrange;
        }
    }
}
