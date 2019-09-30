using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementParagraph : ITextElement
    {
        public Size DesiredSize { get; protected set; }

        private string _text = "";
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (value == null)
                {
                    _text = "";
                }
                else
                {
                    _text = value;
                }
            }
        }

        public bool LineWrap { get; set; } = false;
        public TextOrientationValues TextOrientation { get; set; } = TextOrientationValues.LeftToRightTopToBottom;
        public TextAlignment TextAlignment { get; set; } = TextAlignment.Beginning;



        public TextElementParagraph() { }

        public TextElementParagraph(string text)
        {
            Text = text;
        }



        private TextPage BuildTextPage(Size maxSize, char fill)
        {
            TextPageBuilder builder;

            if (OrientedTextPageView.IsOrientationVertical(TextOrientation))
            {
                builder = new TextPageBuilder(maxSize.Height);
            }
            else
            {
                builder = new TextPageBuilder(maxSize.Width);
            }

            builder.LineWrap = LineWrap;
            builder.TextOrientation = TextOrientation;
            builder.TextAlignment = TextAlignment;

            builder.Text.Append(Text);

            return builder.Build().Page;
        }

        public void Measure(Size maxSize)
        {
            TextPage page = BuildTextPage(maxSize, '▓');

            DesiredSize = new Size(page.LineCount, page.LineLength);
        }

        public TextPage ToTextPage(Size maxSize, char fill)
        {
            TextPage page = BuildTextPage(maxSize, fill);

            Size clipSize = Size.MinPositiveSize(new Size(page.LineCount, page.LineLength), maxSize);
            Index2D clipPosition = new Index2D(0, 0);

            if (OrientedTextPageView.IsOrientationVerticallyReversed(TextOrientation))
            {
                clipPosition.Row = page.LineCount - clipSize.Height;
            }

            if (OrientedTextPageView.IsOrientationHorizontallyReversed(TextOrientation))
            {
                clipPosition.Column = page.LineLength - clipSize.Width;
            }

            TextPage clippedPage = new TextPage(clipSize.Height, clipSize.Width, fill);
            TextPage.Blit(page, clipPosition.Column, clipPosition.Row, clipSize.Width, clipSize.Height, clippedPage, 0, 0);

            return clippedPage;
        }
    }
}
