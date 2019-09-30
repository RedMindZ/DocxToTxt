using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.TextRendering
{
    public class TextPagePrinter
    {
        public TextPage Page { get; set; }

        public int LineIndex { get; set; } = 0;
        public int CharIndex { get; set; } = 0;
        public bool LineWrap { get; set; } = false;

        private int _indent = 0;
        public int Indent
        {
            get
            {
                return _indent;
            }

            set
            {
                _indent = value;
                CharIndex = Math.Max(_indent, CharIndex);
            }
        }

        public PageFlowValues PageFlow { get; set; } = PageFlowValues.TopToBottom;

        private TextOrientationValues _textOrientation = TextOrientationValues.LeftToRightTopToBottom;
        public TextOrientationValues TextOrientation
        {
            get
            {
                return _textOrientation;
            }

            set
            {
                //Point bufferPos = TextPageBuilder.TranslateLinePosition(LineIndex, CharIndex, Page, _textOrientation);
                //Point translatedPos = TextPageBuilder.TranslateLinePosition(bufferPos.Y, bufferPos.X, Page, value);
                
                _textOrientation = value;

                //LineIndex = translatedPos.Y;
                //CharIndex = translatedPos.X;
            }
        }


        public TextPagePrinter(TextPage page)
        {
            Page = page;
        }

        public void Print(string text, int maxLineLength)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            if (LineIndex >= Page.LineCount)
            {
                return;
            }

            if (maxLineLength < 0)
            {
                return;
            }

            int actualMaxLineLength = GetTextPageLineLength(Page, TextOrientation) - Indent;
            if (maxLineLength > 0)
            {
                actualMaxLineLength = Math.Min(actualMaxLineLength, maxLineLength);
            }

            int indentMargin = CharIndex - Indent;

            List<string> textLines = TextPageBuilder.SplitTextLines(new string('■', indentMargin) + text, actualMaxLineLength, LineWrap);
            textLines[0] = textLines[0].Substring(indentMargin);

            int minCharIndex = CharIndex;
            int maxCharIndex = CharIndex;
            int minLineIndex = LineIndex;
            int maxLineIndex = LineIndex;

            for (int i = 0; i < textLines.Count; i++)
            {
                string line = textLines[i];

                for (int j = 0; j < line.Length; j++)
                {
                    Point pagePos = OrientedTextPageView.TranslateLinePosition(LineIndex, CharIndex, Page, TextOrientation);
                    Page.CharBuffer[pagePos.Y, pagePos.X] = line[j];

                    CharIndex++;
                }

                if (i + 1 < textLines.Count)
                {
                    AdvancePrintPositionToNextLine();
                }
            }

            //TextPageBuilder pageBuilder = new TextPageBuilder(lineLength)
            //{
            //    LineWrap = LineWrap,
            //    TextOrientation = TextOrientation
            //};
            //
            //int indentMargin = CharIndex - Indent;
            //
            //pageBuilder.Text.Append('■', indentMargin);
            //pageBuilder.Text.Append(text);
            //TextPageBuilderResult pageBuilderResult = pageBuilder.Build('■');
            //
            //int totalCharactersToCopy = pageBuilderResult.LineLengths.Sum() - indentMargin;
            //
            //for (int i = 0; i < totalCharactersToCopy; i++)
            //{
            //    Point sourcePos = TextPageBuilder.TranslateLinePosition(LineIndex, CharIndex, pageBuilderResult.TextPage, TextOrientation);
            //    Point destPos = TextPageBuilder.TranslateLinePosition(LineIndex, CharIndex, Page, TextOrientation);
            //}

            //for (int i = 0; i < pageBuilderResult.LineLengths.Length; i++)
            //{
            //    for (int j = 0; j < pageBuilderResult.LineLengths[i]; j++)
            //    {
            //        Point pagePos = TranslateLinePosition(LineIndex, CharIndex, page, TextOrientation);
            //        page.CharBuffer[pagePos.Y, pagePos.X] = line[j];
            //    }
            //}

            //OrientedBlit
            //(
            //    pageBuilderResult.TextPage,
            //    indentMargin,
            //    0,
            //    pageBuilderResult.LineLengths[0] - indentMargin,
            //    1,
            //    TextOrientation,
            //    Page,
            //    CharIndex,
            //    LineIndex,
            //    TextOrientation
            //);
            //
            //
            //if (pageBuilderResult.LineLengths.Length < 2)
            //{
            //    AdvancePrintPosition(pageBuilderResult.LineLengths[0]);
            //    return;
            //}
            //else
            //{
            //    AdvancePrintPositionToNextLine();
            //}
            //
            //for (int i = 1; i < pageBuilderResult.LineLengths.Length - 1; i++)
            //{
            //    OrientedBlit
            //    (
            //        pageBuilderResult.TextPage,
            //        0,
            //        i,
            //        pageBuilderResult.LineLengths[i],
            //        1,
            //        TextOrientation,
            //        Page,
            //        CharIndex,
            //        LineIndex,
            //        TextOrientation
            //    );
            //
            //    AdvancePrintPositionToNextLine();
            //}
            //
            //OrientedBlit
            //(
            //    pageBuilderResult.TextPage,
            //    0,
            //    pageBuilderResult.LineLengths.Length - 1,
            //    pageBuilderResult.LineLengths[pageBuilderResult.LineLengths.Length - 1],
            //    1,
            //    TextOrientation,
            //    Page,
            //    CharIndex,
            //    LineIndex,
            //    TextOrientation
            //);
            //
            //AdvancePrintPosition(pageBuilderResult.LineLengths[pageBuilderResult.LineLengths.Length - 1]);
        }

        private void AdvancePrintPosition(int charCount)
        {
            int lineLength = GetTextPageLineLength(Page, TextOrientation) - Indent;
            int currentCharIndex = CharIndex - Indent;

            CharIndex = (currentCharIndex + charCount) % lineLength + Indent;
            LineIndex = (currentCharIndex + charCount) / lineLength + LineIndex;
        }

        private void AdvancePrintPositionToNextLine()
        {
            CharIndex = Indent;
            LineIndex++;
        }

        private static void OrientedBlit
        (
            TextPage sourcePage,
            int sourceCharIndex,
            int sourceLineIndex,
            int sourceLineLength,
            int sourceLineCount,
            TextOrientationValues sourceOrientation,
            TextPage destinationPage,
            int destinationCharIndex,
            int destinationLineIndex,
            TextOrientationValues destinationOrientation
        )
        {
            Rectangle sourceRect = new Rectangle
            (
                OrientedTextPageView.TranslateLinePosition(sourceLineIndex, sourceCharIndex, sourcePage, sourceOrientation),
                OrientedTextPageView.TranslateLinePosition(sourceLineIndex + sourceLineCount - 1, sourceCharIndex + sourceLineLength - 1, sourcePage, sourceOrientation)
            );

            Rectangle destRect = new Rectangle
            (
                OrientedTextPageView.TranslateLinePosition(destinationLineIndex, destinationCharIndex, destinationPage, destinationOrientation),
                OrientedTextPageView.TranslateLinePosition(destinationLineIndex + sourceLineCount - 1, destinationCharIndex + sourceLineLength - 1, destinationPage, destinationOrientation)
            );

            TextPage.Blit(sourcePage, sourceRect.X, sourceRect.Y, sourceRect.Width + 1, sourceRect.Height + 1, destinationPage, destRect.X, destRect.Y);
        }

        public static int GetTextPageLineLength(TextPage page, TextOrientationValues orientation)
        {
            return ((int)orientation & 0b100) == 0b000 ? page.LineLength : page.LineCount;
        }

        public static int GetTextPageLineCount(TextPage page, TextOrientationValues orientation)
        {
            return ((int)orientation & 0b100) == 0b000 ? page.LineCount : page.LineLength;
        }
    }
}
