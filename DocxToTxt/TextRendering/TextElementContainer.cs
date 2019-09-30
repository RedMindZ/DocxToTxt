using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public abstract class TextElementContainer : ITextElement
    {
        private ITextElement _child = new TextPage(0, 0);
        public virtual ITextElement Child
        {
            get
            {
                return _child;
            }

            set
            {
                if (value == null)
                {
                    _child = new TextPage(0, 0);
                }
                else
                {
                    _child = value;
                }
            }
        }

        public Size DesiredSize { get; protected set; }

        public abstract void Measure(Size maxSize);
        public abstract TextPage ToTextPage(Size maxSize, char fill);
    }
}
