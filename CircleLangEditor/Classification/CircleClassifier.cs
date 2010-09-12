using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace CircleLangEditor.Classification
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("circles")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class CircleClassifierProvider : ITaggerProvider
    {
        [Export]
        [Name("circles")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition _cirlcleContentType = null;

        [Export]
        [FileExtension(".circle")]
        [ContentType("circles")]
        internal static FileExtensionToContentTypeDefinition _circlesExtensionDef = null;

        [Import]
        internal IClassificationTypeRegistryService _classificationTypeRegistry = null;
        [Import]
        internal IBufferTagAggregatorFactoryService _aggregatorFactory = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            ITagAggregator<CircleTokenTag> circleTagAggregator =
                _aggregatorFactory.CreateTagAggregator<CircleTokenTag>(buffer);
            return new CircleClassifier(buffer, circleTagAggregator, _classificationTypeRegistry) as ITagger<T>;
        }
    }


    internal sealed class CircleClassifier : ITagger<ClassificationTag>
    {
        private ITextBuffer _buffer;
        private ITagAggregator<CircleTokenTag> _aggregator;
        private Dictionary<CircleTagType, IClassificationType> _types;

        public CircleClassifier(ITextBuffer buffer, ITagAggregator<CircleTokenTag> circleTagAggregator, IClassificationTypeRegistryService classificationTypeRegistry)
        {
            _buffer = buffer;
            _aggregator = circleTagAggregator;
            _types = new Dictionary<CircleTagType, IClassificationType>();
            _types[CircleTagType.CircleKeyword] = classificationTypeRegistry.GetClassificationType("circleKeyword");
            _types[CircleTagType.Numeric] = classificationTypeRegistry.GetClassificationType("numeric");
           
        }

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var tagSpan in _aggregator.GetTags(spans))
            {
                var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
                yield return new TagSpan<ClassificationTag>(tagSpans[0], new ClassificationTag(_types[tagSpan.Tag.type]));
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
    }
}
