using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace CircleLangEditor
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("circles")]
    [TagType(typeof(CircleTokenTag))]
    internal sealed class CircleTagProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new CircleTokenTagger(buffer) as ITagger<T>;
        }
    }
}
