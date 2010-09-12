using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace CircleLangEditor.Classification
{
    internal static class OrdinaryClassificationDefintion
    {
        [Export(typeof (ClassificationTypeDefinition))]
        [Name("circleKeyword")]
        internal static ClassificationTypeDefinition _circleKeyword;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("numeric")]
        internal static ClassificationTypeDefinition _numeric;
    }
}