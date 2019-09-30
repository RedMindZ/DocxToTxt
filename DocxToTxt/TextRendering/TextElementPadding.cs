using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementPadding : TextElementContainer
    {
        public Padding Padding { get; set; } = new Padding();



        public TextElementPadding() { }

        public TextElementPadding(ITextElement child)
        {
            Child = child;
        }



        public override void Measure(Size maxSize)
        {
            Child.Measure(new Size(maxSize.Height - Padding.Top - Padding.Bottom, maxSize.Width - Padding.Left - Padding.Right));
            Size childSize = Child.DesiredSize;

            DesiredSize = new Size(childSize.Height + Padding.Top + Padding.Bottom, childSize.Width + Padding.Left + Padding.Right);
        }

        public override TextPage ToTextPage(Size maxSize, char fill)
        {
            TextPage childPage = Child.ToTextPage(new Size(maxSize.Height - Padding.Top - Padding.Bottom, maxSize.Width - Padding.Left - Padding.Right), fill);

            TextPage page = new TextPage(childPage.LineCount + Padding.Top + Padding.Bottom, childPage.LineLength + Padding.Left + Padding.Right, fill);
            TextPage.Blit(childPage, page, Padding.Left, Padding.Top);

            return page;
        }
    }
}
