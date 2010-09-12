using System;
using Microsoft.VisualStudio.Text.Tagging;

namespace CircleLangEditor
{
    public class CircleTokenTag : ITag
    {
        public CircleTokenTag(CircleTagType circleTagType)
        {
            type = circleTagType;
        }

        internal CircleTagType type { get; private set; }
    }
}