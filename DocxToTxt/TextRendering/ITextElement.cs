using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public interface ITextElement
    {
        Size DesiredSize { get; }

        void Measure(Size maxSize);
        TextPage ToTextPage(Size maxSize, char fill);
    }
}
