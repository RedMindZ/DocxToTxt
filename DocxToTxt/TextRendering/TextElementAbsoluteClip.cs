using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementAbsoluteClip : TextElementContainer
    {
        public Index2D Position { get; set; } = new Index2D();



        public TextElementAbsoluteClip()
        {

        }

        public TextElementAbsoluteClip(ITextElement child)
        {
            Child = child;
        }



        public override void Measure(Size maxSize)
        {
            Child.Measure(maxSize);

            DesiredSize = Size.MinPositiveSize(Child.DesiredSize, maxSize);
        }

        public override TextPage ToTextPage(Size maxSize, char fill)
        {
            TextPage childPage = Child.ToTextPage(maxSize, fill);

            Size clippedSize = Size.MinPositiveSize(new Size(childPage.LineCount, childPage.LineLength), maxSize);

            TextPage page = new TextPage(clippedSize.Height, clippedSize.Width, fill);
            TextPage.Blit(childPage, Position.Column, Position.Row, clippedSize.Width, clippedSize.Height, page, 0, 0);

            return page;
        }
    }
}
