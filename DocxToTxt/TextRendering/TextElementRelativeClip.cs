using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementRelativeClip : TextElementContainer
    {
        public Padding Clip { get; set; } = new Padding();



        public TextElementRelativeClip(ITextElement child)
        {
            Child = child;
        }



        public override void Measure(Size maxSize)
        {
            Child.Measure(maxSize);
            Size childSize = Child.DesiredSize;

            DesiredSize = new Size(childSize.Height - Clip.Top - Clip.Bottom, childSize.Width - Clip.Left - Clip.Right);
        }

        public override TextPage ToTextPage(Size maxSize, char fill)
        {
            TextPage childPage = Child.ToTextPage(maxSize, fill);

            TextPage page = new TextPage(childPage.LineCount - Clip.Top - Clip.Bottom, childPage.LineLength - Clip.Left - Clip.Right, fill);
            TextPage.Blit(childPage, Clip.Left, Clip.Top, page.LineLength, page.LineCount, page, 0, 0);

            return page;
        }
    }
}
