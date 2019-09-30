using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementTableCell : ITextElement
    {
        public Size DesiredSize { get; protected set; }

        public Rectangle TableRectangle { get; set; } = new Rectangle(0, 0, 1, 1);

        private readonly TextElementAbsoluteClip _clip;
        public Size CellSize { get; set; }

        private readonly TextElementPadding _padding;
        public Padding CellPadding { get => _padding.Padding; set => _padding.Padding = value; }

        public ITextElement Child { get => _clip.Child; set => _clip.Child = value; }

        protected ITextElement InternalChild => _padding;



        public TextElementTableCell() : this(null, new Size(0, 0)) { }
        public TextElementTableCell(Size cellSize) : this(null, cellSize) { }
        public TextElementTableCell(ITextElement child) : this(child, new Size(0, 0)) { }
        public TextElementTableCell(ITextElement child, Size cellSize)
        {
            _clip = new TextElementAbsoluteClip(child);
            _padding = new TextElementPadding(_clip);

            CellSize = cellSize;
        }



        public void Measure(Size maxSize)
        {
            InternalChild.Measure(CellSize);

            DesiredSize = CellSize;
        }

        public TextPage ToTextPage(Size maxSize, char fill)
        {
            return InternalChild.ToTextPage(DesiredSize, fill);
        }
    }
}
