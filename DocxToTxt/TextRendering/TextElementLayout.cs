using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public abstract class TextElementLayout : ITextElement
    {
        public List<ITextElement> Children { get; } = new List<ITextElement>();

        public Size DesiredSize { get; protected set; }

        public abstract void Measure(Size maxSize);
        public abstract TextPage ToTextPage(Size maxSize, char fill);
    }
}
