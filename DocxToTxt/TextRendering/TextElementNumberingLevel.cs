using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextElementNumberingLevel : ITextElement
    {
        public Size DesiredSize { get; protected set; }

        public int LevelNumber { get; set; }
        public int LevelIndex { get; set; }
        public LevelConverter LevelConverter { get; set; } = new LevelConverter();

        private ITextElement _content = new TextPage(0, 0);
        public ITextElement Content
        {
            get
            {
                return _content;
            }

            set
            {
                if (value == null)
                {
                    _content = new TextPage(0, 0);
                }
                else
                {
                    _content = value;
                }
            }
        }

        private TextElementParagraph _indexParagraph = new TextElementParagraph();

        public int IndexContentSpacing { get; set; } = 1;
        public int IndentPerLevel { get; set; } = 4;

        public TextOrientationValues Orientation { get; set; } = TextOrientationValues.LeftToRightTopToBottom;



        public TextElementNumberingLevel() : this(0, 0, null) { }
        public TextElementNumberingLevel(int levelNumber) : this(levelNumber, 0, null) { }
        public TextElementNumberingLevel(int levelNumber, int levelIndex) : this(levelNumber, levelIndex, null) { }
        public TextElementNumberingLevel(ITextElement content) : this(0, 0, content) { }
        public TextElementNumberingLevel(int levelNumber, int levelIndex, ITextElement content)
        {
            LevelNumber = levelNumber;
            LevelIndex = levelIndex;
            Content = content;
        }



        public void Measure(Size maxSize)
        {
            Size sizeLeft = new Size(maxSize);

            if (OrientedTextPageView.IsOrientationVertical(Orientation))
            {
                sizeLeft.Height = Math.Max(sizeLeft.Height - (IndentPerLevel * LevelIndex), 0);
            }
            else
            {
                sizeLeft.Width = Math.Max(sizeLeft.Width - (IndentPerLevel * LevelIndex), 0);
            }

            string indexString = LevelConverter.ToIndexString(LevelNumber, LevelIndex, Orientation);
            string indexDelimiter = LevelConverter.ToDelimiterString(LevelNumber, LevelIndex, Orientation);

            _indexParagraph.Text = indexString + indexDelimiter;
            _indexParagraph.TextOrientation = Orientation;
            _indexParagraph.Measure(sizeLeft);

            if (OrientedTextPageView.IsOrientationVertical(Orientation))
            {
                sizeLeft.Height = Math.Max(sizeLeft.Height - _indexParagraph.DesiredSize.Height - IndexContentSpacing, 0);
            }
            else
            {
                sizeLeft.Width = Math.Max(sizeLeft.Width - _indexParagraph.DesiredSize.Width - IndexContentSpacing, 0);
            }

            _content.Measure(sizeLeft);

            if (OrientedTextPageView.IsOrientationVertical(Orientation))
            {
                DesiredSize = new Size
                (
                    (IndentPerLevel * LevelIndex) + _indexParagraph.DesiredSize.Height + IndexContentSpacing + _content.DesiredSize.Height,
                    Math.Max(_indexParagraph.DesiredSize.Width, _content.DesiredSize.Width)
                );
            }
            else
            {
                DesiredSize = new Size
                (
                    Math.Max(_indexParagraph.DesiredSize.Height, _content.DesiredSize.Height),
                    (IndentPerLevel * LevelIndex) + _indexParagraph.DesiredSize.Width + IndexContentSpacing + _content.DesiredSize.Width
                );
            }
        }

        public TextPage ToTextPage(Size maxSize, char fill)
        {
            Size sizeLeft = new Size(maxSize);

            if (OrientedTextPageView.IsOrientationVertical(Orientation))
            {
                sizeLeft.Height = Math.Max(sizeLeft.Height - (IndentPerLevel * LevelIndex), 0);
            }
            else
            {
                sizeLeft.Width = Math.Max(sizeLeft.Width - (IndentPerLevel * LevelIndex), 0);
            }

            TextPage indexPage = _indexParagraph.ToTextPage(sizeLeft, fill);

            if (OrientedTextPageView.IsOrientationVertical(Orientation))
            {
                sizeLeft.Height = Math.Max(sizeLeft.Height - indexPage.Height - IndexContentSpacing, 0);
            }
            else
            {
                sizeLeft.Width = Math.Max(sizeLeft.Width - indexPage.Width - IndexContentSpacing, 0);
            }

            TextPage contentPage = _content.ToTextPage(sizeLeft, fill);

            Size totalSize = new Size();

            if (OrientedTextPageView.IsOrientationVertical(Orientation))
            {
                totalSize.Height = (IndentPerLevel * LevelIndex) + indexPage.Height + IndexContentSpacing + contentPage.Height;
                totalSize.Width = Math.Max(indexPage.Width, contentPage.Width);
            }
            else
            {
                totalSize.Height = Math.Max(indexPage.Height, contentPage.Height);
                totalSize.Width = (IndentPerLevel * LevelIndex) + indexPage.Width + IndexContentSpacing + contentPage.Width;
            }

            TextPage page = new TextPage(totalSize.Height, totalSize.Width, fill);

            Index2D indexPosition = new Index2D(0, IndentPerLevel * LevelIndex);
            Index2D contentPosition = new Index2D(0, indexPosition.Column + indexPage.Width + IndexContentSpacing);

            if (OrientedTextPageView.IsOrientationVertical(Orientation))
            {
                indexPosition = new Index2D(indexPosition.Column, indexPosition.Row);
                contentPosition = new Index2D(indexPosition.Row + indexPage.Height + IndexContentSpacing, 0);
            }

            if (OrientedTextPageView.IsOrientationHorizontallyReversed(Orientation))
            {
                indexPosition.Column = page.Width - indexPosition.Column - indexPage.Width;
                contentPosition.Column = page.Width - contentPosition.Column - contentPage.Width;
            }

            if (OrientedTextPageView.IsOrientationVerticallyReversed(Orientation))
            {
                indexPosition.Row = page.Height - indexPosition.Row - indexPage.Height;
                contentPosition.Row = page.Height - contentPosition.Row - contentPage.Height;
            }

            TextPage.Blit(indexPage, page, indexPosition.Column, indexPosition.Row);
            TextPage.Blit(contentPage, page, contentPosition.Column, contentPosition.Row);

            return page;
        }
    }
}
