using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public enum HorizontalDock
    {
        Left,
        Middle,
        Right
    }

    public enum VerticalDock
    {
        Top,
        Middle,
        Bottom
    }

    public class TextElementDocker : TextElementContainer
    {
        public HorizontalDock HorizontalDock { get; set; }
        public VerticalDock VerticalDock { get; set; }



        public TextElementDocker() { }

        public TextElementDocker(ITextElement child)
        {
            Child = child;
        }



        public override void Measure(Size maxSize)
        {
            Child.Measure(maxSize);

            DesiredSize = Child.DesiredSize;
        }

        public override TextPage ToTextPage(Size maxSize, char fill)
        {
            TextPage childTextPage = Child.ToTextPage(maxSize, fill);

            TextPage page = new TextPage(maxSize.Height, maxSize.Width, fill);

            int topMargin = 0;
            int leftMargin = 0;

            if (VerticalDock == VerticalDock.Top)
            {
                topMargin = 0;
            }
            else if (VerticalDock == VerticalDock.Middle)
            {
                topMargin = (maxSize.Height - childTextPage.LineCount) / 2;
            }
            else if (VerticalDock == VerticalDock.Bottom)
            {
                topMargin = maxSize.Height - childTextPage.LineCount;
            }

            if (HorizontalDock == HorizontalDock.Left)
            {
                leftMargin = 0;
            }
            else if (HorizontalDock == HorizontalDock.Middle)
            {
                leftMargin = (maxSize.Width - childTextPage.LineLength) / 2;
            }
            else if (HorizontalDock == HorizontalDock.Right)
            {
                leftMargin = maxSize.Width - childTextPage.LineLength;
            }

            TextPage.Blit(childTextPage, page, leftMargin, topMargin);

            return page;
        }
    }
}
