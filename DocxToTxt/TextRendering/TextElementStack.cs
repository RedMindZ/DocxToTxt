using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public enum StackOrientation
    {
        Horizontal,
        Vertical
    }

    public class TextElementStack : TextElementLayout
    {
        private int _maxChildLength = 0;

        public StackOrientation Orientation { get; set; } = StackOrientation.Vertical;

        public int Spacing { get; set; } = 0;



        public TextElementStack() { }

        public TextElementStack(StackOrientation orientation)
        {
            Orientation = orientation;
        }



        public override void Measure(Size maxSize)
        {
            Size sizeLeft = new Size(maxSize);
            Size desiredSize = new Size(0, 0);

            for (int i = 0; i < Children.Count; i++)
            {
                ITextElement child = Children[i];

                child.Measure(sizeLeft);
                Size childSize = child.DesiredSize;

                int extraSpace = 0;

                if (i + 1 < Children.Count)
                {
                    extraSpace = Spacing;
                }

                if (Orientation == StackOrientation.Vertical)
                {
                    sizeLeft.Height = Math.Max(sizeLeft.Height - childSize.Height - extraSpace, 0);

                    desiredSize.Height += childSize.Height + extraSpace;
                    desiredSize.Width = Math.Max(desiredSize.Width, childSize.Width);

                    _maxChildLength = Math.Max(_maxChildLength, childSize.Width);
                }
                else
                {
                    sizeLeft.Width = Math.Max(sizeLeft.Width - childSize.Width - extraSpace, 0);

                    desiredSize.Width += childSize.Width + extraSpace;
                    desiredSize.Height = Math.Max(desiredSize.Height, childSize.Height);

                    _maxChildLength = Math.Max(_maxChildLength, childSize.Height);
                }


            }

            DesiredSize = desiredSize;
        }

        public override TextPage ToTextPage(Size maxSize, char fill)
        {
            Size sizeLeft = new Size(maxSize);
            Size totalSize = new Size(0, 0);

            List<TextPage> childPages = new List<TextPage>();

            for (int i = 0; i < Children.Count; i++)
            {
                ITextElement child = Children[i];

                //Size finalChildSize = Size.MinPositiveSize(child.DesiredSize, sizeLeft);
                Size finalChildSize;

                if (Orientation == StackOrientation.Vertical)
                {
                    finalChildSize = Size.MinPositiveSize(sizeLeft, new Size(child.DesiredSize.Height, DesiredSize.Width));
                }
                else
                {
                    finalChildSize = Size.MinPositiveSize(sizeLeft, new Size(DesiredSize.Height, child.DesiredSize.Width));
                }

                int extraSpace = 0;

                if (i + 1 < Children.Count)
                {
                    extraSpace = Spacing;
                }

                childPages.Add(child.ToTextPage(finalChildSize, fill));

                if (Orientation == StackOrientation.Vertical)
                {
                    sizeLeft.Height = Math.Max(sizeLeft.Height - finalChildSize.Height - extraSpace, 0);

                    totalSize.Height += finalChildSize.Height + extraSpace;
                    totalSize.Width = Math.Max(totalSize.Width, finalChildSize.Width);
                }
                else
                {
                    sizeLeft.Width = Math.Max(sizeLeft.Width - finalChildSize.Width - extraSpace, 0);

                    totalSize.Width += finalChildSize.Width + extraSpace;
                    totalSize.Height = Math.Max(totalSize.Height, finalChildSize.Height);
                }

                if (sizeLeft.Width <= 0 || sizeLeft.Height <= 0)
                {
                    break;
                }
            }

            TextPage compositePage = new TextPage(totalSize.Height, totalSize.Width, fill);
            LinePosition compositePosition = new LinePosition();

            foreach (TextPage page in childPages)
            {
                TextPage.Blit(page, compositePage, compositePosition.CharIndex, compositePosition.LineIndex);

                if (Orientation == StackOrientation.Vertical)
                {
                    compositePosition.LineIndex += page.LineCount + Spacing;
                }
                else
                {
                    compositePosition.CharIndex += page.LineLength + Spacing;
                }
            }

            return compositePage;
        }
    }
}
